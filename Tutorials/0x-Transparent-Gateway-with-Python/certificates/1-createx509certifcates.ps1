# $openSSLBinSource = "<full_path_to_the_binaries>\OpenSSL\bin"
$openSSLBinSource = "C:\OpenSSL-Win32\bin"
$errorActionPreference    = "stop"

# OpenSsl-Tools - Target Folder
$openSSLToolsFolder = "openssltools"

# Note that these values are for test purpose only
$_rootCertSubject         = "CN=Azure IoT Root CA"
$_intermediateCertSubject = "CN=Azure IoT Intermediate {0} CA"
$_privateKeyPassword      = "123"

$rootCACerFileName          = "./RootCA.cer"
$rootCAPemFileName          = "./RootCA.pem"
$intermediate1CAPemFileName = "./Intermediate1.pem"
$intermediate2CAPemFileName = "./Intermediate2.pem"
$intermediate3CAPemFileName = "./Intermediate3.pem"

# $openSSLBinDir              = Join-Path $ENV:TEMP "openssl-bin"
$openSSLBinDir              = "openssl-bin"

# Whether to use ECC or RSA.
$useEcc                     = $true

function Initialize-CAOpenSSL()
{
    Write-Host ("Beginning copy of openssl binaries to {0} (and setting up env variables...)" -f $openSSLBinDir)
    if (-not (Test-Path $openSSLBinDir))
    {
        mkdir $openSSLBinDir | Out-Null
    }

    robocopy $openSSLBinSource $openSSLBinDir * /s 
    # robocopy $openSSLBinSource . * /s 

    Write-Host "Setting up PATH and other environment variables."
    $ENV:PATH += "; $openSSLBinDir"
    $ENV:OPENSSL_CONF = Join-Path $openSSLBinDir "openssl.cnf"

    Write-Host "Success"
}

Initialize-CAOpenSSL

function Get-CACertBySubjectName([string]$subjectName)
{
    $certificates = gci -Recurse Cert:\LocalMachine\ |? { $_.gettype().name -eq "X509Certificate2" }
    $cert = $certificates |? { $_.subject -eq $subjectName -and $_.PSParentPath -eq "Microsoft.PowerShell.Security\Certificate::LocalMachine\My" }
    if ($NULL -eq $cert)
    {
        throw ("Unable to find certificate with subjectName {0}" -f $subjectName)
    }

    write $cert
}
function Test-CAPrerequisites()
{
    $certInstalled = $null
    try
    {
        $certInstalled = Get-CACertBySubjectName $_rootCertSubject
    }
    catch {}

    if ($NULL -ne $certInstalled)
    {
        throw ("Certificate {0} already installed.  Cleanup CA certs 1st" -f $_rootCertSubject)
    }

    if ($NULL -eq $ENV:OPENSSL_CONF)
    {
        throw ("OpenSSL not configured on this system.  Run 'Initialize-CAOpenSSL' (even if you've already done so) to set everything up.")
    }
    Write-Host "Success"
}

Test-CAPrerequisites

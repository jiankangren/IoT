function New-CAVerificationCert([string]$requestedSubjectName)
{
    $cnRequestedSubjectName = ("CN={0}" -f $requestedSubjectName)
    $verifyRequestedFileName = ".\verifyCert4.cer"
    $rootCACert = Get-CACertBySubjectName $_rootCertSubject
    Write-Host "Using Signing Cert:::" 
    Write-Host $rootCACert

    $verifyCert = New-CASelfsignedCertificate $cnRequestedSubjectName $rootCACert $false

    Export-Certificate -cert $verifyCert -filePath $verifyRequestedFileName -Type Cert
    if (-not (Test-Path $verifyRequestedFileName))
    {
        throw ("Error: CERT file {0} doesn't exist" -f $verifyRequestedFileName)
    }

    Write-Host ("Certificate with subject {0} has been output to {1}" -f $cnRequestedSubjectName, (Join-Path (get-location).path $verifyRequestedFileName)) 
}

# New-CAVerificationCert "<your verification code>"


function New-CASelfsignedCertificate([string]$subjectName, [object]$signingCert, [bool]$isASigner=$true)
{
 # Build up argument list
 $selfSignedArgs =@{"-DnsName"=$subjectName; 
                    "-CertStoreLocation"="cert:\LocalMachine\My";
                    "-NotAfter"=(get-date).AddDays(30); 
                   }

 if ($isASigner -eq $true)
 {
     $selfSignedArgs += @{"-KeyUsage"="CertSign"; }
     $selfSignedArgs += @{"-TextExtension"= @(("2.5.29.19={text}ca=TRUE&pathlength=12")); }
 }
 else
 {
     $selfSignedArgs += @{"-TextExtension"= @("2.5.29.37={text}1.3.6.1.5.5.7.3.2,1.3.6.1.5.5.7.3.1", "2.5.29.19={text}ca=FALSE&pathlength=0")  }
 }

 if ($signingCert -ne $null)
 {
     $selfSignedArgs += @{"-Signer"=$signingCert }
 }

 if ($useEcc -eq $true)
 {
     $selfSignedArgs += @{"-KeyAlgorithm"="ECDSA_nistP256";
                      "-CurveExport"="CurveName" }
 }

 # Now use splatting to process this
 Write-Host ("Generating certificate {0} which is for prototyping, NOT PRODUCTION.  It will expire in 30 days." -f $subjectName)
 write (New-SelfSignedCertificate @selfSignedArgs)
}

function New-CAIntermediateCert([string]$subjectName, [Microsoft.CertificateServices.Commands.Certificate]$signingCert, [string]$pemFileName)
{
 $certFileName = ($subjectName + ".cer")
 $newCert = New-CASelfsignedCertificate $subjectName $signingCert
 Export-Certificate -Cert $newCert -FilePath $certFileName -Type CERT | Out-Null
 Import-Certificate -CertStoreLocation "cert:\LocalMachine\CA" -FilePath $certFileName | Out-Null

 # Store public PEM for later chaining
 openssl x509 -inform deer -in $certFileName -out $pemFileName

 del $certFileName

 write $newCert
}  

function New-CACertChain()
{
 Write-Host "Beginning to install certificate chain to your LocalMachine\My store"
 $rootCACert =  New-CASelfsignedCertificate $_rootCertSubject $null

 Export-Certificate -Cert $rootCACert -FilePath $rootCACerFileName  -Type CERT
 Import-Certificate -CertStoreLocation "cert:\LocalMachine\Root" -FilePath $rootCACerFileName

 openssl x509 -inform der -in $rootCACerFileName -out $rootCAPemFileName

 $intermediateCert1 = New-CAIntermediateCert ($_intermediateCertSubject -f "1") $rootCACert $intermediate1CAPemFileName
 $intermediateCert2 = New-CAIntermediateCert ($_intermediateCertSubject -f "2") $intermediateCert1 $intermediate2CAPemFileName
 $intermediateCert3 = New-CAIntermediateCert ($_intermediateCertSubject -f "3") $intermediateCert2 $intermediate3CAPemFileName
 Write-Host "Success"
}    

New-CACertChain
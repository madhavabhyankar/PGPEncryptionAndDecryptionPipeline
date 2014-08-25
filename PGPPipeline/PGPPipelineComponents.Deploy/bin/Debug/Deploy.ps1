function RaiseException([string]$message )
{
	Write-Host " Exception: " + $message
	Throw "Exception - Cannot Continue"
    
}
function InstallAssembly([string]$assemblyName){
	
	Write-Host "Gacing $assemblyName"
	$currentExceutionDir = split-path $SCRIPT:MyInvocation.MyCommand.Path -parent
	write-host $currentExceutionDir
	$assemblyNameWithPath = $currentExceutionDir + "\" + $assemblyName
	#$assemblyNameWithPath =  $assemblyName
	$fullAssemblyName = ([system.reflection.assembly]::loadfile($assemblyNameWithPath)).FullName
	$gacPath = "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools"
	if(-Not (Test-Path($gacPath))){
		$gacpath = "C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools"
		if (-Not(Test-Path($gacPath)))
		{
			RaiseException("Gacutil location not found");
		}
	}
	$gacutil =$gacPath + "\gacutil.exe"
	$argumentList = "/i `"" + $assemblyName + "`""
	write-host "Start-Process -NoNewWindow -FilePath $gacutil -ArgumentList $argumentList -Wait -PassThru"
	$gacUninstallCode = (Start-Process -NoNewWindow -FilePath $gacutil -ArgumentList $argumentList -Wait -PassThru).ExitCode
    if($gacUninstallCode -ne 0)
    {
        RaiseException("Gacing Assembly Failed");
    }
    
}
function UnGacAssemblies([string]$assemblyName)
{
	Write-Host "UnGacing assemblies" + $assemblyName
	$currentExceutionDir = split-path $SCRIPT:MyInvocation.MyCommand.Path -parent
	$assemblyNameWithPath = $currentExceutionDir + "\" + $assemblyName
	#assemblyNameWithPath =  ".\" + $assemblyName
	$fullAssemblyName = ([system.reflection.assembly]::loadfile($assemblyNameWithPath)).FullName
	UnGacAssembliesByDisplayName $fullAssemblyName
	
	

}

function UnGacAssembliesByDisplayName([string]$fullAssemblyName)
{	
	$gacPath = "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools"
	if(-Not (Test-Path($gacPath))){
		$gacpath = "C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools"
		if (-Not(Test-Path($gacPath)))
		{
			RaiseException("Gacutil location not found");
		}
	}
	$gacutil =$gacPath + "\gacutil.exe"
	$argumentList = "/u `"" + $fullAssemblyName + "`""
	write-host "Start-Process -NoNewWindow -FilePath $gacutil -ArgumentList $argumentList -Wait -PassThru"
	$gacUninstallCode = (Start-Process -NoNewWindow -FilePath $gacutil -ArgumentList $argumentList -Wait -PassThru).ExitCode
    if($gacUninstallCode -ne 0)
    {
        RaiseException("UnGacing Assembly Failed");
    }
	
	

}
ungacassemblies "BouncyCastle.Crypto.dll"
ungacassemblies "DecryptPGP.dll"
ungacassemblies "EncryptPGP.dll"

InstallAssembly "BouncyCastle.Crypto.dll"
InstallAssembly "DecryptPGP.dll"
InstallAssembly "EncryptPGP.dll"

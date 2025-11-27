
$logpath = "C:\VaultIISLogParser\IISLogs\*" 
$vaultparserpath = "C:\VaultIISLogParser\"
$reportpath = "C:\VaultIISLogParser\VaultUsage.report"
$Files = Get-ChildItem C:\VaultIISLogParser\archive 
Foreach ($file in $Files) {
	Remove-Item $logpath -recurse
	$path = "C:\VaultIISLogParser\archive\" + $file.name
	Copy-Item $path C:\VaultIISLogParser\IISLogs -recurse
	[IO.Directory]::SetCurrentDirectory($vaultparserpath)
	$args = "(local)\EETAnalysis " +  $file.name
	$p = [diagnostics.process]::Start("cmd.exe", "/c start /wait processdata.bat " + $args)
    $p.WaitForExit()
	$newname = $file.name + ".report"
	Rename-Item $reportpath $newname
}


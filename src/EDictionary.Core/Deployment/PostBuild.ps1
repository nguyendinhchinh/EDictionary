param(
   [String] $SolutionDir,
   [String] $TargetDir)

$ErrorActionPreference = "Stop"

$Prompt = "PostBuild>"

$SourceSqlitePath = "${SolutionDir}EDictionary\Data\words.sqlite"
$TargetSqlitePath = "${TargetDir}words.sqlite"
$TargetSqliteDir = "$TargetDir"

$SourceAudioPath = "${SolutionDir}..\tool\data\audio"
$TargetAudioPath = "${TargetDir}audio"
$TargetAudioDir = "$TargetDir"

Write-Host "${Prompt} Running PostBuild script..."

if (!(Test-Path -Path "$TargetSqlitePath"))
{
	Write-Host "${Prompt} sqlite file not found. Copying new file to target dir..."
	Copy-Item -Path "$SourceSqlitePath" -Destination "$TargetSqliteDir"
}
else
{
	# copy sqlite file if filesize not equal
	$sourceSqliteFile = Get-ChildItem "$SourceSqlitePath"
	$targetSqliteFile = Get-ChildItem "$TargetSqlitePath"

	if (Compare-Object $sourceSqliteFile $targetSqliteFile -Property Length)
	{
		Write-Host "${Prompt} Detect sqlite file size changed ($($sourceSqliteFile.Length) vs $($targetSqliteFile.Length)). Copying new file to target dir..."
		Copy-Item -Path "$SourceSqlitePath" -Destination "$TargetSqliteDir"
	}
}

if (!(Test-Path -Path "$TargetAudioPath"))
{
	Write-Host "${Prompt} audio dir not found. Copying audio files to target dir..."
	New-Item -ItemType directory -Path "$TargetAudioPath"
	Copy-Item -Path "${SourceAudioPath}\*.mp3" -Destination "${TargetAudioPath}"
}

# vim: ft=conf

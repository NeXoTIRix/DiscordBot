$PATH = Join-Path -Path $PSScriptRoot -ChildPath "Dependencies\Lavalink"
IF (-not (Test-Path -Path (Join-Path $PATH -ChildPath "Lavalink.jar"))) { Write-Error "$(Join-Path (Get-Location) -ChildPath "Lavalink.jar") nicht gefunden." }
Start-Process powershell -ArgumentList "java -jar .\Lavalink.jar" -NoNewWindow -WorkingDirectory $PATH
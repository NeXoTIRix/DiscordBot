$PATH = Join-Path -Path $PSScriptRoot -ChildPath "LavalinkBot"
IF (-not (Test-Path -Path $PATH)) { Write-Error "$PATH nicht gefunden." }
Start-Process powershell -ArgumentList "dotnet build; dotnet run" -NoNewWindow -WorkingDirectory $PATH
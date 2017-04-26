param([switch]$Elevated)
function Check-Admin {
$currentUser = New-Object Security.Principal.WindowsPrincipal $([Security.Principal.WindowsIdentity]::GetCurrent())
$currentUser.IsInRole([Security.Principal.WindowsBuiltinRole]::Administrator)
}
if ((Check-Admin) -eq $false)  {
if ($elevated)
{
# could not elevate, quit
}
 
else {
 
Start-Process powershell.exe -Verb RunAs -ArgumentList ('-noprofile -noexit -file "{0}" -elevated' -f ($myinvocation.MyCommand.Definition))
}
exit
}

try {
  Get-NetFirewallRule -DisplayName BeershopDocker -ErrorAction Stop
  Write-Host "Rule found"
}
  catch [Exception] {
  New-NetFirewallRule -DisplayName beershop-Inbound -Confirm -Description "beershop Inbound Rule for port range 5100-5105" -LocalAddress Any -LocalPort 5100-5105 -Protocol tcp -RemoteAddress Any -RemotePort Any -Direction Inbound
  New-NetFirewallRule -DisplayName beershop-Outbound -Confirm -Description "beershop Outbound Rule for port range 5100-5105" -LocalAddress Any -LocalPort 5100-5105 -Protocol tcp -RemoteAddress Any -RemotePort Any -Direction Outbound
}
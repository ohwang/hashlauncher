# HKEY_LOCAL_MACHINE\Software\classes\hashbang.script\shell\open
# HKEY_Current_USER\Software\classes\
# http://superuser.com/questions/204354/how-do-i-get-ftype-assoc-to-match-windows-explorer

$config = cat config.json | ConvertFrom-Json

echo $config

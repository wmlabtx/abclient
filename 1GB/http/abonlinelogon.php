<html><head></head><body>
<?php
$nick = $_POST['nick'];
$version = $_POST['version'];
$days = 0;
if( isset($_POST['days']) )
{
  $days = $_POST['days'];
}

$server = 'mysql74.1gb.ru';
$database = 'gb_abclient';
$username = 'gb_abclient';
$password = '77f0f47e90';
$link = mysqli_connect($server, $username, $password, $database);
if(!$link)
{
  echo "Error: Unable to connect to MySQL." . PHP_EOL;
  echo "Debugging errno: " . mysqli_connect_errno() . PHP_EOL;
  echo "Debugging error: " . mysqli_connect_error() . PHP_EOL;
  exit;
}
else
{
  echo "Success: A proper connection to MySQL was made! The database is great." . PHP_EOL;
  echo "Host information: " . mysqli_get_host_info($link) . PHP_EOL;

  mysqli_query($link, "SET character_set_client='utf8'");
  mysqli_query($link, "SET character_set_results='utf8'");
  mysqli_query($link, "SET SESSION collation_connection = 'utf8_general_ci'");
  mysqli_query($link, "SET NAMES 'utf8'");

  if (strlen($nick) >= 2)
  {
    if ($stmt = mysqli_prepare($link, "SELECT COUNT(*) FROM users WHERE nick=?"))
    {
      mysqli_stmt_bind_param($stmt, "s", $nick);
      mysqli_stmt_execute($stmt);
      mysqli_stmt_bind_result($stmt, $existance);
      mysqli_stmt_fetch($stmt);
      mysqli_stmt_close($stmt);
    }
    
    echo "Existance is: " . $existance;  

    if ($existance == 0)
    {
      if ($stmt = mysqli_prepare($link, "INSERT INTO users (nick,version,days,lastlogon,notes) VALUES (?,?,?,NOW(),' ')"))
      {
        mysqli_stmt_bind_param($stmt, "sss", $nick, $version, $days);
        mysqli_stmt_execute($stmt);
        mysqli_stmt_close($stmt);
      }
    }
    else
    {
      if ($stmt = mysqli_prepare($link, "UPDATE users SET version=?, days=?, lastlogon=NOW() WHERE nick=?"))
      {
        mysqli_stmt_bind_param($stmt, "sss", $version, $days, $nick);
        mysqli_stmt_execute($stmt);
        mysqli_stmt_close($stmt);
      }
    }

    if ($stmt = mysqli_prepare($link, "SELECT COUNT(*) FROM users WHERE (lastlogon > NOW() - INTERVAL 1 DAY) AND (days <> 0)"))
    {
      mysqli_stmt_execute($stmt);
      mysqli_stmt_bind_result($stmt, $amount);
      mysqli_stmt_fetch($stmt);
      mysqli_stmt_close($stmt);
      echo '<span id=dayuserscount>' . $amount . '</span>';
    }

    if ($stmt = mysqli_prepare($link, "SELECT notes FROM users WHERE nick=?"))
    {
      mysqli_stmt_bind_param($stmt, "s", $nick);
      mysqli_stmt_execute($stmt);
      mysqli_stmt_bind_result($stmt, $notes);
      mysqli_stmt_fetch($stmt);
      mysqli_stmt_close($stmt);
      echo '<span id=notes>' . $notes . '</span>';
    }
  }

  mysqli_close($link);
}
?>
</body></html>
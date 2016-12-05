<?php
include('config.php');

$uid = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['uid']))));

// Check connection
if (mysqli_connect_errno($con)) {
    return "4";
}

$result = mysql_query("UPDATE users SET loggedin = '0' WHERE username = '" . $uid . "'");
$result = mysql_query("SELECT loggedin FROM users WHERE username = '" . $uid . "'");
$row    = mysql_fetch_array($result);

if ($row["loggedin"] == "0")
    echo "1";
else if ($row["loggedin"] == "1")
    echo "0";

?>
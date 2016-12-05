<?php
include('config.php');
include('security.php');

$Security = new Security();

$uid  = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['uid']))));
$pwd  = $Security->Encrypt(mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['pwd'])))), "@#^!@(^%#*@");
$npwd  = $Security->Encrypt(mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['npwd'])))), "@#^!@(^%#*@");

// Check connection
if (mysqli_connect_errno($con)) {
    return "4";
}

$result = mysql_query("SELECT username FROM users WHERE username = '" . $uid . "' AND password = '" . $pwd . "'");
$row    = mysql_fetch_array($result);
if (!empty($row["username"])) {
    $result = mysql_query("UPDATE users SET password = '" . $npwd . "' WHERE username = '" . $uid . "'");
    echo "1";
} else {
    echo "Could not update account details." .$pwd. " " .$npwd;
}

?>
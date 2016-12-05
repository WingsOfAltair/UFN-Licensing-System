<?php
include('../api/config.php');
include('../api/security.php');

$Security = new Security();

session_start();

$uid = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['uid']))));
$pwd = $Security->Encrypt(mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['pwd'])))), "@#^!@(^%#*@");
$today      = date('Y/m/d h:i:s A');
$expirydate = date('Y/m/d h:i:s A');

// Check connection
if (mysqli_connect_errno($con)) {
    return "4";
}

$result = mysql_query("SELECT username, password, rank, loggedin FROM users WHERE username = '" . $uid . "' AND password = '" . $pwd . "' AND rank = 'Administrator'");
$row    = mysql_fetch_array($result);
if ($row["username"] == $uid) {
    if ($row["password"] == $pwd) {
	$_SESSION['uid'] = $row["username"];
	$_SESSION['pwd'] = $row["password"];
	$_SESSION['rank'] = $row["rank"];
	header('Location: main');
    } else {
        echo "Incorrect username/password combination.";
    }
} else {
    echo "Incorrect username/password combination.";
}

?>
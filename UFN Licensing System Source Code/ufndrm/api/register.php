<?php
include('config.php');
include('security.php');
require_once("authkey.php");

$Security = new Security();

$uid        = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['uid']))));
$pwd        = $Security->Encrypt(mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['pwd'])))), "@#^!@(^%#*@");
$email      = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['email']))));
$sq         = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['sq']))));
$sa         = $Security->Encrypt(mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['sa'])))), "@#^!@(^%#*@");
$key        = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['key']))));
$rank       = "Trial Account";
$expirydate = date('Y/m/d h:i:s A');

// Check connection
if (mysqli_connect_errno($con)) {
    return "0";
}

$result = mysql_query("SELECT username FROM users WHERE username = '" . $uid . "'");
$row    = mysql_fetch_array($result);
if ($row["username"] == $uid) {
    echo "2";
} else {
    $result = mysql_query("SELECT email FROM users WHERE email = '" . $email . "'");
    $row    = mysql_fetch_array($result);
    if ($row["email"] == $email) {
        echo "3";
    } else {
        if (!empty($key)) {
            if (activatecode($key, $uid) == "1") {
                $rank       = "VIP Member";
                $expirydate = date('Y/m/d h:i:s A', strtotime("+1 month"));
                mysql_query("INSERT INTO users(username, password, email, sq, sa, rank, expirydate)VALUES('$uid', '$pwd', '$email', '$sq', '$sa', '$rank', '$expirydate')");
                echo "1";
            } else {
                echo "-1";
            }
        } else if (empty($key)) {
            $rank       = "Trial Account";
            $expirydate = date('Y/m/d h:i:s A', strtotime("+7 days"));
            mysql_query("INSERT INTO users(username, password, email, sq, sa, rank, expirydate)VALUES('$uid', '$pwd', '$email', '$sq', '$sa', '$rank', '$expirydate')");
            echo "1";
        }
    }
}

?>
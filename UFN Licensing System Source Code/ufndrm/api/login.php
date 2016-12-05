<?php
include('config.php');
include('security.php');

$Security = new Security();

$uid        = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['uid']))));
$pwd        = $Security->Encrypt(mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['pwd'])))), "@#^!@(^%#*@");
$today      = date('Y/m/d h:i:s A');
$expirydate = date('Y/m/d h:i:s A');

// Check connection
if (mysqli_connect_errno($con)) {
    return "4";
}

$result = mysql_query("SELECT username, password, rank, loggedin, expirydate FROM users WHERE username = '" . $uid . "' AND password = '" . $pwd . "'");
$row    = mysql_fetch_array($result);
if ($row["username"] == $uid) {
    if ($row["password"] == $pwd) {
		$today = strtotime($today);
		$expirydate = strtotime($row["expirydate"]);
        if ($expirydate - 7200 > $today) {
            if ($row["loggedin"] == "0") {
                $result = mysql_query("UPDATE users SET loggedin = '1' WHERE username = '" . $uid . "'");
                echo "2|" .$row["rank"]. "|" .$row["expirydate"];
            } else {
                echo "5||";
            }
        } else {
            echo "3|" .$row["rank"]. "|" .$row["expirydate"];
        }
    } else {
        echo "1||";
    }
} else {
    echo "0||";
}

?>
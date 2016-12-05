<?php

include('config.php');
require_once("authkey.php");

$uid        = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['uid']))));
$key        = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['key']))));
$rank       = "Trial Account";
$expirydate = date('Y/m/d h:i:s A');

// Check connection
if (mysqli_connect_errno($con)) {
	return "4";
}

$result = mysql_query("SELECT username FROM users WHERE username = '" . $uid . "'");
$row    = mysql_fetch_array($result);
if ($row["username"] == $uid) {
	if (!empty($key)) {
		if (activatecode($key, $uid) == "1") {
			$rank       = "VIP Member";
			$expirydate = date('Y/m/d h:i:s A', strtotime("+1 month"));
			mysql_query("UPDATE users SET rank = '" . $rank . "' WHERE username = '" . $uid . "'");
			mysql_query("UPDATE users SET expirydate = '" . $expirydate . "' WHERE username = '" . $uid . "'");
			echo "1";
		} else {
			echo "0";
		}
	}
} else {
	echo "-1";
}

?>
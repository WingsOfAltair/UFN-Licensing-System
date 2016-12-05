<?php

include('config.php');

// Check connection
if (mysqli_connect_errno($con)) {
	header("location: ../ui/main");
}

$userid = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_GET['userid']))));
$result = mysql_query("SELECT * FROM users WHERE id = '" . $userid . "'");
$row    = mysql_fetch_array($result);
if ($row["id"] == $userid) {
	if (!empty($userid)) {
		mysql_query("DELETE FROM users WHERE id = '" . $userid . "'");
		header("location: ../ui/main");
	}
} else {
	header("location: ../ui/main");
}

?>
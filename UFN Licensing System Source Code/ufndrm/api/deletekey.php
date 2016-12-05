<?php

include('config.php');

// Check connection
if (mysqli_connect_errno($con)) {
	header("location: ../ui/keys");
}

$keyid = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_GET['keyid']))));

$result = mysql_query("SELECT * FROM codes WHERE id = '" . $keyid . "'");
$row    = mysql_fetch_array($result);
if ($row["id"] == $keyid) {
	if (!empty($keyid)) {
		mysql_query("DELETE FROM codes WHERE id = '" . $keyid . "'");
		header("location: ../ui/keys");
	}
} else {
	header("location: ../ui/keys");
}

?>
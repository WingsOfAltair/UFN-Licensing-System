<?php
include('config.php');
$uid = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['uid']))));

// Check connection
if (mysqli_connect_errno($con)) {
    return "4";
}
$result = mysql_query("SELECT sq FROM users WHERE username = '" . $uid . "'");
$row    = mysql_fetch_array($result);
if (!empty($row["sq"])) {
    echo $row["sq"];
} else {
    echo "0";
}

?>
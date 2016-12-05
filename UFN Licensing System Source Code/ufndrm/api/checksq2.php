<?php
include('config.php');
$email = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['email']))));

// Check connection
if (mysqli_connect_errno($con)) {
    return "4";
}

$result = mysql_query("SELECT sq FROM users WHERE email = '" . $email . "'");
$row    = mysql_fetch_array($result);
if (!empty($row["sq"])) {
    echo $row["sq"];
} else {
    echo "0";
}

?>
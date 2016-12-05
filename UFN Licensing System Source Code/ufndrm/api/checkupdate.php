<?php
include('config.php');
$userversion   = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['version']))));
$updateversion = "1.0.0.0";
$updatelink    = "http://localhost/ufndrm/updates/update1.rar";
$updatedetails = "This update contains lots of new features, bug fixes, and stability improvements.";

// Check connection
if (mysqli_connect_errno($con)) {
    return "4";
}

if ($updateversion == $userversion) {
    echo "0";
} else {
    echo $updateversion . "|" . $updatedetails . "|" . $updatelink;
}
?>
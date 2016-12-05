<?php
include('config.php');
include('security.php');

$Security = new Security();

$email = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['email']))));
$sq    = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['sq']))));
$sa    = $Security->Encrypt(mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['sa'])))), "@#^!@(^%#*@");

// Check connection
if (mysqli_connect_errno($con)) {
    return "4";
}

$result = mysql_query("SELECT username FROM users WHERE email = '" . $email . "' AND sa = '" . $sa . "' AND sq = '" . $sq . "'");
$row    = mysql_fetch_array($result);
if (!empty($row["username"])) {
    $U=$row["username"];
    $to      = $email;
    $subject = 'UFN DRM | Forgotten User';
    $message = 'Dear customer,\n\n This email was sent automaticlly by UFN DRM in 
		response to your request to recover your username.
		 \n Your username is: '.$U;
    $headers = 'aaa@example.com' . "\r\n".phpversion();
    mail($to, $subject, $message, $headers); 
    echo "Your username will be sent to the registered email address.";
} else {
    echo "Invalid answer.";
}

?>
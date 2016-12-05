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

$result = mysql_query("SELECT password FROM users WHERE email = '" . $email . "' AND sa = '" . $sa . "' AND sq = '" . $sq . "'");
$row    = mysql_fetch_array($result);
if (!empty($row["password"])) {
    $A=$Security->decrypt($row["password"], "@#^!@(^%#*@");
    $to      = $email;
    $subject = 'UFN DRM | Forgotten Passwprd';
    $message = 'Dear customer,\n\n This email was sent automaticlly by the system in 
		response to your request to recover your password.
		 \n Your password is: '.$A;
    $headers = 'fradees_sameh@yahoo.com' . "\r\n".phpversion();
    mail($to, $subject, $message, $headers); 
    echo "Your password: " . $A;
} else {
    echo "Invalid answer.";
}

?>
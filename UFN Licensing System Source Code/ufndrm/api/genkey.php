<?php
include('config.php');
require_once("authkey.php");

$keyamount = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['amountofkeys']))));
$key = "";
$keys = "";

if (isset(mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['req']))))))
	$req = 1;
else $req = 0;

// Check connection
if (mysqli_connect_errno($con)) {
	return "4";
}

for ($i = 0; $i < $keyamount; $i++){
	$key = genkey(5) . "-" . genkey(5) . "-" . genkey(6) . "-" . genkey(5) . "-" . genkey(7) . "-" . genkey(4) . "-" . genkey(2) . "UFN";
	mysql_query("INSERT INTO codes(name, code, details, activated)VALUES('A new random key.', '$key', 'This is a randomly generated test code to experiment the code redemption feature in the api.', '0')");
	if ($keys != "")
		$keys = $keys . "|" . $key;
	else $keys = $key;
}
if ($req == 0)
	echo $keys;
else header('location: ../ui/keygen/?success=1&keysamount=' . $keyamount);

function genkey($length = 0){
	$characters = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
	$charactersLength = strlen($characters);
	$randomString = '';
	for ($i = 0; $i < $length; $i++) {
        $randomString .= $characters[rand(0, $charactersLength - 1)];
	}
	return $randomString;
}

?>
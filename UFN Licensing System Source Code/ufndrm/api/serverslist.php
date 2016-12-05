<?php
include('config.php');

$serverslist = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['serverslist']))));
$servers = "";

// Check connection
if (mysqli_connect_errno($con)) {
    return "4";
}

$query = mysql_query("SELECT * FROM servers");
while($result = mysql_fetch_array($query))
{
	if ($servers != "")
		$servers = $servers . "|" . $result['ip'];
	else $servers = $result['ip'];
}
echo $servers;

?>
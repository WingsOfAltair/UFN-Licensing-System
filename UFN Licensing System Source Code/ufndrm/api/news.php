<?php
include('config.php');

// Check connection
if (mysqli_connect_errno($con)) {
    return "4";
}

$received = mysql_real_escape_string(htmlspecialchars(strip_tags(addslashes($_POST['news']))));
$news = "";

$query = mysql_query("SELECT * FROM news");
$newsnumber = 1;

while($result = mysql_fetch_array($query))
{
	if ($news != "")
		$news = $news . "|" . $newsnumber++ . "- " . $result['description'];
	else $news = $newsnumber++ . "- " . $result['description'];
}
echo $news;
?>
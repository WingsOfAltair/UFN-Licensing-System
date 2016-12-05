<?php

session_start();

if (isset($_SESSION['uid'])){
	if ($_SESSION['uid'] == ""){
		header('location: ../');
	}
}
else header('location: ../');

if (isset($_GET['logout'])){
	if ($_GET['logout'] == "1"){
		session_destroy();
		header('location: ../');
	}
}
?>
<html>
<title>UFN Digital Rights Management - Web Based Administration Panel</title>
<link href="../ufn_ui.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../ufn_scripts.js"></script>
<body>
<div id="container">
<div id="header">
<h1 class="frm">UFN DRM System - Web Based Administration Panel</h1>
</div>
<div id="menu">
<ul>
<li><input type="button" class="btn" name="support" value="Support" onclick="gotoSupport()" /></li>
<li><input type="button" class="btn" name="about" value="About" onclick="gotoAbout()" /></li>
<li><input type="button" class="btn" name="download" value="Download" onclick="gotoDownload()" /></li>
<li><input type="button" class="btn" name="mngUsers" value="Manage Users" onclick="gotoMngUsers()" /></li>
<li><input type="button" class="btn" name="keygen" value="License Generator" onclick="gotoKeygen()" /></li>
<li><input type="button" class="btn" name="logout" value="Log out" onclick="Logout()" /></li>
</ul>
</div>
<div id="licensecontent">
<form name="tbl_licenses" class="frm">
<table border="1px solid black;" style="border-collapse:collapse;" cellspacing="0" cellpadding="4">
<?php
echo '<tr>';
echo '<th>ID</th>';
echo '<th>License Name</th>';
echo '<th>License Key</th>';
echo '<th>License Details</th>';
echo '<th>License Status</th>';
echo '<th>License Activation Date</th>';
echo '<th>License Activator</th>';
echo '<th><label  style="color:green;">Available</label> = Not activated<br/><label  style="color:red;">Used</label> = Already activated</th>';
echo '</tr>';
include('../../api/config.php');
$keys = mysql_query("SELECT * FROM codes");
while($key = mysql_fetch_array($keys))
{
	echo '<tr>';
	echo '<td align="center">' . $key['id'] . ' </td>';
	echo '<td align="center">' . $key['name'] . '</td>';
	echo '<td align="center">' . $key['code'] . '</td>';
	echo '<td align="center">' . $key['details'] . '</td>';
	if ($key['activated'] == 1)
		echo '<td align="center"><label style="color:red;">Used</label></td>';
	else echo '<td align="center"><label style="color:green;">Available</label></td>';
	echo '<td align="center">' . $key['dateofactivation'] . '</td>';
	echo '<td align="center">' . $key['activator'] . '</td>';
	echo '<td align="center"><input type="button" class="btn2" value="Delete Key" onclick="gotoDeleteKey(' . $key['id'] . ')" /></td>';
	echo '</tr>';
}
?>
</table>
</form>
</div>
</div>
</body>
</html>
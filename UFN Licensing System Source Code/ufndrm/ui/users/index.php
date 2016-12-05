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
<li><input type="button" class="btn" name="licenses" value="Manage Licenses" onclick="gotoMngLicenses()" /></li>
<li><input type="button" class="btn" name="keygen" value="License Generator" onclick="gotoKeygen()" /></li>
<li><input type="button" class="btn" name="logout" value="Log out" onclick="Logout()" /></li>
</ul>
</div>
<div id="licensecontent">
<form name="tbl_users" class="frm">
<center>
<table border="1px solid black;" style="border-collapse:collapse;" cellspacing="0" cellpadding="4">
<?php
include('../../api/config.php');
$users = mysql_query("SELECT * FROM users");
echo '<tr>';
echo '<th>ID</th>';
echo '<th>Username</th>';
echo '<th>Password</th>';
echo '<th>Rank</th>';
echo '<th>Email</th>';
echo '<th>Expiration Date</th>';
echo '<th>Status</th>';
echo '<th>Sec. Question</th>';
echo '<th>Sec. Answer</th>';
echo '<th><label  style="color:green;">Online Users</label>: ' . (int)mysql_num_rows(mysql_query("SELET id FROM users WHERE loggedin = '1'")) . '<br/><label  style="color:red;">Total Users</label>: ' . (int)mysql_num_rows($users) . '</th>';
echo '</tr>';
while($user = mysql_fetch_array($users))
{
	echo '<tr>';
	echo '<td align="center">' . $user['id'] . ' </td>';
	echo '<td align="center">' . $user['username'] . '</td>';
	echo '<td align="center">' . $user['password'] . '</td>';
	echo '<td align="center">' . $user['rank'] . '</td>';
	echo '<td align="center">' . $user['email'] . '</td>';
	echo '<td align="center">' . $user['expirydate'] . '</td>';
	if ($user['loggedin'] == 0)
		echo '<td align="center"><label style="color:red;">Offline</label></td>';
	else echo '<td align="center"><label style="color:green;">Available</label></td>';
	echo '<td align="center">' . $user['sq'] . '</td>';
	echo '<td align="center">' . $user['sa'] . '</td>';
	echo '<td align="center"><input type="button" class="btn2" value="Delete User" onclick="gotoDeleteUser(' . $user['id'] . ')" /></td>';
	echo '</tr>';
}
?>
</center>
</table>
</form>
</div>
</div>
</body>
</html>
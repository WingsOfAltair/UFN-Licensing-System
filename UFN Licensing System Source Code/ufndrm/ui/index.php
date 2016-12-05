<?php

session_start();

if (isset($_SESSION['uid'])){
	if ($_SESSION['uid'] != ""){
		header('location: main');
	}
}

if (isset($_GET['logout'])){
	if ($_GET['logout'] == "1"){
		session_destroy();
		header('location: ../');
	}
}
?>
<html>
<head>
<title>UFN Digital Rights Management - Administration Login Portal</title>
<link href="ufn_ui.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="ufn_scripts.js"></script>
<div id="container">
<div id="header">
<h1 class="frm">UFN DRM System - Web Based Administration Panel</h1>
</div>
</head>
<body>
<div id="menu">
<ul>
<li><input type="button" class="btn" name="support" value="Support" onclick="gotoSupport()" /></li>
<li><input type="button" class="btn" name="about" value="About" onclick="gotoAbout()" /></li>
<li><input type="button" class="btn" name="download" value="Download" onclick="gotoDownload2()" /></li>
</ul>
</div>
<div id="content">
<form name="lgn" method="post" action="login.php" class="frm">
<input type="text" name="uid" id="uid" placeholder="Enter username" /><br/>
<input type="password" name="pwd" id="pwd" placeholder="Enter password" /><br/><br/>
<input type="submit" class="btn" name="login" id="login" value="Log in" />
</form>
</div>
</body>
<div id="footer">
<foot>
UFN DRM System 2015, no rights reserved. Get rekt faggits! 420 no ragrets neegrumps ~~
</foot>
</div>
</div>
</html>
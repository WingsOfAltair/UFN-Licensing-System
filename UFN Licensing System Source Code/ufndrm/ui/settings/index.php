<?php

session_start();

if (isset($_SESSION['uid'])){
	if ($_SESSION['uid'] == ""){
		header('location: ../ui');
	}
}
else header('location: ../ui');
?>
<html>
<head>
<title>UFN Digital Rights Management - Web Based Administration Panel</title>
<link href="../ufn_ui.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../ufn_scripts.js"></script>
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
<li><input type="button" class="btn" name="download" value="Download" onclick="gotoDownload()" /></li>
<li><input type="button" class="btn" name="mngLicenses" value="Manage Licenses" onclick="gotoMngLicenses()" /></li>
<li><input type="button" class="btn" name="mngUsers" value="Manage Users" onclick="gotoMngUsers()" /></li>
<li><input type="button" class="btn" name="logout" value="Log out" onclick="Logout()" /></li>
</ul>
</div>
<div id="content">
<form name="keygen" method="post" class="frm">
<input type="number" name="amountofkeys" id="amountofkeys" placeholder="Amount of keys" /><br/>
<?php 
if (isset($_GET['success'])){
	if ($_GET['success'] == "1"){
		echo '<label style="color:green;">Successfully generated ' . $_GET['keysamount'] . ' key.</label><br/><br/>';
	}
}
?>
<input type="submit" class="btn" name="genkeys" id="genkeys" value="Generate Keys" />
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
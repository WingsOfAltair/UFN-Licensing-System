function gotoSupport(){
	//window.location.href = "support";
}

function gotoAbout(){
	//window.location.href = "about";
}

function Logout(){
	window.location.href = "../main/index.php?logout=1";
}

function gotoMngLicenses(){
	window.location.href = "../keys";
}

function gotoMngUsers(){
	window.location.href = "../main";
}

function gotoKeygen(){
	window.location.href = "../keygen";
}

function gotoDeleteKey(keyid){
	window.location.href = "../../api/deletekey.php?keyid=" + keyid;
}

function gotoDeleteUser(userid){
	window.location.href = "../../api/deleteuser.php?userid=" + userid;
}

function gotoDownload(){
	window.location.href = "../../downloads/FlappyBox.exe";
}

function gotoDownload2(){
	window.location.href = "../downloads/FlappyBox.exe";
}
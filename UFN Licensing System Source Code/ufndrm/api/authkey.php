<?php

function activatecode($key, $activator)
{
    include('config.php');
    
    $keystate  = 0;
    $keystateu = 1;
    $today     = date('Y/m/d h:i:s A');
    
    // Check connection
    if (mysqli_connect_errno($con)) {
        return "0";
    }
    
    $result = mysql_query("SELECT id FROM codes WHERE code = '" . $key . "' AND activated = '" . $keystate . "'");
    $row    = mysql_fetch_array($result);
    if (!empty($row["id"])) {
        $result = mysql_query("UPDATE codes SET activated = '" . $keystateu . "' WHERE code = '" . $key . "'");
        $result = mysql_query("UPDATE codes SET dateofactivation = '" . $today . "' WHERE code = '" . $key . "'");
        $result = mysql_query("UPDATE codes SET activator = '" . $activator . "' WHERE code = '" . $key . "'");
        return "1";
    } else {
        return "-1";
    }
}

?>
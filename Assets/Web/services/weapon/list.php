<?php
ini_set('display_errors', 1);
require_once __DIR__ . "/../../dao/WeaponDAO.php";

header('Content-Type: application/json');

$weapon = WeaponDAO::listWeapons();

if (isset($_GET["isWebsite"]) && $_GET["isWebsite"] == 1){
    echo json_encode($weapon);
    http_response_code(200);
    return;
}

echo '{"weapons":' . json_encode($weapon) . '}';
http_response_code(200);

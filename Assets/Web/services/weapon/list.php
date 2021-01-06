<?php
ini_set('display_errors', 1);
require_once __DIR__ . "/../../dao/WeaponDAO.php";

header('Content-Type: application/json');

$weapon = WeaponDAO::listWeapons();

echo '{"weapons":' . json_encode($weapon) . '}';
http_response_code(200);

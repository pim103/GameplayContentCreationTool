<?php
ini_set('display_errors', 1);
require_once __DIR__ . "/../../dao/EffectDAO.php";

header('Content-Type: application/json');

$effect = EffectDAO::listEffects();

if (isset($_GET["isWebsite"]) && $_GET["isWebsite"] == 1){
    echo json_encode($effect);
    http_response_code(200);
    return;
}

echo '{"effects":' . json_encode($effect) . '}';
http_response_code(200);

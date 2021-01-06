<?php
ini_set('display_errors', 1);
require_once __DIR__ . "/../../dao/EffectDAO.php";

if (isset($_POST["effectName"]) && isset($_POST["intervalTime"]) && isset($_POST["lifeTime"]) && isset($_POST["amount"]) && isset($_POST["specialEffect"]))
{
    $success = EffectDAO::addEffect($_POST["effectName"], floatval($_POST["intervalTime"]), floatval($_POST["lifeTime"]), floatval($_POST["amount"]), intval($_POST["specialEffect"]));
    if ($success)
    {
        http_response_code(201);
    }
    else {
        http_response_code(406);
    }
}
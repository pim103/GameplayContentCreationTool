<?php
ini_set('display_errors', 1);
require_once __DIR__ . "/../../dao/WeaponDAO.php";

if (isset($_POST["weaponName"]) && isset($_POST["damages"]) && isset($_POST["rateOfFire"]) && isset($_POST["projSpeed"]) && isset($_POST["projLifeTime"]) && isset($_POST["modelId"]) && isset($_POST["effectId"]))
{
    $success = WeaponDAO::addWeapon($_POST["weaponName"], intval($_POST["damages"]), floatval($_POST["rateOfFire"]), intval($_POST["projSpeed"]), intval($_POST["projLifeTime"]), intval($_POST["modelId"]), intval($_POST["effectId"]));
    if ($success)
    {
        http_response_code(201);
    }
    else {
        http_response_code(406);
    }
}
<?php
ini_set('display_errors', 1);
require_once __DIR__ . "/../../dao/WeaponDAO.php";

if (isset($_POST["id"]))
{
    $success = WeaponDAO::deleteWeapon(intval($_POST["id"]));
    if ($success)
    {
        http_response_code(201);
    }
    else {
        http_response_code(406);
    }
}
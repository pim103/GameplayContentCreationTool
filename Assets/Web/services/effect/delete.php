<?php
ini_set('display_errors', 1);
require_once __DIR__ . "/../../dao/EffectDAO.php";

if (isset($_POST["id"]))
{
    $success = EffectDAO::deleteEffect(intval($_POST["id"]));
    if ($success)
    {
        http_response_code(201);
    }
    else {
        http_response_code(406);
    }
}
<?php
require_once __DIR__ . "/../DatabaseManager.php";
require_once __DIR__ . "/../models/Effect.php";

class EffectDAO
{
    private static function effectFromRows(&$rows)
    {
        $arr = [];
        foreach ($rows as &$row)
        {
            $arr[] = EffectDAO::effectFromRow($row);
        }


        return $arr;
    }

    private static function effectFromRow(&$row)
    {
        return new Effect(
            intval($row["id"]),
            $row["effectName"],
            floatval($row["intervalTime"]),
            floatval($row["lifeTime"]),
            floatval($row["amount"]),
            intval($row["specialEffect"])
        );
    }

    public static function listEffects()
    {
        $rows = DatabaseManager::getSharedInstance()->getAll('SELECT * FROM effect');
        return EffectDAO::effectFromRows($rows);
    }

    public static function getOneEffect($id)
    {
        $rows = DatabaseManager::getSharedInstance()->getAll('SELECT * FROM effect WHERE id='. $id);
        return EffectDAO::effectFromRows($rows);
    }
}
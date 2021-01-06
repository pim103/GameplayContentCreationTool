<?php
require_once __DIR__ . "/../DatabaseManager.php";
require_once __DIR__ . "/../models/Weapon.php";
require_once __DIR__ . "/../dao/EffectDAO.php";


class WeaponDAO
{
    private static function weaponFromRows(&$rows)
    {
        $arr = [];
        foreach ($rows as &$row)
        {
            $arr[] = WeaponDAO::weaponFromRow($row);
        }


        return $arr;
    }

    private static function weaponFromRow(&$row)
    {
        return new Weapon(
            intval($row["id"]),
            $row["weaponName"],
            intval($row["damages"]),
            floatval($row["rateOfFire"]),
            intval($row["projSpeed"]),
            intval($row["projLifeTime"]),
            intval($row["modelId"]),
            EffectDAO::getOneEffect(intval($row["effect"]))
        );
    }

    public static function listWeapons()
    {
        $rows = DatabaseManager::getSharedInstance()->getAll('SELECT * FROM weapon');
        return WeaponDAO::weaponFromRows($rows);
    }
}
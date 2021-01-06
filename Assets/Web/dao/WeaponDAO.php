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

    public static function addWeapon($name, $damages, $rateOfFire, $projSpeed, $projLifeTime, $modelId, $effectId)
    {
        $db = DatabaseManager::getSharedInstance();
        $query = $db->exec('INSERT INTO weapon (weaponName, damages, rateOfFire, projSpeed, projLifeTime, modelId, effect) VALUES (?, ?, ?, ?, ?, ?, ?)', [$name, $damages, $rateOfFire, $projSpeed, $projLifeTime, $modelId, $effectId]);

        return $query != 0;
    }

    public static function deleteWeapon($id)
    {
        $db = DatabaseManager::getSharedInstance();
        $query = $db->exec('DELETE FROM weapon WHERE id='.$id);

        return $query != 0;
    }

    public static function updateWeapon($id, $name, $damage, $rateOfFire, $projSpeed, $projLifeTime, $modelId, $effectId)
    {
        $db = DatabaseManager::getSharedInstance();

        $sql = "UPDATE weapon SET weaponName=?, damages=?, rateOfFire=?, projSpeed=?, projLifeTime=?, modelId=?, effect=? WHERE id=?";
        $params = [$name, $damage, $rateOfFire, $projSpeed, $projLifeTime, $modelId, $effectId, $id];

        $query = $db->getPDO()->prepare($sql);
        $res = $query->execute($params);

        return $res != 0;
    }
}
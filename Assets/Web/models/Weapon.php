<?php


class Weapon implements JsonSerializable
{

    private $weaponId;
    private $weaponName;
    private $damages;
    private $rateOfFire;
    private $projSpeed;
    private $projLifeTime;
    private $modelId;
    private $effect;

    /**
     * Weapon constructor.
     * @param $weaponId
     * @param $weaponName
     * @param $damages
     * @param $rateOfFire
     * @param $projSpeed
     * @param $projLifeTime
     * @param $modelId
     * @param $effect
     */
    public function __construct($weaponId, $weaponName, $damages, $rateOfFire, $projSpeed, $projLifeTime, $modelId, $effect)
    {
        $this->weaponId = $weaponId;
        $this->weaponName = $weaponName;
        $this->damages = $damages;
        $this->rateOfFire = $rateOfFire;
        $this->projSpeed = $projSpeed;
        $this->projLifeTime = $projLifeTime;
        $this->modelId = $modelId;
        $this->effect = $effect;
    }

    /**
     * @return mixed
     */
    public function getWeaponId()
    {
        return $this->weaponId;
    }

    /**
     * @param mixed $weaponId
     */
    public function setWeaponId($weaponId)
    {
        $this->weaponId = $weaponId;
    }

    /**
     * @return mixed
     */
    public function getWeaponName()
    {
        return $this->weaponName;
    }

    /**
     * @param mixed $weaponName
     */
    public function setWeaponName($weaponName)
    {
        $this->weaponName = $weaponName;
    }

    /**
     * @return mixed
     */
    public function getDamages()
    {
        return $this->damages;
    }

    /**
     * @param mixed $damages
     */
    public function setDamages($damages)
    {
        $this->damages = $damages;
    }

    /**
     * @return mixed
     */
    public function getRateOfFire()
    {
        return $this->rateOfFire;
    }

    /**
     * @param mixed $rateOfFire
     */
    public function setRateOfFire($rateOfFire)
    {
        $this->rateOfFire = $rateOfFire;
    }

    /**
     * @return mixed
     */
    public function getProjSpeed()
    {
        return $this->projSpeed;
    }

    /**
     * @param mixed $projSpeed
     */
    public function setProjSpeed($projSpeed)
    {
        $this->projSpeed = $projSpeed;
    }

    /**
     * @return mixed
     */
    public function getProjLifeTime()
    {
        return $this->projLifeTime;
    }

    /**
     * @param mixed $projLifeTime
     */
    public function setProjLifeTime($projLifeTime)
    {
        $this->projLifeTime = $projLifeTime;
    }

    /**
     * @return mixed
     */
    public function getModelId()
    {
        return $this->modelId;
    }

    /**
     * @param mixed $modelId
     */
    public function setModelId($modelId)
    {
        $this->modelId = $modelId;
    }

    /**
     * @return mixed
     */
    public function getEffect()
    {
        return $this->effect;
    }

    /**
     * @param mixed $effect
     */
    public function setEffect($effect)
    {
        $this->effect = $effect;
    }

    /**
     * @inheritDoc
     */
    public function jsonSerialize()
    {
        return get_object_vars($this);
    }
}
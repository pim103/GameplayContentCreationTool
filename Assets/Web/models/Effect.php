<?php


class Effect implements JsonSerializable
{

    private $effectId;
    private $effectName;
    private $interval;
    private $lifeTime;
    private $amount;
    private $SpecialEffect;

    /**
     * Effect constructor.
     * @param $effectId
     * @param $effectName
     * @param $interval
     * @param $lifeTime
     * @param $amount
     * @param $SpecialEffect
     */
    public function __construct($effectId, $effectName, $interval, $lifeTime, $amount, $SpecialEffect)
    {
        $this->effectId = $effectId;
        $this->effectName = $effectName;
        $this->interval = $interval;
        $this->lifeTime = $lifeTime;
        $this->amount = $amount;
        $this->SpecialEffect = $SpecialEffect;
    }

    /**
     * @return mixed
     */
    public function getEffectId()
    {
        return $this->effectId;
    }

    /**
     * @param mixed $effectId
     */
    public function setEffectId($effectId)
    {
        $this->effectId = $effectId;
    }

    /**
     * @return mixed
     */
    public function getEffectName()
    {
        return $this->effectName;
    }

    /**
     * @param mixed $effectName
     */
    public function setEffectName($effectName)
    {
        $this->effectName = $effectName;
    }

    /**
     * @return mixed
     */
    public function getInterval()
    {
        return $this->interval;
    }

    /**
     * @param mixed $interval
     */
    public function setInterval($interval)
    {
        $this->interval = $interval;
    }

    /**
     * @return mixed
     */
    public function getLifeTime()
    {
        return $this->lifeTime;
    }

    /**
     * @param mixed $lifeTime
     */
    public function setLifeTime($lifeTime)
    {
        $this->lifeTime = $lifeTime;
    }

    /**
     * @return mixed
     */
    public function getAmount()
    {
        return $this->amount;
    }

    /**
     * @param mixed $amount
     */
    public function setAmount($amount)
    {
        $this->amount = $amount;
    }

    /**
     * @return mixed
     */
    public function getSpecialEffect()
    {
        return $this->SpecialEffect;
    }

    /**
     * @param mixed $SpecialEffect
     */
    public function setSpecialEffect($SpecialEffect)
    {
        $this->SpecialEffect = $SpecialEffect;
    }

    /**
     * @inheritDoc
     */
    public function jsonSerialize()
    {
        return get_object_vars($this);
    }
}
namespace Weapons
{
    public enum SpecialEffect
    {
        Dot,
        Heal,
        Slow
    }

    public class Effect
    {
        public string effectName;
        public int effectId;
        public float interval;
        public float lifeTime;
        public float amount;
        public SpecialEffect SpecialEffect;
    }
}
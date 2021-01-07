using System.Collections.Generic;
using Weapons;

namespace Database
{
    public class WeaponJson : Weapon
    {
        public List<Effect> effect;

        public Weapon GetWeapon()
        {
            Weapon weapon = this;
            weapon.effect = effect != null && effect.Count > 0 ? effect[0] : null;

            return weapon;
        }
    }
    
    public class WeaponListJson
    {
        public List<WeaponJson> weapons;

        public List<Weapon> GetWeapons()
        {
            List<Weapon> weaponsConverted = new List<Weapon>();
            
            foreach (WeaponJson weapon in weapons)
            {
                weaponsConverted.Add(weapon.GetWeapon());
            }

            return weaponsConverted;
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace Weapons
{
    public class WeaponList
    {
        private static List<Weapon> weapons;
        
        public static void InitWeaponList()
        {
            weapons = new List<Weapon>();
            
            weapons.Add(new Weapon
            {
                damages = 10,
                projSpeed = 10,
                weaponId = 1,
                weaponName = "helloWorld",
                rateOfFire = 1,
                modelId = 0,
                projLifeTime = 5,
                effect = new Effect
                {
                    amount = 5,
                    interval = 1,
                    effectId = 1,
                    effectName = "Brulure",
                    lifeTime = 3,
                    SpecialEffect = SpecialEffect.Dot
                }
            });
            
            weapons.Add(new Weapon
            {
                damages = 0,
                projSpeed = 10,
                weaponId = 2,
                weaponName = "helloWorldHeal",
                rateOfFire = 1,
                modelId = 0,
                projLifeTime = 5,
                effect = new Effect
                {
                    amount = 5,
                    interval = 1,
                    effectId = 2,
                    effectName = "Heal",
                    lifeTime = 3,
                    SpecialEffect = SpecialEffect.Heal
                }
            });
        }

        public static Weapon GetWeaponByName(string name)
        {
            Weapon weaponFound = null;
            
            if (weapons.Exists(weapon => weapon.weaponName == name))
            {
                weaponFound = weapons.First(weapon => weapon.weaponName == name);
            }

            return weaponFound;
        }
        
        public static Weapon GetWeaponById(int id)
        {
            Weapon weaponFound = null;
            
            if (weapons.Exists(weapon => weapon.weaponId == id))
            {
                weaponFound = weapons.First(weapon => weapon.weaponId == id);
            }

            return weaponFound;
        }
    }
}
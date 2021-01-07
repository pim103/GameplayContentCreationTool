using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database;
using FullSerializer;
using UnityEngine;

namespace Weapons
{
    public class WeaponList
    {
        private static List<Weapon> weapons;
        
        public static IEnumerator InitWeaponList()
        {
            weapons = new List<Weapon>();

            yield return DatabaseManager.ListWeapon(AddWeaponsToList);
        }

        private static void AddWeaponsToList()
        {
            weapons.AddRange(DatabaseManager.weaponsLoad);
        }

        private static void LoadWeaponFromDirectory()
        {
            fsSerializer serializer = new fsSerializer();

            foreach (string path in Directory.GetFiles(Application.dataPath + "/Data"))
            {
                if (!path.EndsWith(".json"))
                {
                    continue;
                }
    
                fsData data = fsJsonParser.Parse(File.ReadAllText(path));

                Weapon weapon = null;
                serializer.TryDeserialize(data, ref weapon);

                weapons.Add(weapon);
            }
            
            Debug.Log("Found " + weapons.Count + " weapons");
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

        public static int GetNbWeapons()
        {
            return weapons.Count;
        }
        
        public static Weapon GetNextWeaponInList(int index)
        {
            if (weapons.Count <= index)
            {
                return weapons[0];
            }

            return weapons[index];
        }
    }
}
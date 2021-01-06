#if UNITY_EDITOR_64 || UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using FullSerializer;
using TMPro;
using UnityEditor;
using UnityEngine;
using Weapons;

namespace Editor
{
    public enum EditorType
    {
        Weapon,
        Effect
    }
    
    public class ContentEditor : EditorWindow
    {
        public Weapon newWeapon;

        private bool weaponIsImported;
        private bool eraseIfPossible;
        private string pathWeaponImported;

        public List<Effect> effectsCreated;
        public List<string> effectsChoiceList;

        public EditorType editorType = EditorType.Weapon;

        private EffectEditor effectEditor;
        
        [MenuItem("Tools/Content Editor")]
        private static void ShowWindow()
        {
            Type gameType = Type.GetType("UnityEditor.GameView,UnityEditor.dll");
            ContentEditor window = GetWindow<ContentEditor>("Content Editor", new Type[] {gameType});
        }

        private void OnGUI()
        {
            GUILayout.Label("Content Editor");
            DisplayBody();
        }

        private void DisplayBody()
        {
            if (editorType == EditorType.Effect)
            {
                effectEditor.DisplayEffectPanel(this);
                return;
            }
            
            if (GUILayout.Button("Create Effect"))
            {
                editorType = EditorType.Effect;

                if (effectEditor == null)
                {
                    effectEditor = new EffectEditor();
                }

                if (newWeapon.effect != null)
                {
                    effectEditor.newEffect = newWeapon.effect;
                }
            }
            
            if (newWeapon == null)
            {
                Debug.Log("Create new weapon");
                newWeapon = new Weapon();
            }

            newWeapon.weaponName = EditorGUILayout.TextField("Name", newWeapon.weaponName);
            newWeapon.damages = EditorGUILayout.IntField("Damage", newWeapon.damages);
            newWeapon.rateOfFire = EditorGUILayout.FloatField("Rate of fire", newWeapon.rateOfFire);

            newWeapon.modelId = EditorGUILayout.IntField("Model id", newWeapon.modelId);
            newWeapon.projSpeed = EditorGUILayout.IntField("Projectile speed", newWeapon.projSpeed);
            newWeapon.projLifeTime = EditorGUILayout.IntField("Projectile life time", newWeapon.projLifeTime);

            if (effectsChoiceList == null)
            {
                InitEffectChoiceList();
            }
            
            EditorGUI.BeginChangeCheck();
            int effectSelected = newWeapon.effect != null ? effectsChoiceList.IndexOf(newWeapon.effect.effectName) : 0;
            effectSelected = EditorGUILayout.Popup("Effect", effectSelected, effectsChoiceList.ToArray());

            if (EditorGUI.EndChangeCheck())
            {
                if (effectSelected > 0)
                {
                    newWeapon.effect = effectsCreated[effectSelected - 1];
                }
                else
                {
                    newWeapon.effect = null;
                }
            }

            if (weaponIsImported)
            {
                eraseIfPossible = EditorGUILayout.Toggle("Erase weapon", eraseIfPossible);
            }

            if (GUILayout.Button("Reset Panel"))
            {
                ResetPanel();
            }
            
            if (GUILayout.Button("Import weapon"))
            {
                ImportWeapon();
            }

            if (GUILayout.Button("Save weapon") && CheckValidName())
            {
                string path = Application.dataPath + "/Data/" + newWeapon.weaponName + ".json";

                fsSerializer serializer = new fsSerializer();
                serializer.TrySerialize(newWeapon.GetType(), newWeapon, out fsData data);
                File.WriteAllText(path, fsJsonPrinter.CompressedJson(data));
                
                Debug.Log("Want to save weapon : " + newWeapon.weaponName);
                ResetPanel();
            }

            if (GUILayout.Button("Compute Ids"))
            {
                ComputeIds();
            }
        }

        private void InitEffectChoiceList()
        {
            effectsChoiceList = new List<string>();
            effectsChoiceList.Add("Nothing");

            if (effectsCreated == null)
            {
                return;
            }
    
            foreach (Effect effect in effectsCreated)
            {
                effectsChoiceList.Add(effect.effectName);
            }
        }

        public bool AddEffectToList(Effect newEffect)
        {
            if (effectsCreated == null)
            {
                effectsCreated = new List<Effect>();
            }

            if (effectsCreated.Exists(effect => effect.effectName == newEffect.effectName))
            {
                return false;
            }

            effectsCreated.Add(newEffect);
            InitEffectChoiceList();

            return true;
        }

        private void ComputeIds()
        {
            fsSerializer serializer = new fsSerializer();
            int id = 1;
            int effectId = 1;

            foreach (string path in Directory.GetFiles(Application.dataPath + "/Data"))
            {
                if (!path.EndsWith(".json"))
                {
                    continue;
                }
    
                fsData data = fsJsonParser.Parse(File.ReadAllText(path));

                Weapon weapon = null;
                serializer.TryDeserialize(data, ref weapon);

                weapon.weaponId = id;
                ++id;

                if (weapon.effect != null)
                {
                    weapon.effect.effectId = effectId;
                    ++effectId;
                }

                serializer.TrySerialize(weapon.GetType(), weapon, out fsData dataToSave);
                File.WriteAllText(path, fsJsonPrinter.CompressedJson(dataToSave));
            }
        }

        private bool CheckValidName()
        {
            if (newWeapon.weaponName == "")
            {
                return false;
            }

            if (!weaponIsImported && File.Exists(Application.dataPath + "/Data/" + newWeapon.weaponName + ".json"))
            {
                Debug.LogWarning("File already exist");
                return false;
            }

            if (eraseIfPossible && !String.IsNullOrEmpty(pathWeaponImported))
            {
                Debug.Log("Delete old file");
                Debug.Log(pathWeaponImported);

                if (File.Exists(pathWeaponImported))
                {
                    File.Delete(pathWeaponImported);
                    File.Delete(pathWeaponImported + ".meta");
                }
            }

            return true;
        }

        private void ImportWeapon()
        {
            string path = EditorUtility.OpenFilePanel("Choose a weapon", Application.dataPath + "/Data", "json");

            if (String.IsNullOrEmpty(path))
            {
                return;
            }
            
            fsSerializer serializer = new fsSerializer();
            fsData data = fsJsonParser.Parse(File.ReadAllText(path));

            serializer.TryDeserialize(data, ref newWeapon);
            weaponIsImported = true;
            eraseIfPossible = true;
            pathWeaponImported = path;

            if (newWeapon.effect != null)
            {
                AddEffectToList(newWeapon.effect);
            }
        }

        private void ResetPanel()
        {
            eraseIfPossible = false;
            pathWeaponImported = "";
            weaponIsImported = false;
            newWeapon = null;
        }
    }
}

#endif
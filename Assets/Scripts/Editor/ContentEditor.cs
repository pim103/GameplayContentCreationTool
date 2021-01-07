#if UNITY_EDITOR_64 || UNITY_EDITOR

using Unity.EditorCoroutines.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using Database;
using FullSerializer;
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
        public List<Weapon> weaponsCreated;
        public List<string> effectsChoiceList;
        public List<string> weaponsChoiceList;

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

            if (GUILayout.Button("Load Data"))
            {
                LoadData();
            }

            if (DatabaseManager.weaponsLoad == null || DatabaseManager.effectsLoad == null)
            {
                return;
            }

            if (newWeapon == null)
            {
                newWeapon = new Weapon();
            }

            EditorGUI.BeginChangeCheck();
            int weaponSelected = newWeapon.weaponId != 0 ? weaponsChoiceList.IndexOf(newWeapon.weaponName) : 0;
            weaponSelected = EditorGUILayout.Popup("Choose or create a weapon", weaponSelected, weaponsChoiceList.ToArray());

            if (EditorGUI.EndChangeCheck())
            {
                if (weaponSelected == 0)
                {
                    newWeapon = new Weapon();
                }
                else
                {
                    newWeapon = weaponsCreated[weaponSelected - 1];
                }
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

            if (GUILayout.Button("Save weapon") && CheckValidName())
            {
                AddWeaponToList(newWeapon, weaponSelected != 0);
            }

            if (weaponSelected != 0 && GUILayout.Button("Delete weapon"))
            {
                DeleteWeapon(newWeapon);
            }
        }

        private void LoadData()
        {
            void LoadEffectLambda() => InitEffectChoiceList();
            void LoadWeaponLambda() => InitWeaponChoiceList();
            this.StartCoroutine(DatabaseManager.ListEffect(LoadEffectLambda));
            this.StartCoroutine(DatabaseManager.ListWeapon(LoadWeaponLambda));
        }
        
        private void InitEffectChoiceList()
        {
            if (effectsCreated == null)
            {
                effectsCreated = new List<Effect>();
            }
            
            if (DatabaseManager.effectsLoad != null)
            {
                effectsCreated.Clear();
                effectsCreated.AddRange(DatabaseManager.effectsLoad);
            }

            effectsChoiceList = new List<string> {"Nothing"};

            foreach (Effect effect in effectsCreated)
            {
                effectsChoiceList.Add(effect.effectName);
            }
        }

        private void InitWeaponChoiceList()
        {
            if (weaponsCreated == null)
            {
                weaponsCreated = new List<Weapon>();
            }

            if (DatabaseManager.weaponsLoad != null)
            {
                weaponsCreated.Clear();
                weaponsCreated.AddRange(DatabaseManager.weaponsLoad);
            }

            weaponsChoiceList = new List<string> {"New"};

            foreach (Weapon weapon in weaponsCreated)
            {
                weaponsChoiceList.Add(weapon.weaponName);
            }
        }

        public bool AddEffectToList(Effect newEffect, bool updateExistingEffect = false)
        {
            if (String.IsNullOrEmpty(newEffect.effectName))
            {
                return false;
            }

            this.StartCoroutine(updateExistingEffect
                ? DatabaseManager.UpdateEffect(newEffect, LoadData)
                : DatabaseManager.SaveEffect(newEffect, LoadData));

            return true;
        }

        public bool AddWeaponToList(Weapon weaponToSave, bool updateExistingWeapon = false)
        {
            if (String.IsNullOrEmpty(weaponToSave.weaponName))
            {
                return false;
            }

            this.StartCoroutine(updateExistingWeapon
                ? DatabaseManager.UpdateWeapon(weaponToSave, LoadData)
                : DatabaseManager.SaveWeapon(weaponToSave, LoadData));

            LoadData();
            ResetPanel();

            return true;
        }

        private void DeleteWeapon(Weapon weaponToDelete)
        {
            this.StartCoroutine(DatabaseManager.DeleteWeapon(weaponToDelete, LoadData));
        }
        
        public void DeleteEffect(Effect effectToDelete)
        {
            this.StartCoroutine(DatabaseManager.DeleteEffect(effectToDelete, LoadData));
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

//        private void ImportWeapon()
//        {
//            string path = EditorUtility.OpenFilePanel("Choose a weapon", Application.dataPath + "/Data", "json");
//
//            if (String.IsNullOrEmpty(path))
//            {
//                return;
//            }
//            
//            fsSerializer serializer = new fsSerializer();
//            fsData data = fsJsonParser.Parse(File.ReadAllText(path));
//
//            serializer.TryDeserialize(data, ref newWeapon);
//            weaponIsImported = true;
//            eraseIfPossible = true;
//            pathWeaponImported = path;
//
//            if (newWeapon.effect != null)
//            {
//                AddEffectToList(newWeapon.effect);
//            }
//        }

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
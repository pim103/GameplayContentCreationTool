using System;
using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using UnityEngine;
using UnityEngine.Networking;
using Weapons;

namespace Database
{
    public static class DatabaseManager
    {
        public static List<Effect> effectsLoad;
        public static List<Weapon> weaponsLoad;
        
        public static IEnumerator ListEffect(Action successEndCallback = null)
        {
            var www = UnityWebRequest.Get("http://fyc/services/effect/list.php");

            yield return www.SendWebRequest();
            yield return new WaitForSeconds(0.5f);

            if (www.responseCode == 200)
            {
                fsSerializer serializer = new fsSerializer();
                fsData data;

                try
                {
                    EffectListJson effectListJson = null;
                    data = fsJsonParser.Parse(www.downloadHandler.text);
                    serializer.TryDeserialize(data, ref effectListJson);

                    if (effectListJson != null)
                    {
                        effectsLoad = effectListJson.effects;
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("error " + e.Message);
                }

                successEndCallback?.Invoke();
            }
            
            Debug.Log("Response code : " + www.responseCode);
        }

        public static IEnumerator ListWeapon(Action successEndCallback = null)
        {
            var www = UnityWebRequest.Get("http://fyc/services/weapon/list.php");

            yield return www.SendWebRequest();
            yield return new WaitForSeconds(0.5f);

            if (www.responseCode == 200)
            {
                fsSerializer serializer = new fsSerializer();
                fsData data;

                try
                {
                    WeaponListJson weaponListJson = null;
                    data = fsJsonParser.Parse(www.downloadHandler.text);
                    serializer.TryDeserialize(data, ref weaponListJson);

                    if (weaponListJson != null)
                    {
                        weaponsLoad = weaponListJson.GetWeapons();
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("error " + e.Message);
                }

                successEndCallback?.Invoke();
            }
            
            Debug.Log("Response code : " + www.responseCode);
        }

        public static IEnumerator SaveEffect(Effect newEffect, Action successEndCallback = null)
        {
            WWWForm form = new WWWForm();
            form.AddField("effectName", newEffect.effectName);
            form.AddField("intervalTime", newEffect.interval.ToString());
            form.AddField("lifeTime", newEffect.lifeTime.ToString());
            form.AddField("amount", newEffect.amount.ToString());
            form.AddField("specialEffect", newEffect.SpecialEffect.ToString());

            UnityWebRequest www = UnityWebRequest.Post("http://fyc/services/effect/add.php", form);

            yield return SendAddRequest(www, successEndCallback);
        }
        
        public static IEnumerator UpdateEffect(Effect effectUpdated, Action successEndCallback = null)
        {
            WWWForm form = new WWWForm();
            form.AddField("id", effectUpdated.effectId);
            form.AddField("effectName", effectUpdated.effectName);
            form.AddField("intervalTime", effectUpdated.interval.ToString());
            form.AddField("lifeTime", effectUpdated.lifeTime.ToString());
            form.AddField("amount", effectUpdated.amount.ToString());
            form.AddField("specialEffect", (int) effectUpdated.SpecialEffect);

            UnityWebRequest www = UnityWebRequest.Post("http://fyc/services/effect/update.php", form);

            yield return SendAddRequest(www, successEndCallback);
        }
        
        public static IEnumerator DeleteEffect(Effect effectToDelete, Action successEndCallback = null)
        {
            WWWForm form = new WWWForm();
            form.AddField("id", effectToDelete.effectId);

            UnityWebRequest www = UnityWebRequest.Post("http://fyc/services/effect/delete.php", form);

            yield return SendAddRequest(www, successEndCallback);
        }
        
        public static IEnumerator SaveWeapon(Weapon newWeapon, Action successEndCallback = null)
        {
            WWWForm form = new WWWForm();
            form.AddField("weaponName", newWeapon.weaponName);
            form.AddField("damages", newWeapon.damages);
            form.AddField("rateOfFire", newWeapon.rateOfFire.ToString().Replace(",", "."));
            form.AddField("projSpeed", newWeapon.projSpeed);
            form.AddField("projLifeTime", newWeapon.projLifeTime);
            form.AddField("modelId", newWeapon.modelId);
            form.AddField("effectId", newWeapon.effect != null ? newWeapon.effect.effectId.ToString() : "");

            UnityWebRequest www = UnityWebRequest.Post("http://fyc/services/weapon/add.php", form);

            yield return SendAddRequest(www, successEndCallback);
        }

        public static IEnumerator UpdateWeapon(Weapon weaponUpdated, Action successEndCallback = null)
        {
            WWWForm form = new WWWForm();
            form.AddField("id", weaponUpdated.weaponId);
            form.AddField("weaponName", weaponUpdated.weaponName);
            form.AddField("damages", weaponUpdated.damages);
            form.AddField("rateOfFire", weaponUpdated.rateOfFire.ToString().Replace(",", "."));
            form.AddField("projSpeed", weaponUpdated.projSpeed);
            form.AddField("projLifeTime", weaponUpdated.projLifeTime);
            form.AddField("modelId", weaponUpdated.modelId);
            form.AddField("effectId", weaponUpdated.effect != null ? weaponUpdated.effect.effectId.ToString() : "");

            UnityWebRequest www = UnityWebRequest.Post("http://fyc/services/weapon/update.php", form);

            yield return SendAddRequest(www, successEndCallback);
        }

        public static IEnumerator DeleteWeapon(Weapon weaponToDelete, Action successEndCallback = null)
        {
            WWWForm form = new WWWForm();
            form.AddField("id", weaponToDelete.weaponId);

            UnityWebRequest www = UnityWebRequest.Post("http://fyc/services/weapon/delete.php", form);

            yield return SendAddRequest(www, successEndCallback);
        }

        public static IEnumerator SendAddRequest(UnityWebRequest www, Action successEndCallback)
        {
            yield return www.SendWebRequest();
            yield return new WaitForSeconds(0.5f);

            if (www.responseCode == 201)
            {
                Debug.Log("Request was send");
                Debug.Log(www.downloadHandler.text);

                successEndCallback?.Invoke();
            }
            else
            {
                Debug.Log("error");
                Debug.Log(www.responseCode);
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}
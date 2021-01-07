using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Weapons;

namespace Editor
{
    public class EffectEditor
    {
        public Effect newEffect;
        
        public void DisplayEffectPanel(ContentEditor editorParent)
        {
            if (newEffect == null)
            {
                Debug.Log("Create new effect");
                newEffect = new Effect();
            }

            newEffect.effectName = EditorGUILayout.TextField("Name", newEffect.effectName);
            newEffect.amount = EditorGUILayout.FloatField("Amount", newEffect.amount);
            newEffect.interval = EditorGUILayout.FloatField("Interval", newEffect.interval);
            newEffect.lifeTime = EditorGUILayout.FloatField("Duration", newEffect.lifeTime);
            newEffect.SpecialEffect = (SpecialEffect) EditorGUILayout.EnumPopup("Special effect", newEffect.SpecialEffect);

            if (GUILayout.Button("Save Effect"))
            {
                if (!editorParent.AddEffectToList(newEffect))
                {
                    return;
                }

                newEffect = null;
                editorParent.editorType = EditorType.Weapon;
            }
            
            if (GUILayout.Button("Edit weapon"))
            {
                editorParent.editorType = EditorType.Weapon;
            }
        }
    }
}
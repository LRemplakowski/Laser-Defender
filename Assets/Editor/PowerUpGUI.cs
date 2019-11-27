using UnityEngine;
using UnityEditor;
using System;


[CustomEditor(typeof(PowerUp))]
public class PowerUpGUI : Editor 
{
	bool fireModeToggle, healthBonusToggle, shiedldToggle;
	override public void OnInspectorGUI()
    { 
		var powerUp = target as PowerUp;
        powerUp.powerUpType = (PowerUpType)EditorGUILayout.EnumPopup(powerUp.powerUpType);

        powerUp.chanceToDrop = EditorGUILayout.IntSlider("Chance to drop", powerUp.chanceToDrop, 0, 100);

        if (powerUp.powerUpType == PowerUpType.laserMode) { fireModeToggle = true; } else { fireModeToggle = false; }
        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(fireModeToggle)))
         {
            if (group.visible == true)
             {
                EditorGUI.indentLevel++;
                EditorGUILayout.PrefixLabel("Weapon Type (GameObject): ");
                powerUp.weapon = EditorGUILayout.ObjectField(powerUp.weapon, typeof(GameObject), false) as GameObject;
                if (powerUp.weapon)
                {
                    using (new EditorGUI.DisabledScope(true))
                    {
                        Projectile projectile;
                        projectile = powerUp.weapon.GetComponent<Projectile>();
                        projectile.damage = EditorGUILayout.FloatField("Damage:", projectile.damage);
                        projectile.projectileSpeed = EditorGUILayout.FloatField("Projectile Speed:", projectile.projectileSpeed);
                        projectile.fireRate = EditorGUILayout.FloatField("Fire Rate:", projectile.fireRate);
                    }
                }
                EditorGUI.indentLevel--;
             }
         }

        if (powerUp.powerUpType == PowerUpType.healthUp) { healthBonusToggle = true; } else { healthBonusToggle = false; }
        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(healthBonusToggle)))
        {
            if (group.visible == true)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PrefixLabel("Health Bonus: ");
                powerUp.healthBonus = EditorGUILayout.FloatField(powerUp.healthBonus);
                EditorGUI.indentLevel--;
            }
        }

        if (powerUp.powerUpType == PowerUpType.shield) { shiedldToggle = true; } else { shiedldToggle = false; }
        using (var group = new EditorGUILayout.FadeGroupScope(Convert.ToSingle(shiedldToggle)))
        {
            if (group.visible == true)
            {
                EditorGUI.indentLevel++;
                powerUp.shield = EditorGUILayout.ObjectField(obj: powerUp.shield, objType: typeof(GameObject), allowSceneObjects: false) as GameObject;
                EditorGUILayout.PrefixLabel("Shield Druation (seconds): ");
                powerUp.shieldDuration = EditorGUILayout.FloatField(powerUp.shieldDuration);
                EditorGUILayout.PrefixLabel("Shield Health: ");
                powerUp.shieldHealth = EditorGUILayout.FloatField(powerUp.shieldHealth);
                EditorGUI.indentLevel--;
            }
        }
        EditorUtility.SetDirty(powerUp);
	}
}

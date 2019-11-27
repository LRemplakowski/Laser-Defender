using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(PowerUpSpawner))]

public class PowerUpSpawnerGUI : Editor
{
    private SerializedObject powerUpSpawner;
    private SerializedProperty healthUpsArray, shieldUpsArray, laserModesArray;
    private SerializedProperty powerUpFallSpeed, healthChance, shieldChance, laserChance, powerUpChance;

    void OnEnable()
    {
        powerUpSpawner = new SerializedObject(target);
        
        healthChance = powerUpSpawner.FindProperty("healthChance");
        shieldChance = powerUpSpawner.FindProperty("shieldChance");
        laserChance = powerUpSpawner.FindProperty("laserChance");
        powerUpChance = powerUpSpawner.FindProperty("powerUpChance");
        powerUpFallSpeed = powerUpSpawner.FindProperty("powerUpFallSpeed");
        healthUpsArray = powerUpSpawner.FindProperty("healthUps");
        shieldUpsArray = powerUpSpawner.FindProperty("shieldUps");
        laserModesArray = powerUpSpawner.FindProperty("laserModes");
    }

    public override void OnInspectorGUI()
    {
        powerUpChance.intValue = EditorGUILayout.IntSlider("Chance to drop Power Up", powerUpChance.intValue, 0, 100);
        EditorGUILayout.PropertyField(powerUpFallSpeed, new GUIContent("Power Up Fall Speed"), true);

        EditorGUILayout.PropertyField(healthUpsArray, new GUIContent("Health Ups"), true);
        EditorGUILayout.PropertyField(shieldUpsArray, new GUIContent("Shield Ups"), true);
        EditorGUILayout.PropertyField(laserModesArray, new GUIContent("Fire Modes"), true);


        healthChance.intValue = EditorGUILayout.IntSlider("Health Chance", healthChance.intValue, 0, 100);
        shieldChance.intValue = EditorGUILayout.IntSlider("Shield Chance", shieldChance.intValue, 0, 100);
        laserChance.intValue = EditorGUILayout.IntSlider("Laser Mode Chance", laserChance.intValue, 0, 100);
        powerUpSpawner.ApplyModifiedProperties();
        powerUpSpawner.UpdateIfRequiredOrScript();
    }
}

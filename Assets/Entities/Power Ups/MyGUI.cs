using UnityEngine;
using UnityEditor;
using System;


[CustomEditor(typeof(PowerUp))]
public class MyGUI : Editor 
{
	bool fireModeToggle;
	override public void OnInspectorGUI()
	{
		var powerUp = target as PowerUp;
		if (powerUp.powerUpType == PowerUpType.laserMode) {fireModeToggle = true;} else {fireModeToggle = false;}

//		fireModeToggle = GUILayout.Toggle(fireModeToggle, "Disable Fields");

//		using (var group = new EditorGUILayout.FadeGroupScope (Convert.ToSingle(fireModeToggle)))
//		{
//			if (group.visible == false)
//			{
//				EditorGUI.indentLevel++;
//				EditorGUILayout.PrefixLabel("Fire Mode");
//				powerUp.fireMode = (FireMode) EditorGUILayout.EnumPopup (powerUp.fireMode);
//			}
//		}

		using (new EditorGUI.DisabledScope(fireModeToggle))
		{
			powerUp.fireMode = (FireMode) EditorGUILayout.EnumPopup (powerUp.fireMode);
		}
	}
}

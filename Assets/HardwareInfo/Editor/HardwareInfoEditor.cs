using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Softdrink{
	[CustomEditor(typeof(HardwareInfo))]
	public class HardwareInfoEditor : Editor {

		public override void OnInspectorGUI(){
			HardwareInfo hwInfo = target as HardwareInfo;
			// Button for calculating score
			if(GUILayout.Button("Calculate Score")){
				hwInfo.CalculateHardwareScore();
			}
			// Buttons for setting and clearing reference config
			if(GUILayout.Button("Set Reference Configuration")){
				hwInfo.SetReference();
			}
			if(GUILayout.Button("Clear Reference Configuration")){
				hwInfo.ClearReference();
			}

			// Display warnings if they exist
			string warnings = hwInfo.compatibilityWarnings;
			if(warnings.Length > 0){
				string[] splitWarnings = warnings.Split('\n');
				for(int i = 0; i < splitWarnings.Length-1; i++){
					EditorGUILayout.Space();
					EditorGUILayout.HelpBox(splitWarnings[i], MessageType.Warning);
					EditorGUILayout.Space();
				}
			}

			DrawDefaultInspector();

		} 
	}
}

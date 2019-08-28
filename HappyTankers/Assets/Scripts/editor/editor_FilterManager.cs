using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class editor_FilterManager : EditorWindow
{
	[MenuItem("Window/My Window")]
	static void Init()
	{
		// Get existing open window or if none, make a new one:
		editor_FilterManager window = (editor_FilterManager)EditorWindow.GetWindow(typeof(editor_FilterManager));
		window.Show();
	}

	void OnGUI()
	{
		GUILayout.Label("Base Settings", EditorStyles.boldLabel);
		if(GUILayout.Button("DROP THE FILTER"))
		{
			FilterManager.IsHappy = false;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupWindowScript : MonoBehaviour {

	public bool popupWindow_enabled = false;
	public Rect windowRect = new Rect(20, 20, 120, 50);

	void OnGUI()
	{
		//doWindow0 = GUI.Toggle(new Rect(10, 10, 100, 20), doWindow0, "Window 0");
		//if (doWindow0)
		if(popupWindow_enabled)
			windowRect = GUI.Window(0, new Rect(110, 10, 200, 60), DoMyWindow, "Basic Window");
	}

	void DoMyWindow(int windowID)
	{
		// do stuff
	}

	public void togglePopupWindow()
	{
		popupWindow_enabled = !popupWindow_enabled;
	}
}

using UnityEngine;
using System.Collections;
using System;


//globals and game specific global functions
public static class Game
{
	public static bool IsOnOSX = (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer);
	public static bool IsOnWindows = (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer);
	public static bool IsOnLinux = (Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.LinuxPlayer);

	public static void SetInvis(GameObject obj, int playerNum){
		if (obj.layer < 16) {//if it's not one of the visibility affected layers
			return;
		}
		int oldLayerValue = Convert.ToInt32(LayerMask.LayerToName (obj.layer),2);
		oldLayerValue |= (1 << (playerNum - 1));
		obj.layer = LayerMask.NameToLayer(Convert.ToString (oldLayerValue, 2).PadLeft(4,'0'));
	}
}


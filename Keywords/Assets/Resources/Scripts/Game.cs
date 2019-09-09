using UnityEngine;
using System.Collections;
using System;

//all enums go here

//keeping layer orders consistent
public enum Height {
    Floor = -9,
    OnGridSquare = -8,
    OnFloor = -1,
    Player = 1,
    Held = 2,
    Wall = 10,
}

//globals and game specific global functions
public static class Game {
    public static bool IsOnOSX = (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer);
    public static bool IsOnWindows = (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer);
    public static bool IsOnLinux = (Application.platform == RuntimePlatform.LinuxEditor || Application.platform == RuntimePlatform.LinuxPlayer);

    public static void SetInvis(GameObject obj, int playerNum) {
        if (obj.layer < 16) {//if it's not one of the visibility affected layers
            return;
        }
        int oldLayerValue = Convert.ToInt32(LayerMask.LayerToName(obj.layer), 2);
        oldLayerValue |= (1 << (playerNum - 1));
        obj.layer = LayerMask.NameToLayer(Convert.ToString(oldLayerValue, 2).PadLeft(4, '0'));
    }

    //called on an object and recursively sets its and its children's sorting order to be based on the desired Height
    public static void RepositionHeight(GameObject obj, Height height) {
        RepositionInSortingOrder(obj, (int)height);
    }
    private static void RepositionInSortingOrder(GameObject obj, int height) {
        int originalSortingOrder = obj.GetComponent<SpriteRenderer>().sortingOrder;
        obj.GetComponent<SpriteRenderer>().sortingOrder = height;
        foreach (Transform child in obj.transform) {
            int diff = child.gameObject.GetComponent<SpriteRenderer>().sortingOrder - originalSortingOrder;
            RepositionInSortingOrder(child.gameObject, height + diff);
        }
    }

    //C# mod is not too useful
    public static int correctmod(int a, int n) {
        return ((a % n) + n) % n;
    }
}


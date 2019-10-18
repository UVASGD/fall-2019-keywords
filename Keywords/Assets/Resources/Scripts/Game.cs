using UnityEngine;
using System.Collections;
using System;

//all enums go here

//keeping layer orders consistent
public enum Height {
    Background = -20,
    Floor = -9,
    OnGridSquare = -8,
    OnFloor = -1,
    Player = 1,
    Held = 2,
    Wall = 10,
    UI = 101,
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
        int originalSortingOrder = 0;
        if (obj.GetComponent<SpriteRenderer>())
        {
            originalSortingOrder = obj.GetComponent<SpriteRenderer>().sortingOrder;
            obj.GetComponent<SpriteRenderer>().sortingOrder = height;
        }
        foreach (Transform child in obj.transform) {
            int diff = 0;
            if (child.GetComponent<SpriteRenderer>())
            {
                diff = child.gameObject.GetComponent<SpriteRenderer>().sortingOrder - originalSortingOrder;
            }
            RepositionInSortingOrder(child.gameObject, height + diff);
        }
    }

    public static Bounds GetBounds(GameObject obj)
    {
        // Generate parent bounds object
        Bounds bounds;
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
            bounds = sr.bounds;
        else
            bounds = new Bounds(obj.transform.position, Vector3.zero);
        // Recursively expand bounds to accomodate children
        // If no children, won't have to worry about recursive case
        for (int i=0; i<obj.transform.childCount; i++)
        {
            GameObject childObj = obj.transform.GetChild(i).gameObject;
            Bounds childBounds = GetBounds(childObj);
            bounds.Encapsulate(childBounds);
        }
        return bounds;
    }

    //C# mod is not too useful. This one acts identically to the python one (and the math one)
    public static int correctmod(int a, int n) {
        return ((a % n) + n) % n;
    }

    public static Collider2D[] ItemsInRadius(Vector3 pos, float radius) {
        return Physics2D.OverlapCircleAll(pos, radius, 1 << LayerMask.NameToLayer("Pickup"));
    }

    public static GameObject ClosestItemInRadius(Vector3 pos, float radius) {
        Collider2D[] itemsWithinRadius = ItemsInRadius(pos, radius);
        if (itemsWithinRadius.Length == 0) {
            return null;
        }
        float minDistance = (itemsWithinRadius[0].gameObject.transform.position - pos).magnitude;
        GameObject closestObject = itemsWithinRadius[0].gameObject;
        foreach (Collider2D item in itemsWithinRadius) {
            if ((item.gameObject.transform.position - pos).magnitude < minDistance) {
                minDistance = (item.gameObject.transform.position - pos).magnitude;
                closestObject = item.gameObject;
            }
        }
        return closestObject;
    }

    public static void DisablePhysics(GameObject obj) {
        if (obj.GetComponent<BoxCollider2D>() != null) {
            obj.GetComponent<BoxCollider2D>().enabled = false;
        }
        if (obj.GetComponent<Collider2D>() != null) {
            obj.GetComponent<Collider2D>().enabled = false;
        }
        if (obj.GetComponent<Rigidbody2D>() != null) {
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.GetComponent<Rigidbody2D>().isKinematic = true;
            obj.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
        foreach (Transform child in obj.transform) {
            DisablePhysics(child.gameObject);
        }
    }

    public static void EnablePhysics(GameObject obj) {
        if (obj.GetComponent<BoxCollider2D>() != null) {
            obj.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (obj.GetComponent<Collider2D>() != null) {
            obj.GetComponent<Collider2D>().enabled = true;
        }
        if (obj.GetComponent<Rigidbody2D>() != null) {
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.GetComponent<Rigidbody2D>().isKinematic = false;
            obj.GetComponent<Rigidbody2D>().freezeRotation = false;
        }
        foreach (Transform child in obj.transform) {
            EnablePhysics(child.gameObject);
        }
    }

    //set layer recursively
    public static void SetLayer(GameObject obj, LayerMask layer) {
        obj.layer = layer;
        foreach (Transform child in obj.transform) {
            SetLayer(child.gameObject, layer);
        }
    }

    public static Color RandomDarkColor() {
        return UnityEngine.Random.ColorHSV(0f, 1f, 0.5f, 1f, 0f, 0.3f, 1f, 1f);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {
    public GameObject[] items; //references to the gameobjects in inventory
    private int inventorySize = 7; //how big is the inventory?
    public int activeSlot;//which slot is currently active?
    private GameObject UI;//the inventory
    private const float UIScaleFactor = 28;//items in UI appear much smaller for some reason, so I just blow them up

    public Color unselectedSlotColor;
    public Color selectedSlotColor;

    // Use this for initialization
    void Start() {
        items = new GameObject[inventorySize];
        UI = GetComponent<PlayerInfo>().UI.transform.Find("Inventory").gameObject;
        SwitchSlot(0);
    }

    //C# mod is not too useful
    int mod(int a, int n) {
        return ((a % n) + n) % n;
    }

    private void SwitchSlot(int n) {
        if (n < 0 || n >= inventorySize) {
            print("tried to change inventory slot to a weird number");
            return;
        }
        Transform OldSlotUI = UI.transform.Find("Slot" + activeSlot);
        OldSlotUI.gameObject.GetComponent<Image>().color = unselectedSlotColor;
        if (items[activeSlot] != null) {
            items[activeSlot].SetActive(false);
        }
        activeSlot = n;
        Transform NewSlotUI = UI.transform.Find("Slot" + activeSlot);
        NewSlotUI.gameObject.GetComponent<Image>().color = selectedSlotColor;
        if (items[activeSlot] != null) {
            items[activeSlot].SetActive(true);
        }

    }

    public void IncSlot() {
        SwitchSlot(mod(activeSlot + 1, inventorySize));
    }

    public void DecSlot() {
        SwitchSlot(mod(activeSlot - 1, inventorySize));
    }

    public void Add(GameObject obj) {
        //TODO: if inventory is not full, cycle through spots until an empty slot is found, then add the objcet to that slot
        //Currently, Add is simply not called when activeSlot has something in it
        //while (Get()) {
        //    IncSlot();
        //}
        items[activeSlot] = obj;

        GrapplingHook gh = obj.GetComponent<GrapplingHook>();
        if (gh) {
            gh.onPickup();
        }

        //create item preview in inventory UI
        Transform SlotUI = UI.transform.Find("Slot" + activeSlot);
        GameObject ItemInUI = Instantiate(obj, SlotUI.position, Quaternion.identity, SlotUI);
        Game.RepositionHeight(ItemInUI, Height.UI);
        GameObject scaleReference = Instantiate(obj, Vector3.zero, Quaternion.identity, null);
        Bounds bounds = Game.GetBounds(scaleReference);
        bool scaleToWidth = (bounds.size.x >= bounds.size.y);
        if (scaleToWidth) {
            float scaleFactor = UIScaleFactor * (scaleReference.transform.localScale.x / bounds.size.x);
            float aspectRatio = scaleReference.transform.localScale.y / scaleReference.transform.localScale.x;
            ItemInUI.transform.localScale = new Vector3(scaleFactor, scaleFactor * aspectRatio, ItemInUI.transform.localScale.z);
        } else {
            float scaleFactor = UIScaleFactor * (scaleReference.transform.localScale.y / bounds.size.y);
            float aspectRatio = scaleReference.transform.localScale.x / scaleReference.transform.localScale.y;
            ItemInUI.transform.localScale = new Vector3(scaleFactor * aspectRatio, scaleFactor, ItemInUI.transform.localScale.z);
        }
        Destroy(scaleReference);
        Game.SetLayer(ItemInUI, LayerMask.NameToLayer("P" + GetComponent<PlayerInfo>().playerNum));
        Game.DisablePhysics(ItemInUI);
    }
    public void Remove() {
        items[activeSlot] = null;

        //remove item preview in UI
        Transform SlotUI = UI.transform.Find("Slot" + activeSlot);
        Destroy(SlotUI.GetChild(0).gameObject);
    }

    public GameObject Get() {
        return items[activeSlot];
    }

    public int Size() {
        return inventorySize;
    }
}

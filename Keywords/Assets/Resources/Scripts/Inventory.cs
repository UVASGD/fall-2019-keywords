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
        activeSlot = 0;
        SwitchSlot(0);
    }

    //C# mod is not too useful
    int correctmod(int a, int n) {
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
        SwitchSlot(correctmod(activeSlot + 1, inventorySize));
    }

    public void DecSlot() {
        SwitchSlot(correctmod(activeSlot - 1, inventorySize));
    }

    public void Add(GameObject obj) {
        //TODO: if inventory is not full, cycle through spots until an empty slot is found, then add the objcet to that slot
        //Currently, Add is simply not called when activeSlot has something in it
        //while (Get()) {
        //    IncSlot();
        //}
        items[activeSlot] = obj;

        //create item preview in inventory UI
        Transform SlotUI = UI.transform.Find("Slot" + activeSlot);
        GameObject ItemInUI = Instantiate(obj, SlotUI.position, Quaternion.identity, SlotUI);
        Game.RepositionHeight(ItemInUI, Height.UI);
        bool scaleToWidth = (ItemInUI.transform.localScale.x >= ItemInUI.transform.localScale.y);
        if (scaleToWidth) {
            ItemInUI.transform.localScale = new Vector3(UIScaleFactor, UIScaleFactor * (ItemInUI.transform.localScale.y / ItemInUI.transform.localScale.x), ItemInUI.transform.localScale.z);
        } else {
            ItemInUI.transform.localScale = new Vector3(UIScaleFactor * (ItemInUI.transform.localScale.x / ItemInUI.transform.localScale.y), UIScaleFactor, ItemInUI.transform.localScale.z);
        }
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
}

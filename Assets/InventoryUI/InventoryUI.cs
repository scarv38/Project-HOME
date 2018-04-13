using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemParent;
    Inventory inventory;

    InventorySlot[] slotsUI;
	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;
        slotsUI = itemParent.GetComponentsInChildren <InventorySlot>();
	}

    void UpdateUI()
    {
		//slotsUI.Length = 4
        for (int i = 0; i < slotsUI.Length; i++)
        {
            if(i < inventory.items.Count && inventory.items.Count < 5)
			{
				slotsUI[i].AddItem(inventory.items[i]);
			}
        }
    }
}

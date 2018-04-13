using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Invetory instance duplicated!!");
            return;
        }   
        instance = this;
    }

    #endregion

    public delegate void ItemChanged();
    public ItemChanged onItemChangedCallBack;
    public ItemChanged onItemChangeNotif;

	public List<Item> items = new List<Item>();

	//public void AddItem(int ID)
	//{
	//	items[ID].enabled = true;
	//	if (onItemChangedCallBack != null)
	//		onItemChangedCallBack.Invoke();
	//}

    
    public void Add(Item item,bool showMenu = true)
    {
        items.Add(item);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();

		if (showMenu && onItemChangeNotif != null)
			onItemChangeNotif.Invoke();
	}

    public void Remove(Item item, bool showMenu = true)
    {
        items.Remove(item);
        if (showMenu && onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
	
}

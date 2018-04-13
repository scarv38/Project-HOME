using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class NotifBar : MonoBehaviour
{

	public Transform itemParent;
	Inventory inventory;
	// Use this for initialization
	void Start()
	{
		inventory = Inventory.instance;
		inventory.onItemChangeNotif += UpdateUI;
	}

	void UpdateUI()
	{
		itemParent.gameObject.SetActive(true);
		Transform textChild = itemParent.Find("Message");
		string temp = inventory.items[inventory.items.Count - 1].name;
		textChild.GetComponent<Text>().text = "Optained " + temp;
		StartCoroutine(wait(1.5f));
	}

	IEnumerator wait(float second)
	{
		yield return new WaitForSeconds(second);
		itemParent.gameObject.SetActive(false);
	}

}

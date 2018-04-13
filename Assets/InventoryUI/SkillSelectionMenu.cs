using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectionMenu : MonoBehaviour {

	Inventory inventory;
	public Item[] items;
	DataManager data;
	// Use this for initialization
	void Start () {
		data = DataManager.instance;
        inventory = Inventory.instance;

	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.A))
		{
			data.totalSkills++;
			print("ITEM #0 ON");
			if(inventory.items.Count < 4)
				inventory.Add(items[0]);
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			data.totalSkills++;
			print("ITEM #1 ON");
			if(inventory.items.Count < 4)
				inventory.Add(items[1]);
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			data.totalSkills++;
			print("ITEM #2 ON");
			if(inventory.items.Count < 4)
				inventory.Add(items[2]);
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			print("ITEM #3 ON");
			if(inventory.items.Count < 4)
				inventory.Add(items[3]);
		}
	}
}

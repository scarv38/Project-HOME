using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

	public Sprite Opened;
	bool open;
	Inventory inventory;
	public Item item;

	DataManager check;

	void Start()
	{
		check = DataManager.instance;
		inventory = Inventory.instance;
		open = false;
		for(int i  = 0;i<inventory.items.Count;i++)
		{
			if(item == inventory.items[i])
			{
				open = true;
				this.GetComponent<SpriteRenderer>().sprite = Opened;
				break;
			}
		}
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			if(!open)
			{
				if (Input.GetButtonDown("Attack"))
				{
					GetComponent<AudioSource>().Play();
					this.GetComponent<SpriteRenderer>().sprite = Opened;
					open = true;
					inventory.Add(item);
					check.totalSkills += 1;
				}
			}
		
		}
	}
}

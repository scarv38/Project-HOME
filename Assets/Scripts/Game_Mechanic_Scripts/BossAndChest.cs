using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAndChest : MonoBehaviour {

	public int id;
	public GameObject boss;
	public GameObject chest;
	public GameObject bossTrigger;
	public GameObject[] Doors;
	public GameObject[] Doors_Horizontal;
	public GameObject doorOpened;
	public GameObject doorOpenedHorizontal;
	bool switched;

	DataManager check;

	void Start()
	{
		check = DataManager.instance;
		switched = false;
		if(check.puzzleList[id])
			BossKilled();

	}

	void FixedUpdate () {
		if(boss == null && !switched)
		{
			switched = true;
			BossKilled();
			check.puzzleList[id] = true;
		}
	}

	void BossKilled()
	{
		if(chest != null)
		{
			chest.GetComponent<SpriteRenderer>().enabled = true;
			chest.GetComponent<BoxCollider2D>().enabled = true;
			chest.GetComponent<Chest>().enabled = true;
		}
		
		for (int i = 0; i < Doors.Length; i++)
		{
			Vector2 newPos = Doors[i].transform.position;
			DestroyImmediate(Doors[i].gameObject);
			if(doorOpened != null)
				Instantiate(doorOpened, newPos, Quaternion.identity);
			Doors[i] = null;
		}
		for (int i = 0; i < Doors_Horizontal.Length; i++)
		{
			Vector2 newPos = Doors_Horizontal[i].transform.position;
			Destroy(Doors_Horizontal[i].gameObject);
			if(doorOpenedHorizontal != null)
				Instantiate(doorOpenedHorizontal, newPos, Quaternion.identity);
			Doors_Horizontal[i] = null;
		}

		if (bossTrigger != null)
			bossTrigger.SetActive(false);
	}
}

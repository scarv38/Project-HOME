using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_And_key : MonoBehaviour {

	public int id;
	DataManager check;
	void Start()
	{
		check = DataManager.instance;

		if (check.puzzleList[id])
		{
			if(check.Keys > 0)
				check.Keys--;

			this.gameObject.SetActive(false);
		}
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			if(check.Keys > 0 && Input.GetButtonDown("Attack"))
			{
				GetComponent<AudioSource>().Play();
				this.gameObject.SetActive(false);
				check.Keys--;
				check.puzzleList[id] = true;
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			if (check.Keys > 0 && Input.GetButtonDown("Attack"))
			{
				GetComponent<AudioSource>().Play();

				this.gameObject.SetActive(false);
				check.Keys--;
				check.puzzleList[id] = true;
			}
		}
	}
}

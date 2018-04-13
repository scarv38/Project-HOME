using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

	DataManager check;

	void Start()
	{
		check = DataManager.instance;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag == "Player")
		{
			GetComponent<AudioSource>().Play();
			check.Keys++;
			GetComponent<SpriteRenderer>().enabled = false;
			StartCoroutine(wait(GetComponent<AudioSource>().time + 0.8f));
		}
	}

	IEnumerator wait(float second)
	{
		yield return new WaitForSeconds(second);
		Destroy(this.gameObject);

	}
}

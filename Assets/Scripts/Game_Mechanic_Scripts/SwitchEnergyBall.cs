using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchEnergyBall : MonoBehaviour {

	public Sprite normal;
	public Sprite triggered;
	public bool isOn;
	// Use this for initialization
	void Start () {
		isOn = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isOn)
			GetComponent<SpriteRenderer>().sprite = triggered;
		else
			GetComponent<SpriteRenderer>().sprite = normal;

	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "PlayerBullet")
		{
			if(!isOn)
				isOn = true;

			Destroy(collision.gameObject);

		}

	}
}

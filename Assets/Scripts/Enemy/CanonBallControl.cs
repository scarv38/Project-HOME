using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBallControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Wall" || collision.tag == "Player")
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Bound")
		{
				Destroy(this.gameObject);
		}
	}

}

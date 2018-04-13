using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour {

	float distance;
	Vector2 start;
	void Start()
	{
		start = transform.position;
	}

	void FixedUpdate()
	{
		distance = Vector2.Distance(transform.position,start);

		if (distance >= 5.5)
			Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag == "Wall" || coll.tag == "Object" || coll.tag == "Enemy" || coll.tag == "Switch")
		{
			if(this.gameObject != null)
				Destroy(this.gameObject);

		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.tag == "Bound")
		{
			Destroy(this.gameObject);
		}
	}

}

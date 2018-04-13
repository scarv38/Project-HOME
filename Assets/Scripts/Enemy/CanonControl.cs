using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonControl : MonoBehaviour {

	public GameObject ball;
	public float delay;
	float waitTime;
	Vector2 dir;
	public int direction;

	void Start()
	{
		switch(direction)
		{
			case 1:
				dir = Vector2.up;
				break;
			case 2:
				dir = Vector2.down;
				break;
			case 3:
				dir = Vector2.left;
				break;
			case 4:
				dir = Vector2.right;
				break;
			default:
				dir = Vector2.zero;
				break;
		}
	}

	void FixedUpdate ()
	{
		if(waitTime >= delay)
		{
			GetComponent<AudioSource>().Play();
			GameObject temp = (GameObject)Instantiate(ball, this.transform.position, Quaternion.identity);
			temp.transform.parent = this.transform;
			temp.GetComponent<Rigidbody2D>().AddForce(dir * 100f);

			waitTime = 0;
		}
		waitTime += Time.deltaTime;
	}
}

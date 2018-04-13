using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

	public float timer;
	Animator anim;
	void Start () {

		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		anim.SetFloat("timer", timer);

		if(timer <= 0)
		{
			GetComponent<CircleCollider2D>().enabled = true;
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Explose") )
			{
				Destroy(this.gameObject);
			}
		}

		timer -= Time.deltaTime;
	}
}

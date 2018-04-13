using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatus : EnemyStatus{

	public Transform StunWeakness;
	public bool isStuned;
	// Use this for initialization
	void Start () {
		isStuned = false;
		GetComponent<Animator>().SetBool("IsStuned", false);

	}

	new void OnTriggerEnter2D(Collider2D collision)
	{
		if(!isStuned && collision.gameObject.tag == StunWeakness.gameObject.tag)
		{
			isStuned = true;
			GetComponent<Animator>().SetBool("IsStuned", true);
			Destroy(collision.gameObject);
			StartCoroutine(waitToMove(2.5f));
		}
		else
		if(isStuned && collision.tag == "Player_Sword")
		{
			HP--;
			AudioClip swordsound = Resources.Load("Audio/boss_hit") as AudioClip;
			GetComponent<AudioSource>().PlayOneShot(swordsound);
			StartCoroutine(Flash());
		}
	}

	IEnumerator waitToMove(float time)
	{
		yield return new WaitForSeconds(time);
		isStuned = false;
		GetComponent<Animator>().SetBool("IsStuned", false);

	}
}

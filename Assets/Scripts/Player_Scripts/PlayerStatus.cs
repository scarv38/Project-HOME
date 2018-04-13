using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

	//Save Data
	public Game_Data saveTemp;
	////HP & MP////
	[Range(0, 10)]
	public float HP;
	[Range(0, 100)]
	public float MP;

	//Status
	protected bool immune;

	//SceneManager
	protected DataManager sceneManager;

	protected Rigidbody2D rigi;
	protected bool canAttack;
	protected BoxCollider2D hitBox;
	protected Animator anim;

	void Awake ()
	{
		rigi = GetComponent<Rigidbody2D>();
		hitBox = GetComponent<BoxCollider2D>();
		anim = GetComponent<Animator>();
		sceneManager = DataManager.instance;
	}

	protected void ManaRegeneration()
	{
		if (MP < 100)
			MP += Time.deltaTime * 1.5f;
		else
			MP = 100;
	}
		

	//COLLISION
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (!immune) 
		{
			if (coll.gameObject.tag == "Enemy")
			{
				Vector2 dir = transform.position - coll.transform.position;
				dir.Normalize();
				rigi.AddForce(dir * 2000);
				AudioClip swordsound = Resources.Load("Audio/player_hit") as AudioClip;
				GetComponent<AudioSource>().PlayOneShot(swordsound);
				Damaged(1, 0);
			}
		}


		if (coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Object")
			rigi.velocity = Vector2.zero;

		if (coll.gameObject.tag == "Block")
			canAttack = false;
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Block")
			canAttack = false;

		if (!immune) 
		{
			if (coll.gameObject.tag == "Enemy")
			{
				Vector2 dir = transform.position - coll.transform.position;
				dir.Normalize();
				rigi.AddForce(dir * 2000);
				AudioClip swordsound = Resources.Load("Audio/player_hit") as AudioClip;
				GetComponent<AudioSource>().PlayOneShot(swordsound);
				Damaged(1, 0);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (!immune)
		{
			if (coll.gameObject.tag == "Bomb" || coll.tag == "EnemyBullet" || coll.tag == "CanonBall")
			{
				Destroy(coll.gameObject);
				Vector2 dir = transform.position - coll.transform.position;
				dir.Normalize();
				rigi.AddForce(dir * 2000);
				AudioClip swordsound = Resources.Load("Audio/player_hit") as AudioClip;
				GetComponent<AudioSource>().PlayOneShot(swordsound);
				Damaged(1, 0);
			}


		}

	}

	void Damaged(float hp, float mp)
	{
		immune = true;
		MP = (MP >= mp) ? (MP - mp) : 0;
		HP = (HP >= hp) ? (HP - hp) : 0;
		StartCoroutine(Flash());
	}

	IEnumerator Flash()
	{
		for (int i = 0; i < 5; i++)
		{
			GetComponent<SpriteRenderer>().material.color = Color.clear;
			yield return new WaitForSeconds(0.05f);
			GetComponent<SpriteRenderer>().material.color = Color.white;
			yield return new WaitForSeconds(0.05f);
		}
		immune = false;
	}
}

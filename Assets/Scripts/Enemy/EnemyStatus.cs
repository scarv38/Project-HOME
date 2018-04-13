using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour {

	//Add this script to every enemy object
	public int HP;
	public bool canRespawn;
	public string RoomName; //leave it blank if canRespawn = false
	Vector2 startPos;
	int startHP;
	public Transform[] weaknesses; 

	public bool isDead;

	DataManager check;
	MonoBehaviour[] comps;
	SpriteRenderer sprite;
	Animator animator;
	Collider2D coll;
	Rigidbody2D rigi;

	void Start()
	{
		comps = GetComponents<MonoBehaviour>();
		check = DataManager.instance;

		sprite = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		coll = GetComponent<Collider2D>();
		rigi = GetComponent<Rigidbody2D>();

		startPos = this.transform.position;
		startHP = HP;
	}
	

	void FixedUpdate () {

		if(canRespawn && check.currentRoom != RoomName)
		{
			Activate();
			isDead = false;
			HP = startHP;
			this.transform.position = startPos;
		}

	}

	protected IEnumerator Flash()
	{
		for (int i = 0; i < 5; i++)
		{
			GetComponent<SpriteRenderer>().material.color = Color.clear;
			yield return new WaitForSeconds(0.05f);
			GetComponent<SpriteRenderer>().material.color = Color.white;
			yield return new WaitForSeconds(0.05f);
		}
	}

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if(weaknesses.Length == 0)
		{
			if (collision.tag == "Player_Sword" || collision.tag == "Bomb" || collision.tag == "PlayerBullet")
			{
				HP--;
				AudioClip swordsound = Resources.Load("Audio/enemy_hit") as AudioClip;
				GetComponent<AudioSource>().PlayOneShot(swordsound);
				StartCoroutine(Flash());
			}
		}
		else
		{
			foreach (Transform t in weaknesses)
			{
				if(collision.tag == t.gameObject.tag)
				{
					HP--;
					AudioClip swordsound = Resources.Load("Audio/enemy_hit") as AudioClip;
					GetComponent<AudioSource>().PlayOneShot(swordsound);
					StartCoroutine(Flash());
					break;
				}
			}

		}

	}

	public void Deactivate()
	{
		foreach(MonoBehaviour c in comps)
		{
			if (c != GetComponent<EnemyStatus>())
				c.enabled = false;
		}

		sprite.enabled = false;
		animator.enabled = false;
		coll.enabled = false;
		rigi.isKinematic = true;
		isDead = true;
	}

	void Activate()
	{
		foreach (MonoBehaviour c in comps)
		{
				c.enabled = true;
		}

		sprite.enabled = true;
		animator.enabled = true;
		coll.enabled = true;
		rigi.isKinematic = false;
		animator.SetBool("Dead", false);
		isDead = false;
	}
}

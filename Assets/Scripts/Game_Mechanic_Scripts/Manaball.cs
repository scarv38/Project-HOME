using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manaball : MonoBehaviour {

	bool triggered = false;
	public LayerMask playerMask;

	void FixedUpdate () {
		bool isNearCharacter = Physics2D.OverlapCircle(transform.position, 2, playerMask);
		Debug.Log(isNearCharacter);
		if (isNearCharacter)
			triggered = true;

		if(triggered)
		{
			GameObject player;
			player = GameObject.Find("Player");

			if(player != null)
			{
				Vector3 dir = player.transform.position - transform.position;
				dir.Normalize();
				Vector3.Cross(dir, transform.up);
				transform.position = Vector3.MoveTowards(transform.position,
									transform.position + dir, 6f * Time.deltaTime);
			}
			
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			GameObject player;
			player = GameObject.Find("Player");

			AudioClip audio = Resources.Load("Audio/get_mana") as AudioClip;
			player.GetComponent<AudioSource>().PlayOneShot(audio);
			if (player != null)
			{
				if(player.GetComponent<PlayerControl>().MP <= 95)
					player.GetComponent<PlayerControl>().MP += 5f;
				else
					player.GetComponent<PlayerControl>().MP = 100f;

			}
			Destroy(gameObject);

		}
	}
}

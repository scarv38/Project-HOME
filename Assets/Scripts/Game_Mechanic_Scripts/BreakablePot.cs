using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePot : MonoBehaviour {

	public GameObject broken;
	public GameObject mana;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player_Sword" || collision.tag == "PlayerBullet" || collision.tag == "Bomb")
		{
			GetComponent<AudioSource>().Play();
			GameObject fragment = Instantiate(broken, this.transform.position, Quaternion.identity);
			int num = Random.Range(1, 4);
			for(int i = 0;i<num;i++)
				Instantiate(mana, this.transform.position + new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-0.5f, 0.5f),0), Quaternion.identity);

			this.GetComponent<Collider2D>().enabled = false;
			this.GetComponent<SpriteRenderer>().enabled = false;


			StartCoroutine(waitToDestroy(1f, fragment));
		}
	}

	IEnumerator waitToDestroy(float time,GameObject a)
	{
		yield return new WaitForSeconds(time);
		Destroy(a);
		Destroy(gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_BossAppear : MonoBehaviour {

	public Transform Boss;

	IEnumerator Appear(float second)
	{
		yield return new WaitForSeconds(second);
		Boss.gameObject.SetActive(true);
		Destroy(gameObject);

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			StartCoroutine(Appear(1f));
		}
	}


}

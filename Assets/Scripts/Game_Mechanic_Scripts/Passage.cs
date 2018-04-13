using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{
	public Transform Exit;
	public bool isBelow;
	Vector2 newPos;
	GameObject Fade;
	CanvasGroup canvas;
	Collider2D player;
	public int nextRoomX;
	public int nextRoomY;

	RoomMove move;
	DataManager check;
	void Start()
	{
		check = DataManager.instance;

		Fade = GameObject.Find("Fade");
		canvas = Fade.GetComponent<CanvasGroup>();
		move = RoomMove.instance;

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			GetComponent<AudioSource>().Play();
			player = collision;
			//player.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			player.gameObject.GetComponent<Rigidbody2D>().mass = 999999f;

			PlayerControl.g_playerCanMove = false;

			newPos = Exit.position;
			if (isBelow)
				newPos.y += 1f;
			else
				newPos.y -= 1f;

			StartCoroutine(FadeScreen());
			check.currentRoom = "Bounds[" + nextRoomX + "]" + "[" + nextRoomY + "]";

		}
	}

	IEnumerator FadeScreen()
	{
		while (canvas.alpha < 1)
		{
			Debug.Log("Fade");
			canvas.alpha = 1;
			//canvas.alpha += Time.deltaTime;
		}

		move.MoveToRoom(nextRoomX, nextRoomY);
		player.transform.position = newPos;

		yield return new WaitForSeconds(0.5f);

		while (canvas.alpha > 0)
		{
			player.gameObject.GetComponent<Animator>().Play("Indle_Tree");
			Debug.Log("FadeOut");
			canvas.alpha -= Time.deltaTime;
			yield return null;
		}

		player.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
		PlayerControl.g_playerCanMove = true;
		player.gameObject.GetComponent<Rigidbody2D>().mass = 1f;


	}

}

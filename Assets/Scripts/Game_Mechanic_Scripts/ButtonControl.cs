using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControl : MonoBehaviour {

	public int id;

	public Sprite pushed;
	public int roomToMoveX;
	public int roomToMoveY;
	public int currentRoomX ;
	public int currentRoomY;
	public Transform[] Doors;
	public Transform[] Doors_Horizontal;

	public Transform[] Objects;
	public Transform[] Keys;

	public GameObject doorOpened;
	public GameObject doorOpenedHorizontal;
	RoomMove move;

	GameObject gameManager;
	DataManager check;

	void Start ()
	{
		check = DataManager.instance;
		
		move = RoomMove.instance;

		if(check.puzzleList[id])
		{
			this.GetComponent<SpriteRenderer>().sprite = pushed;
			DoorOpen();
			EraseObject();
			KeyDrop();
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player" && !check.puzzleList[id])
		{

			AudioClip swordsound = Resources.Load("Audio/switch_sound") as AudioClip;
			GetComponent<AudioSource>().PlayOneShot(swordsound);

			this.GetComponent<SpriteRenderer>().sprite = pushed;
			check.puzzleList[id] = true;

			if (roomToMoveX < 0 || roomToMoveY < 0)
			{
				DoorOpen();
				EraseObject();
				KeyDrop();
			}
			else
			{
				collision.gameObject.GetComponent<Rigidbody2D>().mass = 9999f;
				PlayerControl.g_waitForCamera = true;
				StartCoroutine(Moving(2f));
				collision.gameObject.GetComponent<Rigidbody2D>().mass = 1;
			}

		}
	}

	IEnumerator Moving(float second)
	{
		yield return new WaitForSeconds(0.25f);

		move.MoveToRoom(roomToMoveX, roomToMoveY);

		yield return new WaitForSeconds(1.5f);

		DoorOpen();
		EraseObject();
		KeyDrop();


		yield return new WaitForSeconds(second);


		move.MoveToRoom(currentRoomX, currentRoomY);



		yield return new WaitForSeconds(1f);

		PlayerControl.g_playerCanMove = true;
		PlayerControl.g_waitForCamera = false;

	}

	void DoorOpen()
	{
		for (int i = 0; i < Doors.Length; i++)
		{
			if (Doors[i] != null)
			{
				Vector2 newPos = Doors[i].transform.position;

				Destroy(Doors[i].gameObject);
				Instantiate(doorOpened, newPos, Quaternion.identity);
				Doors[i] = null;
			}
		}

		for (int i = 0; i < Doors_Horizontal.Length; i++)
		{
			if (Doors_Horizontal[i] != null)
			{
				Vector2 newPos = Doors_Horizontal[i].transform.position;
				Destroy(Doors_Horizontal[i].gameObject);
				Instantiate(doorOpenedHorizontal, newPos, Quaternion.identity);
				Doors_Horizontal[i] = null;
			}
		}
	}

	void EraseObject()
	{
		for (int i = 0; i < Objects.Length; i++)
		{
			if (Objects[i] != null)
			{
				Destroy(Objects[i].gameObject);
				Objects[i] = null;
			}
		}
		
	}

	void KeyDrop()
	{
		for (int i = 0; i < Keys.Length; i++)
		{
			if (Keys[i] != null)
			{
				Keys[i].gameObject.SetActive(true);
				Keys[i] = null;
			}
		}
		
	}
}

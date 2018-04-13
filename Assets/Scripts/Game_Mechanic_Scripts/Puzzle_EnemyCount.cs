using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_EnemyCount : MonoBehaviour {

	//NOTE: ENEMIES MUST BE ABLE TO RESPAWN SO USE THIS SCRIPT

	public int id;
	int counter;
	public Transform[] enemies;
	int[] statusList;
	DataManager check;
	public string roomName;

	public Transform[] Doors;
	public Transform[] Objects;
	public GameObject doorOpened;

	void Start()
	{
		check = DataManager.instance;
		enemies = GetComponentsInChildren<Transform>();
		counter = enemies.Length-1;

		statusList = new int[enemies.Length];
		
		if(check.puzzleList[id])
		{
			for (int i = 0; i < Doors.Length; i++)
				DoorOpen(i);

			for (int i = 0; i < Objects.Length; i++)
				EraseObject(i);

		}
	}
	
	void FixedUpdate ()
	{
		if(!check.puzzleList[id])
		{
			if (roomName != check.currentRoom)
			{
				counter = enemies.Length - 1;
				for (int i = 0; i < statusList.Length; i++)
					statusList[i] = 0;
			}

			if (counter > 0)
			{
				for (int i = 1; i < enemies.Length; i++)
				{
					if (enemies[i].GetComponent<EnemyStatus>().isDead == true && statusList[i] == 0)
					{
						statusList[i] = 1;
						counter--;
					}
				}
			}
			else
			{
				check.puzzleList[id] = true;
				for (int i = 0; i < Doors.Length; i++)
					DoorOpen(i);

				for (int i = 0; i < Objects.Length; i++)
					EraseObject(i);
			}
		}

	}

	void DoorOpen(int i)
	{
		if (Doors[i] != null)
		{
			Vector2 newPos = Doors[i].transform.position;

			Destroy(Doors[i].gameObject);
			Instantiate(doorOpened, newPos, Quaternion.identity);
			Doors[i] = null;
		}
	}

	void EraseObject(int i)
	{
		if (Objects[i] != null)
		{
			Destroy(Objects[i].gameObject);
			Objects[i] = null;
		}
	}
}

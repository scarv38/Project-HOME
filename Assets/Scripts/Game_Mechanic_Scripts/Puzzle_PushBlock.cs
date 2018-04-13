using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_PushBlock : MonoBehaviour {

	public int id;
	public string RoomName;
	public Transform block;
	public Transform[] subBlock;
	Vector3[] pos;

	DataManager check;

	void Start () {
		check = DataManager.instance;

		if (check.puzzleList[id] == true && block!=null)
		{
			block.transform.position = this.transform.position;
		}

		pos = new Vector3[subBlock.Length + 1];

		pos[0] = block.transform.position;
		for (int i = 1; i < pos.Length; i++)
			pos[i] = subBlock[i - 1].transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Check when player leaves the room without solving the puzzle
		if(check.currentRoom != RoomName)
		{
			if(check.puzzleList[id] == false)
				block.transform.position = pos[0];

			for (int i = 1; i < pos.Length; i++)
			{
				subBlock[i - 1].GetComponent<Rigidbody2D>().isKinematic = true;
				subBlock[i - 1].transform.position = pos[i];
				subBlock[i - 1].GetComponent<Rigidbody2D>().isKinematic = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.transform == block)
		{
			check.puzzleList[id] = true;
		}
	}
}

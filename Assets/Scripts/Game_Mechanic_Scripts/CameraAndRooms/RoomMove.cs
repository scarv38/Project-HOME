using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{

	#region Singleton
	public static RoomMove instance;

	void Awake()
	{
		if (instance != null)
		{
			Debug.Log("Invetory instance duplicated!!");
			return;
		}
		instance = this;
	}

	#endregion

	CameraBound g_cam;
	string desName;
	Transform g_roomParent;
	Transform RoomBound;
	BoxCollider2D boundCollider;

	// Update is called once per frame
	void Update () {
		
	}

	public void MoveToRoom(int x, int y)
	{
		g_cam = Camera.main.gameObject.GetComponent<CameraBound>();
		g_roomParent = GameObject.Find("Room Bounds").transform;

		desName = "Bounds[" + x + "][" + y + "]";
		Debug.Log(desName);
		RoomBound = g_roomParent.Find(desName);
		boundCollider = RoomBound.GetComponent<BoxCollider2D>();
		g_cam.SetNewBounds(boundCollider.bounds, false,true);
	}
}

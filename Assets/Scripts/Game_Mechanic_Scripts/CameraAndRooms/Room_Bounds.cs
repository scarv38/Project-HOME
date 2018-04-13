using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Bounds : MonoBehaviour {
    
    BoxCollider2D boxColl;
    CameraBound cam;
	DataManager check;

	void Start()
	{
		check = DataManager.instance;
	}
		// Use this for initialization
		void Awake () {
        boxColl = GetComponent<BoxCollider2D>();
        cam = Camera.main.gameObject.GetComponent<CameraBound>();
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
		if (coll.gameObject.tag == "Player")
        {
			if(coll.gameObject.GetComponent<Rigidbody2D>().mass > 1f)
				cam.SetNewBounds(boxColl.bounds,false);
			else
				cam.SetNewBounds(boxColl.bounds,true);
			check.currentRoom = this.gameObject.name;

		}
	}

	//void OnTriggerStay2D(Collider2D coll)
	//{
	//	if (coll.gameObject.tag == "Player" && coll.gameObject.GetComponent<Rigidbody2D>().isKinematic == false)

	//}
}

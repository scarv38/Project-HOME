using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour {

    //bool switched = false;    //checked by objects using the block as a switch
    bool initialMov = false;  //control variable, set to true after first input

	bool touch;

   // float startX, startY;
   // float blockLength = 1f; //.16 on both axes. Distance to move one block-length
 
    Rigidbody2D block;
 
    void Start()
    {
		touch = false;
        block  = GetComponent<Rigidbody2D>();
       // startX = transform.position.x;
      //  startY = transform.position.y;
    }
 
    void Update()
    {
		block.velocity = Vector2.zero;
		if (Input.GetButton("Attack"))
		{
			touch = true;
			//block.isKinematic = true;
		}

		if (Input.GetKeyUp(KeyCode.Z))
		{
			touch = false;
		}
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		Rigidbody2D body = coll.gameObject.GetComponent<Rigidbody2D>();

		if (coll.gameObject.tag == "Player")
		{
			block.velocity = Vector2.zero;
			if (body.velocity == Vector2.zero)
			{
				if (touch)
				{
					block.velocity = Vector2.zero;

					//switched = false;
					block.isKinematic = false;

				}
				else
				{
					//switched = true;
					block.isKinematic = true;
				}

			}

		}
	}

	void OnCollisionEnter2D(Collision2D coll)
    {
		Rigidbody2D body = coll.gameObject.GetComponent<Rigidbody2D>();
		if (coll.gameObject.tag == "Player")
		{
			block.velocity = Vector2.zero;

			if (!initialMov)
			{
				if (body.velocity.x < 0) { block.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; }//leftward
				else if (body.velocity.x > 0) { block.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation; }//rightward
				else if (body.velocity.y > 0) { block.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; }//upward
				else if (body.velocity.y < 0) { block.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; }//downward
			}

			if (body.velocity == Vector2.zero)
			{
				if (touch)
				{
					block.velocity = Vector2.zero;
					//switched = false;
					block.isKinematic = false;
					initialMov = true;


				}
				else
				{
					//switched = true;
					block.isKinematic = true;
					initialMov = false;

				}
			}

		}

		
	}
}

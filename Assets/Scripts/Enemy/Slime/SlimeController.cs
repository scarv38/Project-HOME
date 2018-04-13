using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeController : MonoBehaviour {

	EnemyStatus status;

	const int _Down = 1;
	const int _Up = 2;
	const int _Right = 3;
	const int _Left = 4;

	public float moveSpeed;
	private Vector2 moveDirection;
	public int movementStatus;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		status = GetComponent<EnemyStatus>();
		anim.SetInteger ("MoveStatus", movementStatus);
	}
	
	void FixedUpdate() {
		if (status.HP > 0)
		{
			switch (movementStatus)
			{
				case _Down:
					{
						transform.position = Vector3.MoveTowards(transform.position,
							transform.position - new Vector3(0, 1, 0), moveSpeed * Time.deltaTime);

						break;
					}
				case _Up:
					{
						transform.position = Vector3.MoveTowards(transform.position,
							transform.position + new Vector3(0, 1, 0), moveSpeed * Time.deltaTime);
						break;
					}
				case _Right:
					{
						transform.position = Vector3.MoveTowards(transform.position,
							transform.position + new Vector3(1, 0, 0), moveSpeed * Time.deltaTime);
						break;
					}
				case _Left:
					{
						transform.position = Vector3.MoveTowards(transform.position,
							transform.position - new Vector3(1, 0, 0), moveSpeed * Time.deltaTime);
						break;
					}
			}
		}
		else
		if(!anim.GetBool("Dead"))
		{
			AudioClip swordsound = Resources.Load("Audio/enemy_kill") as AudioClip;
			GetComponent<AudioSource>().PlayOneShot(swordsound);
			anim.SetBool("Dead", true);
		}

		if (status.HP <= 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Dying"))
		{
			if (status.canRespawn)
				status.Deactivate();
			else
				Destroy(this.gameObject);
		}

	}


	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Wall") {
			if (movementStatus == _Up) {
				movementStatus = _Down;
			} else if (movementStatus == _Down) {
				movementStatus = _Up;
			} else if (movementStatus == _Right) {
				movementStatus = _Left;
			} else if (movementStatus == _Left) {
				movementStatus = _Right;
			}
		}
		anim.SetInteger ("MoveStatus", movementStatus);
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Wall")
		{
			if (movementStatus == _Up)
			{
				movementStatus = _Down;
			}
			else if (movementStatus == _Down)
			{
				movementStatus = _Up;
			}

			if (movementStatus == _Right)
			{
				movementStatus = _Left;
			}
			else if (movementStatus == _Left)
			{
				movementStatus = _Right;
			}
		}
		anim.SetInteger("MoveStatus", movementStatus);
	}


}

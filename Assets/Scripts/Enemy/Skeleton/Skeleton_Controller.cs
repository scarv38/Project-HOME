using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Controller : MonoBehaviour {

	EnemyStatus status;

	private bool isNearCharacter;
	public Transform charTrans;
	public float moveSpeed;
	public LayerMask playerMask;

	float startX;
	float startY;
	float LastX;
	float LastY;
	float MoveX;
	float	MoveY;
	bool isMoving;
	public int movementStatus;

	Animator anim;
	BoxCollider2D box;

	// Use this for initialization
	void Start () {
		status = GetComponent<EnemyStatus>();
		startX = transform.position.x;
		startY = transform.position.y;
		anim = GetComponent<Animator> ();
		anim.SetInteger ("moveStatus", movementStatus);
		isMoving = false;
		LastY  = MoveY = -1;
		LastX = MoveX = 0;

	}
	

	void FixedUpdate()
	{
		if(status.HP > 0)
			Move();
		else
		if (!anim.GetBool("Dead"))
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

	void Move()
	{
		isNearCharacter = Physics2D.OverlapCircle(transform.position, 5, playerMask);
		anim.SetBool("isMoving", isMoving);

		if (isNearCharacter)
		{
			isMoving = true;
			if (charTrans.transform.position.y > transform.position.y)
			{
				transform.position = Vector3.MoveTowards(transform.position,
					transform.position + new Vector3(0, 1, 0), moveSpeed * Time.deltaTime);
			}
			if (charTrans.transform.position.y < transform.position.y)
			{
				transform.position = Vector3.MoveTowards(transform.position,
					transform.position - new Vector3(0, 1, 0), moveSpeed * Time.deltaTime);
			}
			if (charTrans.transform.position.x > transform.position.x)
			{
				transform.position = Vector3.MoveTowards(transform.position,
					transform.position + new Vector3(1, 0, 0), moveSpeed * Time.deltaTime);

			}
			if (charTrans.transform.position.x < transform.position.x)
			{
				transform.position = Vector3.MoveTowards(transform.position,
					transform.position - new Vector3(1, 0, 0), moveSpeed * Time.deltaTime);
			}
			MoveY = LastY = charTrans.transform.position.y - transform.position.y;
			MoveX = LastX = charTrans.transform.position.x - transform.position.x;
		}
		else
		{
			if (startY > transform.position.y)
			{
				transform.position = Vector3.MoveTowards(transform.position,
					transform.position + new Vector3(0, 1, 0), moveSpeed * Time.deltaTime);
			}
			if (startY < transform.position.y)
			{
				transform.position = Vector3.MoveTowards(transform.position,
					transform.position - new Vector3(0, 1, 0), moveSpeed * Time.deltaTime);

			}
			if (startX > transform.position.x)
			{
				transform.position = Vector3.MoveTowards(transform.position,
					transform.position + new Vector3(1, 0, 0), moveSpeed * Time.deltaTime);
			}
			if (startX < transform.position.x)
			{
				transform.position = Vector3.MoveTowards(transform.position,
					transform.position - new Vector3(1, 0, 0), moveSpeed * Time.deltaTime);
			}
			if (Mathf.Abs(transform.position.x - startX) < 1f && Mathf.Abs(transform.position.y - startY) < 1f)
			{
				transform.position = Vector3.MoveTowards(transform.position,
					new Vector3(startX, startY, 0), moveSpeed * Time.deltaTime);
				isMoving = false;
			}
			MoveY = LastY = startY - transform.position.y;
			MoveX = LastX = startX - transform.position.x;
		}
		anim.SetFloat("LastX", LastX);
		anim.SetFloat("LastY", LastY);
		anim.SetFloat("MoveX", MoveX);
		anim.SetFloat("MoveY", MoveY);
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01Control : MonoBehaviour {

	public GameObject ball;
	Vector2[] directions;
	float waitTime;
	float timeCount;
	public int radius;
	Vector3 startPos;
	EnemyStatus status;

	void Start ()
	{
		waitTime = 0;
		timeCount = 0;
		directions = new Vector2[3];
		directions[0] = new Vector2(-1, -1);
		directions[1] = new Vector2(0, -1);
		directions[2] = new Vector2(1, -1);
		startPos = transform.position;
		status = GetComponent<EnemyStatus>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(status.HP > 0)
		{
			if (GetComponent<BossStatus>().isStuned)
				waitTime = 0.2f;
			else
				Move();

			if (waitTime >= 0.7f)
			{
				for (int i = 0; i < 3; i++)
				{
					GameObject bullet = (GameObject)Instantiate(ball, this.transform.position, Quaternion.identity);
					bullet.GetComponent<Rigidbody2D>().AddForce((directions[i] + new Vector2(Random.Range(-0.5f, 0.5f), 0)) * 40f);
				}
				GameObject left = (GameObject)Instantiate(ball, this.transform.position, Quaternion.identity);
				left.GetComponent<Rigidbody2D>().AddForce((Vector2.left + new Vector2(0, Random.Range(-0.2f, 0.2f))) * 80f);

				GameObject right = (GameObject)Instantiate(ball, this.transform.position, Quaternion.identity);
				right.GetComponent<Rigidbody2D>().AddForce((Vector2.right + new Vector2(0, Random.Range(-0.2f, 0.2f))) * 80f);

				waitTime = 0;
			}

			waitTime += Time.deltaTime;
		}
		else
		if (!GetComponent<Animator>().GetBool("Dead"))
		{
			AudioClip swordsound = Resources.Load("Audio/enemy_kill") as AudioClip;
			GetComponent<AudioSource>().PlayOneShot(swordsound);
			GetComponent<Animator>().SetBool("Dead", true);
		}

		if (status.HP <= 0 && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dying"))
		{
			if (status.canRespawn)
				status.Deactivate();
			else
				Destroy(this.gameObject);
		}
	}

	void Move()
	{
		transform.position = startPos +  new Vector3(Mathf.Sin(timeCount*1.15f) * (float)radius, 0, 0);
		timeCount += Time.deltaTime;
	}
}

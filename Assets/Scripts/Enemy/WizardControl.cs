using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardControl : MonoBehaviour {

	public int HorizonalPanel;
	public int VerticalPanel;
	public Transform player;
	public GameObject Bullet;
	public GameObject BulletBig;
	public float bulletSpeed;

	/* Current Panel
	 0	1
	 2	3
	 */
	int curPanel;
	float waitTime;
	bool isShooting;
	Vector2[] pos = new Vector2[4];
	Vector2 dir;
	Animator anim;

	EnemyStatus status;

	private void Awake()
	{
		gameObject.SetActive(false);
		status = GetComponent<EnemyStatus>();
	}

	void Start () {
		
		waitTime = 0;
		curPanel = 2;
		isShooting = false;

		anim = GetComponent<Animator>();

		//Calculate Posistion
		pos[2] = transform.position;
		pos[0] = new Vector2(pos[2].x, pos[2].y + VerticalPanel);
		pos[1] = new Vector2(pos[2].x + HorizonalPanel, pos[2].y + VerticalPanel);
		pos[3] = new Vector2(pos[2].x + HorizonalPanel, pos[2].y);
	}

	void FixedUpdate()
	{
		if (status.HP > 0)
		{
			if (curPanel >= 2)
				anim.SetFloat("isUp", 1f);
			else
				anim.SetFloat("isUp", 0f);


			if (waitTime >= 1 && !isShooting)
			{
				isShooting = true;
				anim.SetFloat("isShooting", 1f);
				StartCoroutine(Shoot(6));
			}

			waitTime += Time.deltaTime;
		}
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

	IEnumerator Shoot(int n)
	{
		for(int i = 0;i<n;i++)
		{
			GameObject temp = (GameObject)Instantiate(Bullet, this.transform.position, Quaternion.identity);
			dir = player.transform.position - this.transform.position;
			dir.Normalize();
			temp.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed);
			yield return new WaitForSeconds(0.75f);
		}

		GameObject temp2 = (GameObject)Instantiate(BulletBig, this.transform.position, Quaternion.identity);
		temp2.transform.parent = this.transform;
		dir = player.transform.position - this.transform.position;
		dir.Normalize();
		temp2.GetComponent<Rigidbody2D>().AddForce(dir * bulletSpeed);

		anim.SetFloat("isShooting", 0f);

		yield return new WaitForSeconds(2f);
		Move();
	}

	void Move()
	{
		int newPos = curPanel;

		do
		{
			newPos = Random.Range(0, 4);
		} while (newPos == curPanel);
		curPanel = newPos;

		transform.position = pos[newPos];
		isShooting = false;

		waitTime = 0;
	}

	

}

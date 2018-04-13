using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : PlayerStatus {

		////LastPos////
	Vector2 lastPos;

	//Move
    public float speed;
   
    bool isMoving;
  
    public static bool g_playerCanMove;
    public static bool g_waitForCamera;
    //

    //Attack_Sword
    Transform sword;
    CircleCollider2D swordColl;
    bool attacking;
	
	//Healing
	float UseMP = 0;
	
	//Attack Shoot
	bool isUsingSkill = false;
	public GameObject prefab_ball;

	//Attack Bomb
	public GameObject prefab_bomb;

	
    void Start ()
	{
		immune = false;
		canAttack = true;
		lastPos.y = -1;
		anim.SetFloat("LastY", lastPos.y);
		g_playerCanMove = true;
		g_waitForCamera = false;
		
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Player_Sword"))
            {
                sword = child;
                break;
            }
        }
		
		swordColl = sword.GetComponent<CircleCollider2D>();
    }


	void FixedUpdate () 
    {
		if (!SelectItem.isOpen)
        {
			if(!g_waitForCamera)
			{
				if (g_playerCanMove)
					Move();
				else
					Debug.Log ("KHONGTHEDICHUYENDCNHACHO");
				
				if (canAttack)
					AttackSword();

				switch (sceneManager.skillEquipped)
				{
					case 0:
						Healing();
						break;
					case 1:
						AttackShoot();
						break;
					case 2:
						AttackBomb();
						break;
					case 3:
						break;
					default:
						break;
				}
			}
			
		}

		//Check if player touch the box
		Vector2 raySource = transform.position;
		if (lastPos.y != 0)
			raySource.y += (hitBox.size.y/2)*lastPos.y;

		RaycastHit2D hit = Physics2D.Raycast(raySource, lastPos,0.5f, LayerMask.GetMask("Block"));

		Debug.DrawLine(raySource, hit.point,Color.yellow);
		if (hit.collider != null)
			canAttack = false;
		else
			canAttack = true;

		//Regenerate MP
		ManaRegeneration();

	}

	void Move()
    {
        isMoving = false;
		float movex, movey;

		//Check Input
		movex = Input.GetAxisRaw("Horizontal");
        movey = Input.GetAxisRaw("Vertical");

        if (movex != 0 || movey != 0)
        {
            isMoving = true;

            if(movey != 0) //Up and Down
                swordColl.offset = new Vector2 (0f,movey/1.25f);
            else
                if(movex != 0) //Left and Right
                    swordColl.offset = new Vector2 (movex/2,movey/2);

            if (!hitBox.IsTouchingLayers(LayerMask.GetMask("BlockingLayer")))
			{
				lastPos.x = movex;
				lastPos.y = movey;
			}

		}

		rigi.velocity = Vector2.zero;
		transform.position = Vector3.MoveTowards(transform.position,
			transform.position + new Vector3(movex, movey, 0), speed * Time.deltaTime);

		//Set Animation
		anim.SetBool("isMoving", isMoving);
		anim.SetFloat("MoveX", movex);
		anim.SetFloat("MoveY", movey);
		anim.SetFloat("LastX", lastPos.x);
		anim.SetFloat("LastY", lastPos.y);
	}

    void AttackSword()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        attacking = stateInfo.IsName("SwordAttack_Tree");
		

		if (Input.GetButtonDown("Attack") && !attacking)
        {
			AudioClip swordsound = Resources.Load("Audio/sword_sound") as AudioClip;
			GetComponent<AudioSource>().PlayOneShot(swordsound);
			anim.SetTrigger("isAttacking");
        }

        if (attacking)
        {
            float playbackTime = stateInfo.normalizedTime;
           // print(playbackTime);
            if(playbackTime > 0.33f && playbackTime < 0.66f)
                swordColl.enabled = true;
            else
                swordColl.enabled = false;

        }
    }

	void Healing()
	{
		if (MP > 10 && HP < 10) 
		{
			if(Input.GetButton("Skill"))
			{
				rigi.mass = 99999f;
				g_playerCanMove = false;
				canAttack = false;
				MP -= 0.2f;
				UseMP += 0.2f;
				if (UseMP >= 10f) 
				{
					AudioClip swordsound = Resources.Load("Audio/heal_sound") as AudioClip;
					GetComponent<AudioSource>().PlayOneShot(swordsound);
					HP += 1;
					UseMP = 0;
				}
			}
			else
			{
				rigi.mass = 1f;
				//if(!g_waitForCamera)
					g_playerCanMove = true;
				canAttack = true;

				UseMP = 0;
			}
		}

		if (Input.GetButtonUp ("Skill"))
		{
			rigi.mass = 1f;
			//if(!g_waitForCamera)
				g_playerCanMove = true;
			canAttack = true;
			UseMP = 0;	
		}

	}
	
	void AttackBomb()
	{
		if(MP >= 5)
		{
			AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
			isUsingSkill = stateInfo.IsName("SpellAttack_Tree");

			if (Input.GetButtonDown("Skill") && !isUsingSkill)
			{
				anim.SetTrigger("isShooting");
				Instantiate(prefab_bomb, this.transform.position, Quaternion.identity);

				if (MP >= 15)
					MP -= 15;
			}
		}
		
	}

	void AttackShoot()
	{
		if (MP >= 5)
		{
			AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
			isUsingSkill = stateInfo.IsName("SpellAttack_Tree");

			if (Input.GetButtonDown("Skill") && !isUsingSkill)
			{
				AudioClip swordsound = Resources.Load("Audio/fire_sound") as AudioClip;
				GetComponent<AudioSource>().PlayOneShot(swordsound);
				anim.SetTrigger("isShooting");
				GameObject bullet = (GameObject)Instantiate(prefab_ball, this.transform.position, Quaternion.identity);

				if (lastPos.x != 0 && lastPos.y != 0)
					bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, lastPos.y) * 200);
				else
					bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(lastPos.x, lastPos.y) * 200);

				if (MP >= 5)
					MP -= 5;
			}
		}

	}
}

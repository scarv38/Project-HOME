using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoom01 : MonoBehaviour {

	public Transform Boss;
	public GameObject[] switches;
	public Transform[] canons;
	public GameObject ball;
	bool[] ready;

	void Start()
	{

		ready = new bool[switches.Length];

		for (int i = 0; i < ready.Length; i++)
			ready[i] = true;


	}
	
	void FixedUpdate () {
		int i = 0;

		for (i = 0; i < switches.Length; i++)
		{
			if (switches[i].GetComponent<SwitchEnergyBall>().isOn)
			{
				if(ready[i])
				{
					AudioClip swordsound = Resources.Load("Audio/cannon") as AudioClip;
					GetComponent<AudioSource>().PlayOneShot(swordsound);
					GameObject temp = (GameObject)Instantiate(ball, canons[i].transform.position, Quaternion.identity);
					temp.transform.parent = canons[i].transform;
					temp.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000f);

					ready[i] = false;
					StartCoroutine(wait(4f, i));

				}

				break;
			}
			else
				ready[i] = true;

		}


	}

	IEnumerator wait(float time,int switchpos)
	{
		yield return new WaitForSeconds(time);
		switches[switchpos].gameObject.GetComponent<SwitchEnergyBall>().isOn = false;

	}
}

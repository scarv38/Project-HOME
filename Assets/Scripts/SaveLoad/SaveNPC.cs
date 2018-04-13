using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveNPC : MonoBehaviour {

	public Game_Data saveTemp;
	DataManager sceneManager;

	void Start ()
	{
		sceneManager = DataManager.instance;

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player_Sword")
		{
			GetComponent<AudioSource>().Play();
			sceneManager.LoadDataToSave(saveTemp);
			SaveLoad.Save(saveTemp);
			GetComponent<Animator>().SetTrigger("Hit");
		}
	}



}

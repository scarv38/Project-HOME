using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNPC : MonoBehaviour {

	public Game_Data saveTemp;
	DataManager sceneManager;
	PlayerStatus playerStatus;

	void Awake()
	{
		sceneManager = DataManager.instance;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player_Sword")
		{
			SaveLoad.Load(saveTemp);
			sceneManager.GetDataFromSave(saveTemp);
		}
	}

}

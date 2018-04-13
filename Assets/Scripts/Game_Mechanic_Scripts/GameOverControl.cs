using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverControl : MonoBehaviour {

	public Transform[] comps;
	public Game_Data saveTemp;
	int curPos;

	void Start()
	{
		comps = GetComponentsInChildren<Transform>();
		curPos = 1;
	}

	void Update()
	{
		/*
		3	1
			2
			*/

		int cursor = comps.Length - 1;

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (curPos < comps.Length - 2)
			{
				curPos++;
				Vector3 newPos = new Vector3(comps[cursor].localPosition.x, comps[curPos].localPosition.y, comps[cursor].localPosition.z);
				comps[cursor].localPosition = newPos;
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (curPos > 1)
			{
				curPos--;
				Vector3 newPos = new Vector3(comps[cursor].localPosition.x, comps[curPos].localPosition.y, comps[cursor].localPosition.z);
				comps[cursor].localPosition = newPos;
			}
		}

		if (Input.GetButtonDown("Attack") || Input.GetKeyDown(KeyCode.Return))
		{
			switch (curPos)
			{
				case 1: //Load Game
					if (SaveLoad.Load(saveTemp))
					{
						DataManager data = DataManager.instance;
						data.GetDataFromSave(saveTemp);

						switch (saveTemp.g_scene)
						{
							case 1:
								SceneManager.LoadScene("Dungeon01");
								break;
							default:
								break;
						}
					}
					break;
				case 2: //To Title
					SceneManager.LoadScene("TitleScreen");
					break;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenControl : MonoBehaviour {

	public Transform[] comps;
	public Game_Data saveTemp;
	int curPos;
	bool inHelpMenu;
	public GameObject helpMenu;

	void Awake()
	{
		inHelpMenu = false;	
	}

	void Start () {
		comps = GetComponentsInChildren<Transform>();
		curPos = 1;
	}
	
	
	void Update ()
	{
		DataManager data = DataManager.instance;
		/*
		6	1
			2
			3
			4
			5
			*/
		if (inHelpMenu)
		{
			if (helpMenu.gameObject.activeSelf && Input.anyKeyDown)
			{
				helpMenu.gameObject.SetActive(false);
				inHelpMenu = false;
			}

			return;
		}
			

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if(curPos < comps.Length-2)
			{
				curPos++;
				Vector3 newPos = new Vector3(comps[6].localPosition.x, comps[curPos].localPosition.y, comps[6].localPosition.z);
				comps[6].localPosition = newPos;
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (curPos > 1)
			{
				curPos--;
				Vector3 newPos = new Vector3(comps[6].localPosition.x, comps[curPos].localPosition.y, comps[6].localPosition.z);
				comps[6].localPosition = newPos;
			}
		}

		if(Input.GetButtonDown("Attack") || Input.GetKeyDown(KeyCode.Return))
		{
			switch(curPos)
			{
				case 1: //New Game
					saveTemp.g_HP = 10;
					saveTemp.g_MP = 100;
					saveTemp.g_totalSkills = 1;
					saveTemp.g_equipSkill = 0;

					saveTemp.g_posX = 41.58f;
					saveTemp.g_posY = -113.36f;

					saveTemp.g_keys = 0;

					for (int i = 0; i < 4; i++)
						saveTemp.g_clear[i] = false;

					saveTemp.g_totalPuzzle = 10;
					saveTemp.g_puzzleState = new bool[saveTemp.g_totalPuzzle];

					for (int i = 0; i < saveTemp.g_puzzleState.Length; i++)
						saveTemp.g_puzzleState[i] = false;

					
					data.GetDataFromSave(saveTemp);
					SaveLoad.Save(saveTemp);
					SceneManager.LoadScene("Dungeon01");
					break;
				case 2: //Load Game
					if(SaveLoad.Load(saveTemp))
					{
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
				case 3: //Audio On/Off
					if (AudioListener.pause) //If is off -> on
					{
						AudioListener.pause = false;
						comps[3].GetComponent<Text>().text = "Audio Off";
					}
					else
					{
						AudioListener.pause = true;
						comps[3].GetComponent<Text>().text = "Audio On";
					}
					break;
				case 4:
					helpMenu.SetActive(true);
					inHelpMenu = true;
					break;
				case 5:
					Application.Quit();
					break;

			}
		}
	}
}

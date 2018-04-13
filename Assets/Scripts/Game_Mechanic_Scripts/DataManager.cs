using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour {

	public Game_Data saveTemp;
	public int sceneID;
	public bool isCleared;
	public bool[] puzzleList;
	public int totalSkills;
	public int skillEquipped;
	public Item[] skills;
	public Transform gui_Skill;
	public string currentRoom;
	public int Keys;
	Inventory inventory;
	GameObject player;


	public static DataManager instance;

	void Awake()
	{
		if (instance != null)
			Debug.Log("Invetory instance duplicated!!");
		else
			instance = this;
		Debug.Log("CREATE NEW INSTANCE");

		player = GameObject.Find("Player");
		

		if (saveTemp.g_scene == sceneID)
			GetDataFromSave(saveTemp);

		if (isCleared)
		{
			for (int i = 0; i < puzzleList.Length; i++)
				puzzleList[i] = true;
		}

	}


	void Start()
	{
		inventory = Inventory.instance;
		if (inventory)
		{
			while (totalSkills > inventory.items.Count)
			{
				int i = inventory.items.Count;
				inventory.Add(skills[i], false);
			}
		}

		if (gui_Skill != null)
		{
			Image img = gui_Skill.GetComponent<Image>();
			Debug.Log(inventory);
			img.sprite = skills[skillEquipped].icon;
		}

	}

	void Update()
	{
		if (player != null)
		{
			if (player.GetComponent<PlayerControl>().HP <= 0)
			{
				SceneManager.LoadScene("GameOver");

				//SaveLoad.Load(saveTemp);
				//GetDataFromSave(saveTemp);
				//switch (saveTemp.g_scene)
				//{
				//	case 1:
				//		SceneManager.LoadScene("Dungeon01");
				//		break;
				//	default:
				//		break;
				//}
			}
		}

	}

	public void LoadDataToSave(Game_Data saveTemp)
	{
		saveTemp.g_scene = sceneID;
		saveTemp.g_keys = Keys;
		saveTemp.g_clear[sceneID] = isCleared;
		saveTemp.g_totalPuzzle = puzzleList.Length;
		saveTemp.g_puzzleState = new bool[puzzleList.Length];

		for (int i = 0; i < saveTemp.g_puzzleState.Length; i++)
			saveTemp.g_puzzleState[i] = puzzleList[i];
		
		//Player Related Data
		if(player != null)
		{
			saveTemp.g_totalSkills = totalSkills;
			saveTemp.g_equipSkill = skillEquipped;
			saveTemp.g_posX = player.transform.position.x;
			saveTemp.g_posY = player.transform.position.y;
			saveTemp.g_HP = player.GetComponent<PlayerControl>().HP;
			saveTemp.g_MP = player.GetComponent<PlayerControl>().MP;

		}
	}

	public void GetDataFromSave(Game_Data saveTemp)
	{
		sceneID = saveTemp.g_scene;
		isCleared = saveTemp.g_clear[sceneID];
		puzzleList = new bool[saveTemp.g_totalPuzzle];
		Keys = saveTemp.g_keys;

		for (int i = 0; i < puzzleList.Length; i++)
			puzzleList[i] = saveTemp.g_puzzleState[i];

		//Player Related Data
		if (player != null)
		{
			totalSkills = saveTemp.g_totalSkills;
			skillEquipped = saveTemp.g_equipSkill;

			player.transform.position = new Vector2(saveTemp.g_posX, saveTemp.g_posY);
			player.GetComponent<PlayerControl>().HP = saveTemp.g_HP;
			player.GetComponent<PlayerControl>().MP = saveTemp.g_MP;
		}

	}

}

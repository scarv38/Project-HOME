using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public static class SaveLoad{

	public static void Save(Game_Data data)
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = new FileStream (Application.persistentDataPath + "/data.sav", FileMode.Create);
		DataSave dt = new DataSave (data);

		bf.Serialize (stream,dt);
		stream.Close ();
	}

	public static bool Load(Game_Data data)
	{
		if (File.Exists (Application.persistentDataPath + "/data.sav")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + "/data.sav", FileMode.Open);

			DataSave dt = bf.Deserialize (stream) as DataSave;
			dt.LoadVar (data);
			stream.Close ();
			return true;
		}
		return false;
	}

}


[Serializable]
public  class DataSave{

	int g_scene; // Màn chơi hiện tại
	float g_HP; //HP
	float g_MP; //MP
	int g_totalSkills; // Skill từ 0->4. Vì trang bị mình lấy theo thứ tự cố định nên ví dụ g_totalItems = 2 nghĩa là có 2 Skills đầu tiên
	int g_equipSkill; // ID skill đang trang bị. 0->3

	//Nếu save Vector2 đc thì đổi thành 2 cái Vector2 g_pos và g_last
	float g_posX; //tọa độ X
	float g_posY; //tọa độ Y

	int g_keys; //Số lượng chìa khóa đang có trong mỗi màn

	bool[] g_clear = new bool[3]; //Những màn đã clear, trạng thái của puzzle chính

	//Puzzle:
	int g_totalPuzzle;
	bool[] g_puzzleState; //Trạng thái của n puzzle trong màn chơi hiện tại. 
						  //Đối với puzzle chính, chỉ áp dụng khi g_clear[i] = false. 

	public DataSave(Game_Data data)
	{
		g_scene = data.g_scene;
		g_HP = data.g_HP;
		g_MP = data.g_MP;
		g_totalSkills = data.g_totalSkills;
		g_equipSkill = data.g_equipSkill;

		g_posX = data.g_posX;
		g_posY = data.g_posY;

		g_keys = data.g_keys;

		g_clear[0] = data.g_clear[0];
		g_clear[1] = data.g_clear[1];
		g_clear[2] = data.g_clear[2];


		g_totalPuzzle = data.g_totalPuzzle;
		g_puzzleState = new bool[g_totalPuzzle];

		for (int i=0;i< g_totalPuzzle; i++)
			g_puzzleState[i] = data.g_puzzleState[i];

	}

	public void LoadVar(Game_Data data)
	{
		data.g_scene = g_scene;
		data.g_HP	= g_HP;
		data.g_MP = g_MP;
		data.g_totalSkills = g_totalSkills;
		data.g_equipSkill = g_equipSkill;

		data.g_posX = g_posX;
		data.g_posY = g_posY;

		data.g_keys = g_keys;
		
		data.g_clear [0] = g_clear [0];
		data.g_clear [1] = g_clear [1];
		data.g_clear [2] = g_clear [2];


		data.g_totalPuzzle = g_totalPuzzle;
		data.g_puzzleState = new bool[data.g_totalPuzzle];

		for (int i = 0; i < data.g_puzzleState.Length ; i++)
			data.g_puzzleState[i] = g_puzzleState[i];

	}

	



}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Save Data", menuName = "Data/SaveData")]
public class Game_Data : ScriptableObject{
	
	public int g_scene; // Màn chơi hiện tại
	public float g_HP; //HP
	public float g_MP; //MP
	public int g_totalSkills; // Skill từ 0->4. Vì trang bị mình lấy theo thứ tự cố định nên ví dụ g_totalItems = 2 nghĩa là có 2 Skills đầu tiên
	public int g_equipSkill; // ID skill đang trang bị. 0->3


	//Player

	//Nếu save Vector2 đc thì đổi thành 2 cái Vector2 g_pos và g_last
	public float g_posX; //tọa độ X
	public float g_posY; //tọa độ Y

	public int g_keys; //Số lượng chìa khóa đang có trong mỗi màn

	public bool[] g_clear = new bool[3]; //Những màn đã clear, trạng thái của puzzle chính

	//Vector2 g_subpuzzle = Vector2(ID màn chơi 0->2,trạng thái 0 hoặc 1); //Trạng thái của puzzle phụ


	//-Puzzle:
	public int g_totalPuzzle;
	public bool[] g_puzzleState; //Trạng thái của n puzzle trong màn chơi hiện tại. 
	//Đối với puzzle chính, chỉ áp dụng khi g_clear[i] = false. 
	//Đối với puzzle phụ, chỉ áp dụng khi g_subpuzzle[i][0]


}



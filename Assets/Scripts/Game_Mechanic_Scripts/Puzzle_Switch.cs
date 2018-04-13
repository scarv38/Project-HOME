using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Puzzle_Switch : MonoBehaviour {

	public int id;
	public string RoomName;
	public Transform Door;
	public Transform[] Switches;
	public State[] CorrectState;
	State[] startState;
	int completed;

	DataManager check;
	void Start()
	{
		check = DataManager.instance;

		completed = 0;

		if(check.puzzleList[id])
		{
			Door.gameObject.SetActive(false);
			this.GetComponent<Puzzle_Switch>().enabled = false;

		}
		startState = new State[Switches.Length];
		for (int i = 0; i < Switches.Length; i++)
			startState[i] = Switches[i].GetComponent<ColoredSwitches>().CurrentState;

	}

	void FixedUpdate()
	{
		if(!check.puzzleList[id])
		{
			if(check.currentRoom != RoomName)
				Reset();

			for (int i = 0; i < Switches.Length; i++)
			{
				if (CorrectState[i] == Switches[i].GetComponent<ColoredSwitches>().CurrentState)
					completed++;
				else
					completed--;
			}

			if (completed >= 3)
			{
				check.puzzleList[id] = true;
				Door.gameObject.SetActive(false);
				this.GetComponent<Puzzle_Switch>().enabled = false;

				AudioClip swordsound = Resources.Load("Audio/door_sound") as AudioClip;
				GetComponent<AudioSource>().PlayOneShot(swordsound);
			}
			else
				completed = 0;
		}

		
	}

	void Reset()
	{
		for (int i = 0; i < Switches.Length; i++)
			Switches[i].GetComponent<ColoredSwitches>().CurrentState = startState[i];

		completed = 0;

	}

}

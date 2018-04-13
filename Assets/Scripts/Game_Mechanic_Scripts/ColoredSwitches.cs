using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum State { Red, Blue, Green };

public class ColoredSwitches : MonoBehaviour {


	public	State CurrentState;

	public Sprite CL_Red;
	public Sprite CL_Blue;
	public Sprite CL_Green;

	SpriteRenderer spriteRender;
	private void OnGUI()
	{
		CurrentState = (State) GUILayout.SelectionGrid((int)CurrentState, State.GetNames(typeof(State)), 3, GUIStyle.none);
		
	}

	void Start () {
		spriteRender = GetComponent<SpriteRenderer>();
	}
	
	void FixedUpdate () {
		switch (CurrentState)
		{
			case State.Red:
				spriteRender.sprite = CL_Red;
				break;
			case State.Blue:
				spriteRender.sprite = CL_Blue;
				break;
			case State.Green:
				spriteRender.sprite = CL_Green;
				break;
			default:
				break;

		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag == "PlayerBullet")
		{
			GetComponent<AudioSource>().Play();
			switch (CurrentState)
			{
				case State.Red:
					CurrentState = State.Blue;
					break;
				case State.Blue:
					CurrentState = State.Green;
					break;
				case State.Green:
					CurrentState = State.Red;
					break;
				default:
					break;

			}
			Destroy(coll.gameObject);
		}
	}
}

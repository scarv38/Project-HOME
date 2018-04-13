using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectItem : MonoBehaviour
{
	Inventory inventory;

	public Transform[] itemList;
	public Sprite small;
	public Sprite big;
	public Vector2 smallSize;
	public Vector2 bigSize;

	public Sprite volumnOn;
	public Sprite volumnOff;

	RectTransform rect;

	public Transform menuParent;

	public Transform gui_Skill;

	Vector2[] posArray = new Vector2[6];
	int curPos;

	//Menu
	public static bool isOpen = false;

	
	DataManager sceneManager;

	void Awake()
	{
		
	}

	// Use this for initialization
	void Start()
	{
		sceneManager = DataManager.instance;
		inventory = Inventory.instance;
		rect = GetComponent<RectTransform>();
		menuParent.GetComponent<Canvas>().enabled = false;
		int j = 0;
		for (int i = 0; i < itemList.Length; i++)
		{
			posArray[j].x = itemList[i].localPosition.x;
			posArray[j].y = itemList[i].localPosition.y;
			j++;
		}

		transform.localPosition = new Vector2(posArray[0].x, posArray[0].y);
		curPos = 0;

	}

	// Update is called once per frame
	void Update()
	{
		Menu();

		if (isOpen)
		{
			Move();
			Choose();
		}

	}

	void Move()
	{
		//0 1 2
		//3 4 5

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (curPos > 0)
			{
				curPos--;
				transform.localPosition = new Vector2(posArray[curPos].x, posArray[curPos].y);
			}
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (curPos < 5)
			{
				curPos++;
				transform.localPosition = new Vector2(posArray[curPos].x, posArray[curPos].y);
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			switch(curPos)
			{
				case 3:
					transform.localPosition = new Vector2(posArray[0].x, posArray[0].y);
					curPos = 0;
					break;
				case 4:
					transform.localPosition = new Vector2(posArray[1].x, posArray[1].y);
					curPos = 1;
					break;
				case 5:
					transform.localPosition = new Vector2(posArray[2].x, posArray[2].y);
					curPos = 2;
					break;
				default:
					break;
			}
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (curPos >= 0 && curPos <= 2)
			{
				curPos += 3;
				transform.localPosition = new Vector2(posArray[curPos].x, posArray[curPos].y);
			}
			else
			if (curPos >= 3 && curPos <= 5)
			{
				curPos -= 3;
				transform.localPosition = new Vector2(posArray[curPos].x, posArray[curPos].y);
			}
		}

		ChangeShape();
	}

	void Choose()
	{
		if(Input.GetButtonDown("Attack"))
		{
			if (curPos < inventory.items.Count)
			{
				Debug.Log(inventory.items[curPos].ID);
				sceneManager.skillEquipped = inventory.items[curPos].ID;
				gui_Skill.GetComponent<Image>().sprite = inventory.items[curPos].icon;
			}
			else
			{
				switch (curPos)
				{
					case 3:
						menuParent.GetComponent<Canvas>().enabled = false;
						Time.timeScale = 1;
						isOpen = false;
						break;
					case 4:
						Time.timeScale = 1;
						isOpen = false;
						SceneManager.LoadScene("TitleScreen");
						break;
					case 5:
						

						if (AudioListener.pause) //If is off -> on
						{
							Camera.main.GetComponent<AudioSource>().Play();
							AudioListener.pause = false;
							itemList[5].GetComponent<Image>().sprite = volumnOff;
						}
						else
						{
							StopAllAudio();
							AudioListener.pause = true;
							itemList[5].GetComponent<Image>().sprite = volumnOn;
						}
						break;
				}
			}
		}
	}

	void Menu()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (!isOpen)
			{
				menuParent.GetComponent<Canvas>().enabled = true;
				Time.timeScale = 0;
			}
			else
			{
				menuParent.GetComponent<Canvas>().enabled = false;
				Time.timeScale = 1;
			}
			isOpen = !isOpen;
		}
	
	}

	void ChangeShape()
	{
		if (curPos >= 0 && curPos <= 2)
		{
			this.GetComponent<Image>().sprite = small;
			rect.sizeDelta = smallSize;
		}
		else
		{
			this.GetComponent<Image>().sprite = big;
			rect.sizeDelta = bigSize;

		}
	}

	void StopAllAudio()
	{
		AudioSource[] allAudio;
		allAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		foreach (AudioSource audio in allAudio)
			audio.Stop();
	}
}

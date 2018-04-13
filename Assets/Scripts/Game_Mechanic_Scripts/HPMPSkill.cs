using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPMPSkill : MonoBehaviour {

	public Image HP;
	public Image MP;
	public Transform EquippedSkill;

	public GameObject character;

	void Update () {

		HP.GetComponent<Image>().fillAmount = character.GetComponent<PlayerControl>().HP*10 / 100;
		MP.GetComponent<Image>().fillAmount = character.GetComponent<PlayerControl>().MP / 100;

	}

}

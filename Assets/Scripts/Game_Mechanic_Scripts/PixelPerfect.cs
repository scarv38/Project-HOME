using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect : MonoBehaviour {

	public Material material;
	void Awake () {

		SpriteRenderer [] list = FindObjectsOfType(typeof(SpriteRenderer)) as SpriteRenderer[];
		foreach (SpriteRenderer s in list)
			s.material = material;

	}
	

}

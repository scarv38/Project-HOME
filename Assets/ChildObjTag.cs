using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildObjTag : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		setTag(this.transform);
	}
	
	void setTag(Transform child)
	{
		if (child == null)
			return;

		child.gameObject.tag = gameObject.tag;

		foreach (Transform t in child)
		{
			t.gameObject.tag = gameObject.tag;
			setTag(t);
		}

	}
}

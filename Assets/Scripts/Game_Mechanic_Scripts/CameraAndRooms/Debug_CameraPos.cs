using UnityEngine;
using System.Collections;

public class Debug_CameraPos : MonoBehaviour {

    public float defaultX = 14.99f;
    public float defaultY = -8.49f;
    public uint blockX = 0;
    public uint blockY = 0;
    float newX;
    float newY;
	// Update is called once per frame
	public void ChangePos() {
        newX = defaultX + (defaultX*2)*blockX;
        newY = defaultY + (defaultY*2)*blockY;
        transform.position = new Vector3(newX, newY, transform.position.z);

	}
}
    
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBoundsGenerator : MonoBehaviour {

    public Transform bounds;
    public Transform carrier;
    float defaultX = 14.99f;
    float defaultY = -8.49f;
    float newX;
    float newY;
    public int RowNum,ColNum;

    public void makeRoom()
    {
        Transform parent = Instantiate(carrier,Vector3.zero, transform.rotation);
        parent.name = ("Room Bounds");
        for (int i = 0; i < ColNum; i++)
        {
            for (int j = 0; j < RowNum; j++)
            {
                newX = defaultX + (defaultX*2)*i;
                newY = defaultY + (defaultY*2)*j;
                Transform child = Instantiate(bounds, new Vector3(newX, newY, 0), transform.rotation);
                child.name = "Bounds" + "[" + i + "][" + j + "]";
                child.parent = parent;
            }
           
        }
    }
}

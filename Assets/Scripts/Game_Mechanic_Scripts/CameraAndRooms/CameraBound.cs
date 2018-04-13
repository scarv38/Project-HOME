using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBound : MonoBehaviour {

    Bounds currentBounds;
    float moveTime = 1f;
	public Transform player;
	public int force;

	IEnumerator moveToNewBounds(bool includePlayer,bool isFarRoom)
    {
        Vector3 startPos = transform.position;
        Vector3 nextPos = transform.position;

        float targX = currentBounds.center.x;
        float targY = currentBounds.center.y;

        Vector3 targetPos = new Vector3(targX, targY, transform.position.z);

        float countTime = 0f;

		PlayerControl.g_playerCanMove = false;
		PlayerControl.g_waitForCamera = true;

		if(includePlayer)
		{
			Animator anim = player.GetComponent<Animator>();
			float aX = anim.GetFloat("MoveX");
			float aY = anim.GetFloat("MoveY");
			player.GetComponent<Rigidbody2D>().AddForce(new Vector2(aX * force, aY * force), ForceMode2D.Force);
		}
		

		while (countTime < moveTime)
        {
            countTime += Time.deltaTime;
            nextPos = VLerp(countTime, moveTime, startPos, targetPos);
            transform.position = nextPos;
            yield return 0;
        }
			
		if (countTime >= moveTime)
		{
			if (!isFarRoom) {
				PlayerControl.g_playerCanMove = true;
				PlayerControl.g_waitForCamera = false;
			}

			if (includePlayer)
			{
				player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				transform.position = targetPos;
			}
		}
    }

	public void SetNewBounds(Bounds newBounds,bool includePlayer,bool isFarRoom = false)
    {
        currentBounds = newBounds;
		StartCoroutine(moveToNewBounds(includePlayer,isFarRoom));

    }

    public Vector3 VLerp(float curTime, float duration, Vector3 start, Vector3 des)
    {
        float t = curTime / duration;
        Vector3 result = Vector3.Lerp(start, des, t);

        return result;
    }
        
}

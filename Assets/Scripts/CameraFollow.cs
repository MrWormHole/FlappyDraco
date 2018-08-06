using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public static float offsetX;

	void Update () {
        if (FlappyDraco.instance != null)
        {
            if (FlappyDraco.instance.isAlive)
            {
                MoveTheCamera();
            }
        }
	}

    public void MoveTheCamera()
    {
        Vector3 temp = transform.position;
        temp.x = FlappyDraco.instance.getPositionX() + offsetX ;
        transform.position = temp;
    }
}

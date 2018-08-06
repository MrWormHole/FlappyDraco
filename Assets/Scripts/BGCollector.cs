using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCollector : MonoBehaviour {
    private GameObject[] backgrounds;
    private GameObject[] grounds;

    private float lastBGX;
    private float lastGroundX;

	void Awake () {
        backgrounds = GameObject.FindGameObjectsWithTag("Background");
        grounds = GameObject.FindGameObjectsWithTag("Ground");
        
        lastBGX = backgrounds[0].transform.position.x;
        lastGroundX = grounds[0].transform.position.x;

        for (int i=1; i < backgrounds.Length; i++)
        {
            if(lastBGX< backgrounds[i].transform.position.x)
            {
                lastBGX = backgrounds[i].transform.position.x;
            }
        }

         for (int i = 1; i < grounds.Length; i++)
        {
            if (lastGroundX < grounds[i].transform.position.x)
            {
                lastGroundX = grounds[i].transform.position.x;
            }
        }

        //Debug.Log("last bg x coord: " + lastBGX);
        //Debug.Log("last ground x coord: " + lastGroundX);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Background")
        {
            Vector3 temp = target.transform.position; //assigned the first bg's position when target touched first

            float width = 13.31f; //((BoxCollider2D)target).size.x; collider's width and backgrounds's width different

            temp.x = lastBGX + width; //we changed x value of temp to new lastBGX

            target.transform.position = temp; //we finally added our first bg to the near the lastBGX

            lastBGX = temp.x; //ladies and gentlemen i introduce you our new lastBGX

        }

        else if (target.tag == "Ground")
        {
            //same steps for grounds like backgrounds
            Vector3 temp = target.transform.position;

            float width = 13.31f; //((BoxCollider2D)target).size.x; collider's width and backgrounds's width different

            temp.x = lastGroundX + width;

            target.transform.position = temp;

            lastGroundX = temp.x;

        }
    }
}

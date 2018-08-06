using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCollector : MonoBehaviour {

    private GameObject[] pipeHolders;
    private float distance; //distance between pipeholders(OPTIONAL)
    private float lastPipesX = 0f;
    private float pipeMin = -1.5f;
    private float pipeMax = 2.5f;

    private void Awake()
    {
        pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");

        for (int i = 0; i < pipeHolders.Length; i++)
        {
            Vector3 temp = pipeHolders[i].transform.position;
            temp.y = Random.Range(pipeMin, pipeMax);
            pipeHolders[i].transform.position = temp;
        }

        for (int i = 0; i < pipeHolders.Length; i++)
        {
            if (lastPipesX < pipeHolders[i].transform.position.x)
            {
                lastPipesX = pipeHolders[i].transform.position.x;
            }
        }
        //Debug.Log("last pipes x coord: " + lastPipesX);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "PipeHolder")
        {
            Vector3 temp = target.transform.position;
            temp.x = lastPipesX + distance;
            temp.y = Random.Range(pipeMin, pipeMax);
            target.transform.position = temp;
            lastPipesX = temp.x;
        }
    }

	void Update () {
        if (FlappyDraco.instance.isAlive)
        {
            distance = Random.Range(3, 6);
        }
        
    }
}

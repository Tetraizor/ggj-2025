using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puffpuff : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        var rand = Random.Range(0, 2);
        if (rand == 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        GetComponent<Animator>().speed = Random.Range(-0.5f, 0.5f) + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

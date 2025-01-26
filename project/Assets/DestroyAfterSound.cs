using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSound : MonoBehaviour
{
    [SerializeField] AudioSource asss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!asss.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}

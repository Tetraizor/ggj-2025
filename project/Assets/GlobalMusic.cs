using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMusic : MonoSingleton<GlobalMusic>
{
    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroToNextLevel : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string scene;

    public void ToLevel()
    {
        TransitionManager.Instance.PlayDeath(scene);
    }
}

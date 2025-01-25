using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleManager : MonoSingleton<BubbleManager>
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool GameStarted;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameStarted)
        {
            GameStarted = true;
            ToolboxManager.Instance.GetComponent<ToolboxManager>().Close();
            UshersManager.Instance.GetComponent<ToolboxManager>().Close();
        } 
        if(Input.GetKeyDown(KeyCode.R))
        {
            TransitionManager.Instance.PlayDeath(SceneManager.GetActiveScene().name);
        }
    }
}

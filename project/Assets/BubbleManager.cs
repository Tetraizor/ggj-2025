using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleManager : MonoSingleton<BubbleManager>
{
    protected override void Awake()
    {
        foreach (GameObject pos in TransitionManager.Instance.ResetPositionMap.Keys)
        {
            print(pos.name);
            Instantiate(pos, TransitionManager.Instance.ResetPositionMap[pos], TransitionManager.Instance.ResetRotationMap[pos]);
            
        }
        TransitionManager.Instance.ResetPositionMap.Clear();
        TransitionManager.Instance.ResetRotationMap.Clear();
    }
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

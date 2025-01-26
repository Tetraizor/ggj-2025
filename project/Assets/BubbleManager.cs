using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleManager : MonoSingleton<BubbleManager>
{
    public List<Vector3> bubblePosiitons;
    public List<GameObject> bubbles;
    [SerializeField] private GameObject NextLevelContainer;
    // Start is called before the first frame update
    void Awake()
    {
        if(bubbles.Count == 0)
        {
            SummonBubbles();
        }
    }
    public bool GameStarted;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GameStarted)
        {
            GameStarted = true;
            ToolboxManager.Instance.GetComponent<ToolboxManager>().Close();
            UshersManager.Instance.GetComponent<UshersManager>().HideUshers();
        } 
        if(Input.GetKeyDown(KeyCode.R))
        {
            TransitionManager.Instance.PlayReset();
        }
    }

    public void KillBubbles()
    {
        GameStarted= false;
        NextLevelContainer.GetComponent<ToNextLevel>().volume = 0;
        ToolboxManager.Instance.Open();
        foreach(GameObject bub in bubbles)
        {
            Destroy(bub);
        }
        bubbles.Clear();
        SummonBubbles();
    }

    [SerializeField] private List<GameObject> bubbleObject;
    public void SummonBubbles()
    {
        int i = 0;
        foreach (Vector3 bub in bubblePosiitons)
        {
            GameObject obj = Instantiate(bubbleObject[i], bub, Quaternion.identity);
            bubbles.Add(obj);
            i += 1;
        }
    }
}

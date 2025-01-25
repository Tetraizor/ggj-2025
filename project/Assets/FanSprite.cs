using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSprite : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject ps;
    [SerializeField] private GameObject ps2;
    // Start is called before the first frame update
    private void Awake()
    {
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(BubbleManager.Instance.GameStarted)
        {
            anim.SetBool("run", true);
            ps.SetActive(true);
            ps2.SetActive(true);
            
        }
        else
        {
            anim.SetBool("run", false);
            ps.SetActive(false);
            ps2.SetActive(false);
        }
    }
}

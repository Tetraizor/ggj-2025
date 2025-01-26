using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSprite : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private GameObject ps;
    [SerializeField] private GameObject Popsfx;
    // Start is called before the first frame update
    private void Awake()
    {
        anim= GetComponent<Animator>();
    }

    GameObject MySlot;
    private void Start()
    {
        //0 for fan
        MySlot = ToolboxManager.Instance.transform.GetChild(0).GetChild(0).gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if(BubbleManager.Instance.GameStarted)
        {
            anim.SetBool("run", true);
            ps.SetActive(true);
            if(!GetComponent<AudioSource>().isPlaying)
            {
                GetComponent<AudioSource>().Play();
            }
            
        }
        else
        {
            anim.SetBool("run", false);
            ps.SetActive(false);
            GetComponent<AudioSource>().Stop();
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !BubbleManager.Instance.GameStarted)
        {
            Instantiate(Popsfx, transform.position, Quaternion.identity);
            MySlot.GetComponent<Slot>().Count += 1;
            MySlot.GetComponent<Slot>().UpdateGraphics();
            Destroy(transform.parent.gameObject);
        }
    }
}

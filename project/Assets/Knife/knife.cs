using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    GameObject MySlot;
    private void Start()
    {
        //0 for fan
        MySlot = ToolboxManager.Instance.transform.GetChild(0).GetChild(1).gameObject;
    }
    private void Update()
    {
        if(!BubbleManager.Instance.GameStarted)
        {
            GetComponent<Collider2D>().enabled = true;
            GetComponentInChildren<Animator>().SetBool("back", true);
        }
        else
        {
            GetComponentInChildren<Animator>().SetBool("back", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            collision.gameObject.GetComponent<bubble2>().Pop(new Vector2(-transform.right.y, transform.right.x));
            GetComponent<Collider2D>().enabled = false;
            GetComponentInChildren<Animator>().SetTrigger("boom");
        }
    }
    [SerializeField] private GameObject Popsfx;
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !BubbleManager.Instance.GameStarted)
        {
            Instantiate(Popsfx, transform.position, Quaternion.identity);
            MySlot.GetComponent<Slot>().Count += 1;
            MySlot.GetComponent<Slot>().UpdateGraphics();
            Destroy(gameObject);
        }
    }
}

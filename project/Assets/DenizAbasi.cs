using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenizAbasi : MonoBehaviour
{
    public bool IsPlacable;
    GameObject MySlot;
    private void Start()
    {
        //0 for fan
        MySlot = ToolboxManager.Instance.transform.GetChild(0).GetChild(2).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble") && BubbleManager.Instance.GameStarted)
        {
            collision.attachedRigidbody.velocity = Vector2.zero;
            collision.attachedRigidbody.AddForce(transform.up * 20, ForceMode2D.Impulse);
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && !BubbleManager.Instance.GameStarted && IsPlacable)
        {
            MySlot.GetComponent<Slot>().Count += 1;
            MySlot.GetComponent<Slot>().UpdateGraphics();
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife : MonoBehaviour
{
    private void Update()
    {

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

}

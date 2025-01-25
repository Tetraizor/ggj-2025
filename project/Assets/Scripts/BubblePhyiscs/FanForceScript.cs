using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanForceScript : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] private float speed;
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bubble") && BubbleManager.Instance.GameStarted)
        {
            other.attachedRigidbody.velocity /= 4;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bubble") && BubbleManager.Instance.GameStarted)
        {
            print("working");
            other.GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
        }
    }
}

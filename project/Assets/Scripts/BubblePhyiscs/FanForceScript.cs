using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanForceScript : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] private float speed;
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("works");
        if (other.gameObject.CompareTag("Bubble") && BubbleManager.Instance.GameStarted)
        {
            print("working");
            other.GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
        }
    }
}

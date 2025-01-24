using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanForceScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bubble"))
        {
            other.GetComponent<Rigidbody2D>().AddForce(transform.up * 10);
        }
    }
}

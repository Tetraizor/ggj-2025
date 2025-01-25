using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenizAbasi : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bubble"))
        {
            collision.attachedRigidbody.velocity = Vector2.zero;
            collision.attachedRigidbody.AddForce(transform.up * 20, ForceMode2D.Impulse);
        }
    }
}

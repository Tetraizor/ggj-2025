using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class bubble2 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float size = 1;
    [SerializeField] private GameObject bubbleO;
    private Transform Target;
    public bool Dominated = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if(Dominated)
        {
            Vector3 dir = -transform.position + Target.position;
            rb.velocity= dir*20;
            if (dir.magnitude < 1)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            bubble2 bub = collision.gameObject.GetComponent<bubble2>();
            Rigidbody2D colrb = collision.gameObject.GetComponent<Rigidbody2D>();
            float colSize = bub.size;
            Vector3 center = (transform.position * Mathf.Sqrt(size) + collision.transform.position * Mathf.Sqrt(colSize) / (Mathf.Sqrt(size) + Mathf.Sqrt(colSize)));
            Vector2 newVel = (rb.mass * rb.velocity + colrb.velocity * colrb.mass) / (rb.mass + colrb.mass);
            //No idea how to do it if the yare equal
            if(colrb.velocity.magnitude < rb.velocity.magnitude)
            {
                bub.Target = transform;
                bub.Dominated = true;
                
                collision.gameObject.GetComponent<bubble2>().Dominated = true;
                size += colSize;
                collision.transform.DOScale(Vector3.zero, 0.2f);
                transform.DOScale(new Vector3(Mathf.Sqrt(size), Mathf.Sqrt(size), 1), 0.2f);
                rb.velocity = newVel;
                
            }
        }
    }
}

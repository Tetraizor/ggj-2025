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
            rb.velocity= dir *3 ;
            if (dir.magnitude < 0.3 || GetComponent<SpriteRenderer>().color.a == 0)
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
            //Check the intense difference in radius
            //No idea how to do it if they are equal
            if(colrb.velocity.magnitude < rb.velocity.magnitude)
            {
                if (Mathf.Sqrt(size) * 1.2f < Mathf.Sqrt(colSize))
                {
                    Target = collision.transform;
                    Dominated = true;
                    colSize += size;
                    transform.DOScale(new Vector3(0, 0, 1), 0.5f);
                    GetComponent<SpriteRenderer>().DOFade(0, 0.2f);
                    collision.transform.DOScale(new Vector3(Mathf.Sqrt(colSize), Mathf.Sqrt(colSize), 1), 0.2f);
                    colrb.velocity = newVel;
                }
                else
                {
                    bub.Target = transform;
                    bub.Dominated = true;

                    size += colSize;
                    collision.transform.DOScale(new Vector3(0, 0, 1), 0.5f);
                    collision.GetComponent<SpriteRenderer>().DOFade(0, 0.2f);
                    transform.DOScale(new Vector3(Mathf.Sqrt(size), Mathf.Sqrt(size), 1), 0.2f);
                    rb.velocity = newVel;
                }
                
            }
        }
    }
}

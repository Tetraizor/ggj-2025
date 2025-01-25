using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bubble2 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float size = 1;
    [SerializeField] private GameObject bubbleO;
    private Transform Target;
    public bool Dominated = false;

    private bool enable = false;
    private float e_timer = 1.5f;
    private Collider2D myCol;
    private bool enable_self = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(BubbleManager.Instance.GameStarted)
        {
            enable_self = true;
        }
    }
    private void FixedUpdate()
    {
        if(Dominated)
        {
            Vector3 dir = -transform.position + Target.position;
            rb.velocity= dir *3 ;
            if (GetComponent<SpriteRenderer>().color.a == 0)
            {
                Destroy(gameObject);
            }
        }
        e_timer -= Time.fixedDeltaTime;
        if (e_timer < 0 && !enable)
        {
            enable= true;
        }

    }
    [SerializeField] private GameObject summonWhenPop;
    public void Pop(Vector3 collisionPoint)
    {
        if(enable_self)
        {
            GameObject x = Instantiate(summonWhenPop, transform.position, Quaternion.identity);
            x.transform.localScale = transform.localScale;
            Vector3 onePos = (collisionPoint);
            Vector3 secPos = -(collisionPoint);
            GameObject b = Instantiate(bubbleO, transform.position, Quaternion.identity);
            b.GetComponent<bubble2>().size = size / 2;
            b.transform.localScale = new Vector3(Mathf.Sqrt(size / 2), Mathf.Sqrt(size / 2), 1);
            b.GetComponent<Rigidbody2D>().velocity = (onePos * Mathf.Sqrt(size)) / 3;
            b.GetComponent<Rigidbody2D>().velocity += rb.velocity / 2;

            GameObject k = Instantiate(bubbleO, transform.position, Quaternion.identity);
            k.GetComponent<bubble2>().size = size / 2;
            k.transform.localScale = new Vector3(Mathf.Sqrt(size / 2), Mathf.Sqrt(size / 2), 1);
            k.GetComponent<Rigidbody2D>().velocity = (secPos * Mathf.Sqrt(size)) / 3;
            k.GetComponent<Rigidbody2D>().velocity += rb.velocity / 2;
            Destroy(gameObject);
        }
    }
    public void Death()
    {
        GameObject x = Instantiate(summonWhenPop, transform.position, Quaternion.identity);
        x.transform.localScale = transform.localScale;
        Destroy(gameObject);
        TransitionManager.Instance.PlayDeath(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble") && enable && enable_self)
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
                    print("waltuh?");
                    Target = collision.transform;
                    Dominated = true;
                    colSize += size;
                    bub.size = colSize;
                    colrb.mass = colSize;
                    transform.DOScale(new Vector3(0, 0, 1), 0.5f);
                    GetComponent<SpriteRenderer>().DOFade(0, 0.3f);
                    collision.transform.DOScale(new Vector3(Mathf.Sqrt(colSize), Mathf.Sqrt(colSize), 1), 0.2f);
                    colrb.velocity = newVel;
                }
                else
                {
                    bub.Target = transform;
                    bub.Dominated = true;

                    size += colSize;
                    rb.mass = size;
                    collision.transform.DOScale(new Vector3(0, 0, 1), 0.5f);
                    collision.GetComponent<SpriteRenderer>().DOFade(0, 0.2f);
                    transform.DOScale(new Vector3(Mathf.Sqrt(size), Mathf.Sqrt(size), 1), 0.2f);
                    rb.velocity = newVel;
                }
                
            }
        }
        else if (collision.CompareTag("Wall"))
        {
            Death();
        }
    }
    
}

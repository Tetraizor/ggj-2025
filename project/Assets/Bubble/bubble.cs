using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble : MonoBehaviour
{
    private float starTimer = 1.5f;
    public float size = 1;
    [SerializeField] private GameObject bubbleO;
    public bool aliv = true;
    [SerializeField]private bool enable = false;

    private Rigidbody2D selfrb;
    private void Awake()
    {
       selfrb= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(!enable)
        {
            starTimer -= Time.deltaTime;
        }
        if(starTimer < 0)
        {
            enable= true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enable)
        {
            if (collision.gameObject.GetComponent<bubble>() != null)
            {
                float targetRadius = Mathf.Sqrt(collision.transform.GetComponent<bubble>().size);
                Vector3 center = (transform.position * Mathf.Sqrt(size) + collision.transform.position * targetRadius) / (Mathf.Sqrt(size) + targetRadius);
                Vector3 TrueRad = new Vector3(Mathf.Sqrt(size + targetRadius), Mathf.Sqrt(size + targetRadius), 1);
                if (selfrb.velocity.magnitude < 4 && collision.GetComponent<Rigidbody2D>().velocity.magnitude < 4)
                {
                    
                    transform.DOMove(center, 0.2f);
                    if (size >= collision.gameObject.GetComponent<bubble>().size)
                    {
                        transform.DOScale(TrueRad, 0.2f);
                        StartCoroutine(WaitForSecondsExample(TrueRad, center, collision, targetRadius));

                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().DOFade(0, 0.1f);
                    }
                }
                else
                {
                    print("spawn");
                    collision.gameObject.GetComponent<bubble>().aliv = false;
                    GameObject b = Instantiate(bubbleO, center, Quaternion.identity);
                    b.GetComponent<bubble>().size = size + targetRadius;
                    b.transform.localScale = new Vector3(Mathf.Sqrt(size + targetRadius), Mathf.Sqrt(size + targetRadius), 1);
                    Vector2 MaxVel = Vector2.zero;
                    if (collision.GetComponent<Rigidbody2D>().velocity.magnitude < selfrb.velocity.magnitude)
                    {
                        MaxVel = selfrb.velocity;
                        
                    }
                    else { MaxVel = collision.GetComponent<Rigidbody2D>().velocity; }
                   
                    b.GetComponent<Rigidbody2D>().velocity = MaxVel;
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
    [SerializeField] GameObject explosionEffect;
    public void Pop(Vector3 collisionPoint)
    {
        Instantiate(explosionEffect, transform.position,Quaternion.identity);
        Vector3 onePos = (collisionPoint);
        Vector3 secPos = -(collisionPoint);
        GameObject b = Instantiate(bubbleO, transform.position, Quaternion.identity);
        b.GetComponent<bubble>().size = size/2;
        b.transform.localScale = new Vector3(Mathf.Sqrt(size/2), Mathf.Sqrt(size/2), 1);
        b.GetComponent<Rigidbody2D>().velocity = onePos* 2;
        b.GetComponent<Rigidbody2D>().velocity += selfrb.velocity;

        GameObject k = Instantiate(bubbleO, transform.position, Quaternion.identity);
        k.GetComponent<bubble>().size = size / 2;
        k.transform.localScale = new Vector3(Mathf.Sqrt(size / 2), Mathf.Sqrt(size / 2), 1);
        k.GetComponent<Rigidbody2D>().velocity = secPos * 2;
        k.GetComponent<Rigidbody2D>().velocity += selfrb.velocity;
        Destroy(gameObject);
    }
    IEnumerator WaitForSecondsExample(Vector3 TrueRad, Vector3 center, Collider2D collision, float targetRadius)
    {
        yield return new WaitForSeconds(0.21f);
        if (aliv)
        {
            try
            {
                collision.gameObject.GetComponent<bubble>().aliv = false;
                GameObject b = Instantiate(bubbleO, center, Quaternion.identity);
                b.GetComponent<bubble>().size = size + targetRadius;
                b.transform.localScale = new Vector3(Mathf.Sqrt(size + targetRadius), Mathf.Sqrt(size + targetRadius), 1);
                Vector2 MaxVel = Vector2.zero;
                if(collision.GetComponent<Rigidbody2D>().velocity.magnitude < selfrb.velocity.magnitude)
                {
                    MaxVel = selfrb.velocity;
                }
                else { MaxVel = collision.GetComponent<Rigidbody2D>().velocity; }
                b.GetComponent<Rigidbody2D>().velocity = MaxVel;
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            catch
            {
                //Destroy(gameObject);
            }
        }
    }
}

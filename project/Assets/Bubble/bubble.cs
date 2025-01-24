using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble : MonoBehaviour
{
    public float size = 1;
    [SerializeField] private GameObject bubbleO;
    public bool aliv = true;
    private void Update()
    {
        if(!GetComponent<Collider2D>().enabled)
        {
            Debug.Log("waltuh");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            float targetRadius = Mathf.Sqrt(collision.transform.GetComponent<bubble>().size);
            Vector3 center = (transform.position * Mathf.Sqrt(size) + collision.transform.position * targetRadius) / (Mathf.Sqrt(size) + targetRadius);

            transform.DOMove(center, 0.2f);
            Vector3 TrueRad = new Vector3(Mathf.Sqrt(size + targetRadius), Mathf.Sqrt(size + targetRadius), 1);
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
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            catch
            {
                Destroy(gameObject);
            }
        }
    }
}

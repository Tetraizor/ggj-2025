using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twitch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("decor"))
        {
            Sequence rotationSequence = DOTween.Sequence();
            rotationSequence.Append(transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, 5), 0.2f));
            rotationSequence.Append(transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 0, -5), 0.2f));

        }
    }
}

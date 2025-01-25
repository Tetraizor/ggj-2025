using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnFinish : MonoBehaviour
{
   private void Destroy_()
    {
        Destroy(gameObject);
    }
    private void DestroyParent() { Destroy(transform.parent.gameObject); }
}

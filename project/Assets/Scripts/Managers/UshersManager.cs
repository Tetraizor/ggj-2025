using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UshersManager : MonoSingleton<UshersManager>
{
    List<GameObject> children;
    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i).gameObject);
        }
    }
    public void HideUshers()
    {
        //gameObject.SetActive(false);
    }
}

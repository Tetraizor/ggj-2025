using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UshersManager : MonoSingleton<UshersManager>
{ 

    public void HideUshers()
    {
        gameObject.SetActive(false);
    }
}

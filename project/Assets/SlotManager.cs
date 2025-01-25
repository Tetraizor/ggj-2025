using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private List<Slot> slots;
    // Start is called before the first frame update
    void Start()
    {
        if (TransitionManager.Instance.slot.Count > 0)
        {
            print("waltuh");
            slots[0].Count = TransitionManager.Instance.slot[0];
            slots[0].UpdateGraphics();
        }
        else
        {
            TransitionManager.Instance.slot.Add(slots[0].Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TransitionManager.Instance.slot[0] = slots[0].Count;
    }
}

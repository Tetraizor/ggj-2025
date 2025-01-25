using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToNextLevel : MonoBehaviour
{
    public float volume;
    public float ShouldBeVolume;
    [SerializeField] private string scene;
    [SerializeField] private GameObject SlotManager;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble"))
        {
            volume += collision.GetComponent<bubble2>().size;
            Destroy(collision.gameObject);
        }
        if(volume == ShouldBeVolume)
        {
            TransitionManager.Instance.PlayDeath(scene);
        }
    }
}

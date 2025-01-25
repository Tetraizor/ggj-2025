using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSprite : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private ParticleSystem ps;
    // Start is called before the first frame update
    private void Awake()
    {
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(BubbleManager.Instance.GameStarted)
        {
            anim.SetBool("run", true);
            if (!ps.isPlaying)
            {
                ps.Play();
            }
            
        }
        else
        {
            anim.SetBool("run", false);
            ps.Stop();
        }
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoSingleton<TransitionManager>
{
    // Start is called before the first frame update

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    public string scene;
    // Update is called once per frame
    public void EndScreen()
    {

        SceneManager.LoadScene(scene);
    }
    public void PlayDeath(string s)
    {
        scene = s;
        GetComponent<Animator>().SetTrigger("go");

    }
    public void PlayReset()
    {
        GetComponent<Animator>().SetTrigger("re");
    }
    private void Reset_()
    {
        GetComponent<Animator>().ResetTrigger("re");
        BubbleManager.Instance.KillBubbles();

    }
}



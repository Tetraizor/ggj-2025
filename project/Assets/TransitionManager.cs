using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoSingleton<TransitionManager>
{
    // Start is called before the first frame update
    public Dictionary<GameObject, Vector3> ResetPositionMap = new Dictionary<GameObject, Vector3>();
    public Dictionary<GameObject, Quaternion> ResetRotationMap = new Dictionary<GameObject, Quaternion>();
    public List<int> slot;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    public string scene;
    // Update is called once per frame
    public void EndScreen()
    {
        Destroy(ItemManager.Instance.gameObject);

        SceneManager.LoadScene(scene);
    }
    public void PlayDeath(string s)
    {
        scene = s;
        GetComponent<Animator>().SetTrigger("go");

    }

    public void Resett()
    {
        slot = new List<int>();
        ResetPositionMap.Clear();
        ResetRotationMap.Clear();
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueBallonTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private string[] texts;
    [SerializeField] private int count;
    [SerializeField] private GameObject ballon;
    [SerializeField] private GameObject posedion; 

    void Start()
    {
        ballon = gameObject;
        ballon.SetActive(true);
        posedion = gameObject.transform.parent.gameObject;
        posedion.SetActive(true);
        _textMeshPro = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        if (texts.Length > 0)
        {
            _textMeshPro.text = texts[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeText();
        }
    }

    void ChangeText()
    {
        count++;
        if (count < texts.Length)
        {
            _textMeshPro.text = texts[count];
        }
        else if (count >= texts.Length)
        {
            ballon.SetActive(false);
            posedion.SetActive(false);
        }
    }
}

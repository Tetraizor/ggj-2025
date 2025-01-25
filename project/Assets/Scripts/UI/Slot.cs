using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Data")]
    [SerializeField] private Item _item;
    public Item Item => _item;

    [Header("References")]
    [SerializeField] private Button _button;
    public Button Button => _button;

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameLabel;
    [SerializeField] private TextMeshProUGUI _countLabel;

    public int Count;

    public void UpdateGraphics()
    {
        _icon.sprite = _item.Icon;

        _nameLabel.text = _item.Name;

        if (Count > 0)
        {
            _countLabel.text = Count.ToString();
            _icon.color = Color.white;
            GetComponent<Image>().color = Color.white;

            _button.interactable = true;
        }
        else
        {
            _countLabel.text = string.Empty;
            _icon.color = new Color(1, 1, 1, .5f);
            GetComponent<Image>().color = new Color(.7f, .7f, .7f, .5f);

            _button.interactable = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Animator>().SetTrigger("TextOut");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Animator>().SetTrigger("TextIn");
    }
}
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] private Item _item;
    public Item Item => _item;

    [SerializeField]
    private Button _button;
    public Button Button => _button;

    [SerializeField]
    private Image _icon;
    public Image Icon => _icon;
}
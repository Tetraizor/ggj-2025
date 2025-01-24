using UnityEngine;

[System.Serializable]
public class Item
{
    [SerializeField]
    private int _id;
    public int Id => _id;

    [SerializeField]
    private string _name;
    public string Name => _name;

    [SerializeField]
    private Sprite _icon;
    public Sprite Icon => _icon;

    [SerializeField]
    private GameObject _relatedPrefab;
    public GameObject RelatedPrefab => _relatedPrefab;

    public Item(int id, string name, Sprite icon, GameObject relatedPrefab)
    {
        _id = id;
        _name = name;
        _icon = icon;
        _relatedPrefab = relatedPrefab;
    }
}
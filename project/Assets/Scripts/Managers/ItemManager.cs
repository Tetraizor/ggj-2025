using System;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    [SerializeField] private GameObject _cursor;
    [SerializeField] private Item _selectedItem => ToolboxManager.Instance.SelectedSlot?.Item;

    private bool _isPlacingItem = false;
    private GameObject _currentPlacable;

    public delegate void ItemPlacedEventHandler(Item item);
    public event ItemPlacedEventHandler ItemPlaced;

    protected override void Awake()
    {
        base.Awake();
        ToolboxManager.SlotClicked += OnSlotClicked;
        _cursor.GetComponent<SpriteRenderer>().sprite = null;
    }

    private void OnSlotClicked(Slot slot)
    {
        _cursor.GetComponent<SpriteRenderer>().sprite = _selectedItem.Icon;
    }

    

    protected void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(_cursor.transform.position, Vector2.zero, 1, LayerMask.GetMask("Wall", "DontIgnore"));

        if (hit.collider == null && _selectedItem != null)
        {
            _cursor.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (_selectedItem == null)
        {
            _cursor.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            _cursor.GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider == null)
            {
                if (_selectedItem != null)
                {

                    _cursor.GetComponent<SpriteRenderer>().color = Color.red;
                    var placable = Instantiate(
                        _selectedItem.RelatedPrefab,
                        _cursor.transform.position,
                        Quaternion.identity

                    );
                    _cursor.GetComponent<SpriteRenderer>().color = Color.white;
                    _cursor.GetComponent<SpriteRenderer>().sprite = null;

                    placable.name = _selectedItem.Name;

                    _currentPlacable = placable;
                    _isPlacingItem = true;

                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (_isPlacingItem && _currentPlacable != null)
            {
                
                Vector3 placablePosition = _currentPlacable.transform.position;
                Vector2 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - placablePosition;
                float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

                _currentPlacable.transform.eulerAngles = new Vector3(0, 0, angle);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_isPlacingItem && _currentPlacable != null)
            {
                _currentPlacable = null;
                _isPlacingItem = false;

                ItemPlaced?.Invoke(_selectedItem);
            }
        }

        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _cursor.transform.position = new Vector3(position.x, position.y, 0);
    }
    private void OnDestroy()
    {
        ToolboxManager.SlotClicked -= OnSlotClicked;
    }
}

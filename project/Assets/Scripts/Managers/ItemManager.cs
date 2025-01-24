using System;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    [SerializeField] private Transform _cursor;

    private Item _selectedItem => ToolboxManager.Instance.SelectedSlot?.Item;

    private bool _isPlacingItem = false;
    private GameObject _currentPlacable;

    public delegate void ItemPlacedEventHandler(Item item);
    public event ItemPlacedEventHandler ItemPlaced;

    protected override void Awake()
    {
        ToolboxManager.Instance.SlotClicked += OnSlotClicked;
    }

    private void OnSlotClicked(Slot slot)
    {
    }

    protected void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(_cursor.position, Vector2.zero);

            if (hit.collider == null)
            {
                if (_selectedItem != null)
                {
                    var placable = Instantiate(
                        _selectedItem.RelatedPrefab,
                        _cursor.position,
                        Quaternion.identity
                    );

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

        _cursor.position = new Vector3(position.x, position.y, 0);
    }
}

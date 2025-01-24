using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToolboxManager : MonoSingleton<ToolboxManager>
{
    private Transform _root;
    private Transform _slotContainer;

    private List<Slot> _slots;
    private Slot _selectedSlot;
    public Slot SelectedSlot => _selectedSlot;

    public delegate void SlotClickedEventHandler(Slot slot);
    public static event SlotClickedEventHandler SlotClicked;

    private bool _isToolboxOpen = true;
    public bool IsToolboxOpen => _isToolboxOpen;

    private void Start()
    {
        _root = transform;
        _slotContainer = _root.Find("SlotContainer");

        _slots = _slotContainer.GetComponentsInChildren<Slot>().ToList();

        foreach (Slot slot in _slots)
        {
            slot.Button.onClick.AddListener(() => OnSlotClicked(slot));
            slot.Button.onClick.AddListener(() => SlotClicked?.Invoke(slot));
        }

        ItemManager.Instance.ItemPlaced += OnItemPlaced;
    }

    private void OnItemPlaced(Item item)
    {
        _selectedSlot = null;
        Open();
    }

    private void OnSlotClicked(Slot slot)
    {
        if (_selectedSlot != null)
        {
            // Deselect old one.
            if (_selectedSlot == slot)
            {
                _selectedSlot = null;
            }
            else
            {
                _selectedSlot = slot;
            }
        }
        else
        {
            // Select new one.
            _selectedSlot = slot;
        }

        if (_selectedSlot != null)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void Toggle()
    {
        _isToolboxOpen = !_isToolboxOpen;

        if (_isToolboxOpen)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    public void Open()
    {
        if (_isToolboxOpen)
        {
            return;
        }

        _isToolboxOpen = true;

        GetComponent<Animator>().SetTrigger("In");
    }

    public void Close()
    {
        if (!_isToolboxOpen)
        {
            return;
        }

        _isToolboxOpen = false;

        GetComponent<Animator>().SetTrigger("Out");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _selectedSlot = null;
            Open();
        }
    }
}

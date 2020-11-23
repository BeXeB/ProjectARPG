﻿using UnityEngine;

public class Equipment : MonoBehaviour
{
    public static Equipment instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one Equipment instance");
            return;
        }
        instance = this;
    }

    Equipable[] currentEquipment;
    Inventory inventory;

    public delegate void OnEquipmentChanged(Equipable newItem, Equipable oldItem);
    public OnEquipmentChanged onEquipmentChangedCallback;

    private void Start()
    {
        inventory = Inventory.instance;
        int numberOfslots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipable[numberOfslots];
    }

    public void Equip(Equipable newItem)
    {
        int slotIndex = (int)newItem.equipSlot;
        Equipable oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);
        }
        currentEquipment[slotIndex] = newItem;
        if (onEquipmentChangedCallback != null)
        {
            onEquipmentChangedCallback.Invoke(newItem, oldItem);
        }
    }

    public void Unequip(int slotIndex)
    {
        Equipable oldItem = null;
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.AddItem(oldItem);
            currentEquipment[slotIndex] = null;
        }
        if (onEquipmentChangedCallback != null)
        {
            onEquipmentChangedCallback.Invoke(null, oldItem);
        }
    }

    public Equipable[] GetEquipment()
    {
        return currentEquipment;
    }
}

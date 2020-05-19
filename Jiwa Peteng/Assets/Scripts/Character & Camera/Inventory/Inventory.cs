using System;
using System.Collections.Generic;
using UnityEngine;


namespace Jiwa.Peteng
{
    public class Inventory : MonoBehaviour
    {
        private const int SLOTS = 6;

        private List<InventorySlot> mSlots = new List<InventorySlot>();

        public EventHandler<InventoryEventArgs> ItemAdded;

        public EventHandler<InventoryEventArgs> ItemRemoved;

        public EventHandler<InventoryEventArgs> ItemUsed;

        public Inventory()
        {
            for (int i = 0; i < SLOTS; i++)
            {
                mSlots.Add(new InventorySlot(i));
            }
        }

        private InventorySlot FindStackableSlot(InventoryItemBase item)
        {
            foreach (InventorySlot slot in mSlots)
            {
                if (slot.IsStackable(item))
                    return slot;
            }
            return null;
        }

        private InventorySlot FindNextEmptySlot()
        {
            foreach (InventorySlot slot in mSlots)
            {
                if (slot.IsEmpty)
                    return slot;
            }
            return null;
        }

        public void AddItem(InventoryItemBase item)
        {
            InventorySlot freeSlot = FindStackableSlot(item);
            if (freeSlot == null)
            {
                freeSlot = FindNextEmptySlot();
            }
            if (freeSlot != null)
            {
                freeSlot.AddItem(item);

                if (ItemAdded != null)
                    ItemAdded(this, new InventoryEventArgs(item));
            }
        }

        internal void UseItem(InventoryItemBase item)
        {
            if (ItemUsed != null)
                ItemUsed(this, new InventoryEventArgs(item));
        }

        public void RemoveItem(InventoryItemBase item)
        {
            foreach(InventorySlot slot in mSlots)
            {
                if (slot.Remove(item))
                {
                    if (ItemRemoved != null)
                        ItemRemoved(this, new InventoryEventArgs(item));
                    break;
                }
            }
        }
    }
}

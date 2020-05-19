using System;
using UnityEngine;

namespace Jiwa.Peteng
{
    public class InventoryEventArgs : EventArgs
    {
        public InventoryItemBase Item;

        public InventoryEventArgs(InventoryItemBase item)
        {
            Item = item;
        }

    }
}
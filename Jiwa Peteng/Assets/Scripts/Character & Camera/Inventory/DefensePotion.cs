using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiwa.Peteng
{
    public class DefensePotion : InventoryItemBase
    {
        public override string Name
        {
            get { return "Defense Potion"; }
        }

        public override ItemType itemType
        {
            get { return ItemType.Consumable; }
        }
    }
}
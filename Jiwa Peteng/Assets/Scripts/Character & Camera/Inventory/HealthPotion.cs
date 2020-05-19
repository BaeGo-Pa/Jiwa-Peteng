using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiwa.Peteng
{
    public class HealthPotion : InventoryItemBase
    {
        public override string Name
        {
            get { return "Health Potion"; }
        }

        public override ItemType itemType
        {
            get { return ItemType.Consumable; }
        }
    }
}
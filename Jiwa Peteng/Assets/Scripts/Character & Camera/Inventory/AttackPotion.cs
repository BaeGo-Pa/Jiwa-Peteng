using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiwa.Peteng
{
    public class AttackPotion : InventoryItemBase
    {
        public override string Name
        {
            get { return "Attack Potion"; }
        }

        public override ItemType itemType
        {
            get { return ItemType.Consumable; }
        }
    }
}
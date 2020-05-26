using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiwa.Peteng
{
    public class Sword : InventoryItemBase
    {
        public override string Name
        {
            get { return "Sword"; }
        }

        public override ItemType itemType
        {
            get { return ItemType.Weapon; }
        }

        public override int Damage
        {
            get { return 15; }
        }

        public override Vector3 weaponPosition
        {
            get { return new Vector3(0.088f, 0.009f, -0.05f); }
        }

        public override Vector3 weaponRotation
        {
            get { return new Vector3(-80f, 0, 0); }
        }

        public override Vector3 DropRotation
        {
            get { return new Vector3(180f, 0f, 0f); }
        }
    }
}
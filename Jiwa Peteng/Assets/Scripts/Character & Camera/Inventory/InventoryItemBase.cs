using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiwa.Peteng
{
    public enum ItemType
    {
        Consumable,
        Weapon
    }
    public class InteractableItemBase : MonoBehaviour
    {
        public string Name;

        public Sprite Image;

        public string InteractText = "- Press E to pickup the item -";

        public ItemType itemType;

        public bool IsConsumable;

        public GameObject Owner;


        public virtual void OnInteract()
        {

        }
    }

    public class InventoryItemBase : InteractableItemBase
    {
        public virtual Vector3 weaponPosition
        {
            get { return Vector3.zero; }
        }
        public virtual Vector3 weaponRotation
        {
            get { return Vector3.zero; }
        }

        public virtual Vector3 DropRotation
        {
            get { return Vector3.zero; }
        }

        public virtual string Name
        {
            get { return "_base item_"; }
        }

        public string InteractText
        {
            get { return "Press E to pickup the " + Name; }
        }

        public virtual ItemType itemType
        {
            get { return  itemType; }
        }
        public virtual int Damage
        {
            get
            {
                if (itemType == ItemType.Consumable)
                    return 0;
                return Damage;
            }
        }

        public Sprite _Image;

        public Sprite Image
        {
            get { return _Image; }
        }

        public InventorySlot Slot
        {
            get; set;
        }

        public virtual void OnUse()
        {
            transform.localPosition = weaponPosition;
            transform.localEulerAngles = weaponRotation;
        }

        public virtual void OnDrop()
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                gameObject.SetActive(true);
                gameObject.transform.position = hit.point;
                gameObject.transform.eulerAngles = DropRotation;
            }
        }

        public virtual void OnPickup()
        {
            gameObject.SetActive(false);
        }
    }
}
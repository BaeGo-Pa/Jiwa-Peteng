﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jiwa.Peteng
{
    public class ItemClickHandler : MonoBehaviour
    {
        public Inventory _Inventory;

        public KeyCode _Key;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(_Key))
            {
                FadeToColor(_button.colors.pressedColor);
                //Click the button
                _button.onClick.Invoke();
            }
            else
                FadeToColor(_button.colors.normalColor);
        }

        void FadeToColor(Color color)
        {
            Graphic graphic = GetComponent<Graphic>();
            graphic.CrossFadeColor(color, _button.colors.fadeDuration, true, true);
        }

        private InventoryItemBase AttachedItem
        {
            get
            {
                ItemDragHandler dragHandler = transform.Find("ItemImage").GetComponent<ItemDragHandler>();

                return dragHandler.Item;
            }
        }

        public void OnItemClicked()
        {
            InventoryItemBase item = AttachedItem;

            if (item != null)
            {
                _Inventory.UseItem(item );

                item.OnUse();
            }
        }
    }
}
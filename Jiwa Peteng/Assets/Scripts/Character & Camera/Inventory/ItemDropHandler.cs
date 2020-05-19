using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Jiwa.Peteng
{
    public class ItemDropHandler : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            RectTransform invPanel = transform as RectTransform;

            if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
                Debug.Log("Drop item");
        }
    }
}
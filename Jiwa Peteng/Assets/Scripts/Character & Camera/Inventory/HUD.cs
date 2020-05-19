using UnityEngine;
using UnityEngine.UI;

namespace Jiwa.Peteng
{
    public class HUD : MonoBehaviour
    {
        public Inventory Inventory;

        private GameObject MessagePanel;

        private void Awake()
        {
            MessagePanel = transform.Find("MessagePanel").gameObject;
        }

        private void Start()
        {
            Inventory.ItemAdded += InventoryScript_ItemAdded;
            Inventory.ItemRemoved += Inventory_ItemRemoved;
        }

        private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
        {
            Transform inventoryPanel = transform.Find("InventoryPanel");
            int index = -1;
            foreach(Transform slot in inventoryPanel)
            {
                index++;

                Transform imageTransform = slot.GetChild(0).GetChild(0);
                Transform textTransform = slot.GetChild(0).GetChild(1);

                Image image = imageTransform.GetComponent<Image>();
                Text txtCount = textTransform.GetComponent<Text>();

                ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

                if(index == e.Item.Slot.Id)
                {
                    image.enabled = true;
                    image.sprite = e.Item.Image;

                    int itemCount = e.Item.Slot.Count;
                    if (itemCount > 1)
                        txtCount.text = itemCount.ToString();
                    else
                        txtCount.text = "";

                    OpenMessagePanel(e.Item.Name);

                    Invoke("CloseMessagePanel", 1);

                    //Store a reference to the item
                    itemDragHandler.Item = e.Item;

                    break;
                }
            }
        }

        private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
        {
            Transform inventoryPanel = transform.Find("InventoryPanel");

            int index = -1;
            foreach (Transform slot in inventoryPanel)
            {
                index++;

                Transform imageTransform = slot.GetChild(0).GetChild(0);
                Transform textTransform = slot.GetChild(0).GetChild(1);

                Image image = imageTransform.GetComponent<Image>();
                Text txtCount = textTransform.GetComponent<Text>();

                ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

                //We found the item in the UI
                if (itemDragHandler.Item == null)
                    continue;

                //Found the slot to remove fom
                if(e.Item.Slot.Id == index)
                {
                    int itemCount = e.Item.Slot.Count;
                    itemDragHandler.Item = e.Item.Slot.FirsItem;

                    if (itemCount < 2)
                    {
                        txtCount.text = "";
                    }
                    else
                    {
                        txtCount.text = itemCount.ToString();
                    }

                    if (itemCount == 0)
                    {
                        image.enabled = false;
                        image.sprite = null;
                    }
                    break;
                }
            }
        }

        public void OpenMessagePanel(string text)
        {
            MessagePanel.SetActive(true);

            MessagePanel.GetComponentInChildren<Text>().text = text;
        }

        public void CloseMessagePanel()
        {
            MessagePanel.SetActive(false);
        }
    }
}

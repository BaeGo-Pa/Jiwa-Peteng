using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiwa.Peteng
{
    public class ItemDatabase : MonoBehaviour
    {
        public List<Item> items = new List<Item>();

        private void Awake()
        {
            BuildDatabase();
        }

        public Item GetItem(int id)
        {
            return items?.Find(item => item.Id == id);
        }

        public Item GetItem(string name)
        {
            return items?.Find(item => item.Name == name);
        }

        void BuildDatabase()
        {
            items = new List<Item>()
            {
                new Item(0, "Health Potion", Item.ItemType.CONSUMABLE, "A potion to increase Health",
                new Dictionary<string, int>
                {
                    {"Health added", 35 },
                }),
                new Item(1, "Attack Potion", Item.ItemType.CONSUMABLE, "A potion to increase dealt damage", null),
                new Item(2, "Defense Potion", Item.ItemType.CONSUMABLE, "A Potion to increase your armor",
                new Dictionary<string, int>
                {
                    {"Armor added", 15 },
                }),
                new Item(3, "Sword", Item.ItemType.WEAPON, "A sword nothing more",
                new Dictionary<string, int>
                {
                    {"Power", 15 },
                    {"Defense", 4 }
                }),
                new Item(4, "Pick", Item.ItemType.WEAPON, "A useful tool",
                new Dictionary<string, int>
                {
                    {"Power", 9 },
                    {"Defense", 1 }
                })
            };
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiwa.Peteng
{
    public class Item
    {
        public int Id;
        public string Name;
        public enum ItemType
        {
            WEAPON,
            CONSUMABLE
        }
        public ItemType Type;
        public string Description;
        public Sprite Icon;
        public Dictionary<string, int> Stats = new Dictionary<string, int>();

        public Item(int id, string name, ItemType type, string description,
                    Dictionary<string, int> stats)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Description = description;
            this.Icon = Resources.Load<Sprite>("Sprites/Items/" + name);
            this.Stats = stats;
        }

        public Item(Item item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.Type = item.Type;
            this.Description = item.Description;
            this.Icon = Resources.Load<Sprite>("Sprites/Items/" + item.Name);
            this.Stats = item.Stats;
        }
    }
}

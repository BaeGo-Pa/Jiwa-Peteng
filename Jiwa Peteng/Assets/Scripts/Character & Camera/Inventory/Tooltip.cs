using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Jiwa.Peteng
{
    public class Tooltip : MonoBehaviour
    {
        private Text tooltipText;

        void Start()
        {
            tooltipText = GetComponentInChildren<Text>();
            this.gameObject.SetActive(false);
        }

        public void GenerateTooltip(Item item)
        {
            string statText = "";
            if(item.Stats.Count > 0)
            {
                foreach(var stat in item.Stats)
                {
                    statText += stat.Key.ToString() + ": " + stat.Value.ToString() + "\n";
                }
            }
            string tooltip = string.Format("<b>{0}</b>\n{1}\n\n<b>{2}</b>",
                                    item.Name, item.Description, statText);
            tooltipText.text = tooltip;
            gameObject.SetActive(true);
        }
    }
}

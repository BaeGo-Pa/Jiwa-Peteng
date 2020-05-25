using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

using System;
using TMPro;

namespace Jiwa.Peteng
{
    public class PlayerUI : MonoBehaviourPunCallbacks
    {
        #region Public Fields

        [Tooltip("UI Text to display Player's Name")]
        private TextMeshProUGUI playerNameText;


        [Tooltip("UI Slider to display Player's Health")]
        private Slider playerHealthSlider;

        private Text HealthText;

        [Tooltip("UI Slider to display Player's Armor")]
        private Slider playerArmorSlider;
        private Text ArmorText;

        [SerializeField]
        private GameObject player;

        private PlayerManager target;

        private GameObject DeathPanel;

        private GameObject WinPanel;

        private GameObject PauseMenu;

        #endregion

        #region MonoBehaviour Callbacks

        void Awake()
        {
            target = player.GetComponent<PlayerManager>();

            playerHealthSlider = transform.Find("Health Slider").GetComponent<Slider>();
            playerArmorSlider = transform.Find("Armor Slider").GetComponent<Slider>();
            playerNameText = playerHealthSlider.transform.Find("Player Name Text").GetComponent<TextMeshProUGUI>();
            HealthText = playerHealthSlider.transform.Find("HealthText").GetComponent<Text>();
            ArmorText = playerArmorSlider.transform.Find("ArmorText").GetComponent<Text>();

            playerHealthSlider.minValue = 0;
            playerArmorSlider.minValue = 0;

            DeathPanel = transform.Find("DeathPanel").gameObject;
            WinPanel = transform.Find("WinPanel").gameObject;
            PauseMenu = transform.Find("PauseMenu").gameObject;

            if (!PhotonNetwork.IsConnected || player.GetPhotonView().Owner.NickName == PhotonNetwork.LocalPlayer.NickName)
                SetTarget();
            else
                this.gameObject.SetActive(false);
        }

        void Update()
        {
            if(playerHealthSlider != null)
            {
                playerHealthSlider.value = (float) (target.Health) /(float) (target.maxHealth) * 100;
                HealthText.text = playerHealthSlider.value + "%";
            }
            if (playerArmorSlider != null)
            {
                playerArmorSlider.value = (float) (target.Armor) / (float)(target.maxArmor) * 100;
                ArmorText.text = playerArmorSlider.value + "%";
            }
            if (!target.Alive)
            {
                PauseMenu.SetActive(false);
                DeathPanel.SetActive(true);
            }
            else
            {
                DeathPanel.SetActive(false);
            }
            if (target.win)
            {
                PauseMenu.SetActive(false);
                WinPanel.SetActive(true);
            }
            else
            {
                WinPanel.SetActive(false);
            }
        }


        #endregion


        #region Public Methods

        public void SetTarget()
        {
            if(playerNameText != null)
            {
                if (!PhotonNetwork.IsConnected || player.GetPhotonView().Owner.NickName == null)
                {
                    playerNameText.text = "Lucio";
                }
                else
                {
                    if (player.GetPhotonView().Owner.NickName != null)
                    {
                        playerNameText.text = player.GetPhotonView().Owner.NickName;
                    }
                }
            }
        }


        #endregion


    }
}
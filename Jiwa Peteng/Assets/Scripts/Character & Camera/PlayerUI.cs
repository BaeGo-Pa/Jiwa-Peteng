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
        [SerializeField]
        private TextMeshProUGUI playerNameText;


        [Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;

        [Tooltip("UI Slider to display Player's Armor")]
        [SerializeField]
        private Slider playerArmorSlider;

        [SerializeField]
        private GameObject player;

        private PlayerManager target;

        #endregion

        #region MonoBehaviour Callbacks

        void Awake()
        {
            target = player.GetComponent<PlayerManager>();
            if (!PhotonNetwork.IsConnected || player.GetPhotonView().Owner.NickName == PhotonNetwork.LocalPlayer.NickName)
                SetTarget();
            else
                this.gameObject.SetActive(false);
        }

        void Update()
        {
            if(playerHealthSlider != null)
            {
                playerHealthSlider.value = target.Health;
            }
            if (playerArmorSlider != null)
                playerArmorSlider.value = target.Armor;
            Debug.Log("Health: " + target.Health);
            Debug.Log("Armor: " + target.Armor);
            Debug.Log("Attack: " + target.AttackDamage);
        }


        #endregion


        #region Public Methods

        public void SetTarget()
        {
            if(playerNameText != null)
            {
                if (!PhotonNetwork.IsConnected || player.GetPhotonView().Owner.NickName == null)
                {
                    playerNameText.text = "Player " + UnityEngine.Random.Range(0, 1000);
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
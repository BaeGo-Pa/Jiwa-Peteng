using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

using System;

namespace Jiwa.Peteng
{
    public class PlayerUI : MonoBehaviourPunCallbacks
    {
        #region Public Fields

        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        private Text playerNameText;


        [Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;

        [SerializeField]
        private GameObject player;

        private PlayerManager target;

        #endregion

        #region MonoBehaviour Callbacks

        void Awake()
        {
            target = player.GetComponent<PlayerManager>();
            if (player.GetPhotonView().Owner.NickName == PhotonNetwork.LocalPlayer.NickName)
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
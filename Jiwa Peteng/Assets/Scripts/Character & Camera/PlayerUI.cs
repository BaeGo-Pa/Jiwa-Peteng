using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;

using System;

namespace Jiwa.Peteng
{
    public class PlayerUI : MonoBehaviour
    {
        #region Public Fields

        [Tooltip("UI Text to display Player's Name")]
        [SerializeField]
        private Text playerNameText;


        [Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;

        [SerializeField]
        private GameObject _target;


        private PlayerManager target;

        #endregion

        #region Public Fields

        #endregion


        #region MonoBehaviour Callbacks

        void Awake()
        {
            target = _target.GetComponent<PlayerManager>();
            SetTarget();
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
                if (!PhotonNetwork.IsConnected || PhotonNetwork.LocalPlayer.NickName == null)
                {
                    playerNameText.text = "Player " + UnityEngine.Random.Range(0, 1000);
                }
                else
                {
                    if (PhotonNetwork.LocalPlayer.NickName != null)
                    {
                        playerNameText.text = PhotonNetwork.LocalPlayer.NickName;
                    }
                }
            }
        }


        #endregion


    }
}
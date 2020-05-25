using Photon.Pun;
using System.IO;
using UnityEngine;

namespace Jiwa.Peteng
{
    public class GameManager : MonoBehaviour
    {
        #region Public Fields

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;

        public static GameManager Instance;

        public bool gameIsFinished = false;

        public static bool playersWin = false;

        GameObject audioSource;

        private GameObject[] monsters
        {
            get { return GameObject.FindGameObjectsWithTag("Monster"); }
        }

        private GameObject[] players
        {
            get { return GameObject.FindGameObjectsWithTag("Player"); }
        }

        private int alivePlayers
        {
            get
            {
                int n = 0;
                foreach (GameObject player in players)
                {
                    if (player.GetComponent<PlayerManager>().Alive)
                        n++;
                }
                return n;
            }
        }


        void Start()
        {
            Instance = this;

            if (playerPrefab == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
            }
            else
            {
                if (PlayerManager.LocalPlayerInstance == null)
                {
                    Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);
                    // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
                    Debug.Log(PhotonNetwork.LocalPlayer.NickName);
                    PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
                    audioSource = GameObject.FindGameObjectWithTag("Music");
                    audioSource.GetComponent<MusicClass>().PlayMusic();
                }
                else
                {
                    Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
                }
            }

        }


        private void LateUpdate()
        {
            if (players.Length != 0 && monsters.Length == 0)
            {
                foreach (GameObject player in players)
                {
                    player.GetComponent<PlayerManager>().win = true;
                }
            }

            foreach (GameObject player in players)
            {
                if (!player.GetComponent<PlayerManager>().Alive && alivePlayers > 0)
                {
                    player.GetComponent<PlayerManager>().Invoke("Resurrect", 3f);
                }
            }

        }
        #endregion


        #region Public Methods

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion
        

        #region Photon Callbacks


        #endregion




    }
}

using UnityEngine;
using Photon.Pun;


namespace Jiwa.Peteng
{
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {

        #region Private Fields

        [Tooltip("The current Health of our player")]
        public float Health = 100f;


        [Tooltip("The current Health of our player")]
        public float Armor = 100f;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        public bool Alive = true;

        public float AttackDamage;

        #endregion

        #region IPunObservable implementation


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if(stream.IsWriting)
            {
                stream.SendNext(Health);
                stream.SendNext(Armor);
            }
            else
            {
                this.Health = (float)stream.ReceiveNext();
                this.Armor = (float)stream.ReceiveNext();
            }
        }


        #endregion

        #region MonoBehaviourCallBacks
       
        void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine || !PhotonNetwork.IsConnected)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
                transform.Find("Robot2").transform.Find("Player Cam").gameObject.SetActive(true);
                GetComponent<CameraWork>().enabled = true;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();


            if (_cameraWork != null)
            {
                if (photonView.IsMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }
            else
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }
        }

        void Update()
        {
            if (Health <= 0)
                Alive = false;
        }

        void OnTriggerEnter(Collider other)
        {
            if (photonView.IsMine && other.tag.Contains("Attack"))
            {
                Health -= 0.1f * Time.deltaTime;
            }
        }

        public void Heal()
        {
            if (Health + 35 >= 100f)
                Health = 100f;
            else
                Health = Health + 35;
        }

        public void BoostAttack()
        {
            AttackDamage += AttackDamage * 0.2f;
        }

        public void GiveArmor()
        {
            if (Armor + 15 >= 100f)
                Armor = 100f;
            else
                Armor = Armor + 15;
        }
        #endregion
    }
}
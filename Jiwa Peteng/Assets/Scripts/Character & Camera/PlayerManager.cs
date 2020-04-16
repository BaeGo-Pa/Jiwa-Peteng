using UnityEngine;
using Photon.Pun;


namespace Jiwa.Peteng
{
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {

        #region Private Fields

        [Tooltip("The current Health of our player")]
        public float Health = 100f;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;


        public bool Alive = true;
        #endregion

        #region IPunObservable implementation


        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if(stream.IsWriting)
            {
                stream.SendNext(Health);
            }
            else
            {
                this.Health = (float)stream.ReceiveNext();
            }
        }


        #endregion

        #region MonoBehaviourCallBacks
       
        void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            if (photonView.IsMine)
            {
                PlayerManager.LocalPlayerInstance = this.gameObject;
            }
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);
        }

        void Start()
        {
            if (photonView.IsMine)
            {
                // initialize whatever you need to for the LOCAL player
                enabled = true;
            }
            else
            {
                // destroy or disable things for REMOTE players
                enabled = false;
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
        #endregion
    }
}
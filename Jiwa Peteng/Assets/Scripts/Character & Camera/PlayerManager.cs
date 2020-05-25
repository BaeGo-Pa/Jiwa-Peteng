using UnityEngine;
using Photon.Pun;


namespace Jiwa.Peteng
{
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {

        #region Private Fields
        public int maxHealth = 100;

        [Tooltip("The current Health of our player")]
        public int Health;

        public int maxArmor = 100;

        [Tooltip("The current Health of our player")]
        public int Armor;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;

        private PlayerAnimatorManager playerAnimatorManager;

        private PlayerController playerController;

        public bool win;

        public bool Alive
        {
            get { return !(Health <= 0); }
        }

        public int AttackDamage = 2;

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
                this.Health = (int)stream.ReceiveNext();
                this.Armor = (int)stream.ReceiveNext();
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
                playerController = GetComponent<PlayerController>();
                playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
                Health = maxHealth;
                Armor = maxArmor;
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
            if (playerController.mCurrentItem != null)
                AttackDamage =  playerController.mCurrentItem.Damage;
            else
                AttackDamage = 2;
            if (Health < 0)
                Health = 0;
            if (Health > maxHealth)
                Health = maxHealth;
            if (Armor < 0)
                Armor = 0;
            if (Armor > maxArmor)
                Armor = maxArmor;
        }

        public void TakeDamage(int amount)
        {
            playerAnimatorManager.Hurt();
            if (Armor > amount)
                Armor -= amount;
            else
            {
                amount -= Armor;
                if (Armor > 0)
                    Armor = 0;
                if (amount > 0)
                    Health -= amount;
            }
            if (Health < 0)
                Health = 0;
            if (!Alive)
            {
                //Set trigger to animate the death animation
                playerAnimatorManager.Die();
            }
        }

        public void Resurrect()
        {
            Health = 1;
        }


        #region Item Effects
        public void Heal()
        {
            if (Health + 25 >= 100f)
                Health = 100;
            else
                Health = Health + 25;
        }

        public void BoostAttack()
        {
            AttackDamage += (int) (AttackDamage * 0.5f);
        }

        public void GiveArmor()
        {
            if (Armor + 30 >= 100f)
                Armor = 100;
            else
                Armor = Armor + 30;
        }
        #endregion

        #endregion
    }
}
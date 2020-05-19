using UnityEngine;
using Photon.Pun;
using System;

namespace Jiwa.Peteng
{
    public class PlayerController : MonoBehaviourPunCallbacks
    {

        CharacterController cc;

        float gravity;

        [SerializeField]
        private float moveSpeed = 3f;

        Inventory inventory;

        public GameObject Hand;

        public InventoryItemBase mCurrentItem = null;

        private bool mLockPickup = false;

        private HUD Hud;

        private InventoryItemBase mItemToPickup = null;

        PlayerManager playerManager;

        private ParticleSystem healparticles;

        private ParticleSystem attackParticles;

        private ParticleSystem defenseParticles;

        void Start()
        {
            cc = GetComponent<CharacterController>();
            playerManager = GetComponent<PlayerManager>();

            healparticles = transform.Find("Robot2").Find("Heal Particles").GetComponent<ParticleSystem>();

            attackParticles = transform.Find("Robot2").Find("Attack Particles").GetComponent<ParticleSystem>();

            defenseParticles = transform.Find("Robot2").Find("Defense Particles").GetComponent<ParticleSystem>();

            Hud = transform.Find("Robot2").Find("Player Cam").Find("Player UI").gameObject.GetComponent<HUD>();
            inventory = transform.Find("Robot2").Find("Player Cam").Find("Inventory").gameObject.GetComponent<Inventory>();
            
            inventory.ItemUsed += Inventory_ItemUsed;
            inventory.ItemRemoved += Inventory_ItemRemoved;
        }


        private void FixedUpdate()
        {
            if (playerManager.Alive && mCurrentItem != null && Input.GetKeyDown(KeyCode.Q))
                DropCurrentItem();
        }

        void Update()
        {
            if (playerManager.Alive && !PhotonNetwork.IsConnected || photonView.IsMine)
                MovePlayer();
            else
            {
                this.enabled = false;
                GetComponent<PlayerAnimatorManager>().enabled = false;
            }
             
            //Pickup item
            if(playerManager.Alive && Input.GetKeyDown(KeyCode.E) && mItemToPickup != null)
            {
                inventory.AddItem(mItemToPickup);
                mItemToPickup.OnPickup();
                mItemToPickup = null;
            }

            //Attack
            if(playerManager.Alive && Input.GetMouseButtonDown(0))
            {
                //TODO: trigger animation for attack depending on mCurrentItem
            }


        }
        #region Inventory
        private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
        {
            InventoryItemBase item = e.Item;

            GameObject goItem = (item as MonoBehaviour).gameObject;
            goItem.SetActive(true);

            goItem.transform.parent = null;
        }

        private void SetItemActive(InventoryItemBase item, bool active)
        {
            GameObject currentItem = (item as MonoBehaviour).gameObject;
            currentItem.SetActive(active);
            currentItem.transform.parent = active ? Hand.transform : null;
        }

        private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
        {
            InventoryItemBase item = e.Item;

            e.Item.Owner = this.gameObject;

            //Use item

            switch (item.Name)
            {
                case "Health Potion":
                    if (playerManager.Health < 100)
                    {
                        playerManager.Heal();
                        inventory.RemoveItem(item);
                        Destroy((item as MonoBehaviour).gameObject);
                        healparticles.Play();
                    }
                    break;
                case "Attack Potion":
                    playerManager.BoostAttack();
                    inventory.RemoveItem(item);
                    Destroy((item as MonoBehaviour).gameObject);
                    attackParticles.Play();
                    break;
                case "Defense Potion":
                    if (playerManager.Armor < 100)
                    {
                        playerManager.GiveArmor();
                        inventory.RemoveItem(item);
                        Destroy((item as MonoBehaviour).gameObject);
                        defenseParticles.Play();
                    }
                    break;
                default:
                    if (mCurrentItem != null)
                        SetItemActive(mCurrentItem, false);
                    SetItemActive(item, true);
                    mCurrentItem = e.Item;
                    break;
            }
        }

        private void DropCurrentItem()
        {
            mLockPickup = true;
            GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;

            inventory.RemoveItem(mCurrentItem);

            //Throw animation
            Rigidbody rbItem = goItem.AddComponent<Rigidbody>();
            rbItem.AddForce(transform.forward * 2.0f, ForceMode.Impulse);

            Invoke("DoDropItem", 0.25f);
        }

        public void DoDropItem()
        {
            mLockPickup = false;

            //Remove Rigidbody
            Destroy((mCurrentItem as MonoBehaviour).GetComponent<Rigidbody>());

            mCurrentItem = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            InventoryItemBase item = other.GetComponent<InventoryItemBase>();
            if (item != null)
            {
                if (mLockPickup)
                    return;

                mItemToPickup = item;
                Hud.OpenMessagePanel(item.InteractText);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            InventoryItemBase item = other.GetComponent<InventoryItemBase>();
            if (item != null)
            {
                Hud.CloseMessagePanel();
                mItemToPickup = null;
            }
        }
        #endregion

        private void MovePlayer()
        {
            AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("Jump"))
            {
                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");

                Vector3 direction = new Vector3(x, 0, z).normalized;
                Vector3 velocity = direction * moveSpeed * Time.deltaTime;
                direction = transform.Find("Robot2").transform.Find("Player Cam").TransformDirection(direction);
                direction.y = 0f;

                if (cc.isGrounded)
                {
                    gravity = 0;
                }
                else
                {
                    gravity += 0.25f;
                    gravity = Mathf.Clamp(gravity, 1f, 20f);
                }

                Vector3 gravityVector = -Vector3.up * gravity * Time.deltaTime;

                cc.Move(velocity + gravityVector);

                if (velocity.magnitude > 0)
                {
                    float yAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    transform.localEulerAngles = new Vector3(0, yAngle, 0);
                }
            }
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace Jiwa.Peteng
{
    public class FightEnemy : MonoBehaviour
    {
        public int hp;

        public int damage;

        public int cooldown;

        public int wait;

        private void Start()
        {
            if (hp == 0)
            {
                hp = 50;
            }

            if (damage == 0)
            {
                damage = 3;
            }

            if (cooldown == 0)
            {
                cooldown = 10;
            }
        }


        public bool CanAttack
        {
            get {
                if (wait <= 0)
                {
                    wait = cooldown;
                    return true;
                }
                else
                    return false; }
        }

        public bool isDead
        {
            get { return hp <= 0; }
        }

        private void OnCollisionEnter(Collision collision)
        {
            InteractableItemBase item = collision.gameObject.GetComponent<InteractableItemBase>();
            bool haveOwner = item?.Owner != null;
            PlayerManager player = null;
            if (haveOwner)
                player = item.Owner.GetComponent<PlayerManager>();
            else
            {
                player = collision.gameObject.GetComponent<PlayerManager>();
                if (player != null)
                    hp -= 5;
            }
            //Hit by player's weapon
            if (item != null && item.itemType == ItemType.Weapon && player != null || collision.gameObject.GetComponent<PlayerManager>())
            {
                hp -= player.AttackDamage;
            }
            if (isDead)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
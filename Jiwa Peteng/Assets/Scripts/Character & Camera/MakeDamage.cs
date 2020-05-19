using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiwa.Peteng
{
    public class MakeDamage : MonoBehaviour
    {
        private bool _isCausingDamage = false;

        public float DamageRepeatRate = 0.1f;

        public int DamageAmount = 1;

        public bool Repeating = true;

        public int Health = 100;

        public bool isDead
        {
            get { return Health <= 0; }
        }

        private void Update()
        {
            if (isDead)
                return;
        }

        private void OnTriggerEnter(Collider other)
        {
            _isCausingDamage = true;

            PlayerManager player = other.gameObject.GetComponent<PlayerManager>();

            if (player != null)
            {
                if (Repeating)
                {
                    //Repeating damage
                    StartCoroutine(TakeDamage(player, DamageRepeatRate));
                }
                else
                    player.TakeDamage(DamageAmount);
            }
        }

        IEnumerator TakeDamage(PlayerManager player, float repeatRate)
        {
            while (_isCausingDamage)
            {
                player.TakeDamage(DamageAmount);
                TakeDamage(player, repeatRate);

                if (!player.Alive)
                    _isCausingDamage = false;

                yield return new WaitForSeconds(repeatRate);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            PlayerManager player = other.gameObject.GetComponent<PlayerManager>();
            if (player != null)
                _isCausingDamage = false;
        }


        //Take damage from player
        private void OnCollisionEnter(Collision collision)
        {
            InteractableItemBase item = collision.collider.gameObject.GetComponent<InteractableItemBase>();
            bool haveOwner = item.Owner != null;
            PlayerManager player = null;
            if (haveOwner)
                item.Owner.GetComponent<PlayerManager>();
            //Hit by player's weapon
            if (item != null && item.itemType == ItemType.Weapon && player != null)
            {
                Health -= player.AttackDamage;
                if (isDead)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
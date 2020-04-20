﻿using UnityEngine;
using Photon.Pun;

namespace Jiwa.Peteng
{
    public class PlayerAnimatorManager : MonoBehaviourPun
    {
        #region Private Fields

        private Animator animator;

        #endregion

        #region MonoBehaviour CallBacks

        // Use this for initialization
        void Start()
        {
            animator = GetComponent<Animator>();
            if (!animator)
            {
                Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine && animator)
                Animate();
            else
                this.enabled = false;
        }

        private void Animate()
        {
            // deal with Jumping
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            // only allow jumping if we are running.
            if (!stateInfo.IsName("Jump"))
            {
                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");

                animator.SetFloat("Speed", x * x + z * z);

                if (stateInfo.IsName("Base Layer.Run"))
                {
                    // When using trigger parameter
                    if (GetComponent<CharacterController>().isGrounded && Input.GetKeyDown(KeyCode.Space))
                    {
                        animator.SetTrigger("Jump");
                    }
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        animator.SetFloat("Speed", 2 * (x * x + z * z));
                    }
                }
            }
        }

        #endregion
    }
}
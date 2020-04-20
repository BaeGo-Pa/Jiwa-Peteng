using UnityEngine;

using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{

    CharacterController cc;

    float gravity;

    [SerializeField]
    private float moveSpeed = 3f;


    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (photonView.IsMine)
            MovePlayer();
        else
            this.enabled = false;
    }

    private void MovePlayer()
    {
        AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("Jump"))
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(x, 0, z).normalized;
            Vector3 velocity = direction * moveSpeed * Time.deltaTime;

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

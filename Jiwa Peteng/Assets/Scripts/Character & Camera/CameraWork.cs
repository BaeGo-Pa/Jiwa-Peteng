using UnityEngine;

using Photon.Pun;


namespace Jiwa.Peteng
{
    /// <summary>
    /// Camera work. Follow a target
    /// </summary>
    public class CameraWork : MonoBehaviour
    {
        #region Private Fields


        [Tooltip("The distance in the local x-z plane to the target")]
        [SerializeField]
        private float distance = 4.0f;


        [Tooltip("The height we want the camera to be above the target")]
        [SerializeField]
        private float height = 2.0f;


        [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
        [SerializeField]
        private bool followOnStart = false;


        [Tooltip("The Smoothing for the camera to follow the target")]
        [SerializeField]
        private float smoothSpeed = 1f;

        public Transform Target;


        float mouseX, mouseY;

        // cached transform of the target
        Transform cameraTransform;


        // maintain a flag internally to reconnect if target is lost or camera is switched
        bool isFollowing;


        // Cache for camera offset
        Vector3 cameraOffset = Vector3.zero;


        #endregion


        #region MonoBehaviour Callbacks


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase
        /// </summary>
        void Start()
        {
            // Start following the target if wanted.
            if (followOnStart)
            {
                OnStartFollowing();
            }
        }


        void Update()
        {
            if(PauseMenu.GameIsPaused)
            {
                isFollowing = false;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                isFollowing = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            // The transform target may not destroy on level load,
            // so we need to cover corner cases where the Main Camera is different everytime we load a new scene, and reconnect when that happens
            if (cameraTransform == null && isFollowing)
            {
                OnStartFollowing();
            }


            // only follow is explicitly declared
            if (isFollowing)
            {
                Follow();
            }
        }


        #endregion


        #region Public Methods


        /// <summary>
        /// Raises the start following event.
        /// Use this when you don't know at the time of editing what to follow, typically instances managed by the photon network.
        /// </summary>
        public void OnStartFollowing()
        {
            cameraTransform = transform.Find("Robot2").Find("Player Cam");
            isFollowing = true;
            // we don't smooth anything, we go straight to the right camera shot
            Cut();
        }


        #endregion


        #region Private Methods


        /// <summary>
        /// Follow the target smoothly
        /// </summary>
        void Follow()
        {
            cameraOffset.z = -distance;
            cameraOffset.y = height;
            mouseX += Input.GetAxis("Mouse X") * smoothSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * smoothSpeed;
            mouseY = Mathf.Clamp(mouseY, -35, 60);


            transform.LookAt(Target);

            Target.rotation = Quaternion.Euler(0, mouseX, 0);
        }


        void Cut()
        {
            cameraOffset.z = -distance;
            cameraOffset.y = height;


            cameraTransform.position = this.transform.position + this.transform.TransformVector(cameraOffset);


        }
        #endregion
    }
}
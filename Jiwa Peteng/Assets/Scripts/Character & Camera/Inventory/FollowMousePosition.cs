using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jiwa.Peteng
{
    public class FollowMousePosition : MonoBehaviour
    {
        void LateUpdate()
        {
            transform.position = Input.mousePosition;
        }
    }
}

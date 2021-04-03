using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamSim.Camera
{
    public class Basic_Camera : MonoBehaviour
    {
        #region Variables
        public Transform target;
        public float distance = 5f;
        public float height = 2f;
        public float smoothSpeed = 0.5f;

        private Vector3 smoothVelocity;
        protected float initHeight;
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {
            initHeight = height;
        }

        // Update is called once per frame
        void Update()
        {
            if (target)
            {
                CameraController();
            }
        }
        #endregion

        #region User Methods
        protected virtual void CameraController()
        {
            Vector3 aircraftPos = target.position + (-target.forward * distance) + (Vector3.up * height);
            Debug.DrawLine(target.position, aircraftPos, Color.blue);
            transform.position = Vector3.SmoothDamp(transform.position, aircraftPos, ref smoothVelocity, smoothSpeed);
            transform.LookAt(target);
        }
        #endregion
    }
}
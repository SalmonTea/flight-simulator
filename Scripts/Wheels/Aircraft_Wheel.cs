using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamSim.Scripts
{
    [RequireComponent(typeof(WheelCollider))]
    public class Aircraft_Wheel : MonoBehaviour
    {
        #region Variables
        public WheelCollider wheelCollide;
        #endregion

        #region Builtin Methods
        void Start()
        {
            wheelCollide = GetComponent<WheelCollider>();
        }
        #endregion

        #region User-Made Methods
        public void initWheel()
        {
            if (wheelCollide)
            {
                wheelCollide.motorTorque = 0.0000000000001f;
            }
        }
        #endregion
    }
}
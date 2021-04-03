using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamSim.Camera
{
    public class Airplane_Camera : Basic_Camera
    {
        #region Variables
        public float minHFG = 2f;
        #endregion


        protected override void CameraController()
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if(hit.distance < minHFG && hit.transform.tag == "Ground")
                {
                    float theHeightIAmLookingFor = initHeight + (minHFG - hit.distance);
                    height = theHeightIAmLookingFor;
                }
            }
            base.CameraController();
        }
    }
}
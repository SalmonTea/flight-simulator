using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamSim.Inputs;
using UnityEditor;

namespace SamSim.Camera
{
    public class Other_Camera : MonoBehaviour
    {
        #region Variables
        //private variables
        private int cameraThing = 0;

        //public methods
        [Header("Camera Controller Properties")]
        public KeyboardInput input;
        public List<Basic_Camera> sCamera = new List<Basic_Camera>();
        #endregion

        #region Builtin Methods
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (input)
            {
                if (input.getCameraSwitch)
                {
                    cameraControl();
                }
            }
        }
        #endregion

        #region User Methods
        protected virtual void cameraControl()
        {
            if(sCamera.Count > 0)
            {
                DisableCameras();

                cameraThing++;

                if(cameraThing >= sCamera.Count)
                {
                    cameraThing = 0;
                }

                sCamera[cameraThing].enabled = true;
            }
        }

        void DisableCameras()
        {
            if(sCamera.Count > 0)
            {
                foreach (Basic_Camera cam in sCamera)
                {
                    cam.enabled = false;
                }

                foreach (Airplane_Camera cam in sCamera)
                {
                    cam.enabled = false;
                }
            }
        }
        #endregion
    }
}
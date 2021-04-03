using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamSim.Inputs { 
    public class KeyboardInput : MonoBehaviour
    {
        #region InputVariables
        //Actual control variables
        protected float pitch = 0f;
        protected float roll = 0f;
        protected float yaw = 0f;

        //Throttle controls
        protected float throttle = 0f;
        protected float stickyThrottle;
        public float getStickyThrottle
        {
            get { return stickyThrottle; }
        }

        //Flaps and brakes
        protected int flaps = 0;
        protected float brake = 0f;
        public int maxFlaps = 2;

        //Keys
        public KeyCode brakeKey = KeyCode.Space;

        //Private variables
        [SerializeField]
        protected KeyCode cameraCont = KeyCode.C;
        protected bool switchForCam = false;

        #endregion

        #region Properties
        //Getter methods

        public float getBrake
        {
            get { return brake; }
        }
        public bool getCameraSwitch
        {
            get { return switchForCam; }
        }
        public float getFlaps
        {
            get { return flaps; }
        }
        public float getPitch {
            get { return pitch; }
        }
        public float getRoll
        {
            get { return roll; }
        }
        public float getThrottle
        {
            get { return throttle; }
        }
        public float getYaw
        {
            get { return yaw; }
        }
        #endregion

        #region Code
        // Start is called before the first frame update
        void Start()
        {
         
        }

        // Update is called once per frame
        void Update()
        {
            thatRawInput();
        }
        #endregion

        #region Inputs And Shizz
        protected virtual void thatRawInput()
        {
            //Methods for actual input
            pitch = Input.GetAxis("Vertical");
            roll = Input.GetAxis("Horizontal");
            throttle = Input.GetAxis("Throttle");
            yaw = Input.GetAxis("Yaw");
            /*
            brake = 0f;
            if (Input.GetKey(KeyCode.Space))
            {
                brake = 1f;
            }
            */
            brake = Input.GetKey(brakeKey) ? 1f : 0f;

            //(WIP) Flap controller
            if (Input.GetKeyDown(KeyCode.G))
            {
                flaps += 1;
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                flaps -= 1;
            }
            flaps = Mathf.Clamp(flaps, 0, maxFlaps);

            //Debug.Log("Holy Shi- YOU GOT THOSE INPUTS WORKING !!!!");
            switchForCam = Input.GetKeyDown(cameraCont);
        }
        #endregion
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SamSim.Inputs
{
    public class XboxControllerInputs : KeyboardInput
    {
        #region Variables
        public float throttleSpeed = 0.5f;
        #endregion

        #region Custom Methods
        protected override void thatRawInput()
        {
            //Button mapping
            pitch = Input.GetAxis("X_LV_Stick");
            roll = Input.GetAxis("X_LH_Stick");
            yaw = Input.GetAxis("X_RH_Stick");
            throttle = Input.GetAxis("X_RV_Stick");
            brake = Input.GetAxis("Fire1");

            //Sticky throttle initiation for realistic throttling
            StickyThrottleControl();

            //Controls amount flaps should be set to
            if (Input.GetButtonDown("X_R_Bumper"))
            {
                flaps += 1;
            }
            if (Input.GetButtonDown("X_L_Bumper"))
            {
                flaps -= 1;
            }
            flaps = Mathf.Clamp(flaps, 0, maxFlaps);

            switchForCam = Input.GetButtonDown("X_Y_Button") || Input.GetKeyDown(cameraCont); 
        }

        /*Sticky throttle allows the throttle to act as a real throttle should.
         The throttle will gradually shift to full as opposed to going from 0 to 100
         in a button press. The plane will properly build up speed before it takes off*/
        void StickyThrottleControl()
        {
            stickyThrottle = stickyThrottle + (throttle * throttleSpeed * Time.deltaTime);
            stickyThrottle = Mathf.Clamp01(stickyThrottle);
        }
        #endregion
    }

}

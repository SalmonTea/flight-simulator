using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamSim.Controllers;
using SamSim.Inputs;

namespace SamSim.Scripts
{
    public class Aircraft_Engine : MonoBehaviour
    {
        #region Variables
        [Header("Force and Power")]
        public float maxforce = 200f;
        public float maxRPM = 7385f;
        public AnimationCurve powerCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
        #endregion

        #region User-Made Methods
        public Vector3 CalculateForce(float throttle)
        {
            float theThrottle, currentRPM, thePowerYourAircraftIsCurrentlyAt;

            //Calculating current throttle
            theThrottle = Mathf.Clamp01(throttle);
            theThrottle = powerCurve.Evaluate(theThrottle);

            //RPM calculations that will blow ya freakin mind
            currentRPM = theThrottle * maxRPM;

            //Checking the power of your aircraft because that's very important for the simulations
            thePowerYourAircraftIsCurrentlyAt = theThrottle * maxforce;
            Vector3 finalForce = transform.TransformDirection(transform.forward) * thePowerYourAircraftIsCurrentlyAt;

            //When all is said and done, you still have to return a value
            return finalForce;
        }
        #endregion
    }
}
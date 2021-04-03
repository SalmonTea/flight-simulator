using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamSim.Inputs;
using SamSim.Scripts;
using SamSim.Animations;

namespace SamSim.Controllers
{
    [RequireComponent(typeof(Aircraft_Stats))]
    public class Airplane_Control : Rigidbody_Control
    {
        #region Variables
        [Header("Base Airplane Properties")]
        public KeyboardInput input;
        public Aircraft_Stats stats;
        public Transform center;

        [Tooltip("Weight in lbs")]
        public float weight = 60000f;

        [Header("Animated Parts")]
        public List<Aircraft_Animation> mAnimation = new List<Aircraft_Animation>();

        [Header("Engines")]
        public List<Aircraft_Engine> engines = new List<Aircraft_Engine>();

        [Header("Wheels")]
        public List<Aircraft_Wheel> wheels = new List<Aircraft_Wheel>();
        #endregion

        #region Constants
        const float weightConv = 0.45359f;
        #endregion

        #region Builtin Methods
        public override void Start()
        {
            base.Start();

            float aircraftWeight = weight * weightConv;
            if(rigidBody)
            {
                rigidBody.mass = aircraftWeight;
                if (center)
                {
                    rigidBody.centerOfMass = center.localPosition;
                }

                stats = GetComponent<Aircraft_Stats>();
                if (stats)
                {
                    stats.startStats(rigidBody, input);
                }
            }

            if(wheels != null)
            {
                if(wheels.Count > 0)
                {
                    foreach(Aircraft_Wheel wheel in wheels)
                    {
                        wheel.initWheel();
                    }
                }
            }
        }
        #endregion

        #region Custom Methods
        protected override void HandlePhysics()
        {
            if (input)
            {
                HandleEngines();
                HandleStats();
               // HandleAerodynamics();
               // HandleSteering();
               // HandleBrakes();
               // HandleAltitude();
                AnimationActivation();
            }
        }
        void HandleEngines()
        {
            if (engines != null) 
            {
                if(engines.Count > 0) 
                { 
                    foreach(Aircraft_Engine engine in engines)
                    {
                        rigidBody.AddForce(engine.CalculateForce(input.getStickyThrottle));
                    }
                }
            }
        }
        void HandleStats()
        {
            if (stats)
            {
                stats.updateStats();
            }
        }
        /*void HandleAerodynamics()
        {

        }
        void HandleSteering()
        {

        }
        void HandleBrakes()
        {

        }
        void HandleAltitude()
        {

        }
        */
        void AnimationActivation()
        {
            if(mAnimation.Count > 0)
            {
                foreach(Aircraft_Animation animations in mAnimation)
                {
                    animations.HandleAnimation(input);
                }
            }
        }
        #endregion
    }
}
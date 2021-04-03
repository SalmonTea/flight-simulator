using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

namespace SamSim.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class Rigidbody_Control : MonoBehaviour
    {
        #region Variables
        protected Rigidbody rigidBody;
        protected AudioSource audioSource;
        #endregion

        #region Builtin Methods
        public virtual void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
            if (audioSource)
            {
                audioSource.playOnAwake = false;
            }
        }

        void FixedUpdate()
        {
            if (rigidBody)
            {
                HandlePhysics();
            }
        }
        #endregion

        #region Custom Methods
        protected virtual void HandlePhysics() { }
        #endregion
    }
}
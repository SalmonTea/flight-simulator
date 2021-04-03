using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamSim.Controllers;
using SamSim.Inputs;

namespace SamSim.Scripts
{
    public class Aircraft_Stats : MonoBehaviour
    {

        #region Variables
        [Header("Stats Properties")]
        public float maxMPH = 1473;
        public float rbLerpSpeed = 0.01f;

        [Header("Lift Properties")]
        public float maxLiftPower = 800f;
        public AnimationCurve liftCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        [Header("Drag Properties")]
        public float dragFactor = 0.01f;

        [Header("Control Properties")]
        public float pitchSpeed = 1000f;
        public float rollSpeed = 1000f;
        public float yawSpeed = 1000f;

        [Header("The Speed")]
        private float forwardSpeed;
        public float getForwardSpeed
        {
            get
            {
                return forwardSpeed;
            }
        }
        private float mph;
        public float getMph
        {
            get
            { 
                return mph; 
            }
        }

        [Header("Stats")]
        private KeyboardInput input;
        private Rigidbody mRB;
        private float startDrag;
        private float startAngleDrag;
        private float maxMPS;
        private float normalizeMPH;
        private float angleOfAttack;
        private float pitchAngle;
        private float rollAngle;
        #endregion

        #region Constants
        const float mpsToMph = 2.23694f; //This is "meters per second" to "miles per hour"
        #endregion

        #region User-Made Methods
        public void startStats(Rigidbody rigidbody, KeyboardInput curInput)
        {
            //initialize
            input = curInput;
            mRB = rigidbody;
            startDrag = mRB.drag;
            startAngleDrag = mRB.angularDrag;
            maxMPS = maxMPH / mpsToMph;
        }

        public void updateStats()
        {
            //Process stats
            if (mRB)
            {
                SpeedCalc();
                LiftCalc();
                DragCalc();
                PitchCalc();
                RollCalc();
                YawCalc();
                Banking();
                HandleRigidBodyTransform();
            }
        }

        //Calculations (Alphabetised for better understanding)
        void Banking()
        {
            float bankSide = Mathf.InverseLerp(-90f, 90f, rollAngle);
            float bankAmount = Mathf.Lerp(-1f, 1f, bankSide);
            Vector3 bankTorque = bankAmount * rollSpeed * transform.up;
            mRB.AddTorque(bankTorque);
        }
        void DragCalc()
        {
            float speedDrag = forwardSpeed * dragFactor;
            float finalDrag = startDrag + speedDrag;

            mRB.drag = finalDrag;
            mRB.angularDrag = startAngleDrag * forwardSpeed;
        }
        void HandleRigidBodyTransform()
        {
            if (mRB.velocity.magnitude > 1f)
            {
                Vector3 updatedVelocity = Vector3.Lerp(mRB.velocity, transform.forward * forwardSpeed, forwardSpeed * angleOfAttack * Time.deltaTime * rbLerpSpeed);
                mRB.velocity = updatedVelocity;

                Quaternion updatedRotation = Quaternion.Slerp(mRB.rotation, Quaternion.LookRotation(mRB.velocity.normalized, transform.up), Time.deltaTime * rbLerpSpeed);
                mRB.MoveRotation(updatedRotation);
            }
        }
        void LiftCalc()
        {
            angleOfAttack = Vector3.Dot(mRB.velocity.normalized, transform.forward);
            angleOfAttack *= angleOfAttack;

            Vector3 liftDir = transform.up;
            float liftPower = liftCurve.Evaluate(normalizeMPH) * maxLiftPower;

            Vector3 finalLiftForce = liftDir * liftPower;
            mRB.AddForce(finalLiftForce);
        }
        void PitchCalc()
        {
            Vector3 flatForward = transform.forward;
            flatForward.y = 0f;
            pitchAngle = Vector3.Angle(transform.forward, flatForward);

            Vector3 pitchTorque = input.getPitch * pitchSpeed * transform.right;
            mRB.AddTorque(pitchTorque);
        }
        void RollCalc()
        {
            Vector3 flatRight = transform.right;
            flatRight.y = 0f;
            flatRight = flatRight.normalized;
            rollAngle = Vector3.Angle(transform.right, flatRight);

            Vector3 rollTorque = input.getRoll * rollSpeed * transform.forward;
            mRB.AddTorque(rollTorque);
        }
        void SpeedCalc()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(mRB.velocity);
            forwardSpeed = Mathf.Max(0f, localVelocity.z);
            forwardSpeed = Mathf.Clamp(forwardSpeed, 0f, maxMPS);

            mph = forwardSpeed * mpsToMph;
            mph = Mathf.Clamp(mph, 0f, maxMPH);
            normalizeMPH = Mathf.InverseLerp(0f, maxMPH, mph);
        }
        void YawCalc()
        {
            Vector3 yawTorque = input.getYaw * yawSpeed * transform.up;
            mRB.AddTorque(yawTorque);
        }
        #endregion
    }
}
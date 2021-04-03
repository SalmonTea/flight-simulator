using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamSim.Inputs;
using SamSim.Controllers;
using SamSim.Scripts;

namespace SamSim.Animations
{
    public enum PartType
    {
        Aileron,
        Elevator,
        Flap,
        Rudder
    }

    public class Aircraft_Animation : MonoBehaviour
    {
        #region Private Variables
        private float angle;
        #endregion

        #region Public Variables
        [Header("Part Properties")]
        //Allows us to call part needed
        public PartType mPart = PartType.Aileron;

        //Max angle a part can move
        public float mAngle = 30f;

        //Exactly as the name implies. Allows the part chosen to move
        public Transform movinParts;

        //Chooses what axis (x,y,z) the part rotates along
        public Vector3 axisOfMovement = Vector3.right;

        //Allows speed of movement to be adjusted
        public float speedChange;
        #endregion

        #region Defaults
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (movinParts)
            {
                //Finalize what the angle of the part will be on the axis
                Vector3 endAxisAngle = axisOfMovement * angle;

                //Working on it
                movinParts.localRotation = Quaternion.Slerp(movinParts.localRotation, Quaternion.Euler(endAxisAngle), Time.deltaTime * speedChange);
            }
        }
        #endregion

        #region User Methods
        public void HandleAnimation(KeyboardInput input)
        {
            float val = 0f;
            switch (mPart)
            {
                case PartType.Aileron:
                    val = input.getRoll;
                    break;
                case PartType.Elevator:
                    val = input.getPitch;
                    break;
                case PartType.Flap:
                    val = input.getFlaps;
                    break;
                case PartType.Rudder:
                    val = input.getYaw;
                    break;
                default:
                    break;
            }

            angle = mAngle * val;
        }
        #endregion
    }
}
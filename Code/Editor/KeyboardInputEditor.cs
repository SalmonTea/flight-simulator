using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SamSim.Inputs
{
    [CustomEditor(typeof(KeyboardInput))]
    public class KeyboardInputEditor : Editor
    {
        #region #region Variables
        private KeyboardInput targetInput;
        #endregion
        #region Builtin Methods

        void OnEnable()
        {
            targetInput = (KeyboardInput)target;
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            string debugInfo = "";

            debugInfo += "Pitch = " + targetInput.getPitch + "\n";
            debugInfo += "Roll = " + targetInput.getRoll + "\n";
            debugInfo += "Yaw = " + targetInput.getYaw + "\n";
            debugInfo += "Throttle = " + targetInput.getThrottle + "\n";
            debugInfo += "Brake = " + targetInput.getBrake + "\n";
            debugInfo += "Flaps = " + targetInput.getFlaps + "\n";

            GUILayout.Space(20);
            EditorGUILayout.TextArea(debugInfo, GUILayout.Height(100));
            GUILayout.Space(20);

            Repaint();
        }
        
        #endregion
    }
}
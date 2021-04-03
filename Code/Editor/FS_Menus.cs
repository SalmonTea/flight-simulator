using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SamSim.Controllers;

namespace SamSim.Menus
{
    public class FS_Menus : Editor
    {
        [MenuItem("Airplane Tools/Create New Airplane")]
        public static void CreateNewAirplane()
        {
            GameObject curSelected = Selection.activeGameObject;
            if (curSelected)
            {
                Airplane_Control curController = curSelected.AddComponent<Airplane_Control>();
                GameObject curGrav = new GameObject("COG");
                curGrav.transform.SetParent(curSelected.transform);

                curController.center = curGrav.transform;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Level))]
public class LevelEditorScript : Editor
{
   

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Level level = (Level)target;
        if (GUILayout.Button("Add Car & FinishLine"))
        {
            Selection.activeObject = PrefabUtility.InstantiatePrefab(level.CarPrefab, level.transform);
            var tempCar = Selection.activeGameObject.GetComponent<Car>();
            //tempPrefab.transform.position = Vector3.zero;
            //tempPrefab.transform.rotation = Quaternion.identity;
            /*
            Car instantiatedCar = Instantiate(level.CarPrefab, level.transform);
            */

            Selection.activeObject = PrefabUtility.InstantiatePrefab(level.FinishLinePrefab, level.transform);
            var tempFinish = Selection.activeGameObject;
            //FinishLine instantiatedFinishLine = Instantiate(level.FinishLinePrefab, level.transform);
            //instantiatedFinishLine.neededCar = instantiatedCar;
            tempFinish.GetComponent<FinishLine>().neededCar = tempCar;
            level.AddCarToTheLevel(tempCar);
        }

        if (GUILayout.Button("Add Platform"))
        {
            Instantiate(level.Platform,level.transform);
        }
    }
}

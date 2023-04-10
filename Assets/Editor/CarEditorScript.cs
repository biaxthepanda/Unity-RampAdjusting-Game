using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Car))]
public class CarEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Car car = (Car)target;
        if (GUILayout.Button("StartEngine"))
        {
            car.StartCar();
        }
    }
}

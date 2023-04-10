using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class CarFinishPointInstantiator : MonoBehaviour
{

    bool finishLineInstantiated = false;
    [SerializeField] FinishLine _finishLine;
    [SerializeField] Car thisCar;

    private void Start()
    {
        if (!finishLineInstantiated)
        {
            finishLineInstantiated = true;
            var finish = Instantiate(_finishLine, transform.position, Quaternion.identity);
            Debug.Log("AGHAHAH");
            finish.neededCar = thisCar;
            finish.name = thisCar.name + " Finish Point";
        }
    }
}

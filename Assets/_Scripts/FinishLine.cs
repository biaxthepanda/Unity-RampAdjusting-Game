using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    public Car neededCar;
    [SerializeField] Collider2D _collider;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Car")
        {
            if (collision.transform.parent == neededCar.transform)
            {
                
                Debug.Log(LevelManager.Instance.CurrentLevel.transform.name);
                LevelManager.Instance.CurrentLevel.CarEnteredFinishLine(neededCar);
                Destroy(_collider);
            }
        }
        
    }
}

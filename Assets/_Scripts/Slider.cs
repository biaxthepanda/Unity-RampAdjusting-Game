using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{


    [SerializeField] Transform Blade, Side;
    [Range(-3.0f, 2.0f)] [SerializeField] float _timeOffset = 0;


    private void Update()
    {
        Blade.transform.localPosition = new Vector2(Blade.transform.localPosition.x, Side.transform.localScale.y/2*Mathf.Sin(Time.time + _timeOffset));
    }

}

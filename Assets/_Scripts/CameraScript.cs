using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(Screen.height > 1920)
        {
            Camera.main.orthographicSize = 20 * Screen.height / 1920;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

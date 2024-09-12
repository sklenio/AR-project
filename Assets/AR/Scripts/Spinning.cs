//this script spins the death pillars in the boss area
//primitive and effective


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float degreesPerSecond = 20;
    
    public void Update()
    {
        transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    } 
}


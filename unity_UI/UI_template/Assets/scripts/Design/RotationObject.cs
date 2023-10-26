using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
   
   public float rotationSpeed = 10f;

    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
            ObjectRotateAroundYAxis();
        }
        
    }
    void ObjectRotateAroundYAxis(){
        transform.Rotate(0.0f, 2.0f, 0.0f, Space.World);
    }
}

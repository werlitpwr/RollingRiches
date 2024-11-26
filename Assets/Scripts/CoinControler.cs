using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControler : MonoBehaviour
{
    public float rotateSpeed;

    void Start()
    {

    }
    private void FixedUpdate()
    {
       transform.Rotate(0, 0, rotateSpeed); 
    }

}

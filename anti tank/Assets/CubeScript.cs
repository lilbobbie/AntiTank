using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    bool isPressed = false;

    public float rotSpeed;

    public void pressed()
    {
        isPressed = true;
    }
    public void released()
    {
        isPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        }
    }
}

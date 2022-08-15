using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public int playerSpeed;


    private void Update()
    {
        // move without pressing anything
        // transform.position = transform.position + Camera.main.transform.forward * playerSpeed * Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            transform.position = transform.position + Camera.main.transform.forward * playerSpeed * Time.deltaTime;
        }
    }
}

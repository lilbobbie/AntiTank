using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    public int playerSpeed;

    public bool isLookingAtEnemy;
    public GameObject enemy;

    private void Update()
    {
        // move without pressing anything
        if (!GetComponent<VRGaze>().gvrStatus)
        {
            Walk();
        }
        if (GetComponent<VRGaze>().gvrStatus)
        {
            StopWalk();
        }
        // transform.position = transform.position + Camera.main.transform.forward * playerSpeed * Time.deltaTime;

        /**if (Input.GetButton("Fire1"))
        {
            transform.position = transform.position + Camera.main.transform.forward * playerSpeed * Time.deltaTime;
        }**/


    }
    private void Walk()
    {
        transform.position = transform.position + Camera.main.transform.forward * playerSpeed * Time.deltaTime;
    }

    private void StopWalk()
    {
        transform.position = transform.position;
    }
}

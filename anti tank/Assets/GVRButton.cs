using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GVRButton : MonoBehaviour
{
    //public Image imgCircle;
    public UnityEvent GVRClick;
    public float totalTime = 2;
    bool gvrStatus;
    public float gvrTimer;

    private void Update()
    {
        if(gvrStatus)
        {
            gvrTimer += Time.unscaledDeltaTime;
            Debug.Log(gvrTimer);
            //imgCircle.fillAmount = gvrTimer / totalTime;
        }

        if(gvrTimer > totalTime)
        {
            Debug.Log("Attempting to invoke GVRClick");
            GVRClick.Invoke();
        }
    }

    public void GVROn()
    {
        gvrStatus = true;
    }

    public void GVROff()
    {
        gvrStatus = false;
        gvrTimer = 0;
        //imgCircle.fillAmount = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRGaze : MonoBehaviour
{
    public Image imgGaze;
    public float totalTime=1;
    public bool gvrStatus;
    float gvrTimer;
    bool isAttacking;

    private void Start()
    {
        imgGaze.fillAmount = 0;
    }
    private void Update()
    {
        if (gvrStatus)
        {
            gvrTimer += Time.deltaTime;
            imgGaze.fillAmount = gvrTimer / totalTime;
            StartCoroutine(Attack(totalTime));
        }
    }

    public void GVROn()
    {
        Debug.Log("GVR is ON");
        gvrStatus = true;
    }
    public void GVROff()
    {
        Debug.Log("GVR is OFF");
        gvrStatus = false;
        gvrTimer = 0;
        imgGaze.fillAmount = 0;
    }

    private IEnumerator Attack(float time)
    {
        yield return new WaitForSeconds(time);
        if (GameObject.FindGameObjectWithTag("Weapon").GetComponent<RocketLauncher>().readyToShoot && !GameObject.FindGameObjectWithTag("Weapon").GetComponent<RocketLauncher>().reloading && GameObject.FindGameObjectWithTag("Weapon").GetComponent<RocketLauncher>().bulletsLeft > 0 && GameManager.Instance.State != GameState.Pause)
        {
            GameObject.FindGameObjectWithTag("Weapon").GetComponent<RocketLauncher>().Shoot();
        }
    }
}

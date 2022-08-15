using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
        }
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200) && hit.transform.tag == "Enemy")
        {
            GVROn();
        }
        else
        {
            GVROff();
        }
    }

    public void GVROn()
    {
        Debug.Log("GVR is ON");
        gvrStatus = true;
        if(GameManager.Instance.State == GameState.Play)
        {
            StartCoroutine(Attack(totalTime));
        }
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
        if (GameObject.FindGameObjectWithTag("Weapon").GetComponent<RocketLauncher>().readyToShoot && !GameObject.FindGameObjectWithTag("Weapon").GetComponent<RocketLauncher>().reloading && GameObject.FindGameObjectWithTag("Weapon").GetComponent<RocketLauncher>().bulletsLeft > 0 && GameManager.Instance.State != GameState.Pause && gvrStatus)
        {
            GameObject.FindGameObjectWithTag("Weapon").GetComponent<RocketLauncher>().Shoot();
        }
    }
}

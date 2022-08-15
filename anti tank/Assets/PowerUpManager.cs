using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private static PowerUpManager _instance;
    public static PowerUpManager Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("PowerUpManager is NULL");
            }
            return _instance;
        }
    }

    private float preTBS;
    private float preRT;


    public GameObject weapon;

    private void Awake()
    {
        _instance = this;
        preTBS = weapon.GetComponent<RocketLauncher>().timeBetweenShooting;
        preRT = weapon.GetComponent<RocketLauncher>().reloadTime;

    }
    public IEnumerator PowerUp(int time)
    {
        Debug.Log("PowerUp enabled");
        weapon.GetComponent<RocketLauncher>().timeBetweenShooting = 0f;
        weapon.GetComponent<RocketLauncher>().reloadTime = 0f;
        yield return new WaitForSeconds(time);
        ResetWeapon();
    }

    public void ResetWeapon()
    {
        Debug.Log("ResetWeapon called");
        weapon.GetComponent<RocketLauncher>().timeBetweenShooting = preTBS;
        weapon.GetComponent<RocketLauncher>().reloadTime = preRT;
    }
}

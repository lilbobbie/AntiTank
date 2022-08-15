using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RocketLauncher : MonoBehaviour
{
    //bullet
    public GameObject bullet;

    //bullet force
    public float shootForce, upwardForce;

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    public int bulletsLeft, bulletsShot;

    //recoil
    public Rigidbody playerRb;
    public float recoilForce;

    //bools
    public bool shooting, reloading;
    public bool readyToShoot;

    //reference
    public Camera fpsCam;
    public Transform attackPoint;

    //graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;
    public TextMeshProUGUI reloadingStatus;



    //bug fixing
    public bool allowInvoke = true;

    private void Awake()
    {
        //make sure mag is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
       GameObject.Find("Player").GetComponent<ArduinoMovement>().AmmoLed(bulletsLeft);

        //Set ammo display, if it exists
        if(ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + "/" + magazineSize / bulletsPerTap);
        }
    }

    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //Reloading
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        //Reload automatically on 0 ammo
        if(readyToShoot && !shooting && !reloading && bulletsLeft == 0)
        {
            Reload();
        }

        //Shooting
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0 && GameManager.Instance.State != GameState.Pause)
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    public void Shoot()
    {
        readyToShoot = false;

        //Find exact hit pos using raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);


        //Instantiate muzzle flash, if possible
        if(muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }
        
        bulletsLeft--;
        bulletsShot++;

        //Invoke ResetShot function
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;

            //Add recoil to player
            playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
        }
        
        //if more than one bulletsPerTap make sure to repeat shoot function
        if(bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
        
    }

    public void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
        reloadingStatus.SetText("Reloading");
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloadingStatus.SetText("");
        reloading = false;
    }

}

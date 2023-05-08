using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public GameObject bullet;
    public Transform cameraTrans;
    public GameObject pistol;
    //gun properties
    private float magSize;
    private float currAmmo;
    private float fireRate;
    private float reloadTime;
    private float remainingAmmo;
    private bool canShoot;
    private float bulletTimer;
    // Start is called before the first frame update
    void Start()
    {
        startingAttributes();
    }

    // Update is called once per frame
    void Update()
    {
        //always update the properties and change the bullet timer
        currAmmo = GUIManager.instance.getCurrAmmo();
        magSize = GUIManager.instance.getMagSize();
        remainingAmmo = GUIManager.instance.getRemainingAmmo();
        bulletTimer -= 0.01f;

        if (currAmmo > 0) {
            Shooting();
        }
        Reloading();
    }

    public void startingAttributes() {
        magSize = 30f;
        currAmmo = 30f;
        canShoot = true;
        remainingAmmo = 300f;
        bulletTimer = 5f;
        GUIManager.instance.pistolStartingAttributes(currAmmo, magSize, remainingAmmo);
    }

    void Shooting() 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot == true)
        {
            //shoot based off of camera pos and instaniate the bullet and shoot it staright forward
            Vector3 cameraPos = new Vector3(cameraTrans.position.x, cameraTrans.position.y, cameraTrans.position.z);
            GameObject bulletFire = Instantiate(bullet, cameraPos, Quaternion.identity);
            GUIManager.instance.decreaseAmmo(1);
            bulletFire.transform.position = cameraTrans.transform.position + cameraTrans.transform.forward;
            bulletFire.transform.forward = cameraTrans.transform.forward;
        }
    }


    //never implemented fully
    void ADS() 
    {
        //Work in progress
        Vector3 cameraPos = new Vector3(cameraTrans.position.x, cameraTrans.position.y - 0.2f, cameraTrans.position.z + 0.2f);
        Vector3 pistolPos = new Vector3(pistol.transform.position.x, pistol.transform.position.y, pistol.transform.position.z);
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            pistol.transform.position = Vector3.MoveTowards(pistolPos,cameraPos, 0.01f);
            Shooting();
        }
        else {
            pistol.transform.position = Vector3.MoveTowards(cameraPos, pistolPos, 0.01f);
        }

    }

    void Reloading() 
    {
        if ((Input.GetKey(KeyCode.R) && currAmmo < magSize && remainingAmmo != 0) || (Input.GetKey(KeyCode.R) && currAmmo == 0 && remainingAmmo != 0)) 
        {
            float amount;
            canShoot = false;

            //if they emptied the whole mag
            if (currAmmo == 0) {
                currAmmo = magSize;
                //make sure remaining ammo doesn't dip below 0
                if (remainingAmmo == 0) {
                    remainingAmmo = 0;
                }
                else {
                    remainingAmmo -= 30;
                }
                //Debug.Log("Reloading with 0 ammo");
                canShoot = true;
                GUIManager.instance.setCurrAmmo(currAmmo);
                GUIManager.instance.setRemainingAmmo(remainingAmmo);
                GUIManager.instance.AmmoDisplay();
            }
            //if they emptied a portion of the mag
            if (currAmmo < magSize) {
                amount = magSize - currAmmo;
                currAmmo += amount;
                //make sure remaining ammo doesn't dip below 0
                if (remainingAmmo == 0) {
                    remainingAmmo = 0;
                }
                else {
                    remainingAmmo -= amount;
                }
                //Debug.Log("Reloading with partial mag");
                canShoot = true;
                GUIManager.instance.setCurrAmmo(currAmmo);
                GUIManager.instance.setRemainingAmmo(remainingAmmo);
                GUIManager.instance.AmmoDisplay();
            }
            
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoChestManager : MonoBehaviour
{

    public GameObject player;
    public GameObject chest;
    private float radius;

    private Vector3 chestPos;
    private float currMoney;

    void Start()
    {
        //get the chesPos and the radius of when to show the UI
        radius = 2.0f;
        chestPos = new Vector3(chest.transform.position.x, chest.transform.position.y, chest.transform.position.z);
    }

    void Update()
    {
        ShowChestUI();
    }

    //this method gets the player's position and calculates the distance on whether or not the user can get ammo from the ammo chest
    //checks multiple conditions
    private void ShowChestUI() {
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Vector3 distance = chestPos - playerPos;
        //Debug.Log(distance.magnitude);
        currMoney = GUIManager.instance.getMoneyCount();
        if (distance.magnitude < radius) {
            GUIManager.instance.AmmoChestDisplay();
            if (currMoney >= 300f) {
                if (Input.GetKeyDown(KeyCode.H)) {
                GUIManager.instance.setCurrAmmo(30);
                GUIManager.instance.setRemainingAmmo(300);
                GUIManager.instance.AmmoDisplay();
                GUIManager.instance.UpdateMoneyCount(-300);
                }
            }
        }
        else {
            GUIManager.instance.AmmoDisplayOff();
        }
    }
}

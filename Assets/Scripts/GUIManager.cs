using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
   public static GUIManager instance;

    //properties from the GUI elements on canvas
   [SerializeField] private TextMeshProUGUI ammo_txt;
   [SerializeField] private Image healthBar;

   [SerializeField] private TextMeshProUGUI money_txt;

   [SerializeField] private TextMeshProUGUI refill_txt;
   [SerializeField] private Image background;
   [SerializeField] private Image inside;

   [SerializeField] private TextMeshProUGUI roundNum_txt;

    //gun properites
   private float currAmmo;
   private float magSize;
   private float remainingAmmo;

   private float money;
   private int roundNum;
   private int totalKills;
   private int totalRoundKills;


//altered singleton
   private void Awake() {
        if (instance == null) {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
   }

   private void Start() {
        //set the starting attributes
        ammo_txt.text = "0";
        AmmoDisplay();

        money_txt.text = "0";
        MoneyDisplay();

        
        background.enabled = false;
        refill_txt.enabled = false;
        inside.enabled = false;
        
        roundNum_txt.text = "1";
        roundNum = 1;

        totalKills = 0;
        totalRoundKills = 0;
   }


   public void pistolStartingAttributes(float cA, float mS, float rA) {
        currAmmo = cA;
        magSize = mS;
        remainingAmmo = rA;
   }

    public void AmmoDisplay() {
        ammo_txt.text = currAmmo.ToString() + " / " + magSize.ToString() + " [ " + remainingAmmo.ToString() + " ]";
    }

    //this method decreases ammo count when viable
    public void decreaseAmmo(int value) {
        currAmmo -= value;
        if (remainingAmmo == 0) {
            remainingAmmo = 0;
        }
        else {
            remainingAmmo -= value;
        }

        AmmoDisplay();
    }

    //getters and setters for gun properties
    public float getCurrAmmo() {
        return currAmmo;
    }
    public float getMagSize() {
        return magSize;
    }
    public float getRemainingAmmo() {
        return remainingAmmo;
    }

    public void setCurrAmmo(float cA) {
        currAmmo = cA;
    }
    public void setMagSize(float mS) {
        magSize = mS;
    }
    public void setRemainingAmmo(float rA) {
        remainingAmmo = rA;
    }


    //-------Health Bar----------

    public void UpdateHealthBar(float healthPercent) {
        healthBar.fillAmount = healthPercent;
    }

    //------Money------------------
    public void MoneyDisplay() {
        money_txt.text = "$" + money.ToString() + ".00";
    }

    public void UpdateMoneyCount(float value) {
        money+= value;
        MoneyDisplay();
    }

    public float getMoneyCount() {
        return money;
    }

    //------Ammo Chest------------------
    public void AmmoChestDisplay() {
        background.enabled = true;
        inside.enabled = true;
        refill_txt.enabled = true;
    }

    public void AmmoDisplayOff() {
        background.enabled = false;
        inside.enabled = false;
        refill_txt.enabled = false;
    }

    //-----Round Number-------------------
    public void UpdateRound(int value) {
        roundNum += value;
        roundNum_txt.text = roundNum.ToString();
    }

    public int getRound() {
        return roundNum;
    }

//------Total Eliminations--------------
    public void UpdateTotalKills(int value) {
        totalKills += value;
    }

    public int getTotalKills() {
        return totalKills;
    }


    public void UpdateTotalRoundKills(int value) {
        totalRoundKills += value;
    }

    public int getTotalRoundKills() {
        return totalRoundKills;
    }

    public void setTotalRoundKills(int value) {
        totalRoundKills = value;
    }
}

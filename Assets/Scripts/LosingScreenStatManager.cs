using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LosingScreenStatManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI stats_txt;
    public static LosingScreenStatManager instance;
    private float totalEnemiesEliminated;
    private int roundNum;


    private void Awake() {
        //unlock the cursor and call the stat display function
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        stats_txt.text = "Total Eliminations: 0" + "\nRound Number: 1";
        StatDisplay();
    }

    public void StatDisplay() {
        stats_txt.text = "Total Eliminations: " + GUIManager.instance.getTotalKills() + "\nRound Number: " + GUIManager.instance.getRound();
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public PlayerManager player;

    [SerializeField] private GameObject BadGuyPrefab;

    //spawn points
    [SerializeField] private GameObject spawn1;
    [SerializeField] private GameObject spawn2;
    [SerializeField] private GameObject spawn3;
    [SerializeField] private GameObject spawn4;

    //enemy timer and enemy round properties
    private WaitForSeconds enemySpawnTimer;
    private List<EnemyManager> enemyList;
    private int enemyRoundCount;
    private int enemyCount;
    private int totalRoundKills;
    private float minTime;
    private float maxTime;


    private void Awake() {

        //check to see if the singleton exists already
        if (instance == null) {

            // create singleton by assigning it to this game object
            instance = this;

            // Prevents this game object from getting destroyed when we change scenes
            //DontDestroyOnLoad(gameObject);
        }
        // This singleton exists already -> destroy this game object
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        //starting elements
        enemyCount = 0;
        enemyRoundCount = 5;
        totalRoundKills = GUIManager.instance.getTotalRoundKills();
        minTime = 1f;
        maxTime = 10f;
        enemyList = new List<EnemyManager>();
        //base timer and start the coroutine
        enemySpawnTimer = new WaitForSeconds(10f);
        StartCoroutine (SpawnBadGuys());
    }

    public IEnumerator SpawnBadGuys() {
        //wait, set timer, then run...
        yield return enemySpawnTimer;
        //if the player has killed all the enemies in the round, then update to the next round
        if (enemyCount >= enemyRoundCount && GUIManager.instance.getTotalRoundKills() == enemyRoundCount) {
            
            GUIManager.instance.UpdateRound(1);
            enemyRoundCount += 5;
            enemyCount = 0;
            GUIManager.instance.setTotalRoundKills(0);
        }

        //makes the spawn timer quicker at later rounds
        if (enemyRoundCount == 15) {
            maxTime = 9f;
        }
        else if (enemyRoundCount == 25) {
            maxTime = 8f;
        }
        else if (enemyRoundCount > 25) {
            minTime = 1f;
            maxTime = 5f;
        }
       
       //if the player has yet to kill all the enemies and all the enemies have spawned in for the round... wait until the player kills all of the enemies first
        if (GUIManager.instance.getTotalRoundKills() != enemyRoundCount && enemyCount == enemyRoundCount) {
            do {
                yield return new WaitForSeconds(1.0f);
                if (enemyCount == enemyRoundCount && GUIManager.instance.getTotalRoundKills() == enemyRoundCount) {
                    break;
                }
            } while (GUIManager.instance.getTotalRoundKills() != enemyRoundCount);

        }
        
        //checks if all the enemies have been spawned in and eliminated then start up the coroutine again
        if (GUIManager.instance.getTotalRoundKills() == enemyRoundCount && enemyCount == enemyRoundCount) {
            StartCoroutine(SpawnBadGuys());
        }
        else {
            //spawn more badguys
            SpawnBadGuy();
        }
        

        StartCoroutine (SpawnBadGuys());

        //print("Total Round Kills: " + GUIManager.instance.getTotalRoundKills());
        //print("Total enemies: " + enemyCount);
        //print("EnemyRoundCount: " + enemyRoundCount);
        
    }

    //this method actually spawns in a bad guy prefab at the 4 random spawn points set in the environment
    //adds to the enemy counter and gives a random time to spawn in each time
    public void SpawnBadGuy() {
        int randNum = Random.Range(1, 4);
        GameObject enemy;
        EnemyManager enemyScript;
        print(randNum);
        if (randNum == 1) {
            enemy = Instantiate(BadGuyPrefab, spawn1.transform.position, Quaternion.identity);
            enemyScript = enemy.GetComponent<EnemyManager>();
            enemyList.Add(enemyScript);
            enemyCount += 1;
            enemySpawnTimer = new WaitForSeconds(Random.Range(minTime, maxTime));
        }
        else if (randNum == 2) {
            enemy = Instantiate(BadGuyPrefab, spawn2.transform.position, Quaternion.identity);
            enemyScript = enemy.GetComponent<EnemyManager>();
            enemyList.Add(enemyScript);
            enemyCount += 1;
            enemySpawnTimer = new WaitForSeconds(Random.Range(minTime, maxTime));
        }
        else if (randNum == 3) {
            enemy = Instantiate(BadGuyPrefab, spawn3.transform.position, Quaternion.identity);
            enemyScript = enemy.GetComponent<EnemyManager>();
            enemyList.Add(enemyScript);
            enemyCount += 1;
            enemySpawnTimer = new WaitForSeconds(Random.Range(minTime, maxTime));
        }
        else if (randNum == 4) {
            enemy = Instantiate(BadGuyPrefab, spawn4.transform.position, Quaternion.identity);
            enemyScript = enemy.GetComponent<EnemyManager>();
            enemyList.Add(enemyScript);
            enemyCount += 1;
            enemySpawnTimer = new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }

    public void updateEnemyCount(int value) {
        enemyCount += value;
    }
}

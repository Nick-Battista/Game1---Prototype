using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerManager player;
    public GameObject enemy;
    private float attackHitRange;

    private float distanceToPlayer;

    [SerializeField]private Animator animator;

    private Rigidbody rb;
    private float enemyHealth;

    
    void Start()
    {
        player = GameManager.instance.player;
        
        //specifics for enemy hit range and health
        attackHitRange = 2.2f;
        distanceToPlayer = 2.0f;
        enemyHealth = 100.0f;
    }

    void Update()
    {
        EnemyActions();
    }

    //gets player and enemy pos to look at the player and do all the actions based off of the enemy animator controller
    private void EnemyActions() 
    {
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        Vector3 enemyPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1.5f, enemy.transform.position.z);
        Vector3 distance = playerPos - enemyPos;

        enemy.transform.rotation = Quaternion.LookRotation(distance);
        Debug.DrawLine(enemyPos, playerPos, Color.green);


        //Stand still
        if (player == null) {
            animator.SetFloat("Speed", 0f);
            animator.ResetTrigger("Attack");
        }
        //Attack
        else if (Vector3.Distance(enemy.transform.position, player.transform.position) < distanceToPlayer) {
            animator.SetFloat("Speed", 0f);
            animator.SetTrigger("Attack");
            //Vector3 setY = new Vector3(enemy.transform.position.x, 1.0f, enemy.transform.position.z);
            Vector3 setY = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
            enemy.transform.position = setY;
        }
        //Move
        else {
            animator.SetFloat("Speed", 2f);
            animator.ResetTrigger("Attack");
        }
    }

    //checks if the hit registered inside the range and subtracts the players health
    public void CheckForHit()
    {
        if (Vector3.Distance(enemy.transform.position, player.transform.position) < attackHitRange) {
            GameManager.instance.player.UpdateHealth(-33f);
        }
    }

    //kill the enemy if the bullet hit them 5 times does other stuff for the GameManager/GUIManager
    private void OnCollisionEnter(Collision other) 
    {
        //Debug.Log("Bullet hit enemy");
        if (other.gameObject.CompareTag("bullet"))
        {
            enemyHealth -= 20;
            if (enemyHealth == 0 ) {
                GameManager.instance.player.UpdateMoney(20);
                GUIManager.instance.UpdateTotalKills(1);
                //print("Killed enemy");
                GUIManager.instance.UpdateTotalRoundKills(1);
                Object.Destroy(enemy, 1f);
                //GameManager.instance.updateEnemyCount(1);

            }
            GameManager.instance.player.UpdateMoney(1);
        }
    }
}

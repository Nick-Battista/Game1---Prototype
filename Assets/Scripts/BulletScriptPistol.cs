using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScriptPistol : MonoBehaviour
{
    public float bulletSpeed;

    public GameObject bullet;
    public GameObject enemy;


    void Start()
    {
        
    }


    void Update()
    {
        //bullet movement
        transform.position += transform.forward * bulletSpeed * Time.deltaTime;
        Destroy(bullet, 4);
    }

    private void OnCollisionEnter(Collision other) 
    {
        //Check for collision
        //Debug.Log("bullet destroyed");
        if (other.gameObject.CompareTag("enemy"))
        {
            Destroy(bullet);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public string losingScreen;
    public Transform playerTransform;

    public GameObject ground;
    [SerializeField]private float playerSpeed;
    [SerializeField]private float sprintMultiplier;
    [SerializeField]private float walkClamp;
    [SerializeField]private float sprintClamp;
    private float baseHeight;
    private bool isGrounded;
    [SerializeField]private float jumpSpeed;
    [SerializeField]private float fallSpeed;
    private Vector3 direction;
    private Rigidbody rb;
    public Transform orientation;
    float xMove;
    float zMove;
    [SerializeField]private float jumpCooldown;
    private float cooldown;
    private float currHealth;
    private float maxHealth;
    private float money;

    


    // Start is called before the first frame update
    void Start()
    {
        baseHeight = 1f;
        isGrounded = true;
        cooldown = jumpCooldown;

        //set the starting position
        playerTransform.position = new Vector3 (0f, baseHeight, -0.9f);
        //create the rigid body
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        //set player heatlh
        currHealth = 100;
        maxHealth = 100;
        UpdateHealth(0);

        //set money
        money = 0;

        //set the game manager player equal to this
        GameManager.instance.player = this;
    }

    // Update is called once per frame
    void Update()
    {
        // get the horizontal and vertical inputs using the getAxisRaw function
        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");

        //Either sprinting or walking functions
        if (Input.GetKey(KeyCode.Semicolon)) 
        {
            sprint();
        }
        else
        {
            move();
        }
        
        //if the timer has ended do the jump
        if (jumpCooldown <= 0f)
        {
            doJump();
        }

        //used this to prevent a forever floaty jump
        rb.AddForce(Vector3.down * fallSpeed, ForceMode.Force);

        //Always let the player heal
        UpdateHealth(0.1f);
        
    }

    //move function based off of the direction had help from a YT video
    //clamped the base walk movement
    private void move() 
    {
        // allows us to move in the direction we are facing
        direction = orientation.forward * zMove + orientation.right * xMove;
        // adding force to direction to actually move in that direction

        //clamps velocty of rigid body to not exceed a certain velocity because we are constantly adding force
        if (rb.velocity.magnitude > walkClamp)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, walkClamp);
        }
        rb.AddForce(direction.normalized * playerSpeed * 10f, ForceMode.Force);
        //Debug.Log(rb.velocity.magnitude);

    }

    //based off of move funtion except if the player hits the sprint key... move faster (and clamp)
    private void sprint()
    {
        if (Input.GetKey(KeyCode.Semicolon))
        {
            //same as move function but faster force is being applied
            direction = orientation.forward * zMove + orientation.right * xMove;

            //clamps velocty of rigid body to not exceed a certain velocity because we are constantly adding force
            if (rb.velocity.magnitude > sprintClamp)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, sprintClamp);
            }
            rb.AddForce(direction.normalized * playerSpeed * 10f * sprintMultiplier, ForceMode.Force);
            //Debug.Log(rb.velocity.magnitude);
        }
    }

    private void doJump()
    {
        //performs the jump only if the player is on the ground and hitting space
        if (Input.GetKey(KeyCode.Space) && isGrounded == true) {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionStay(Collision other) {
        //as long as the player is on the ground the timer for the jump cooldown will tick downward
        if (other.gameObject.CompareTag("Ground") || (other.gameObject.CompareTag("Stairs")))
        {
            if (jumpCooldown > -0.1f) {
                jumpCooldown -= Time.deltaTime;
            }
            
        }

        //stair movement that wasn't implemented fully and not used
        if (other.gameObject.CompareTag("Stairs")) {
            //Debug.Log("On stairs");
            Debug.Log(rb.velocity.magnitude);
            if (rb.velocity.magnitude <= 4) {

                if (rb.velocity.x <= 0 && rb.velocity.z <= 0) {

                }
                else {
                    rb.AddForce(Vector3.up * 30f, ForceMode.Impulse);
                }
                
            }
            
        }
    }

    


    private void OnCollisionEnter(Collision other) 
    {
        //this is for jumping to check if the player can jump and the cooldown can restart back to the desired amount
        //Debug.Log("collision");
        if (other.gameObject.CompareTag("Ground") || (other.gameObject.CompareTag("Stairs"))) 
        {
            isGrounded = true;
            jumpCooldown = cooldown;
        }
    }



    /// <summary>
    /// Adds supplied value to the player's current health. Positive health... Negative damages
    /// </summary>
    public void UpdateHealth(float value) {
        currHealth += value;

        if (currHealth < 0) {
            print("Player is dead");
            SceneManager.LoadScene(losingScreen);
        }

        if (currHealth > maxHealth) {
            currHealth = maxHealth;
        }
        GUIManager.instance.UpdateHealthBar(currHealth / maxHealth);
    }

    public void UpdateMoney(float value) {
        money+= value;

        if (money < 0) {
            print("You have no money");
        }

        GUIManager.instance.UpdateMoneyCount(value);
    }
}

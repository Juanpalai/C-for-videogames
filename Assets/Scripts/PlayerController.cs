using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Varible player momvent
    public float jumpForce = 6f;
    public float runningSpeed = 2f;

    Rigidbody2D rigidBody;
    Animator animator;
    Vector3 initialPosition;

    private const string STATE_LIVE = "isAlive";
    private const string STATE_ON_THE_GROUND = "isOnTheGround";
    private const string STATE_WALLKING = "isWallking"; 




    public LayerMask groundMask;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }


    // Start is called before the first frame update
    void Start()
    {
        
        initialPosition = transform.position; // The position at which the character starts is saved.
    }

    public void Stargame()
    {
        animator.SetBool(STATE_LIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        Invoke("Restarposition", 0.1f);
    }
    //Restar the position of the player and the camera when the player respawn
    void Restarposition()
    {
        this.transform.position = initialPosition;
        this.rigidBody.velocity = Vector2.zero;
        GameObject MainCamera = GameObject.Find("Main Camera");
        MainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) Jump(); //calling the jump function

        animator.SetBool(STATE_ON_THE_GROUND, IsTochingTheGround()); //calling the function that detects if the character is touching the ground or not

        //conditioners to know if you are going left or right
        if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            animator.SetBool(STATE_WALLKING, true);
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            animator.SetBool(STATE_WALLKING, true);
        }  
        

        if(rigidBody.velocity.x==0) //Pause walking animation when character is not moving
        {
            animator.SetBool(STATE_WALLKING, false);
        }


        Debug.DrawRay(this.transform.position, Vector2.down * 1f, Color.red);
        
    }

    void FixedUpdate()
    {
        if (GameManager.instance.currentGameState == GameSate.inGame)
        {
            if (rigidBody.velocity.x <= runningSpeed)
            {
                rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * runningSpeed, rigidBody.velocity.y);
            }
        }
        else //If we are not in the game, the speed is paused.
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            
        }
    }

    void Jump()
    {
        if (GameManager.instance.currentGameState == GameSate.inGame)
        {
            if (IsTochingTheGround())
            {
                rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

    }

    //bollean for know is the player is toching the ground

    bool IsTochingTheGround()
    {
        if(Physics2D.Raycast(this.transform.position,
            Vector2.down,
            1f, groundMask))
        {
            return true;
        }
        else
        {
           
            return false;
        }

           
    }

    public void Die()
    {
        animator.SetBool(STATE_LIVE, false);
        GameManager.instance.GameOver();
    }
}

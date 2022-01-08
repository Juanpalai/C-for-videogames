using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Varible player momvent
    public float jumpForce = 6f;
    Rigidbody2D rigidBody;
    Animator animator;

    private const string STATE_LIVE = "isAlive";
    private const string STATE_ON_THE_GROUND = "isOnTheGround";   


    public LayerMask groundMask;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }


    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool(STATE_LIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||　Input.GetMouseButtonDown(0)) Jump();

        animator.SetBool(STATE_ON_THE_GROUND, IsTochingTheGround());

        Debug.DrawRay(this.transform.position, Vector2.down * 1.4f, Color.red);
    }

    void Jump()
    {
        if (IsTochingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }        

    }

    //bollean for ksnow is the player is toching the ground

    bool IsTochingTheGround()
    {
        if(Physics2D.Raycast(this.transform.position,
            Vector2.down,
            1.4f, groundMask))
        {
            
            return true;
        }
        else
        {
           
            return false;
        }

           
    }
}

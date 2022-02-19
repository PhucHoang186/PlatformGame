using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;
using DG.Tweening;
public class PlayerController : MonoBehaviour
{
    [Header("Player Properties")]
    //Player Properties
    public float playerSpeed = 300f;
    private float HInput;
    private Rigidbody rb;
    // Jump Properties
    public float jumpForce = 100f;
    public int maxJump = 1;
    private int currentJump;
    public ParticleSystem jumpFx;
    //Player Dash properties
    public bool playerCanDash =false;// check to turn dash ability on
    private bool canDash = false;
    public float dashForce = 0f;
    public float dashTime = 0.2f;
    public float startDash = 0f;
    private bool Dashing = false;
    //Checking if Player is On ground
    public bool isGround;
    //Check if player have a key
    public bool haveKey = false;


    void Start()
    {
        currentJump = maxJump;
        rb = GetComponent<Rigidbody>();
        startDash = dashTime;
    }

    private void Update()
    {
        HInput = Input.GetAxisRaw("Horizontal");
        
        //Rotate Player
        RotatePlayer();
        //Dashing
        if (Input.GetKeyDown(KeyCode.LeftShift) && startDash >= 0 && canDash && playerCanDash)
        {
            canDash = false;
            Dashing = true;
            Dash();
        }
        if (startDash < 0)
        {
            Dashing = false;
            
            startDash = dashTime;
        }
        //Jumping
        if (currentJump > 0 && Input.GetButtonDown("Jump"))
        {
            Jump();
            if(!isGround)
                jumpFx.Play();
        }
        if (isGround)
        {
            canDash = true;
            currentJump = maxJump;
        }
    }

    private void FixedUpdate()
    {
        if (!Dashing)
        {
            Movement();
        }
        else
        {
            startDash -= Time.deltaTime;
           
        }
    }
    private void Movement()
    {
        rb.velocity = new Vector3(HInput * playerSpeed , rb.velocity.y, rb.velocity.z);
    }

    private void RotatePlayer()
    {
        Vector3 LookDirect = new Vector3(0f, 0f, HInput);
        //Rotate Look Direction
        if (HInput != 0)
        {
            rb.rotation = Quaternion.LookRotation(LookDirect, Vector3.up);
        }
    }

    private void Dash()
    {
        rb.AddForce(transform.right * dashForce, ForceMode.Acceleration);
    }
    private void Jump()
    {
        currentJump--;
        Vector3 jumpPos = rb.velocity;
        jumpPos.y = jumpForce;
        jumpPos.x = 0f;
        rb.velocity = jumpPos;
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.CompareTag("Enemy"))
        {

            GameManagement.instance.isLoss = true;
            GameManagement.instance.PauseGame();

        }
    }

}


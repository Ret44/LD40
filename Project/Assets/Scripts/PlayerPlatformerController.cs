using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float longJump = 10;
    public float midJump = 7;
    public float shortJump = 3;

    public float jumpTakeOffSpeed
    {
        get
        { 
            int coinsCount = Player.instance.coinsInventory.Count;
            if (coinsCount <= 2)
            {
                return longJump;
            }
            else if (coinsCount > 2 && coinsCount <= 4)
            {
                return midJump;
            }
            else
                return shortJump;
        }
    }

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();
       // animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = InputManager.ActiveDevice.LeftStick.X; // Input.GetAxis("Horizontal");
        
        if (InputManager.ActiveDevice.Action1.WasPressed /*Input.GetButtonDown("Jump")*/ && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (InputManager.ActiveDevice.Action1.WasReleased /*Input.GetButtonUp("Jump")*/)
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

     //   animator.SetBool("grounded", grounded);
     //   animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }
}
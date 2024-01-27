using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTopDown : MonoBehaviour
{
    public float walkSpeed = 5;

    private Animator playerAnimator;
    private SpriteRenderer playerRenderer;
    private Rigidbody2D playerRigidBody;
    private LayerMask interactableLayer;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Walk();
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            Debug.Log("interacted with object");
            Interact();
        }
    }

    private void Walk()
    {
        float dX = Input.GetAxisRaw("Horizontal");
        float dY = Input.GetAxisRaw("Vertical");

        // Player is Idle
        if (dX == 0 && dY == 0)
        {
            playerAnimator.SetBool("isWalking", false);
        }

        // Flip the facing direction -> East
        if (dX > 0)
        {
            playerRenderer.flipX = true;
        }
        else
        {
            playerRenderer.flipX = false;
        }

        if(dX != 0) dY = 0;

        playerAnimator.SetBool("isWalking", true);
        playerAnimator.SetFloat("dX", dX);
        playerAnimator.SetFloat("dY", dY);

        // Move
        Vector2 moveVector = new Vector2();
        moveVector.Set(dX, dY);
        moveVector.Normalize();
        moveVector *= walkSpeed * Time.deltaTime;

        playerRigidBody.MovePosition(playerRigidBody.position + moveVector);
    }

    void Interact() {
        var facingDir = new Vector2(playerAnimator.GetFloat("dX"), playerAnimator.GetFloat("dY"));
        var interactPos = playerRigidBody.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.5f, interactableLayer);
        if(collider) {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }
}

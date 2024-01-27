using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public bool isMoving;
    public Vector2 input;
    public Animator playerAnimator;
    public Rigidbody2D playerRigidBody;
    public SpriteRenderer playerRenderer;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    public LayerMask bullets;


    // Start is called before the first frame update
    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    public void FixedUpdate() {
        if(!isMoving) {

            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x == 0 && input.y == 0)
            {
                playerAnimator.SetBool("isWalking", false);
            }

            float dir = playerAnimator.GetFloat("dX");
            if(transform.position.x + dir > transform.position.x) playerRenderer.flipX = true;
            else playerRenderer.flipX = false;

            if(input != Vector2.zero) {
                //flips the chacter from left to right when moving.
                if (input.x > 0) playerRenderer.flipX = true;
                else playerRenderer.flipX = false;

                playerAnimator.SetFloat("dX", input.x);
                playerAnimator.SetFloat("dY", input.y);

                Vector2 moveVector = new Vector2();
                moveVector.Set(input.x, input.y);
                moveVector.Normalize();
                moveVector *= moveSpeed * Time.deltaTime;
                StartCoroutine(Move(moveVector));
            }
        }

        if(Input.GetKeyDown(KeyCode.E)) 
        {
            Interact();
        }
    }

    IEnumerator Move(Vector2 moveVector) {
        isMoving = true;
        playerAnimator.SetBool("isWalking", isMoving);
        playerRigidBody.MovePosition(playerRigidBody.position + moveVector);
        isMoving = false;
        yield return null;
    }

    void Interact() {
        var facingDir = new Vector3(playerAnimator.GetFloat("dX"), playerAnimator.GetFloat("dY"));
        var interactPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if(collider) {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }

    public float getPositionX() {
        return transform.position.x;
    }

    public float getPositionY() {
        return transform.position.y;
    }

    //  IEnumerator Move(Vector3 targetPos) {
    //     isMoving = true;
    //     while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)  {
    //         transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    //         yield return null;
    //     }
    //     transform.position = targetPos;
    //     isMoving = false;
    // }

    // private bool IsWalkable(Vector3 targetPos) {
    //     if(Physics2D.OverlapCircle(targetPos, 0.01f, interactableLayer | solidObjectsLayer | bullets)) {
    //         Debug.Log("touching:)");
    //         return false;
    //     }
    //     return true;
    // }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    public GameObject sword;
    private Animator swordAnimator;
    private Rigidbody2D swordRigidBody;
    private Collider2D swordCollider;
    private bool trySwing;

    public int amountLifeForceDrained;

    // Update is called once per frame
    void Start() 
    {
        swordAnimator = GetComponent<Animator>();
        swordRigidBody = GetComponent<Rigidbody2D>();
        swordCollider = GetComponent<Collider2D>();
        swordAnimator.SetBool("IsSwinging", false);
        swordCollider.enabled = false;
    }

    override
    protected void Update()
    {
        base.Update();
        if(trySwing) {
            StartCoroutine(Swing());
        }
    }

    override
    public void DrainLifeForce(int lifeForceDrained)
    {
        base.DrainLifeForce(lifeForceDrained);
    }

    override
    protected bool TryAttack() 
    {
        if(Input.GetMouseButtonDown(0)) {
            trySwing = true;
            return trySwing;
        }
        trySwing = false;
        return trySwing;
    }

    public IEnumerator Swing()
    {
        swordCollider.enabled = true;
        swordAnimator.SetBool("IsSwinging", true);
        base.isAtking = true;
        DrainLifeForce(amountLifeForceDrained);
        yield return new WaitForSeconds(base.atkInterval);
        swordAnimator.SetBool("IsSwinging", false);
        base.isAtking = false;
        swordCollider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
 
        if (collider.gameObject != sword)
        {
            Enemy hitCharacter = collider.GetComponent<Enemy>();

            swordRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;

            if (hitCharacter != null)
            {
                hitCharacter.DamageCharacter(base.damage, 0);
            }
        }
    }

    
}

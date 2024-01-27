using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public GameObject shooter;
    private Animator bulletAnimator;
    private Rigidbody2D bulletRb;

    // Start is called before the first frame update
    void Awake()
    {
        TryGetComponent<Animator>(out bulletAnimator);
        bulletRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        if (collider.gameObject != shooter)
        {
            Character hitCharacter = collider.GetComponent<Character>();
            

            if (hitCharacter != null)
            {
                hitCharacter.DamageCharacter(damage, 0);
            }

            bulletRb.constraints = RigidbodyConstraints2D.FreezeAll;
            if (bulletAnimator != null)
            {
                bulletAnimator.SetBool("isHit", true);
            }
            else
            {
                Disable();
            }
        }
    }

    /// <summary>
    /// Reset this Bullet to a disabled state.
    /// </summary>
    protected void Disable()
    {
        if (bulletAnimator != null)
        {
            bulletAnimator.SetBool("isHit", false);
        }

        bulletRb.constraints = RigidbodyConstraints2D.None;
        gameObject.SetActive(false);
    }
}

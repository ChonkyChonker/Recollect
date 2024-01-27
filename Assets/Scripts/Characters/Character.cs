using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public bool canBeDamaged = true;
    public int maxLifeForce;
    public int lifeForce;
    public float knockBackForce;
    //Specially for one enemy AI (Aoi Todo) dont remove;
    public bool midDamage;

    public SpriteRenderer characterRenderer;
    private Coroutine flickerCoroutine;

    protected void Awake()
    {
        characterRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
    }

    /// <summary>
    /// Try to damage this Character. <br></br>
    /// If this.health <= 0 after taking the damage, this Character die.
    ///
    /// Params: <br></br>
    /// <paramref name="dmg"/> - The positive amount of damage to this Character.
    /// 
    /// </summary>
    /// <param name="dmg"></param>
    public virtual void DamageCharacter(int dmg, float kbForce)
    {
        if (canBeDamaged)
        {
            health -= dmg;
            OnCharacterDamaged();
            midDamage = true;
        }

        if (health <= 0)
        {
            OnCharacterDie();
        }
    }

    /// <summary>
    /// Do something on damaging Character: <br></br>
    /// Flicker the Character.
    /// </summary>
    protected virtual void OnCharacterDamaged()
    {
        if (flickerCoroutine == null)
        {
            StartCoroutine(FlickerCharacter());
        }
    }

    private IEnumerator FlickerCharacter()
    {
        characterRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        characterRenderer.color = Color.white;

        // Coroutine Suicide
        flickerCoroutine = null;
    }

    /// <summary>
    /// Do something on Character's death. <br></br>
    /// This method is usually overrided.
    /// </summary>
    protected virtual void OnCharacterDie()
    {
        Destroy(gameObject);
        Debug.Log($"{gameObject.name} died.");
    }

    /// <summary>
    /// Try to heal this Character. <br></br>
    ///
    /// Params: <br></br>
    /// <paramref name="health"/> - The positive amount of health to this Character.
    /// 
    /// </summary>
    public void HealCharacter(int health)
    {
        this.health += health;
        this.health = Mathf.Min(this.health, maxHealth);
    }

    /// <summary>
    /// Check if the Weapon trigger is holding during this frame.
    /// </summary>
    /// <returns></returns>
    public abstract bool isHoldingWeaponTrigger();

    /// <summary>
    /// Check if the Weapon trigger is pressed at this frame.
    /// </summary>
    /// <returns></returns>
    public abstract bool isPressedWeaponTrigger();
}

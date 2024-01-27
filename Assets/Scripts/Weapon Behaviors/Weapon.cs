using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Only Characters can hold & use Weapon. <br></br>
/// Require: Character script in any level parent GameObject.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    public string weaponName;
    public int damage;
    public float atkInterval; // sec
    public float groupedAtkTimeWindow = 0.3f;
    public Vector2 handlePoint;
    public bool isAtking;

    protected Character weaponHolder;
    protected Animator weaponAnimator;
    protected Coroutine atkCoroutine;

    private void Awake()
    {
        TryGetComponent<Animator>(out weaponAnimator);
        weaponHolder = GetComponentInParent<Character>();
    }

    protected virtual void Update()
    {
        Vector2 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        WeaponFollowHand(mouseWorldPoint);
        PointToTarget(mouseWorldPoint);
        TryAttack();
    }

    public virtual void DrainLifeForce(int lifeForceDrained)
    {
        if(weaponHolder.lifeForce - lifeForceDrained <= 0) {
            weaponHolder.lifeForce = 0;
            int lifeLost = lifeForceDrained - weaponHolder.lifeForce;
            weaponHolder.DamageCharacter(lifeLost, 0);
        } else {
            weaponHolder.lifeForce -= lifeForceDrained;
        }
    }

    /// <summary>
    /// Weapon point to the mouse position. <br></br>
    /// Param: <br></br>
    /// <paramref name="aimingPoint"/> - Aiming target world position.
    /// </summary>
    private void PointToTarget(Vector2 aimingPoint)
    {
        Vector2 gunPointDir = aimingPoint - (Vector2)transform.position;

        // retrieve current rotation
        Vector3 eularWorldRotation = transform.rotation.eulerAngles;

        eularWorldRotation.z = Mathf.Atan(gunPointDir.y / gunPointDir.x) * Mathf.Rad2Deg;

        if (gunPointDir.x > 0)
        {
            eularWorldRotation.z *= -1;
            eularWorldRotation.y = 180;
        }
        else
        {
            eularWorldRotation.y = 0;
        }

        transform.rotation = Quaternion.Euler(eularWorldRotation);
    }

    /// <summary>
    /// Param: <br></br>
    /// <paramref name="aimingPoint"/> - Aiming target world position.
    /// </summary>
    private void WeaponFollowHand(Vector2 aimingPoint)
    {
        Vector2 holderPointDir = aimingPoint - (Vector2)weaponHolder.transform.position;
        float handlePointXAbs = Mathf.Abs(handlePoint.x);

        if (holderPointDir.x > handlePointXAbs)
        {
            handlePoint.x = handlePointXAbs;
        }
        else if(holderPointDir.x < -handlePointXAbs)
        {
            handlePoint.x = -handlePointXAbs;
        }

        transform.localPosition = handlePoint;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns><see langword="true"/> -> did some attack. </returns>
    protected abstract bool TryAttack();


}

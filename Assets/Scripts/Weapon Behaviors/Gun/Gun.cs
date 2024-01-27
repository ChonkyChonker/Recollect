using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public enum GunFireMode
    {
        SINGLE, AUTO, TRIPLE, LOCK
    }

    public int currentAmmo = 12;
    public int maxAmmo = 12;
    public float gunImpulse = 1;
    public float muzzleFlashTime = 0.05f;
    public GameObject bulletPrefab;
    public GameObject muzzleFlash;
    public Transform muzzlePoint;
    public GunFireMode fireMode;
    public int amountLifeForceDrained;

    private List<GameObject> bulletPool = new List<GameObject>();

    override
    protected void Update()
    {
        base.Update();
    }

    override
    public void DrainLifeForce(int lifeForceDrained)
    {
        base.DrainLifeForce(lifeForceDrained);
    }

    override
    protected bool TryAttack()
    {
        bool tryShoot = false;
        int bulletsToShoot = 0;

        switch (fireMode)
        {
            case GunFireMode.TRIPLE:
                bulletsToShoot += 2;  // add additional 2 bullets
                goto case GunFireMode.SINGLE;  // fall through
            case GunFireMode.SINGLE:
                bulletsToShoot++;  // add basic 1 bullet
                tryShoot = weaponHolder.isPressedWeaponTrigger(); // Press (even Hold) once -> fire once (may shoot multiple bullets on one fire)
                break;
            case GunFireMode.AUTO:
                bulletsToShoot++;  // add basic 1 bullet
                tryShoot = weaponHolder.isHoldingWeaponTrigger();  // Hold -> consistent firing
                break;
            case GunFireMode.LOCK:
                break;
            default:
                Debug.Log($"Illegal fireMode value: {fireMode}");
                break;
        }

        if (tryShoot)
        {
            return TryShootOnce(bulletsToShoot);
        }

        return false;
    }

    /// <summary>
    /// Try shoot from the muzzle.
    /// </summary>
    /// <param name="bulletsToShoot"></param>
    /// <returns><see langword="true"/> -> full/partial complete shooting task.</returns>
    private bool TryShootOnce(int bulletsToShoot)
    {
        if (atkCoroutine == null && currentAmmo > 0)
        {
            atkCoroutine = StartCoroutine(TryShootBullets(bulletsToShoot));
            return true;
        }

        return false;
    }

    /// <summary>
    /// 
    /// Single Attack: <br></br>
    /// Shooting rate capped by atkInterval. <br></br>
    /// 
    /// Grouped Attack: <br></br>
    /// Shoot these bullets within the atkTimeWindow using a proper calculated interval. <br></br>
    /// (evenly distributed within atkTimeWindow)
    /// 
    /// </summary>
    /// <param name="bulletsToShoot"></param>
    /// <returns></returns>
    private IEnumerator TryShootBullets(int bulletsToShoot)
    {
        // Single Attack
        if (bulletsToShoot == 1)
        {
            base.isAtking = true;
            TryShootBullet();
            goto End_Shoot;
        }

        float interval = groupedAtkTimeWindow / bulletsToShoot;
        // Grouped Attack
        for (int i = 0; i < bulletsToShoot; i++)
        {
            if (!TryShootBullet())
            {
                goto End_Shoot;
            }
            yield return new WaitForSeconds(interval);
        }

    End_Shoot:
        yield return new WaitForSeconds(atkInterval);
        base.isAtking = false;
        atkCoroutine = null;
    }

    /// <summary>
    /// Helper: Shoot one bullet.
    /// </summary>
    private bool TryShootBullet()
    {
        // out of ammo
        DrainLifeForce(amountLifeForceDrained);
        if (currentAmmo <= 0)
        {
            return false;
        }

        GameObject bullet = null;

        // Try to find an inactive bullet in the bullet pool
        foreach (GameObject bulletObj in bulletPool)
        {
            if (!bulletObj.activeSelf)
            {
                bullet = bulletObj;
                bullet.SetActive(true);
                goto Bullet_Ready;
            }
        }

        // not found -> Instantiate a new bullet
        bulletPool.Add(bullet = Instantiate(bulletPrefab));

    Bullet_Ready:
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        bulletScript.damage = damage;
        bulletScript.shooter = transform.parent.gameObject;
        bullet.transform.SetPositionAndRotation(muzzlePoint.position, muzzlePoint.rotation);
        bulletRb.AddForce(-muzzlePoint.up.normalized * gunImpulse, ForceMode2D.Impulse);

        currentAmmo--;
        StartCoroutine(FlashMuzzle());

        return true;
    }

    /// <summary>
    /// Flash muzzle once.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FlashMuzzle()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(muzzleFlashTime);
        muzzleFlash.SetActive(false);
    }

}

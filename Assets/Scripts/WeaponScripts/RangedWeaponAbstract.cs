using System;
using GameNamespace;
using UnityEngine;

public abstract class RangedWeaponAbstract : WeaponAbstract
{
    [SerializeField] protected PlayerAbstract player;
    [SerializeField] public RangedWeaponParams weaponParams;
    [SerializeField] protected Bullet bullet;
    [SerializeField] private float distanceToPlayer = .7f;
    public bool isReloading = false;
    public bool isShotReady = true;
    private Transform _playerTransform;

    protected void InitPlayer() {
        if (player == null) {
            player = FindFirstObjectByType<PlayerAbstract>();
            if (player == null)
            {
                throw new Exception("no player found");
            }
            _playerTransform = player.transform;
        }
    }

    protected void ShootBullet() {
        for (int i = 0; i < weaponParams.bulletsPerShot; i++) {
            Vector3 shotDirection = player.ToMouseDirection();
            if (shotDirection == Vector3.zero) {
                return;
            }
            float shotOffset = UnityEngine.Random.Range(- weaponParams.spreadDegrees, weaponParams.spreadDegrees);
            shotDirection = Quaternion.AngleAxis(shotOffset, Vector3.forward) * shotDirection;

            var bulletInstatce = Instantiate(bullet, transform.position, transform.rotation);
            bulletInstatce.Init(
                weaponParams.bulletParams.bulletSpeed,
                weaponParams.bulletParams.bulletLifetime,
                player.GetCurrentStat(EntityStats.RANGED_DAMAGE_ID) * weaponParams.damageMultiplier,
                shotDirection);
        }

        isShotReady = false;
        weaponParams.DecrementBullets();
        Invoke(nameof(ReadyShot), weaponParams.betweenShotsTime);
    }

    protected bool GetShootInput() {
        if (IsEquiped() && weaponParams.BulletsLeft() > 0) {
            // for automatic weapons button must be held down
            if (weaponParams.isAutomatic) {
                return Input.GetButton("Fire1");
            }
            // for not automatic weapons button must be pressed
            return Input.GetButtonDown("Fire1");
        }
        return false;
    }

    protected bool GetReloadInput() {
        return weaponParams.BulletsLeft() < weaponParams.magazineSize && Input.GetKeyDown(KeyCode.R);
    }

    protected void Draw() {
        Vector3 toMouse = player.ToMouseDirection();
        if (toMouse == Vector3.zero) {
            return;
        }

        transform.position = toMouse * distanceToPlayer + _playerTransform.position;
        float angle = Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
    }

    public void Reload() {
        isReloading = true;
        Invoke(nameof(FinishReloading), weaponParams.reloadTime);
    }

    protected void FinishReloading() {
        isReloading = false;
        weaponParams.ReloadBullets();
    }

    public void ReadyShot() {
        isShotReady = true;
    }
}

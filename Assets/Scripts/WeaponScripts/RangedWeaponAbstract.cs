using System;
using GameNamespace;
using UnityEngine;

public class RangedWeaponAbstract : MonoBehaviour
{
    [SerializeField] protected PlayerAbstract player;
    [SerializeField] protected RangedWeaponParams weaponParams;
    [SerializeField] protected Bullet bullet;
    [SerializeField] private float distanceToPlayer = .7f;
    protected bool isEquiped = false;
    protected bool isReloaded = true;
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
        Vector3 shotDirection = player.ToMouseDirection();
        if (shotDirection == Vector3.zero) {
            return;
        }

        var bulletInstatce = Instantiate(bullet, transform.position, transform.rotation);
        bulletInstatce.Init(
            weaponParams.bulletParams.bulletSpeed,
            weaponParams.bulletParams.bulletLifetime,
            player.GetCurrentStat(EntityStats.RANGED_DAMAGE_ID),
            shotDirection);

        isReloaded = false;
        Invoke(nameof(Reload), weaponParams.reloadTime);
    }

    protected bool GetShootInput() {
        if (isEquiped) {
            return Input.GetButtonDown("Fire1");
        }
        return false;
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

    public void Equip() {
        isEquiped = true;
    }

    public void Reload() {
        isReloaded = true;
    }
}

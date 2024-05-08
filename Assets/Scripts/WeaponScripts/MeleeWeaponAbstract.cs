using System;
using System.Collections.Generic;
using GameNamespace;
using UnityEngine;

public abstract class MeleeWeaponAbstract : WeaponAbstract
{
    [SerializeField] protected PlayerAbstract player;
    [SerializeField] protected MeleeWeaponParams weaponParams;
    [SerializeField] private float distanceToPlayer = .7f;

    [SerializeField] protected Collider2D hitbox;
    protected bool isRecharged = true;
    protected bool isSwinging = false;
    private Transform _playerTransform;
    private float _swingAngle;
    private float _swingRelativeAngle;
    private Vector3 _swingPosition;
    protected List<int> hitEnemiesHashCodes;
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

    protected void InitHitbox() {
        if (hitbox == null) {
            hitbox = GetComponent<Collider2D>();
        }
    }

    protected void Swing() {
        if (!IsEquiped()) {
            return;
        }

        Vector3 toMouse = player.ToMouseDirection();
        if (toMouse == Vector3.zero) {
            return;
        }

        _swingRelativeAngle = Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg;
        _swingAngle = -weaponParams.swingDegrees / 2;
        _swingPosition = player.ToMouseDirection();
        isSwinging = true;
        isRecharged = false;
        hitEnemiesHashCodes = new List<int>();
        Invoke(nameof(EndSwing), weaponParams.swingTime);
        Invoke(nameof(Recharge), weaponParams.swingTime + weaponParams.rechargeTime);
    
    }

    protected void Draw() {
        if (isSwinging) {
            DrawSwinging();
        } else {
            DrawIdle();
        }
    }
    protected void DrawSwinging() {
        Quaternion rotation = Quaternion.Euler(0, 0, _swingAngle);
        Vector3 positionToPlayer = rotation * _swingPosition;
        transform.position = _playerTransform.position + positionToPlayer;
        transform.rotation = Quaternion.Euler(0, 0, _swingRelativeAngle + _swingAngle);

        float swingSpeed = weaponParams.swingDegrees / weaponParams.swingTime;
        _swingAngle += swingSpeed * Time.deltaTime;

    }
    protected void DrawIdle() {
        Vector3 toMouse = player.ToMouseDirection();
        if (toMouse == Vector3.zero) {
            return;
        }

        transform.position = toMouse * distanceToPlayer + _playerTransform.position;
        float angle = Mathf.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
    }
    

    protected bool GetAttackInput() {
        if (IsEquiped()) {
            return Input.GetButtonDown("Fire1");
        }
        return false;
    }

    public void Recharge() {
        isRecharged = true;
    }

    protected void EndSwing() {
        isSwinging = false;
    }

        private void OnTriggerEnter2D(Collider2D other) {
        if (!isSwinging) {
            return;
        }
        var otherGameObject = other.gameObject;
        if (otherGameObject.tag != "Enemy") {
            return;
        }
        if (hitEnemiesHashCodes.Contains(otherGameObject.GetHashCode())) {
            return;
        }
        hitEnemiesHashCodes.Add(otherGameObject.GetHashCode());
        Vector3 knockbackDirection3D = otherGameObject.transform.position - _playerTransform.position;
        Vector2 knockbackDirection = new Vector2(knockbackDirection3D.x, knockbackDirection3D.y);
        var enemy = otherGameObject.GetComponent<BasicEnemyAbstract>();
        enemy.Knockback(knockbackDirection, weaponParams.knockbackForce);
        enemy.DealDamage(new AttackParams(AttackParams.MELEE,
            player.GetCurrentStat(EntityStats.MELEE_DAMAGE_ID)));
    }
}

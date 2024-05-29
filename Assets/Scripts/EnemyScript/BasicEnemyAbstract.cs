using System;
using GameNamespace;
using UnityEngine;
using UnityEngine.AI;

public abstract class BasicEnemyAbstract : MonoBehaviour
{
    public EntityStats stats;
    [SerializeField] protected Rigidbody2D rb2d;
    [SerializeField] protected SpriteRenderer sr;
    // Floats storing default enemy stats, used for initialization
    [SerializeField] protected float baseMaxHP = 100f;
    [SerializeField] protected float baseSpeed = 100f;
    [SerializeField] protected float baseMeleeDamage = 30f;
    [SerializeField] protected float baseRangedDamage = 20f;
    [SerializeField] protected float baseMeleeResistance = 50f;
    [SerializeField] protected float baseRangedResistance = 50f;
    [SerializeField] protected float baseFireResistance = 50f;
    [SerializeField] protected float baseToxineResistance = 50f;
    [SerializeField] protected float AwarnessDisdtanse = 5f;
    [SerializeField] protected float MimDistance = 1.05f;
    protected PlayerAbstract player;
    protected NavMeshAgent navMeshAgent;

    // a utility function that executes all initializations, EnemyStats, RigidBody2d etc...
    // if you do not want to init a component set doInit_componentName_ to false in parameters
    protected void CombinedInit(bool doInitStats = true,
        bool doInitRigidBody2D = true,
        bool doInitSpriteRenderer = true,
        bool doInitNavMeshAgent = true) {
            if (doInitStats) {
                InitEnemyStats();
            }
            if (doInitRigidBody2D) {
                InitRigidbody2D();
            }
            if (doInitSpriteRenderer) {
                InitSpriteRendered();
            }
            if (doInitNavMeshAgent) {
                InitNavMeshAgent();
            }
            player = FindFirstObjectByType<PlayerAbstract>();
            if (player == null)
            {
                throw new Exception("no player found");
            }

            
    }
    protected void InitEnemyStats() {
        if (stats != null) {
            Debug.Log("Warning - Enemy stats are already initialized!");
            return;
        }
        stats = ScriptableObject.CreateInstance<EntityStats>();
        stats.SetEntityStats(baseMaxHP,
            baseSpeed,
            baseMeleeDamage,
            baseRangedDamage,
            baseMeleeResistance,
            baseRangedResistance,
            baseFireResistance,
            baseToxineResistance);
    }
// by default not using gravity (Rigidbody2d.gravityScale = 0.0)
// and increasing the drag
    protected void InitRigidbody2D(float gravityScale = 0f, float drag = 10f, bool freezeRotation = true) {
        if (rb2d == null) {
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.gravityScale = gravityScale;
            rb2d.drag = drag;
            rb2d.freezeRotation = freezeRotation;
        }
    }

    protected void InitSpriteRendered() {
        if (sr == null) {
            sr = GetComponent<SpriteRenderer>();
        }
    }

    protected void InitNavMeshAgent(bool updateRotation = false, bool updateUpAxis = false) {
        if (navMeshAgent == null) {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.updateRotation = updateRotation;
            navMeshAgent.updateUpAxis = updateUpAxis;
        }
    }

    protected void ForceOnRigidBody2D(Vector2 force) {
        if (force == Vector2.zero) {
            return;
        }
        rb2d.AddForce(force);
    }
    protected abstract Vector2 GetMovement();

    protected void FixedMovementOnRigidbody2D() {
        float speed = stats.GetCurrentStat(EntityStats.SPEED_ID);
        Vector2 force = speed * GetMovement();

        ForceOnRigidBody2D(force);
    }

    public void DealDamage(AttackParams attackParams) {
        stats.ApplyDamage(attackParams);
        float hp = stats.GetHP();
        Debug.Log("Current HP: " + hp);
        if (hp < 0) {
            Kill();
        }
    }

    public void Kill() {
        OnKill();
        Destroy(gameObject);
    }
    protected void OnKill() {
        Debug.Log($"{gameObject.name} died");
    }

    public void Knockback(Vector2 direction, float force) {
        rb2d.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    protected void NavMeshMoveToPlayer() {
        Vector3 playerPosition = player.transform.position;

        navMeshAgent.SetDestination(new Vector3(
            playerPosition.x,
            playerPosition.y,
            transform.position.z
        ));
    }
}


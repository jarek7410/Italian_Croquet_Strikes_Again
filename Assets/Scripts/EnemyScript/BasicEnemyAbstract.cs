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
    [SerializeField] protected int expierienceGranted = 5;
    protected GameLogic gameLogic;
    protected PlayerAbstract player;
    protected NavMeshAgent navMeshAgent;
    protected abstract void Attack();
    protected abstract Vector2 GetMovement();

    protected int navAngentUpdateInterval = 8;
    protected int navAgentLastUpdate = -999;
    protected Vector2 navDirToPlayer;

    private bool _isFrozen = false;
    private bool _isDead = false;

    // a utility function that executes all initializations, EnemyStats, RigidBody2d etc...
    // if you do not want to init a component set doInit_componentName_ to false in parameters
    protected void CombinedInit(bool doInitStats = true,
        bool doInitRigidBody2D = true,
        bool doInitSpriteRenderer = true,
        bool doInitNavMeshAgent = true,
        bool doInitGameLogic = true) {
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
            if (doInitGameLogic) {
                gameLogic = GameLogic.Instance;
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

    protected void InitNavMeshAgent(bool updateRotation = false, bool updateUpAxis = false, float speed = 0) {
        if (navMeshAgent == null) {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.updateRotation = updateRotation;
            navMeshAgent.updateUpAxis = updateUpAxis;
            navMeshAgent.speed = speed;
        }
    }

    protected void InitGameLogic() {
        gameLogic = GameLogic.Instance;
    }

    protected void ForceOnRigidBody2D(Vector2 force) {
        if (force == Vector2.zero) {
            return;
        }
        rb2d.AddForce(force);
    }

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
        if (_isDead) {
            return;
        }
        _isDead = true;
        OnKill();
        Destroy(gameObject);
    }
    protected void OnKill() {
        Debug.Log($"{gameObject.name} died");
        gameLogic.OnEnemyDeath(this);
        player.GrantExpierience(expierienceGranted);
    }

    public void Knockback(Vector2 direction, float force) {
        rb2d.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    public float ToPlayerDistance () {
        Vector3 toPlayer = player.transform.position - transform.position;
        toPlayer.z = 0;
        return toPlayer.magnitude;
    }
    public Vector2 ToPlayerDirection() {
        Vector3 toPlayer3D = player.transform.position - transform.position;
        Vector2 toPlayer = new Vector2(toPlayer3D.x, toPlayer3D.y);
        return toPlayer.normalized;
    }
    protected Vector2 NavMeshDirectionToPlayer() {
        if (Time.frameCount - navAgentLastUpdate <= navAngentUpdateInterval) {
            return navDirToPlayer;
        }

        Vector3 playerPosition = player.transform.position;

        navMeshAgent.SetDestination(new Vector3(
            playerPosition.x,
            playerPosition.y,
            transform.position.z
        ));

        Vector3[] corners = navMeshAgent.path.corners;
        if (corners.Length < 2) {
            return new Vector2(playerPosition.x - transform.position.x, playerPosition.y - transform.position.y).normalized;
        }

        navDirToPlayer = new Vector2(corners[1].x - corners[0].x, corners[1].y - corners[0].y).normalized;
        navAgentLastUpdate = Time.frameCount;
        return navDirToPlayer;
    }

    protected void Freeze() {
        _isFrozen = true;
    }
    protected void Unfreeze() {
        _isFrozen = false;
    }

    public bool IsFrozen() {
        return _isFrozen;
    }

}


using System;
using System.Collections;
using System.Collections.Generic;
using GameNamespace;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class BasicEnemyAbstract : MonoBehaviour
{
    public EntityStats stats;
    [SerializeField] protected Rigidbody2D rb2d;
    [SerializeField] protected SpriteRenderer sr;
    // Floats storing default players stats, used for initialization
    [SerializeField] protected float baseMaxHP = 100f;
    [SerializeField] protected float baseSpeed = 100f;
    [SerializeField] protected float baseMeleeDamage = 30f;
    [SerializeField] protected float baseRangedDamage = 20f;
    [SerializeField] protected float baseMeleeResistance = 50f;
    [SerializeField] protected float baseRangedResistance = 50f;
    [SerializeField] protected float baseFireResistance = 50f;
    [SerializeField] protected float baseToxineResistance = 50f;

    [SerializeField] protected float dodgeMultiplier = .2f;
    [SerializeField] protected float dodgeTime = .5f;
    [SerializeField] protected float AwarnessDisdtanse = 5f;
    [SerializeField] protected float MimDistance = 1.05f;
    protected PlayerAbstract[] playerList;

    private bool isDodging = false;

    private Vector2 dodgeDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // a utitlity function that executes all initializations, PlayerStats, RigidBody2d etc...
    // if you do not want to init a component set doInit_componentName_ to false in parameters
    protected void CombinedInit(bool doInitStats = true,
        bool doInitRigidBody2D = true,
        bool doInitSpriteRenderer = true) {
            if (doInitStats) {
                InitPlayerStats();
            }
            if (doInitRigidBody2D) {
                InitRigidbody2D();
            }
            if (doInitSpriteRenderer) {
                InitSpriteRendered();
            }

            playerList = FindObjectsOfType<PlayerAbstract>();
            if (playerList.Length <= 0)
            {
                throw new Exception("no player found");
            }
    }
    protected void InitPlayerStats() {
        if (stats != null) {
            Debug.Log("Warning - player stats are already initialized!");
            return;
        }
        stats = new EntityStats(baseMaxHP,
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

}


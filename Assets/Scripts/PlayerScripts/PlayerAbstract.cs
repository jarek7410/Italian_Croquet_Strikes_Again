using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameNamespace {
    public abstract class PlayerAbstract : MonoBehaviour
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

    protected Vector2 GetMovementInput() {
        float horizontal = Input.GetAxis("Horizontal");
        float veritcal = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(horizontal, veritcal);

        if (inputVector.magnitude > 1.0f) {
            inputVector.Normalize();
        }

        return inputVector;
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
    protected void FixedMovementOnRigidbody2D() {
        float speed = stats.GetCurrentStat(EntityStats.SPEED_ID);
        Vector2 force = speed * GetMovementInput();

        ForceOnRigidBody2D(force);
    }

    protected bool GetDodgeInput()
    {
        bool input = Input.GetButtonDown("Jump");
        if (!input)
        {
            return false;
        }

        if (GetMovementInput() == Vector2.zero)
        {
            return false;
        }

        return true;
    }

    protected void Dodge(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            return;
        }
        direction.Normalize();
        dodgeDirection = direction;
        isDodging = true;
        Invoke(nameof(EndDodge), dodgeTime);
    }

    protected void FixedDodgeMovement()
    {
        float speed = stats.GetCurrentStat(EntityStats.SPEED_ID);
        rb2d.position += Time.fixedDeltaTime * speed * dodgeMultiplier * dodgeDirection;
    }

    private void EndDodge()
    {
        isDodging = false;
    }

    public bool IsDodging()
    {
        return isDodging;
    }
}

}
using Unity.VisualScripting;
using UnityEngine;


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

    [SerializeField] protected float dodgeMultiplier = 1.5f;
    [SerializeField] protected float dodgeTime = .5f;

    protected ExpierienceHelper expierienceHelper = new ExpierienceHelper();
    protected GameLogic gameLogic = GameLogic.Instance;
    private bool isDodging = false;

    private Vector2 dodgeDirection;

    protected Vector2 GetMovementInput() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(horizontal, vertical);

        if (inputVector.magnitude > 1f) {
            inputVector.Normalize();
        }

        return inputVector;
    }

    /// <summary>
    /// A utitlity function that executes all initializations, PlayerStats, RigidBody2d etc...
    /// if you do not want to init a component set doInit_componentName_ to false in parameters
    /// </summary>
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
            DontDestroyOnLoad(this);
    }
    protected void InitPlayerStats() {
        if (stats != null) {
            Debug.Log("Warning - player stats are already initialized!");
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

    protected void ForceOnRigidBody2D(Vector2 force) {
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
        int dodgingLayerIndex = LayerMask.NameToLayer("Dodging");
        gameObject.layer = dodgingLayerIndex;
        Invoke(nameof(EndDodge), dodgeTime);
    }

    protected void FixedDodgeMovement()
    {
        float speed = stats.GetCurrentStat(EntityStats.SPEED_ID);
        rb2d.AddForce(speed * dodgeMultiplier * dodgeDirection);
    }

    private void EndDodge()
    {
        isDodging = false;
        int entitiesLayerIndex = LayerMask.NameToLayer("Entities");
        gameObject.layer = entitiesLayerIndex;
    }

    public bool IsDodging()
    {
        return isDodging;
    }

    public Vector3 ToMouseDirection() {
        Vector3 worldPos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 distToMouse = worldPos3D - transform.position;
        distToMouse.z = 0f;
        return distToMouse.normalized;
    }

    public void DealDamage(AttackParams attackParams) {
        stats.ApplyDamage(attackParams);
        if (stats.GetHP() <= 0) {
            // Game over
            // TODO: Death screen and player HP indicator
            Destroy(gameObject);
        }
    }

    public float GetCurrentStat(byte statId) {
        return stats.GetCurrentStat(statId);
    }

    /// <summary>
    /// Method for performing tasks in <c>Update</c>
    /// </summary>
    /// <returns>
    /// Boolean value escribing whether further inputs / actions should be processed in this update frame
    /// </returns>
    /// <param name="returnIfDodging"> Specify if dodge should cause return (returns <c>false</c>)</param>
    /// <param name="doGetDogdeInput"> Parameter specifing wheter dodge input should be considered in this update frame </param>
    protected bool OnUpdateTasks(
        bool returnIfDodging = true,
        bool doGetDogdeInput = true) {
        if(IsDodging() && returnIfDodging) {
            return false;
        }

        if (GetDodgeInput() && doGetDogdeInput)
        {
            Dodge(GetMovementInput());
            return false;
        }

        return true;
    }
    /// <summary>
    /// Method for performing tasks in <c>FixedUpdate</c>
    /// </summary>
    /// <returns>
    /// Boolean value escribing whether further inputs / actions should be processed in this fixed update frame
    /// </returns>
    /// <param name="returnIfDodging"> Specify if dodge should cause return (returns <c>false</c>)</param>
    /// <param name="doDodgeMovement"> Specify if dodge should be executed when <c>isDodging==true</c> </param>
    /// <param name="doFixedMovement"> Specify wheter movement input should be processed in this fixed update frame </param>
    protected bool OnFixedUpdateTasks(
        bool returnIfDodging = true,
        bool doDodgeMovement = true,
        bool doFixedMovement = true) {
        if(IsDodging() && returnIfDodging) {
            if(doDodgeMovement) {
                FixedDodgeMovement();
            }
            return false;
        }

        if (doFixedMovement) {
            FixedMovementOnRigidbody2D();
            return false;
        }

        return true;
    }

    public void GrantExpierience(int xpAmount) {
        expierienceHelper.GainExpierience(xpAmount);
        Debug.Log(expierienceHelper.XPdescription());
    }

    public void Heal(float health) {
        stats.Heal(health);
    }

    public void RefillAmmo() {
        var gun = FindAnyObjectByType<RangedWeaponAbstract>();
        gun.weaponParams.RefillAmmo();
    }
}

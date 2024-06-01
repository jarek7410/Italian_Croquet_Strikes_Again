using GameNamespace;
using UnityEngine;

namespace EnemyScript
{
    public class Rat:BasicEnemyAbstract
    {
        [SerializeField] protected EnemyMeleeAttack attackHitbox;
        [SerializeField] protected float attackOffset = .9f;
        [SerializeField] protected float attackTime = .2f;
        [SerializeField] protected float attackDistance = .8f;
        [SerializeField] protected float afterAttackCooldown = 0.6f;

        private void Start() {
            Debug.Log("Running Rat.Start()");
            CombinedInit();
            
        }

        private void FixedUpdate()
        {   
            FixedMovementOnRigidbody2D();
        }

        protected override Vector2 GetMovement()
        {
            if (IsFrozen()) {
                return Vector2.zero;
            }
            if (ToPlayerDistance() < attackDistance) {
                Attack();
                return Vector2.zero;
            }

            return NavMeshDirectionToPlayer();
        }

        protected override void Attack()
        {
            Vector2 toPlayer = ToPlayerDirection();
            Vector2 attackPosition = toPlayer * attackOffset
                + new Vector2(transform.position.x, transform.position.y);
            var attackInstance = Instantiate(
                attackHitbox, 
                attackPosition, 
                transform.rotation);
            attackInstance.Init(attackTime,
                stats.GetCurrentStat(EntityStats.MELEE_DAMAGE_ID));
            Freeze();
            Invoke(nameof(Unfreeze), afterAttackCooldown);
        }
    }
}
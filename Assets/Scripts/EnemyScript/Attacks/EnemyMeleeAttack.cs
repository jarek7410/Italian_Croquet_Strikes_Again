using GameNamespace;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [SerializeField] private Collider2D hitbox;
    private float _attackTime;
    private float _damage;
    private float _destroySelfTime = .05f;

    /// <summary>
    /// Initializes the attack with the specified attack time and damage.
    /// </summary>
    /// <param name="attackTime">The time it takes for the attack to become active.</param>
    /// <param name="damage">The amount of damage dealt by the attack.</param>
    public void Init(float attackTime, float damage) {
        _attackTime = attackTime;
        _damage = damage;
        if (hitbox == null) {
            hitbox = GetComponent<Collider2D>();
        }
        hitbox.enabled = false;
        Invoke(nameof(ExecuteAttack), _attackTime);
    }

    private void ExecuteAttack() {
        hitbox.enabled = true;
        Destroy(gameObject, _destroySelfTime);
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            var player = other.gameObject.GetComponent<PlayerAbstract>();
            player.DealDamage(new AttackParams(
                AttackParams.MELEE,
                _damage
            ));
            Debug.Log("Player hit - currnet hp " + player.stats.GetHP());
            Destroy(gameObject);
        }
    }
}

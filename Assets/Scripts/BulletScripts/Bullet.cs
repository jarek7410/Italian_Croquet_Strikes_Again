using Unity.VisualScripting;
using UnityEngine;

namespace GameNamespace
{   
    public class Bullet : MonoBehaviour {
        private float _bulletSpeed;
        private float _bulletLifetime;
        private float _bulletDamage;
        private Vector3 _direction;
        [SerializeField] private Collider2D hitbox;

        public void Init(float bulletSpeed, float bulletLifetime, float bulletDamage, Vector3 direction) {
            _bulletSpeed = bulletSpeed;
            _bulletLifetime = bulletLifetime;
            _bulletDamage = bulletDamage;
            _direction = direction;

            if (hitbox == null) {
                hitbox = gameObject.GetComponent<Collider2D>();
                if (hitbox == null) {
                    Debug.LogWarning("Bullet has to have a Collider2D component");
                    hitbox.isTrigger = true;
                }
            }

            Destroy(gameObject, _bulletLifetime);
        }

        public void Update() {
            Vector3 newPos = transform.position + _bulletSpeed * Time.deltaTime * _direction;
            transform.position = newPos;
        }
        
        private void OnCollisionEnter2D(Collision2D other) {
            string objectTag = other.gameObject.tag;

            switch (objectTag) {
                case "Enemy":
                    Debug.Log("Enemy collision");
                    var enemy = other.gameObject.GetComponent<BasicEnemyAbstract>();
                    if (enemy == null) {
                        Debug.Log("Something went wrong - enemy has no basicenemyabstract");
                    }
                    // TODO: Apply damage
                    break;
                case "Obstacle":
                    Debug.Log("Obstacle collision");
                    Destroy(gameObject);
                    break;
                default:
                    Debug.Log("Collision with something else: " + objectTag);
                    break;
            }
        }
    }
}
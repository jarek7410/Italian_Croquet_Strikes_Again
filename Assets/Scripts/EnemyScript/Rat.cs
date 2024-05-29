using UnityEngine;

namespace EnemyScript
{
    public class Rat:BasicEnemyAbstract
    {
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
            return NavMeshDirectionToPlayer();
        }
    }
}
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
            NavMeshMoveToPlayer();
        }

        protected override Vector2 GetMovement()
        {
            return Vector2.zero;
            Vector2 movementVector=Vector2.zero;
            Vector3 distToPlayer = player.transform.position - transform.position;
            Vector2 distToPlayer2D = new Vector2(distToPlayer.x, distToPlayer.y);

            if (distToPlayer2D.magnitude <= AwarnessDisdtanse)
            {
                movementVector = distToPlayer2D.normalized;
            }
            
            return movementVector;
        }
    }
}
using System;
using GameNamespace;
using Unity.Mathematics;
using Unity.VisualScripting;
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
            Vector2 vectorToClosestEnemy=Vector2.zero;
            float distance = AwarnessDisdtanse;
            foreach (var player in playerList)
            {
                var distanceNew=Vector2.Distance(this.transform.position, player.transform.position);
                if (distance > distanceNew)
                {
                    distance = distanceNew;
                    vectorToClosestEnemy = player.transform.position -this.transform.position;
                }
            }

            Debug.Log(vectorToClosestEnemy);
            return vectorToClosestEnemy.normalized;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private Collider2D collider2d;
    [SerializeField] private float health = 50f;
    void Start()
    {
        if (collider2d == null) {
            collider2d = GetComponent<Collider2D>();
        }

        collider2d.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var otherGameObject = other.gameObject;
        if (otherGameObject.tag == "Player") {
            var player = otherGameObject.GetComponent<PlayerAbstract>();
            player.Heal(health);
            Destroy(gameObject);
        }
    }
}

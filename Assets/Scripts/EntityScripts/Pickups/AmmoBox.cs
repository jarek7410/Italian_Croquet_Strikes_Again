using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private Collider2D collider2d;
    void Start()
    {
        if (collider2d == null) {
            collider2d = GetComponent<Collider2D>();
        }

        collider2d.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var otherGameObject = other.gameObject;
        Debug.Log(otherGameObject.tag);
        if (otherGameObject.tag == "Player") {
            var player = otherGameObject.GetComponent<PlayerAbstract>();
            player.RefillAmmo();
            Destroy(gameObject);
        }
    }
}

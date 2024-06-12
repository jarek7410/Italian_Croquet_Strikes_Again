using UnityEngine;

public class LevelExitPortal : MonoBehaviour
{
    private GameLogic gameLogic;
    [SerializeField] private Collider2D collider2d;
    void Start()
    {
        if (collider2d == null) {
            collider2d = GetComponent<Collider2D>();
        }
        collider2d.enabled = false;
        Invoke(nameof(ActivateCollider), 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (gameLogic == null) {
            gameLogic = GameLogic.Instance;
        }

        Debug.Log(other.gameObject);

        if (other.gameObject.tag.Equals("Player")) {
            gameLogic.OnLevelExit();
        }    
    }

    private void ActivateCollider() {
        collider2d.enabled = true;
    }

}

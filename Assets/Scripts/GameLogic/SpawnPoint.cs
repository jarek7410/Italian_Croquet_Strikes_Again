using UnityEngine;
public class SpawnPoint : MonoBehaviour {
    private GameLogic gameLogic;

    private void Start() {
        gameLogic = GameLogic.Instance;
        gameLogic.TeleportPlayer(transform.position);
    }

}
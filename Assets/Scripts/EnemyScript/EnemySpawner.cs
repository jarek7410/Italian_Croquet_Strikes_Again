using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameLogic gameLogic;
    [SerializeField]
    private float baseSpawnBudget = 2.5f;
    [SerializeField]
    private float spawnBudgetPerLevel = 0.2f;
    // Start is called before the first frame update
    void Update()
    {
        gameLogic = GameLogic.Instance;
        float totalBudget = baseSpawnBudget + spawnBudgetPerLevel * gameLogic.onLevel;
        var state = gameLogic.gameState;

        while (totalBudget > 0f) {
            var enemy = state.GetRandomEnemy();
            var enemyInstance = Instantiate(enemy, transform.position, Quaternion.identity);
            float enemyCost = enemyInstance.GetComponent<BasicEnemyAbstract>().spawnCost;
            totalBudget -= enemyCost;
        }

        gameLogic.FetchEnemies();
        Destroy(gameObject);
           
    }
}

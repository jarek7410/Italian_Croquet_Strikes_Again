using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/GameState")]
public class GameState : ScriptableObject{

    [SerializeField, Tooltip("Names os scenes (SceneAsset type doesnt work in build)\nRemember to add all scenes to build (File > Build Settings)")]
    string[] Levels;
    [SerializeField, Tooltip("List of enemies that will be spawned in levels")]
    GameObject[] EnemiesToSpawn;

    [SerializeField, Tooltip("ListOfGuns")]
    public GameObject[] weapons;
    private float[] enemiesRarities;
    private float cummulativeRarity;

    public string GetRandomLevel() {
        var level = Levels[Random.Range(0, Levels.Length)];
        return level;
    }

    public GameObject GetRandomEnemy() {
        float rand = Random.Range(0f, cummulativeRarity);
        int i = 0;
        while (true) {
            rand -= enemiesRarities[i];
            if (rand <= 0) {
                return EnemiesToSpawn[i];
            }
            i++;
        }
    }

    private void Awake() {
        // creating vector of enemy probabilities, used later to spawn enemies
        enemiesRarities = new float[EnemiesToSpawn.Length];
        cummulativeRarity = 0f;
        for (int i = 0; i < EnemiesToSpawn.Length; i++) {
            var enemyAbstract = EnemiesToSpawn[i].GetComponent<BasicEnemyAbstract>();
            if (enemyAbstract == null) {
                throw new UnityException("Passed enemy does not have BasicEnemyAbstractComponent");
            }
            enemiesRarities[i] = enemyAbstract.spawnRarity;
        }
        cummulativeRarity = enemiesRarities.Sum();
    }
}
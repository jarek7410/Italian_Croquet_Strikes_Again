using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance { get; private set; }
    private List<BasicEnemyAbstract> _enemies;
    private GameObject _playerInstance;
    private PlayerAbstract _player;
    private EntityStats _playerStats;
    [SerializeField]
    private GameState staringState;
    [SerializeField] 
    private GameObject playerPrefab;
    private GameState gameState;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Debug.Log("I exist!!");

        Instance = this;
        DontDestroyOnLoad(this);
        gameState = Instantiate(staringState);
        SpawnPlayer();
        FetchEnemies();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    public void FetchEnemies() {
        _enemies = FindObjectsByType<BasicEnemyAbstract>(FindObjectsSortMode.None).ToList();
    }

    public void OnEnemyDeath(BasicEnemyAbstract enemy) {
        Debug.Log("Enemy died: " + enemy.gameObject.name);
        _enemies.Remove(enemy);
        if (_enemies.Count == 0) {
            Debug.Log("All enemies dead");
            DestroyExitWalls();
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Debug.Log("OnSceneLoaded executed");
        TeleportPlayerToSpawn();
        FetchEnemies();
    }

    private void SpawnPlayer() {
        _playerInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        var _player = _playerInstance.GetComponent<PlayerAbstract>();
        if (_playerStats == null) {
            _playerStats = _player.stats;
        } else {
            _player.stats = _playerStats;
        }
    }

    private void TeleportPlayerToSpawn() {
        var spawnPoint = GameObject.FindGameObjectWithTag("Spawn");
        if (spawnPoint == null) {
            throw new Exception("No spawn point in scene");
        }
        if (_player == null) {
            return;
        }
        _player.transform.position = spawnPoint.transform.position;
    }

    public void OnLevelExit() {
        // Generate new level and teleport player
        var nextLevel = gameState.GetRandomLevel();
        SceneManager.LoadScene(nextLevel.name, LoadSceneMode.Single);
    }

    private void DestroyExitWalls() {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("LevelExitWall");

        foreach (var wall in walls) {
            Destroy(wall);
        }
    }
}

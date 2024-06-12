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
    public GameState gameState {get; private set;}
    public int onLevel {get; private set;} = 1;

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

    public void OnSceneEnter() {
        Debug.Log("OnSceneEnter executed");
        FetchEnemies();
        onLevel++;
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

    public void TeleportPlayer( Vector3 target) {
        Debug.Log("Recieved vector: " + target);
        if (_playerInstance == null) {
            return;
        }
        _playerInstance.transform.position = target;
    }

    public void OnLevelExit() {
        // Generate new level and teleport player
        var nextLevel = gameState.GetRandomLevel();
        SceneManager.LoadScene(nextLevel, LoadSceneMode.Single);
        OnSceneEnter();
    }

    private void DestroyExitWalls() {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("LevelExitWall");

        foreach (var wall in walls) {
            Destroy(wall);
        }
    }
}

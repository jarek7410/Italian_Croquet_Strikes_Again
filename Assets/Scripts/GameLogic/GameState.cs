using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/GameState")]
public class GameState : ScriptableObject{

    [SerializeField, Tooltip("Remember to add all scenes to build (File > Build Settings)")]
     SceneAsset[] Levels;

    public SceneAsset GetRandomLevel() {
        var level = Levels[Random.Range(0, Levels.Length)];
        return level;
    }
}
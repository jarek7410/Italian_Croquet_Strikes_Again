using GameNamespace;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar:MonoBehaviour
{
    [SerializeField] private TestPlayer player;
    [SerializeField] private Slider health;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null) {
            player = FindAnyObjectByType<TestPlayer>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        var maxHp = player.stats.GetCurrentStat(EntityStats.MAX_HP_ID);
        var hp = player.stats.GetHP();
        health.value = hp/maxHp;
    }
}

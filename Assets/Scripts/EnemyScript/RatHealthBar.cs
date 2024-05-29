using System.Collections;
using System.Collections.Generic;
using EnemyScript;
using GameNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rat))]
public class healthbar : MonoBehaviour
{
    
    [SerializeField] private Rat ratStat;
    [SerializeField] private Slider health;
    // Start is called before the first frame update
    void Start()
    {
        if (ratStat == null)
        {
            ratStat=GetComponent<Rat>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var maxHp = ratStat.stats.GetCurrentStat(EntityStats.MAX_HP_ID);
        var hp = ratStat.stats.GetHP();
        health.value = hp/maxHp;
    }
}

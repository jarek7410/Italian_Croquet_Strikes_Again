using System;
using Unity.VisualScripting;
using UnityEngine;
namespace GameNamespace
{
    public class EntityStats
{
    public static readonly byte MAX_HP_ID = 0;
    public static readonly byte SPEED_ID = 1;
    public static readonly byte MELEE_DAMAGE_ID = 2;
    public static readonly byte RANGED_DAMAGE_ID = 3;
    public static readonly byte MELEE_RESISTANCE_ID = 4;
    public static readonly byte RANGED_RESISTANCE_ID = 5;
    public static readonly byte FIRE_RESISTANCE_ID = 6;
    public static readonly byte TOXINE_RESISTANCE_ID = 7;
    private float HP;
    [SerializeField] private float baseMaxHP;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float baseMeleeDamage;
    [SerializeField] private float baseRangedDamage;
    [SerializeField] private float baseMeleeResistance;
    [SerializeField] private float baseRangedResistance;
    [SerializeField] private float baseFireResistance;
    [SerializeField] private float baseToxineResistance;

    public EntityStats(float baseMaxHP = 100f,
                float baseSpeed = 100f,
                float baseMeleeDamage = 30f,
                float baseRangedDamage = 20f,
                float baseMeleeResistance = 50f,
                float baseRangedResistance = 50f,
                float baseFireResistance = 50f,
                float baseToxineResistance = 50f) {
                    this.baseMaxHP = baseMaxHP;
                    this.baseSpeed = baseSpeed;
                    this.baseMeleeDamage = baseMeleeDamage;
                    this.baseRangedDamage = baseRangedDamage;
                    this.baseMeleeResistance = baseMeleeResistance;
                    this.baseRangedResistance = baseRangedResistance;
                    this.baseFireResistance = baseFireResistance;
                    this.baseToxineResistance = baseToxineResistance;

                    HP = baseMaxHP;
                }
    
    public float GetBaseStat(byte statId) {
        switch (statId) {
            case 0:
                return baseMaxHP;
            case 1:
                return baseSpeed;
            case 2:
                return baseMeleeDamage;
            case 3:
                return baseRangedDamage;
            case 4:
                return baseMeleeResistance;
            case 5: 
                return baseRangedResistance;
            case 6: 
                return baseFireResistance;
            case 7:
                return baseToxineResistance;
            default:
                throw new NotImplementedException();
        }
    }

    public float GetCurrentStat(byte statId) {
        // TODO: implement stat modfiers and stats calculations
        return GetBaseStat(statId); 
    }
}
}
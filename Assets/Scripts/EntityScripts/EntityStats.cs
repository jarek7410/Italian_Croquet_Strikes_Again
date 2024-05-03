using System;
using System.Collections.Generic;
using UnityEngine;
namespace GameNamespace
{
    public class EntityStats : ScriptableObject
{
    public static readonly byte MAX_HP_ID = 0;
    public static readonly byte SPEED_ID = 1;
    public static readonly byte MELEE_DAMAGE_ID = 2;
    public static readonly byte RANGED_DAMAGE_ID = 3;
    public static readonly byte MELEE_RESISTANCE_ID = 4;
    public static readonly byte RANGED_RESISTANCE_ID = 5;
    public static readonly byte FIRE_RESISTANCE_ID = 6;
    public static readonly byte TOXINE_RESISTANCE_ID = 7;
    public static readonly int NUM_STATS = 8;
    public static readonly float STAT_MIN_VALUE = 5f;
    private float HP;
    private float _nextExpiration;
    private float[,] _modifiersValues;
    private List<StatModifier> _modifiers;
    [SerializeField] private float baseMaxHP;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float baseMeleeDamage;
    [SerializeField] private float baseRangedDamage;
    [SerializeField] private float baseMeleeResistance;
    [SerializeField] private float baseRangedResistance;
    [SerializeField] private float baseFireResistance;
    [SerializeField] private float baseToxineResistance;

    public void SetEntityStats(float baseMaxHP = 100f,
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
                    Init();
    }

    private void Init()
    {
        HP = baseMaxHP;
        _nextExpiration = float.MaxValue;
        _modifiers = new List<StatModifier>();
        ResetModifierTable();
    }

    public void CheckForModifierExpiration()
    {
        if (Time.time >= _nextExpiration)
        {
            foreach (StatModifier modifier in _modifiers)
            {
                if (modifier.IsExpired())
                {
                    RemoveModifier(modifier);
                }
            }
        }
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
        float baseValue = GetBaseStat(statId);
        float currentValue = (baseValue + _modifiersValues[statId, 0]) * _modifiersValues[statId, 1];
        return Math.Max(currentValue, STAT_MIN_VALUE);
    }

    public void AddModifier(StatModifier modifier)
    {
        _modifiers.Add(modifier);
        if (modifier.HasExpireTime())
        {
            float modifierExpireTime = modifier.ExpiresAt();
            if (modifierExpireTime < _nextExpiration)
            {
                _nextExpiration = modifierExpireTime;
            }
        }
        ApplyModifier(modifier);
    }

    private void ApplyAllModifiers()
    {
        ResetModifierTable();
        foreach (var statModifier in _modifiers)
        {
            ApplyModifier(statModifier);
        }
    }

    public void RefreshModifierExpireTimer()
    {
        _nextExpiration = float.MaxValue;
        foreach (var modifier in _modifiers)
        {
            if (modifier.HasExpireTime())
            {
                if (modifier.ExpiresAt() < _nextExpiration)
                {
                    _nextExpiration = modifier.ExpiresAt();
                }
            }
        }
    }
    public void RemoveModifier(StatModifier modifier)
    {
        if (modifier.GetStatId() == MAX_HP_ID)
        {
            float increment = modifier.IsMultiplicative() ? -baseMaxHP * modifier.GetValue() : -modifier.GetValue();
            if (increment > 0f)
            {
                HP += increment;
            }
            else
            {
                HP = Math.Max(Math.Min(HP, STAT_MIN_VALUE), HP + increment);
            }
        }
        
        int columnIndex = modifier.IsMultiplicative() ? 1 : 0;
        _modifiersValues[modifier.GetStatId(), columnIndex] -= modifier.GetValue();
        _modifiers.Remove(modifier);
    }
    private void ApplyModifier(StatModifier modifier)
    {
        if (modifier.GetStatId() == MAX_HP_ID)
        {
            float increment = modifier.IsMultiplicative() ? baseMaxHP * modifier.GetValue() : modifier.GetValue();
            if (increment > 0f)
            {
                HP += increment;
            }
            else
            {
                HP = Math.Max(Math.Min(HP, STAT_MIN_VALUE), HP + increment);
            }
        }
        
        int columnIndex = modifier.IsMultiplicative() ? 1 : 0;
        _modifiersValues[modifier.GetStatId(), columnIndex] += modifier.GetValue();
    }

    private void ResetModifierTable()
    {
        _modifiersValues = new float[NUM_STATS, 2];
        for (int i = 0; i < NUM_STATS; i++)
        {
            _modifiersValues[i, 0] = 0f; // additive values
            _modifiersValues[i, 1] = 1f; // multiplicative values
        }
    }

    public void ApplyDamage(AttackParams attackParams)
    {
        byte defenceStatId = (byte)(attackParams.GetDamageTypeId() + 4);
        float defenceValue = GetCurrentStat(defenceStatId);
        float damageMultiplier = 100f / (100f + defenceValue);
        float damageDealt = attackParams.getDamageValue() * damageMultiplier;

        HP -= damageDealt;
    }

    private void OnValidate()
    {
        if (baseMaxHP < 1f)
        {
            baseMaxHP = 1f;
        }
        
        // TODO: Add remaining validations for stats
    }

    public float GetHP() {
        return HP;
    }
}
}
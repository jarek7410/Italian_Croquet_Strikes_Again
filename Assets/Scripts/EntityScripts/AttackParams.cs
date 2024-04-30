﻿using System;
using UnityEngine;

namespace GameNamespace 
{
    public class AttackParams  : ScriptableObject
    {
        public static readonly byte MELEE = 0;
        public static readonly byte RANGED = 1;
        public static readonly byte FIRE = 2;
        public static readonly byte TOXINE = 3;

        [SerializeField] private byte damageTypeId;
        [SerializeField] private float value;
        
        public AttackParams(byte damageTypeId, float value)
        {
            if (damageTypeId > 3)
            {
                throw new ArgumentException();
            }
            
            this.damageTypeId = damageTypeId;
            value = this.value;
        }
        public byte GetDamageTypeId()
        {
            return damageTypeId;
        }

        public float getDamageValue()
        {
            return value;
        }
    }
    
        
}
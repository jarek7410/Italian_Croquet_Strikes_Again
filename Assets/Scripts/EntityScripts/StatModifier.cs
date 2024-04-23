using System;
using UnityEngine;

// TODO: Need Testing

namespace GameNamespace
{
    public class StatModifier
    {
        private readonly byte _statId;
        private readonly float _value;
        private readonly bool _isMultiplicative;
        private readonly bool _hasExpireTime;
        private readonly float _expiresAt;
        private bool _isExpired;

        public StatModifier(byte statId,
            float value,
            bool isMultiplicative = false,
            bool hasExpireTime = false,
            float duration = 0f)
        {
            if (statId < 0 || statId >= EntityStats.NUM_STATS)
            {
                throw new ArgumentException($"statId must be in range 0-{EntityStats.NUM_STATS}");
            }
            if (hasExpireTime && duration == 0f)
            {
                throw new ArgumentException("duration cannot be equal to 0 when modifier ha expire time");
            }
            _statId = statId;
            _value = value;
            _isMultiplicative = isMultiplicative;
            _hasExpireTime = hasExpireTime;
            _expiresAt = Time.time + duration;

            if (hasExpireTime)
            {
                
            }
        }

        public bool IsExpired()
        {
            if (!_hasExpireTime)
            {
                return false;
            }

            return _expiresAt >= Time.time;
        }

        public bool HasExpireTime()
        {
            return _hasExpireTime;
        }

        public float ExpiresAt()
        {
            return _expiresAt;
        }

        public byte GetStatId()
        {
            return _statId;
        }

        public bool IsMultiplicative()
        {
            return _isMultiplicative;
        }

        public float GetValue()
        {
            return _value;
        }
        
    }
}
using System;
using UnityEngine;

namespace GameNamespace
{
    [CreateAssetMenu(fileName = "MeleeWeaponParams")]
    public class MeleeWeaponParams : ScriptableObject
    {
        [Range(0.01f, 3f), Tooltip("Amount of time the attack takes")]
        public float swingTime = 0.5f;
        [Range(0.01f, 3f), Tooltip("Amount of time needed to be waited before next attack")]
        public float rechargeTime = 0.2f;
        [Range(0.01f, 360f), Tooltip("Width of the swing in degrees")]
        public float swingDegrees = 90f;
        [Range(0f, 100f), Tooltip("Force applied to enemise when hit (Impulse)")]
        public float knockbackForce = 10f;



        private void OnValidate()
        {
            if (rechargeTime < 0.01f)
            {
                rechargeTime = 0.01f;
            }
        }
    }
}

using System;
using UnityEngine;

namespace GameNamespace
{
    [CreateAssetMenu(fileName = "RangedWeaponParams")]
    public class RangedWeaponParams : ScriptableObject
    {
        [Serializable]
        public class BulletParams
        {
            [Range(0, 10), Tooltip("Bullet Life Time")]
            public float bulletLifetime = 5f;
            [Range(0, 200)]
            public float bulletSpeed = 8f;
        }
        public bool isAutomatic = false;
        public int  magazineSize = 6;
        private int bulletsInMagazine;
        public float betweenShotsTime = .4f; 
        public float reloadTime = 1.5f;
        public float spreadDegrees = 1f;
        public BulletParams bulletParams;

        private void OnValidate()
        {
            if (reloadTime < 0.01f)
            {
                reloadTime = 0.01f;
            }

            if(magazineSize < 1) {
                magazineSize = 1;
            }
            bulletsInMagazine = magazineSize;
        }

        public void DecrementBullets() {
            bulletsInMagazine--;
            Debug.Log($"Bullets {bulletsInMagazine}/{magazineSize}");
        }

        public void ReloadBullets() {
            bulletsInMagazine = magazineSize;
        }

        public int BulletsLeft() {
            return bulletsInMagazine;
        }
    }
}

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
        public float damageMultiplier = 1f;
        public int bulletsPerShot = 1;
        public int  magazineSize = 6;
        public int ammoReserveSize = 150;
        private int bulletsInReserve;
        public int bulletsInAmmoBoxes = 90;
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
            bulletsInReserve = ammoReserveSize;
        }

        public void DecrementBullets() {
            bulletsInMagazine--;
            Debug.Log($"Bullets {bulletsInMagazine}/{magazineSize}, {bulletsInReserve} left in stock");
        }

        public void ReloadBullets() {
            bulletsInReserve += bulletsInMagazine;
            int reloadedCount = bulletsInReserve >= magazineSize ? magazineSize : bulletsInReserve;
            bulletsInMagazine = reloadedCount;
            bulletsInReserve -= reloadedCount;
        }

        public void RefillAmmo() {
            bulletsInReserve += bulletsInAmmoBoxes;
            if (bulletsInReserve > ammoReserveSize) {
                bulletsInReserve = ammoReserveSize;
            }
        }

        public int BulletsLeft() {
            return bulletsInMagazine;
        }
    }
}

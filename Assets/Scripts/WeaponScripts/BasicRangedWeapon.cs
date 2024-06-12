using UnityEngine;

namespace GameNamespace{
    public class BasicRangedWeapon: RangedWeaponAbstract{
        private void Start() {
            InitPlayer();
            weaponParams.Init();
        }

        private void Update() {
            if (!IsEquiped()) {
                return;
            }
            Draw();
            if (player.IsDodging()) {
                return;
            }
            if (isReloading) {
                return;
            }
            if (GetShootInput() && isShotReady) {
                ShootBullet();
                return;
            }
            if (GetReloadInput()) {
                Reload();
            }

        }
    }
}
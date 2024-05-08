using UnityEngine;

namespace GameNamespace{
    public class BasicRangedWeapon: RangedWeaponAbstract{
        private void Start() {
            InitPlayer();
        }

        private void Update() {
            if (!IsEquiped()) {
                return;
            }
            Draw();
            if (isReloaded && !player.IsDodging()) {
                if (GetShootInput()) {
                    ShootBullet();
                }
            }
        }
    }
}
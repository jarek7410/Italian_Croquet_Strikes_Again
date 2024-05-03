using UnityEngine;

namespace GameNamespace{
    public class BasicRangedWeapon: RangedWeaponAbstract{
        private void Start() {
            InitPlayerComponents();
            Equip();
        }

        private void Update() {
            if (!isEquiped) {
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
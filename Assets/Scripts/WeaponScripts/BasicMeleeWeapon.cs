using UnityEngine;

public class BasicMeleeWeapon : MeleeWeaponAbstract {
    private void Start() {
        InitHitbox();
        InitPlayer();
        Equip();
    }

    private void Update() {
        if (!isEquiped) {
            return;
        }
        Draw();
        if( !isRecharged || player.IsDodging() ) {
            return;
        }
        if(GetAttackInput()) {
            Swing();
        }
    }
}
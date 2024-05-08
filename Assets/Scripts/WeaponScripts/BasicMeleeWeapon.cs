using UnityEngine;

public class BasicMeleeWeapon : MeleeWeaponAbstract {
    private void Start() {
        InitHitbox();
        InitPlayer();
    }

    private void Update() {
        if (!IsEquiped()) {
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
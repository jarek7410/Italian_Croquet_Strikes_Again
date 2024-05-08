using UnityEngine;

public class WeaponEquipment : MonoBehaviour {
    [SerializeField] private WeaponAbstract weapon1;
    [SerializeField] private WeaponAbstract weapon2;
    private byte _equipedWeaponId;

    private void Start() {
        weapon1.Equip();
        weapon2.Unequip();
        _equipedWeaponId = 1;
    }

    private void Update() {
        if (_equipedWeaponId == 1 && Input.GetKeyDown(KeyCode.Alpha2)) {
            weapon2.Equip();
            weapon1.Unequip();
            _equipedWeaponId = 2;
            return;
        }
        if (_equipedWeaponId == 2 && Input.GetKeyDown(KeyCode.Alpha1)) {
            weapon1.Equip();
            weapon2.Unequip();
            _equipedWeaponId = 1;
            return;
        }
    }
}
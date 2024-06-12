using UnityEngine;

public class WeaponEquipment : MonoBehaviour {
    [SerializeField] public GameObject weapon1object;
    [SerializeField] public GameObject weapon2object;
    private WeaponAbstract weapon1;
    private WeaponAbstract weapon2;
    private byte _equipedWeaponId;

    private void Start() {
        weapon1 = weapon1object.GetComponent<WeaponAbstract>();
        weapon2 = weapon2object.GetComponent<WeaponAbstract>();
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

    public void SetWeapon1(GameObject weaponObject) {
        weapon1object = weaponObject;
        weapon1 = weapon1object.GetComponent<WeaponAbstract>();
    }
}
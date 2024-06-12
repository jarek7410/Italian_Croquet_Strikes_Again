using UnityEngine;

public class WeaponEquipment : MonoBehaviour {

    [SerializeField] private WeaponAbstract smg;
    [SerializeField] private WeaponAbstract shotgun;
    [SerializeField] private WeaponAbstract revolver;
    public WeaponAbstract weapon1;
    [SerializeField]private WeaponAbstract weapon2;
    private byte _equipedWeaponId;

    private void Start() {

        switch (ChosenSettings.Instance.weaponId) {
            case 0:
            weapon1 = smg;
            Destroy(shotgun.gameObject);
            Destroy(revolver.gameObject);
            break;
            case 1:
            weapon1 = shotgun;
            Destroy(smg.gameObject);
            Destroy(revolver.gameObject);
            break;
            case 2:
            weapon1 = revolver;
            Destroy(smg.gameObject);
            Destroy(shotgun.gameObject);
            break;
        }

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
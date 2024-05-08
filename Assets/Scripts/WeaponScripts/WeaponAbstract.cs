using UnityEngine;

public abstract class WeaponAbstract : MonoBehaviour {
    private bool _isEquiped = false;

    public bool IsEquiped() {
        return _isEquiped;
    }

    public void Equip() {
        _isEquiped = true;
        gameObject.SetActive(true);
    }

    public void Unequip() {
        _isEquiped = false;
        gameObject.SetActive(false);
    }
}
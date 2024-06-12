using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Reloadcomunicat : MonoBehaviour
{
    [SerializeField] private GameObject reloadText;
    [SerializeField] private RangedWeaponAbstract weapon;

    [SerializeField] private TMP_Text buletCountText;

    [SerializeField] private GameObject reloadGrafic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        buletCountText.SetText("x "+weapon.weaponParams.BulletsLeft());
        reloadGrafic.SetActive(weapon.isReloading);
        reloadText.SetActive(weapon.weaponParams.BulletsLeft()<=0 && !weapon.isReloading);
    }
}

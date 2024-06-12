using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using GameNamespace;
using UnityEngine;
using UnityEngine.UI;

public class gunsSelection : MonoBehaviour
{
    [SerializeField] private Toggle smg;

    [SerializeField] private Toggle shotgun;

    [SerializeField] private Toggle revolver;

    [SerializeField] private GameObject smgBasicRangedWeapon;
    [SerializeField] private GameObject revolverBasicRangedWeapon;
    [SerializeField] private GameObject shotgunBasicRangedWeapon;
    // [SerializeField] private Gun[] gunsList;
    public void setWepon()
    { 
        var weponczousesetting = ChosenSettings.Instance;
        if (smg.isOn)
        {
            weponczousesetting.chosenWeapon = smgBasicRangedWeapon;
            return;
        }

        if (shotgun.isOn)
        {
            weponczousesetting.chosenWeapon = shotgunBasicRangedWeapon;
            return;
        }

        if (revolver.isOn)
        {
            weponczousesetting.chosenWeapon = revolverBasicRangedWeapon;
            return;
        }
    }
    
}

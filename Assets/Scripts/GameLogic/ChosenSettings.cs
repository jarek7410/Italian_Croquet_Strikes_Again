using GameNamespace;
using UnityEngine;

public class ChosenSettings {
    // singleton
    public static ChosenSettings Instance {get; private set;} = new ChosenSettings();
    public int weaponId;
}
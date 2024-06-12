using GameNamespace;

public class ChosenSettings {
    // singleton
    public static ChosenSettings Instance {get; private set;} = new ChosenSettings();
    public BasicRangedWeapon chosenWeapon;
}
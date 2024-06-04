
public class ExpierienceHelper {
    private int expierience = 0;
    private int Level = 1;
    private int levelUpInterval = 15;
    private int levelUpIncrement = 5;
    private int nextLevelAt;

    public ExpierienceHelper() {
        nextLevelAt = levelUpInterval;
    }
    public void GainExpierience(int xp) {
        expierience += xp;
        CheckForLevelUp();
    }

    public int GetLevel() {
        return Level;
    }

    private void CheckForLevelUp() {
        if (expierience >= nextLevelAt) {
            Level++;
            levelUpInterval += levelUpIncrement;
            nextLevelAt += levelUpInterval;
            OnLevelUp();

            CheckForLevelUp();
        }
    }

    public float LevelUpProgress() {
        float currentLevelExp = nextLevelAt - expierience;
        return currentLevelExp / levelUpInterval;
    }

    public string XPdescription() {
        return $"Level {Level}, {expierience}/{nextLevelAt} XP, {(int)(100 * LevelUpProgress())}% to next level";
    }

    private void OnLevelUp() {
        // TODO: ImplementLeveling
    }

}
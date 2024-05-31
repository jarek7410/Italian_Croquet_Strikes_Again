using UnityEngine;

namespace GameNamespace {
    public class TestPlayer : PlayerAbstract {
    private void Start() {
        Debug.Log("Running TestPlayer.Start()");
        CombinedInit();
    }

    private void Update()
    {
        OnUpdateTasks();
    }

    private void FixedUpdate() {
        OnFixedUpdateTasks();
    }

}
}
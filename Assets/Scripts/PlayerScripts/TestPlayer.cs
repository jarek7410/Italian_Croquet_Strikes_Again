using UnityEngine;

namespace GameNamespace {
    public class TestPlayer : PlayerAbstract {
        [SerializeField] private bool debugOnUpdate = true;
    private void Start() {
        Debug.Log("Running TestPlayer.Start()");
        CombinedInit();
    }

    private void FixedUpdate() {
        if (IsDodging())
        {
            FixedDodgeMovement();
            return;
        }

        if (GetDodgeInput())
        {
            Dodge(GetMovementInput());
            return;
        }
        
        if (debugOnUpdate) {
            Debug.Log("Running TestPlayer.FixedUpdate");
            Debug.Log(GetMovementInput());
        }
        FixedMovementOnRigidbody2D();
    }

}
}
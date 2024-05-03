using UnityEngine;

namespace GameNamespace {
    public class TestPlayer : PlayerAbstract {
    private void Start() {
        Debug.Log("Running TestPlayer.Start()");
        CombinedInit();
    }

    private void Update()
    {
        if (GetDodgeInput())
        {
            Dodge(GetMovementInput());
            return;
        }
    }

    private void FixedUpdate() {
        if (IsDodging())
        {
            FixedDodgeMovement();
            return;
        }        
        FixedMovementOnRigidbody2D();
    }

}
}
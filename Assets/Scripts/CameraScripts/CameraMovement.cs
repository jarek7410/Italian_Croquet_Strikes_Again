using UnityEngine;

namespace GameNamespace
{
    public class CameraMovement : MonoBehaviour
    {
        private float _z;
        [SerializeField] private float cameraSpeed = .9f;
        [SerializeField] private GameObject followedGameObject;
        void Start()
        {
            _z = transform.position.z;
            if (followedGameObject == null)
            {
                followedGameObject = GameObject.FindGameObjectWithTag("Player");
            }

            Vector3 pos = new Vector3(followedGameObject.transform.position.x,
                followedGameObject.transform.position.y, _z);
            transform.position = pos;
        }
        void FixedUpdate()
        {
            Vector3 targetPosition = followedGameObject.transform.position;
            targetPosition.z = _z;
            Vector3 moveVector = (targetPosition - transform.position) * cameraSpeed;
            transform.position += moveVector;
        }
    }
}
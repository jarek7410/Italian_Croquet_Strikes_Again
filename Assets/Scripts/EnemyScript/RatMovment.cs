using System;
using System.Collections;
using System.Collections.Generic;
using GameNamespace;
using UnityEngine;
using UnityEngine.AI;

public class RatMovment : MonoBehaviour
{
    private Vector3 target;

    private NavMeshAgent agent;
    [SerializeField] private GameObject targetObject;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (targetObject == null) {
            var player = FindFirstObjectByType<PlayerAbstract>();
            if (player == null)
            {
                throw new Exception("no player found");
            }
            targetObject = player.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetTargetPosition();
        SetAgentPosition();
    }

    void SetTargetPosition()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     target = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        // }
        target = targetObject.transform.position;
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y,transform.position.z));
    }
}

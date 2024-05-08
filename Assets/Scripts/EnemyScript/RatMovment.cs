using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RatMovment : MonoBehaviour
{
    private Vector3 target;

    private NavMeshAgent agent;
    [SerializeField] private GameObject TargetObject;
    

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Start is called before the first frame update
    void Start()
    {
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
        target = TargetObject.transform.position;
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y,transform.position.z));
    }
}

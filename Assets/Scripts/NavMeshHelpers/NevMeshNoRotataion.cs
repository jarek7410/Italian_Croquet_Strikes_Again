using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NevMesh : MonoBehaviour
{
    void Start()	{
        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
}

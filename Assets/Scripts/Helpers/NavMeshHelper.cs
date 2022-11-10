using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class NavMeshHelper
{
    public static bool ReachedDestinationOrGaveUp(this NavMeshAgent navMeshAgent) 
    { 
        if (!navMeshAgent.pathPending) 
        { 
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) 
            { if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f) 
                { return true; 
                } 
            } 
        } return false; 
    }

    public static bool TryGetClosestPointOnNavMesh(Vector3 position, out Vector3 navMeshPosition)
    {
        navMeshPosition = Vector3.zero;
        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(position, out myNavHit, 100, -1))
        {
            navMeshPosition = myNavHit.position;
            return true;
        }
        return false;
    }
}

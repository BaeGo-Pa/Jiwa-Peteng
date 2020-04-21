using System;
using System.Collections;
using System.Collections.Generic;
using Jiwa.Peteng;
using UnityEngine;
using UnityEngine.AI;

public class Contrôle_ennemi : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.LocalPlayerInstance.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                FaceTarget();
            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = lookRotation;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}

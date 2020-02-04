using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Mover : MonoBehaviour
{
    [SerializeField] Transform target;
    private NavMeshAgent navMeshAgent;
    Ray lastRay;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            MoveToCursor(Input.mousePosition);
        }
        Debug.DrawRay(lastRay.origin, lastRay.direction * 100);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            MoveToCursor(touch.position);
        }


    }

    private void MoveToCursor(Vector3 device)
    {
        lastRay = Camera.main.ScreenPointToRay(device);
        RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(lastRay, out hitInfo);
        if (hasHit)
        {
            navMeshAgent.SetDestination(hitInfo.point);
        }
    }
}

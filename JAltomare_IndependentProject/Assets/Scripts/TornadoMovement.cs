using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TornadoMovement : MonoBehaviour
{

    //[SerializeField] private NavMeshAgent agent;
    //[SerializeField] private Transform target;

    public Transform TornadoRoute;
    public List<Transform> Locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitializeTornadoMovement();
        MoveToNextTornadoLocation();
    }

    // Update is called once per frame
    private void Update()
    {
        if (agent.remainingDistance < 0.2f && !agent.pathPending) 
        {
            MoveToNextTornadoLocation();
        }
    }
    void InitializeTornadoMovement()
    {
        foreach (Transform child in TornadoRoute)
        {
            Locations.Add(child);
        }
    }
    void MoveToNextTornadoLocation()
    {
        if (Locations.Count == 0)
            return;

        agent.destination = Locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % Locations.Count;
    }

    void OnTriggerEnter(Collider other)
    {
        
    }
    void OnTriggerExit(Collider other)
    {
        
    }
}


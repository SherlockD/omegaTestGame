using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIPlayerMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Vector3 _nextPosition;
    private Vector3 _startPosition;

    public bool IsPlayerMove => transform.position != _startPosition && (transform.position != _nextPosition );

    private void Awake()
    {
        _nextPosition = transform.position;
        _startPosition = transform.position;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
    }

    public void MoveTo(Vector3 position)
    {
        _nextPosition = position;
        StartCoroutine(PlayerMove(position));
    }

    IEnumerator PlayerMove(Vector3 position)
    {
        _startPosition = transform.position;
        
        if (position - transform.position != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation((position-transform.position));
            transform.rotation = lookRotation;
        }

        _navMeshAgent.destination = position;
        yield break;
    }
}

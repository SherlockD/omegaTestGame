using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIPlayer : MonoBehaviour
{
    public event Action CallOnCollideWithCoin;
    
    [SerializeField] private GameObject _coinObject;
    
    private AIPlayerMovement _playerMovement;

    private void Awake()
    {
        _playerMovement = GetComponent<AIPlayerMovement>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);     
            CallOnCollideWithCoin?.Invoke();
        }
    }
    
    private void FixedUpdate()
    {        
        if (!_playerMovement.IsPlayerMove)
        {
            _playerMovement.MoveTo(GetRandomPoint(transform.position, 4));
        }
    }

    public Vector3 GetRandomPoint(Vector3 center, float maxDistance)
    {
        Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas);

        return hit.position;
    }
}

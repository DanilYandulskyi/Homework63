using UnityEngine;
using System;

[RequireComponent(typeof(Mover))]
public class Unit : MonoBehaviour
{
    [SerializeField] private Resource _resource;
    [SerializeField] private Vector3 _initialPosition;
    [SerializeField] private Vector3 _moveDirection;

    private Mover _mover;
    private float _distanceMapToStop = 0.2f;

    public event Action<Resource> CollectedResource;

    public bool IsResourceCollected { get; private set; }
    public bool IsStanding { get; private set; } = true;

    private void Start()
    {
        _mover = GetComponent<Mover>();
        _initialPosition = transform.position;
    }

    private void Update()
    {
        if (_resource != null)
        {
            _mover.Move(_moveDirection);
            IsStanding = false;
        }

        if (IsResourceCollected && Vector3.Distance(transform.position, _initialPosition) < _distanceMapToStop)
        {
            _resource.gameObject.SetActive(false);
            CollectedResource?.Invoke(_resource);
            _mover.Stop();
            IsResourceCollected = false;
            IsStanding = true;
            _resource = null;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Resource resource))
        {
            if (resource == _resource)
            {
                IsResourceCollected = true;
                SetDirectionToBase();
                resource.Attach(transform);
            }
        }
    }

    public void SetTargetResource(Resource resource)
    {
        _resource = resource;
        _resource.BecomeStalking();
        _moveDirection = _resource.transform.position - transform.position;
    }

    public void SetDirectionToBase()
    {
        _moveDirection = _initialPosition - transform.position;
    }
}

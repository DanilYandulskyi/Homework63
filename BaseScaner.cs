using UnityEngine;
using System.Collections.Generic;

public class BaseScaner : MonoBehaviour
{
    [SerializeField] private float _scaningRadius;

    public List<Resource> ScanBase()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _scaningRadius);

        List<Resource> resources = new List<Resource>();

        foreach(Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Resource resource))
            {
                if (resource.IsBeingStalking == false)
                {
                    resources.Add(resource);
                }
            }
        }

        return resources;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private List<Resource> _resources = new List<Resource>();
    [SerializeField] private float _interval;

    private void Awake()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (enabled)
        {
            Spawn();
            yield return new WaitForSeconds(_interval);
        }
    }

    private void Spawn()
    {
        int minValue = -5;
        int maxValue = 5;

        Instantiate(_resources[Random.Range(0, _resources.Count)],
        new Vector3(Random.Range(minValue, maxValue), 0, Random.Range(minValue, maxValue)), Quaternion.identity);
    }
}

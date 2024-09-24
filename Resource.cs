using UnityEngine;

public class Resource : MonoBehaviour
{
    public bool IsBeingStalking { get; private set; }

    public void Attach(Transform transform)
    {
        this.transform.SetParent(transform);
    }

    public void BecomeStalking()
    {
        IsBeingStalking = true;
    }
}
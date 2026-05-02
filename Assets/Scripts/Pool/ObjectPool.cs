using UnityEngine;
using System.Collections.Generic;

// Generic Object Pool. Attach to a GameObject, assign a prefab in Inspector.
// Reduces GC pressure by reusing runtime objects instead of Instantiate/Destroy.
public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int initialSize = 10;

    private readonly Queue<GameObject> available = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < initialSize; i++)
            Grow();
    }

    private void Grow()
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        available.Enqueue(obj);
    }

    public GameObject Get()
    {
        if (available.Count == 0)
            Grow();

        GameObject obj = available.Dequeue();
        obj.SetActive(true);
        obj.GetComponent<IPoolable>()?.OnSpawn();
        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.GetComponent<IPoolable>()?.OnDespawn();
        obj.SetActive(false);
        obj.transform.SetParent(transform);
        available.Enqueue(obj);
    }
}

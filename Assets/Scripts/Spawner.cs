using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [Header("References and values")]
    [SerializeField] GameObject fencePrefab;
    [SerializeField] int fenceAmount;
    [SerializeField] float minSpawnDistance;

    [Header("Spawning Bounds")]
    [SerializeField] Vector2 maxBounds;
    [SerializeField] Vector2 minBounds;

    List<GameObject> fences = new List<GameObject>();

    private void Update()
    {
        CheckFenceAmount();
    }

    void CheckFenceAmount()
    {
        fences.RemoveAll(fence => fence == null);

        if (fences.Count <  fenceAmount)
        {
            GameObject newFence = Instantiate(fencePrefab, SpawnPosition(), Quaternion.identity, transform);
            fences.Add(newFence);
        }
    }

    Vector2 SpawnPosition()
    {
        Vector2 pos = new Vector2(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y));

        while (Physics2D.OverlapCircle(pos, minSpawnDistance))
        {
            pos = new Vector2(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y));
        }

        return pos;
    }
}

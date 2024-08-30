using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [System.Serializable]
    public class CoinSpawnGroup
    {
        public Transform parentObject;       // The parent GameObject for this group of coins
        public Transform[] spawnPoints;      // Array of spawn points for this parent
    }

    public GameObject coinPrefab;             // The coin prefab to spawn
    public CoinSpawnGroup[] coinSpawnGroups;  // Array of coin spawn groups

    private void Start()
    {
        // Spawn coins for each group
        foreach (var group in coinSpawnGroups)
        {
            SpawnCoinsForGroup(group);
        }
    }

    private void SpawnCoinsForGroup(CoinSpawnGroup group)
    {
        foreach (var spawnPoint in group.spawnPoints)
        {
            SpawnCoin(spawnPoint, group.parentObject);
        }
    }

    private void SpawnCoin(Transform spawnPoint, Transform parentObject)
    {
        GameObject coin = Instantiate(coinPrefab, spawnPoint.position, Quaternion.identity);
        coin.transform.SetParent(parentObject); // Set the parent of the coin
    }
}

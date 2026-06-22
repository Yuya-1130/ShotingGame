using UnityEngine;

public class TragetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public float spawnInterval = 1.0f;

    public float upwardForce = 12f;
    public float forwardForce = 8f;

    public BoxCollider spawnAreaCollider;

    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnInterval)
        {
            SpawnTarget();
            timer = 0f;
        }
    }
    void SpawnTarget()
    {
        if (spawnAreaCollider == null) return;

        Vector3 center = spawnAreaCollider.bounds.center;
        Vector3 size = spawnAreaCollider.bounds.size;

        float randomX = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        float randomY = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);
        Vector3 spawnPosition = new Vector3(randomX, randomY, center.z);

        GameObject newTarget = Instantiate(targetPrefab, spawnPosition, Quaternion.identity);

        // Rigidbodyを取得して、斜め上へ力を加える
        Rigidbody rb = newTarget.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 左右どちらに飛ばすか判定
            float directionX = spawnAreaCollider.transform.position.x < 0 ? 1f : -1f;

            // ランダムに少し角度をバラつかせる
            float forceX = directionX * forwardForce * Random.Range(0.8f, 1.2f);
            float forceY = upwardForce * Random.Range(0.9f, 1.1f);

            // 力（速度）を与える
            rb.linearVelocity = new Vector3(forceX, forceY, 0f);
        }
    
    }
}

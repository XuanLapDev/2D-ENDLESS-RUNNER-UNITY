using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs; // Mảng chứa các prefab chướng ngại vật
    [SerializeField] private float obstacleSpawnTime = 2f; // Thời gian giữa các lần sinh chướng ngại vật
    [SerializeField] private float obstacleSpeed = 5f;     // Tốc độ di chuyển của chướng ngại vật

    private float timeUntilObstacleSpawn = 0f; // Bộ đếm thời gian giữa các lần sinh

    private void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            SpawnLoop(); // Kiểm tra và sinh chướng ngại vật
        }
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= obstacleSpawnTime)
        {
            Spawn(); // Tạo chướng ngại vật mới
            timeUntilObstacleSpawn = 0f; // Reset thời gian
        }
    }

    private void Spawn()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0)
        {
            Debug.LogError("Spawner: Không có obstaclePrefab nào được gán!");
            return;
        }

        // Chọn ngẫu nhiên một prefab
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // Tạo đối tượng từ prefab
        Vector3 spawnPosition = transform.position;

        // Kiểm tra tên prefab để chỉ chỉnh vị trí với Projectile Obstacle
        if (obstacleToSpawn.name.Contains("Projectille Obstacle"))
        {
            spawnPosition.y += 1f; // Điều chỉnh chiều cao spawn chỉ cho Projectile Obstacle
            Debug.Log("ProjectileObstacle đã được tạo cao hơn. Vị trí spawn: " + spawnPosition);
        }

        // Tạo đối tượng từ prefab với vị trí đã điều chỉnh
        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, spawnPosition, Quaternion.identity);

        // Kiểm tra vị trí sau khi instantiate
        Debug.Log("ProjectileObstacle được spawn tại vị trí: " + spawnedObstacle.transform.position);

        // Thiết lập Rigidbody2D (phải tồn tại trong prefab)
        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        if (obstacleRB != null)
        {
            obstacleRB.linearVelocity = Vector2.left * obstacleSpeed; // Gán tốc độ di chuyển
        }
        else
        {
            Debug.LogWarning("Spawner: Prefab không có Rigidbody2D!");
        }
    }
}
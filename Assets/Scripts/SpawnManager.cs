using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
 
    public GameObject[] obstaclePrefabs; // Arreglo de prefabs de obstáculos
    private float spawnInterval = 2.0f;   // Tiempo entre spawns
    private float yPosition = 0.75f;
    private float xPosition = -10f;
    private float[] zPositions = new float[] { -3f, 0f, 3f };

    void Start()
    {
        // Llama a SpawnObstacle repetidamente cada cierto tiempo
        InvokeRepeating("SpawnObstacle", 2.0f, spawnInterval);
    }

    void SpawnObstacle()
    {
        // Elegir un prefab al azar
        int prefabIndex = Random.Range(0, obstaclePrefabs.Length);
        GameObject obstacle = obstaclePrefabs[prefabIndex];

        // Elegir posición Z al azar
        float randomZ = zPositions[Random.Range(0, zPositions.Length)];

        // Crear posición de aparición
        Vector3 spawnPosition = new Vector3(xPosition, yPosition, randomZ);

        // Instanciar el obstáculo en la escena
        Instantiate(obstacle, spawnPosition, obstacle.transform.rotation);
    }

}


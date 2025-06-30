using UnityEngine;
using System.Collections.Generic;

public class SpawnPowerUp : MonoBehaviour
{
    [Header("Configuración")]
    public List<GameObject> powerUpPrefabs;    // Lista de prefabs de power-ups
    public float minSpawnTime = 15f;
    public float maxSpawnTime = 30f;

    private float timer;

    void Start()
    {
        ReiniciarTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnPowerUpAleatorio();
            ReiniciarTimer();
        }
    }

    void SpawnPowerUpAleatorio()
    {
        if (powerUpPrefabs.Count == 0) return;

        // Elegir prefab aleatorio
        GameObject prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Count)];

        // Posiciones posibles en Z
        float[] carrilesZ = { -3f, 0f, 3f };
        float z = carrilesZ[Random.Range(0, carrilesZ.Length)];

        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, z);
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    void ReiniciarTimer()
    {
        timer = Random.Range(minSpawnTime, maxSpawnTime);
    }
}

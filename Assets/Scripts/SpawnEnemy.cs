using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Configuración del Spawn")]
    public List<GameObject> enemigosPrefabs; // Lista de tipos de enemigos
    public GameObject efectoSpawnPrefab;     // Prefab del efecto visual de invocación
    public int poolSizePorTipo = 5;
    public float spawnRate = 2f;

    private List<GameObject> pool = new List<GameObject>();
    private float timer;

    void Start()
    {
        // Crear una pool para cada tipo de enemigo
        foreach (GameObject prefab in enemigosPrefabs)
        {
            for (int i = 0; i < poolSizePorTipo; i++)
            {
                GameObject enemigo = Instantiate(prefab);
                enemigo.SetActive(false);
                pool.Add(enemigo);
            }
        }

        timer = spawnRate;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            StartCoroutine(ActivarEnemigoConSpawnFX());
            timer = spawnRate;
        }
    }

    IEnumerator ActivarEnemigoConSpawnFX()
    {
        // Elegimos un prefab aleatorio de la lista
        int tipoAleatorio = Random.Range(0, enemigosPrefabs.Count);
        GameObject prefabSeleccionado = enemigosPrefabs[tipoAleatorio];

        // Carriles posibles en Z
        float[] carrilesZ = { -3f, 0f, 3f };
        float zAleatorio = carrilesZ[Random.Range(0, carrilesZ.Length)];
        Vector3 posicionSpawn = new Vector3(transform.position.x, transform.position.y, zAleatorio);

        // Instanciar el efecto de spawn
        if (efectoSpawnPrefab != null)
        {
            GameObject fx = Instantiate(efectoSpawnPrefab, posicionSpawn, Quaternion.identity);
            Destroy(fx, 2f); // opcional: destruir el efecto después de 2s
        }

        yield return new WaitForSeconds(2f); // esperar antes de que aparezca el enemigo

        foreach (GameObject enemigo in pool)
        {
            if (!enemigo.activeInHierarchy && enemigo.name.StartsWith(prefabSeleccionado.name))
            {
                enemigo.transform.position = posicionSpawn;
                enemigo.SetActive(true);
                AudioSource audio = GetComponent<AudioSource>();
                if (audio != null && !audio.isPlaying)
                {
                    audio.Play();
                }
                break;
            }
        }
    }
}


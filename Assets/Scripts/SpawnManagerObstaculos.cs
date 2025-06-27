using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerObstaculos : MonoBehaviour
{
    [Header("Configuracion del Spawn")]
    public List<GameObject> ObstaclePrefabs; // Lista de tipos de obst�culos
    public int poolSizePorTipo = 5;
    public float spawnRate = 2f;

    private List<GameObject> pool = new List<GameObject>();
    private float timer;

    void Start()
    {
        // Crear una pool para cada tipo de obst�culo
        foreach (GameObject prefab in ObstaclePrefabs)
        {
            for (int i = 0; i < poolSizePorTipo; i++)
            {
                GameObject obstaculo = Instantiate(prefab);
                obstaculo.SetActive(false);
                pool.Add(obstaculo);
            }
        }

        timer = spawnRate;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            ActivarObstaculoAleatorio();
            timer = spawnRate;
        }
    }

    void ActivarObstaculoAleatorio()
    {
        // Elegimos un prefab aleatorio de la lista
        int tipoAleatorio = Random.Range(0, ObstaclePrefabs.Count);
        GameObject prefabSeleccionado = ObstaclePrefabs[tipoAleatorio];

        // Carriles posibles en Z
        float[] carrilesZ = { -3f, 0f, 3f };
        float zAleatorio = carrilesZ[Random.Range(0, carrilesZ.Length)];

        foreach (GameObject obstaculo in pool)
        {
            if (!obstaculo.activeInHierarchy && obstaculo.name.StartsWith(prefabSeleccionado.name))
            {
                // Posicionar en el mismo X/Y del SpawnManager, pero en Z aleatorio
                Vector3 nuevaPos = new Vector3(transform.position.x, transform.position.y, zAleatorio);
                obstaculo.transform.position = nuevaPos;

                // Reiniciar movimiento f�sico si tiene Rigidbody
                Rigidbody rb = obstaculo.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.Sleep(); // Asegura que no est� "activo" f�sicamente
                }

                // (Opcional) Reiniciar scripts de movimiento propios si los tiene
                // Por ejemplo:
                // obstaculo.GetComponent<ObstacleMover>()?.Resetear();

                obstaculo.SetActive(true);
                break;
            }
        }
    }
}



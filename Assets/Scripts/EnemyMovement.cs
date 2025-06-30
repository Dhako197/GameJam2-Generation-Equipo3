using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agente;
    private Transform objetivo;

    private float[] carrilesZ = { -3f, 0f, 3f };
    private float velocidadCambioZ = 10f;
    private float objetivoZActual;

    public float puntoLimiteX = 6f; // Punto en el eje X donde deja de seguir al jugador
    public float velocidadLineaRectaX = 20f; // Velocidad cuando ya no sigue al jugador
    public float puntoDesaparicionX = 20f;   // Punto en el eje X donde se desactiva
    private bool seguirAlJugador = true;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();

        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            objetivo = jugador.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'Player'.");
        }

        objetivoZActual = CarrilMasCercano(transform.position.z);
    }

    void OnEnable()
    {
        seguirAlJugador = true;

        if (agente == null)
            agente = GetComponent<NavMeshAgent>();

        // Reposicionar sobre el NavMesh si es necesario
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} reapareció fuera del NavMesh.");
        }

        agente.enabled = true;
    }

    void Update()
    {
        if (objetivo != null && seguirAlJugador)
        {
            // Condición para dejar de seguir
            if (transform.position.x >= puntoLimiteX)
            {
                seguirAlJugador = false;
                agente.enabled = false; // Desactivar el NavMeshAgent para movimiento libre
                return;
            }

            // Carril más cercano al jugador
            float nuevoCarrilZ = CarrilMasCercano(objetivo.position.z);
            if (Mathf.Abs(nuevoCarrilZ - objetivoZActual) > 0.1f)
            {
                objetivoZActual = nuevoCarrilZ;
            }

            // Mover manualmente en Z
            Vector3 pos = transform.position;
            pos.z = Mathf.MoveTowards(pos.z, objetivoZActual, velocidadCambioZ * Time.deltaTime);
            transform.position = pos;

            // NavMesh mueve en X
            Vector3 destino = new Vector3(objetivo.position.x, transform.position.y, transform.position.z);
            agente.SetDestination(destino);
        }
        else
        {
            // Movimiento recto en X una vez dejado de seguir
            transform.position += Vector3.right * velocidadLineaRectaX * Time.deltaTime;

            if (transform.position.x >= puntoDesaparicionX)
            {
                // Desactiva todas las balas del pool del enemigo antes de desactivarlo
                ProjectilePooler pooler = GetComponent<ProjectilePooler>();
                if (pooler != null)
                {
                    pooler.DisableAllProjectiles();
                }

                // Sumar puntaje
                ScoreManager.Instance?.SumarPuntaje(10); // Puedes ajustar el valor
                gameObject.SetActive(false);
            }
        }
    }

    float CarrilMasCercano(float z)
    {
        float masCercano = carrilesZ[0];
        float distanciaMin = Mathf.Abs(z - masCercano);

        foreach (float carril in carrilesZ)
        {
            float distancia = Mathf.Abs(z - carril);
            if (distancia < distanciaMin)
            {
                masCercano = carril;
                distanciaMin = distancia;
            }
        }

        return masCercano;
    }
}

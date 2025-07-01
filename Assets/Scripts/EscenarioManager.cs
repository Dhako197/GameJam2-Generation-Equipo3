using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EscenarioManager : MonoBehaviour
{
    private Transform[] bloquesEscenario; // Asigna aquí los 4 bloques en el Inspector
    public Transform[] bloquesLaboratorio;
    public Transform[] bloquesCiudad;
    public float velocidad = 5f;         // Velocidad del desplazamiento
    public float limiteX = 40f;          // Límite a la derecha para reposicionar
    public float distanciaEntreBloques = 0.08f; // Espacio entre bloques, puedes ajustar según tu escena

    public GameObject laboratorio;
    public GameObject Ciudad;
    public ParticleSystem particulaCambioEscenario;
    public GameObject spawnObstacle;
    public GameObject spawnEnemy;
    public GameObject spawnPowerUp;

    bool cambioEscena = (false);


    private void Start()
    {
        bloquesEscenario = bloquesLaboratorio;
    }
    void Update()
    {
        int puntaje = ScoreManager.Instance.GetPuntaje();
        bool enLaboratorio = GameManager.Instance.enLaboratorio(puntaje);
        if (!enLaboratorio && !cambioEscena)
        {
            spawnEnemy.SetActive(false);
            spawnObstacle.SetActive(false);
            spawnPowerUp.SetActive(false);
            particulaCambioEscenario.Stop();
            particulaCambioEscenario.Play();
            StartCoroutine("cambiarEscenario");
            cambioEscena = (true);
        }

        foreach (Transform bloque in bloquesEscenario)
        {
            // Mover el bloque a la derecha (sentido negativo en la vista del jugador)
            bloque.Translate(Vector3.right * velocidad * Time.deltaTime);

            // Si pasa el límite, lo reposicionamos al final
            if (bloque.position.x >= limiteX)
            {
                // Buscar el bloque más a la izquierda
                Transform ultimoBloque = ObtenerUltimoBloque();
                float nuevaX = ultimoBloque.position.x - distanciaEntreBloques;
                bloque.position = new Vector3(nuevaX, bloque.position.y, bloque.position.z);
            }
        }
    }

    Transform ObtenerUltimoBloque()
    {
        Transform ultimo = bloquesEscenario[0];
        foreach (Transform bloque in bloquesEscenario)
        {
            if (bloque.position.x < ultimo.position.x)
            {
                ultimo = bloque;
            }
        }
        return ultimo;
    }

    IEnumerator cambiarEscenario()
    {
        yield return new WaitForSeconds(5f);

        GameObject particulasCambioEscenario = particulaCambioEscenario.gameObject;
        particulaCambioEscenario.Stop();
        Destroy( particulaCambioEscenario );
        bloquesEscenario = bloquesCiudad;
        limiteX = 120;
        distanciaEntreBloques = 120;
        laboratorio.SetActive(false);
        Ciudad.SetActive(true);
        spawnEnemy.SetActive(true);
        spawnObstacle.SetActive(true);
        spawnPowerUp.SetActive(true);



    }
}

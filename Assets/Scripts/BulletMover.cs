using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 45f;

    private Vector3 startPosition;
    private ParticleSystem efectoParticula;

    private void Awake()
    {
        efectoParticula = GetComponentInChildren<ParticleSystem>();
    }

    void OnEnable()
    {
        startPosition = transform.position;
        if (efectoParticula != null)
        {
            efectoParticula.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            efectoParticula.Simulate(1f, true, true); // Omitir lo anterior
            efectoParticula.Play();
        }
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        if (distanceTravelled >= maxDistance)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Impactó al jugador");
            gameObject.SetActive(false);
        }
        else if (!other.isTrigger)
        {
            gameObject.SetActive(false);
        }
    }
}

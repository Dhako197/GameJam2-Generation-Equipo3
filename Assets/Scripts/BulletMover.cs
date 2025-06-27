using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 45f;

    private Vector3 startPosition;

    void OnEnable()
    {
        startPosition = transform.position;
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

using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float knockbackForce = 500f;
    private Rigidbody rb;

    private float speed = 20f;          // Velocidad de movimiento
    private float minZ = -3f;          // Límite inferior en Z
    private float maxZ = 3f;           // Límite superior en Z
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputZ = Input.GetAxis("Horizontal"); // Usa W/S o flechas ↑↓
        Vector3 move = new Vector3(0, 0, inputZ) * speed * Time.deltaTime;

        transform.Translate(move);

        // Limitar el movimiento en Z
        Vector3 pos = transform.position;
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.position = pos;

        if (transform.position.x > 13f)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < -2)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Aplica una fuerza hacia atrás y hacia arriba
            Vector3 knockbackDirection = (transform.position - collision.transform.position).normalized + Vector3.up;
            rb.AddForce(knockbackDirection * knockbackForce);

            /*
            if (currentLives > 0)
            {
                StartCoroutine(RespawnAfterDelay(1.5f)); // Tiempo para que "vuele"
            }
            else
            {
                Debug.Log("Game Over");
                // Aquí podrías añadir lógica de game over
            }*/
        }

    }
}



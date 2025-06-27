using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 10f;      // Velocidad de movimiento
    public float minX = -6f;      // Límite izquierdo
    public float maxX = 13f;      // Límite derecho

    private int direction = 1;    // Dirección actual (1 = derecha, -1 = izquierda)

    void Update()
    {
        // Movimiento en eje X
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
        if (transform.position.x > 13f)
        {
            Destroy(gameObject);
        }
        if(transform.position.y<-2)
            Destroy(gameObject);
    }
}

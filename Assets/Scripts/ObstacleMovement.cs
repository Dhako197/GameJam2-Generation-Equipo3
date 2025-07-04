using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 10f;      // Velocidad de movimiento
    public float minX = -6f;      // L�mite izquierdo
    public float maxX = 13f;      // L�mite derecho

    private int direction = 1;    // Direcci�n actual (1 = derecha, -1 = izquierda)

    void Update()
    {
        // Movimiento en eje X
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
        if (transform.position.x > 13f) {
            
                // Sumar puntaje
                ScoreManager.Instance?.SumarPuntaje(5); // Puedes ajustar el valor
                gameObject.SetActive(false);
            
        }
        if (transform.position.y < -2)
        {
            // Sumar puntaje
            ScoreManager.Instance?.SumarPuntaje(5); // Puedes ajustar el valor
            gameObject.SetActive(false);
        }
    }
}

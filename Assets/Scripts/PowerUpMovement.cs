using UnityEngine;

public class PowerUpMovement : MonoBehaviour
{
    public float speed = 10f;      // Velocidad de movimiento
    public float minX = -6f;      // Límite izquierdo
    public float maxX = 13f;      // Límite derecho

    private int direction = 1;    // Dirección actual (1 = derecha, -1 = izquierda)
    public Vector3 velocidadRotacion = new Vector3(0f, 90f, 0f); // grados por segundo
    void Update()
    {
        // Movimiento
        transform.Rotate(velocidadRotacion * Time.deltaTime, Space.Self);
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

        if (transform.position.x > 13f)
        {
            
            // Sumar puntaje
            //ScoreManager.Instance?.SumarPuntaje(5); // Puedes ajustar el valor
            Destroy(gameObject);

        }
        if (transform.position.y < -2)
        {
            // Sumar puntaje
            //ScoreManager.Instance?.SumarPuntaje(5); // Puedes ajustar el valor
            Destroy(gameObject);
        }
    }
}

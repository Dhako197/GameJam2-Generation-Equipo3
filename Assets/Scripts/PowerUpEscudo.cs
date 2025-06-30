using UnityEngine;

public class PowerUpEscudo : MonoBehaviour
{
    private bool recogido = false;

    private void OnTriggerEnter(Collider other)
    {
        if (recogido) return; // evita colisiones múltiples

        if (other.CompareTag("Player"))
        {
            PlayerShield shield = other.GetComponent<PlayerShield>();
            if (shield != null)
            {
                shield.ActivarEscudo();
            }

            recogido = true; // evitar que se active más de una vez
            Destroy(gameObject);
        }
    }
}


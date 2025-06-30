using UnityEngine;

public class PowerUpVida : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Buscar el script que maneja las vidas
            VidaUIManager vidaManager = FindObjectOfType<VidaUIManager>();
            if (vidaManager != null)
            {
                vidaManager.AgregarVida();
            }

            // Desactivar el objeto despu�s de recogerlo
            Destroy(gameObject);
        }
    }
}


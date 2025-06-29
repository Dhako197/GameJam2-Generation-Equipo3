using UnityEngine;
using System.Collections;

public class EnemyShooter : MonoBehaviour
{
    public Transform firePoint;
    public float fireRate = 1f;

    private float timer;
    private ProjectilePooler pooler;
    private bool puedeDisparar = false;


    [Header("Efectos de disparo")]
    public ParticleSystem shootParticle;
    public AudioClip shootSound;

    void Awake()
    {
        pooler = GetComponent<ProjectilePooler>();

        if (firePoint == null)
        {
            firePoint = transform.Find("FirePoint");
            if (firePoint == null)
            {
                Debug.LogError("FirePoint no asignado ni encontrado como hijo. Asignalo manualmente.");
            }
        }
    }

    void OnEnable()
    {
        timer = fireRate;
        StartCoroutine(DelayDisparo());
    }

    IEnumerator DelayDisparo()
    {
        puedeDisparar = false;
        yield return new WaitForSeconds(0.2f); // Permite que el enemigo se posicione
        puedeDisparar = true;
    }

    void Update()
    {
        if (!puedeDisparar || firePoint == null)
            return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Shoot();
            timer = fireRate;
        }
    }

    void Shoot()
    {
        GameObject proj = pooler.GetProjectile();

        // Posición del firePoint
        proj.transform.position = firePoint.position;

        // Rotación levemente hacia abajo
        Vector3 direccion = (firePoint.forward + Vector3.down * 0.1f).normalized;
        proj.transform.rotation = Quaternion.LookRotation(direccion);

        proj.SetActive(true);

        if (shootParticle != null)
        {
            shootParticle.transform.position = firePoint.position;
            shootParticle.transform.rotation = firePoint.rotation;
            shootParticle.Play();
        }

        // Reproducir sonido (si está asignado)
        if (shootSound != null)
        {
            AudioSource.PlayClipAtPoint(shootSound, firePoint.position);
        }
    }
}

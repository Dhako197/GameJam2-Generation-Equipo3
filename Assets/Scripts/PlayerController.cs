using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float knockbackForce = 500f;
    private Rigidbody rb;

    public float laneOffset = 3f;            // Distancia entre carriles
    public float laneSwitchSpeed = 10f;      // Velocidad de interpolación entre carriles
    private int currentLane = 1;             // 0 = izquierda, 1 = centro, 2 = derecha

    public float jumpForce = 7f;
    public float fastFallForce = 20f;
    private bool isGrounded = true;
    
    private Vector3 targetPosition;
    
    private bool noHit = true;
    
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.S) && !isGrounded)
        {
            rb.AddForce(Vector3.down * fastFallForce, ForceMode.Acceleration);
        }
        
        // Movimiento lateral por carriles
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
        {
            currentLane--;
            SetTargetPosition();
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentLane < 2)
        {
            currentLane++;
            SetTargetPosition();
        }
        
        // Movimiento hacia el carril objetivo
        if (noHit)
        {
            Vector3 newPos = new Vector3(
                transform.position.x,
                transform.position.y,
               Mathf.Lerp(transform.position.z, targetPosition.z,Time.deltaTime * laneSwitchSpeed ));

            transform.position = newPos;
           
        }
        

        // Limitar caída o salir del área
        if (transform.position.x > 100f) // o usa un valor adecuado
        {
            Destroy(gameObject);
        }

        if (transform.position.y < -2f)
        {
            Destroy(gameObject);
        }
    }

    void SetTargetPosition()
    {
        float z = (currentLane - 1) * laneOffset; // -1 para centrar el carril medio en z=0
        Vector3 currentPosition = transform.position;
        targetPosition = new Vector3(currentPosition.x, currentPosition.y, z);
    }
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            noHit = false;
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}



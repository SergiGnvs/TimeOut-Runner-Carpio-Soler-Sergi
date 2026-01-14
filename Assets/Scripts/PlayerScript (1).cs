using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 6f;
    private bool faceRight = true;
    public bool isGroundedScript = true;
    public bool isMoving;

    public ContadorScript contador;
    public TMP_Text mensajeVictoria;

    PlayerInput input;
    private Rigidbody2D rb;
    private Animator characterAnimator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();

        mensajeVictoria.gameObject.SetActive(false);

        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Movimiento basado en f�sica, siempre en FixedUpdate
    private void FixedUpdate()
    {

        //Obtener el input del usuario
        float horizontalMovement = Input.GetAxis("Horizontal");

        float absoluteHorizontalMovement = Mathf.Abs(horizontalMovement);


        rb.linearVelocityX = horizontalMovement * speed;
        //characterAnimator.SetFloat("VelocityY", rb.linearVelocityY);

        //Salto
        if (Input.GetAxis("Jump") > 0 && isGroundedScript)
        {
            rb.linearVelocityY = 0f;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGroundedScript = false;
            
        }
        
            
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (!isGroundedScript && rb.linearVelocityY < 0.0f &&  hit.collider != null &&  hit.distance < 1f)
        {

            isGroundedScript = true;
            characterAnimator.SetBool("isGrounded", isGroundedScript);
            
        }
        //Cambiar direcci�n del sprite seg�n al lado que vaya
        
        if (horizontalMovement > 0 && !faceRight)
        {
            Turn();
        }
        else if (horizontalMovement < 0 && faceRight)
        {
            Turn();
        }

        if (Mathf.Abs(horizontalMovement) > 0.1f)
        {
            characterAnimator.SetBool("isMoving", true);
        }
        else
        {
            characterAnimator.SetBool("isMoving", false);
        }


    }
    void Turn()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        faceRight = !faceRight;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Verificar si el objeto con el que colisionamos tiene el tag "Muerte"
        if (col.gameObject.CompareTag("Muerte"))
        {
            // Reinicia la escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (col.gameObject.CompareTag("Checkpoint"))
        {
            // Suma tiempo al contador
            if (contador != null)
            {
                int tiempoSumado = 5;
                contador.SumarTiempo(tiempoSumado);
            }

            // Destruye checkpoint
            Destroy(col.gameObject);
        }

        if (col.gameObject.CompareTag("Victoria"))
        {
            if (mensajeVictoria != null)
            {
                mensajeVictoria.gameObject.SetActive(true);
                mensajeVictoria.text = "¡Has ganado!";
            }

            // Reiniciar escena después de 1 segundo
            Invoke(nameof(ReiniciarEscena), 3f);
        }
    }
    void ReiniciarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

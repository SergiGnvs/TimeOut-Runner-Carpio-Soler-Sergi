using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 6f;
    private bool faceRight = true;
    public bool isGroundedScript = true;
    

    private Rigidbody2D rb;
    private Animator characterAnimator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();

        //Evitar que colisione consigo mismo :D
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Movimiento basado en física, siempre en FixedUpdate
    private void FixedUpdate()
    {

        //Obtener el input del usuario
        float horizontalMovement = Input.GetAxis("Horizontal");

        float absoluteHorizontalMovement = Mathf.Abs(horizontalMovement);

        characterAnimator.SetFloat("MovementSpeed", absoluteHorizontalMovement);

        rb.linearVelocityX = horizontalMovement * speed;
        //characterAnimator.SetFloat("VelocityY", rb.linearVelocityY);

        if (Input.GetAxis("Jump") > 0 && isGroundedScript)
        {
            rb.linearVelocityY = 0f;
            Debug.Log("Jumping");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGroundedScript = false;
            
        }
        
            
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (!isGroundedScript && rb.linearVelocityY < 0.0f &&  hit.collider != null &&  hit.distance < 0.6f)
        {

            Debug.Log($"He colisionado {hit.collider.gameObject.name} a distancia " +  hit.distance);
            isGroundedScript = true;
            characterAnimator.SetBool("isGrounded", isGroundedScript);
            
        }
        //Cambiar dirección del sprite según al lado que vaya
        /*
        if (horizontalMovement > 0 && !faceRight)
        {
            Turn();
        }
        else if (horizontalMovement < 0 && faceRight)
        {
            Turn();
        }
        */

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
            // Reiniciar la escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

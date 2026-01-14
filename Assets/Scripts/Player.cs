using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // 1.Declaramos el Rigidbody2D y un vector2 de direccion para el movimiento para poder utilizarlos
    private Rigidbody2D rb;
    public float movementSpeed = 10.0f;
    PlayerInput input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //2.Inicializamos los componentes para poder utilizarlos más adelante
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {
        
        //float movement = Input.GetAxis("Horizontal");
        //GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * movement * movementSpeed;

    }
}

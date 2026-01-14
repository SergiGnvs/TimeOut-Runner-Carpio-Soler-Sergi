using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public float speed = 1.0f;
    public float LOOP = 5.0f;
    public float looptime = 5.0f;
    public float direction = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        looptime = LOOP;
    }

    // Update is called once per frame
    void Update()
    {
        looptime -= Time.deltaTime;

        if(looptime < 0)
        {
            direction = direction * -1.0f;
            looptime = LOOP;
        }
        transform.position = new Vector2(transform.position.x + direction * speed * Time.deltaTime, transform.position.y);
        
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("Estoy colisionando con la plataforma");
            collision.gameObject.transform.parent = transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.parent = null;
        }
    }
}

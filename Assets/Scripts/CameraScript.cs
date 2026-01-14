using UnityEditor;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3 offset;
    public Transform playerPosition;
    public float smoothing = 5.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        offset = transform.position - playerPosition.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = playerPosition.position + offset;
        transform.position = Vector3.Lerp(transform.position, playerPosition.position + offset, smoothing * Time.deltaTime);
    }
}

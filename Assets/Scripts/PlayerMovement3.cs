using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement3 : MonoBehaviour
{
    public Camera cam;
    private Vector3 offset;

    private Vector3 currentDirection;
    private readonly float speed = 15.0f;
    private int puntos = 0;

    private bool isGrounded = true;
    private Rigidbody rb;
    public float jumpForce = 7f;

    void Start()
    {
        offset = new Vector3(0, 10, -10); // Vista "cenital"
        cam.transform.position = transform.position + offset;
        cam.transform.LookAt(transform.position);
        currentDirection = Vector3.forward;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        cam.transform.position = transform.position + offset;

        if (Input.GetKey(KeyCode.W))
        {
            currentDirection = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentDirection = Vector3.back;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            currentDirection = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            currentDirection = Vector3.right;
        }
        transform.Translate(speed * Time.deltaTime * currentDirection, Space.World);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (puntos >= 10)
        {
            SceneManager.LoadScene("Win");
        }

        if (transform.position.y < -10)
        {
            if (transform.position.y < -20)
            {
                SceneManager.LoadScene("Level3");
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGrounded && collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Prize"))
        {
            Console.WriteLine("Prize collected");
            Destroy(other.gameObject);
            puntos++;
        }
    }
}

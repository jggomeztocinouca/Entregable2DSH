using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement3 : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clipPrize;
    public AudioClip clipObstacle;

    public Camera cam;
    private Vector3 offset;
    public Text contador;
    private Vector3 currentDirection;
    private float speed = 15.0f;
    private int puntos = 0;

    private bool isGrounded = true;
    private bool obstacleHit = false;
    private Rigidbody rb;
    public float jumpForce = 7f;

    void Start()
    {
        Vector3 playerPosition = transform.position;
        offset = new Vector3(0, 10, -10); // Vista "cenital"
        cam.transform.position = playerPosition + offset;
        cam.transform.LookAt(playerPosition);
        currentDirection = Vector3.forward;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        cam.transform.position = transform.position + offset;
        if (!obstacleHit)
        {
            if (Input.GetKey(KeyCode.W))
            {
                currentDirection = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                currentDirection = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                currentDirection = Vector3.right;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                currentDirection = Vector3.back;
            }
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

        if (transform.position.y < -20)
        {
            SceneManager.LoadScene("PreLevel3");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGrounded && collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene("PreLevel3");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Prize"))
        {
            Console.WriteLine("Prize collected");
            Destroy(other.gameObject);
            puntos++;
            contador.text = "" + (20 + puntos);
            source.PlayOneShot(clipPrize);
        }
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Console.WriteLine("Obstacle hit");
            obstacleHit = true;
            currentDirection = Vector3.zero;
            rb.velocity = Vector3.zero; // Detiene el movimiento en todas las direcciones
            rb.angularVelocity = Vector3.zero; // Detiene la rotación
            source.PlayOneShot(clipObstacle);
            // Espera 2 segundos y reinicia el nivel
            Invoke("RestartLevel", 2);
        }
    }
}

using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    private Vector3 offset;
    public GameObject floor;
    float xValue, zValue;
    private Vector3 currentDirection;
    private readonly float speed = 10.0f;

    void Start()
    {
        // Los valores X y Z determinan la posición horizontal de la cámara respecto al jugador.
        // El valor Y determina la altura de la cámara respecto al suelo.
        offset = new Vector3(0, 10, -10); // Vista "cenital"

        cam.transform.position = transform.position + offset;
        cam.transform.LookAt(transform.position); // Hace que la cámara mire hacia el jugador desde el nuevo ángulo.

        GenerateInitialFloor();
        currentDirection = Vector3.forward;
    }

    void Update()
    {
        cam.transform.position = transform.position + offset;
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ChangeDirection();
        }
        transform.Translate(speed * Time.deltaTime * currentDirection, Space.World); // Asegura movimiento relativo al mundo
    }

    void GenerateInitialFloor()
    {
        for (int i = 0; i < 3; i++)
        {
            zValue += 6.0f;
            Instantiate(floor, new Vector3(xValue, 0, zValue), Quaternion.identity);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            StartCoroutine(DestroyFloor(other.gameObject));
        }
    }

    IEnumerator DestroyFloor(GameObject floor)
    {
        float random = Random.Range(0.0f, 1.0f);
        if (random > 0.5)
        {
            xValue += 6.0f;
        }
        else
        {
            zValue += 6.0f;
        }
        Instantiate(floor, new Vector3(xValue, 0, zValue), Quaternion.identity);
        yield return new WaitForSeconds(0);
        if (floor != null) // Verifica si el suelo aún existe para evitar errores
        {
            var rb = floor.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            yield return new WaitForSeconds(2);
            Destroy(floor);
        }
    }

    void ChangeDirection()
    {
        currentDirection = currentDirection == Vector3.forward ? Vector3.right : Vector3.forward;
    }
}

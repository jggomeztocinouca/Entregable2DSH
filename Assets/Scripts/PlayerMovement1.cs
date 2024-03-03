using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement1 : MonoBehaviour
{
    public Text contador;
    public Camera cam;
    private Vector3 offset;
    public GameObject floor;
    public GameObject[] premios; // Almacenar los premios en un arreglo
    float xValue, zValue;
    private Vector3 currentDirection;
    private readonly float speed = 10.0f;
    private int puntos = 0; // Contador de puntos

    private readonly float probabilidadPremio = 0.1f;

    void Start()
    {
        offset = new Vector3(0, 10, -10); // Vista "cenital"
        cam.transform.position = transform.position + offset;
        cam.transform.LookAt(transform.position);
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
        transform.Translate(speed * Time.deltaTime * currentDirection, Space.World);

        if (puntos >= 10)
        {
            SceneManager.LoadScene("PreLevel2");
        }


        if (transform.position.y < -20)
        {
            SceneManager.LoadScene("PreLevel1");
        }
    }

    void GenerateInitialFloor()
    {
        for (int i = 0; i < 3; i++)
        {
            zValue += 6.0f;
            var newFloor = Instantiate(floor, new Vector3(xValue, 0, zValue), Quaternion.identity);
            // Decidir aleatoriamente si generar un premio en este suelo
            if (UnityEngine.Random.Range(0f, 1f) < probabilidadPremio)
            {
                GenerateRandomPrize(newFloor.transform.position);
            }
        }
    }

    void GenerateRandomPrize(Vector3 floorPosition)
    {
        if (premios.Length == 0) return;

        int randomIndex = UnityEngine.Random.Range(0, premios.Length);
        Vector3 prizePosition = floorPosition + new Vector3(0, 1.7f, 0); // Asegurar que el premio esté por encima del suelo
        // Generar el premio y rotarlo en X -90 grados
        Instantiate(premios[randomIndex], prizePosition, Quaternion.Euler(-90, 0, 0));
        // Instantiate(premios[randomIndex], prizePosition, Quaternion.identity);        
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            StartCoroutine(DestroyFloor(other.gameObject));
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
        contador.text = "" + puntos;
    }

    IEnumerator DestroyFloor(GameObject floor)
    {
        float random = UnityEngine.Random.Range(0.0f, 1.0f);
        if (random > 0.5)
        {
            xValue += 6.0f;
        }
        else
        {
            zValue += 6.0f;
        }
        var newFloor = Instantiate(floor, new Vector3(xValue, 0, zValue), Quaternion.identity);
        // Decidir aleatoriamente si generar un premio en el nuevo suelo
        if (UnityEngine.Random.Range(0f, 1f) < probabilidadPremio)
        {
            GenerateRandomPrize(newFloor.transform.position);
        }
        yield return new WaitForSeconds(0);
        if (floor != null)
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

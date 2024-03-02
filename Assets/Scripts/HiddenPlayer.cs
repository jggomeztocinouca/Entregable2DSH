using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HiddenPlayer : MonoBehaviour
{
    public GameObject player; // Jugador original
    public GameObject floor; // Prefab del suelo
    public GameObject[] premios; // Array de premios
    public GameObject obstaculo;
    public float probabilidadPremio = 0.1f; // Probabilidad de generar un premio

    // Variables para generar suelos y premios
    float xValue, zValue;
    enum lastDirectionEnum { forward, left, right };
    private lastDirectionEnum lastDirection = lastDirectionEnum.forward;

    void Start()
    {
        transform.position = player.transform.position;
        GenerateInitialFloor();
    }

    void Update()
    {
        if (player.transform.position.y < 0)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, 0.8f, player.transform.position.z);
        }
    }

    void GenerateInitialFloor()
    {
        for (int i = 0; i < 3; i++)
        {
            zValue += 6.0f;
            var newFloor = Instantiate(floor, new Vector3(xValue, 0, zValue), Quaternion.identity);
            if (Random.Range(0f, 1f) < probabilidadPremio)
            {
                GenerateRandomPrize(newFloor.transform.position);
            }
        }
    }

    void GenerateRandomPrize(Vector3 floorPosition)
    {
        if (premios.Length == 0) return;

        int randomIndex = Random.Range(0, premios.Length);
        Vector3 prizePosition = floorPosition + new Vector3(0, 3.0f, 0);
        Instantiate(obstaculo, floorPosition + new Vector3(0, 1.0f, 0), Quaternion.Euler(0, 0, 0));
        Instantiate(premios[randomIndex], prizePosition, Quaternion.Euler(-90, 0, 0));
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            StartCoroutine(DestroyFloor(other.gameObject));
        }
    }

    private void NewDirection(float x, float z)
    {
        float random = Random.Range(0.0f, 1.0f);
        if (random < 0.33 && lastDirection != lastDirectionEnum.right)
        {
            xValue = x + 6.0f;
            lastDirection = lastDirectionEnum.left;
        }
        else if (random < 0.66 && lastDirection != lastDirectionEnum.left)
        {
            xValue = x - 6.0f;
            lastDirection = lastDirectionEnum.right;
        }
        else
        {
            zValue = z + 6.0f;
            lastDirection = lastDirectionEnum.forward;
        }
    }

    IEnumerator DestroyFloor(GameObject floor)
    {
        NewDirection(xValue, zValue);
        var newFloor = Instantiate(floor, new Vector3(xValue, 0, zValue), Quaternion.identity);
        // Decidir aleatoriamente si generar un premio en el nuevo suelo
        if (Random.Range(0f, 1f) < probabilidadPremio)
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
}

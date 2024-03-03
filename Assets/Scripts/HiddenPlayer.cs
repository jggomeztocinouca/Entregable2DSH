using System.Collections;
using UnityEngine;

public class HiddenPlayer : MonoBehaviour
{
    public GameObject player; // Jugador original
    public GameObject floor; // Prefab del suelo
    public GameObject[] premios; // Array de premios
    public GameObject obstaculo;
    public float probabilidadPremio = 0.2f; // Probabilidad de generar un premio
    private int PremioGenerado = 3; // Contador de premios generados

    // Variables para generar suelos y premios
    private float _xValue, _zValue;
    private enum LastDirectionEnum { Forward, Left, Right };
    private LastDirectionEnum _lastDirection = LastDirectionEnum.Forward;

    private void Start()
    {
        transform.position = player.transform.position;
        GenerateInitialFloor();
    }

    private void Update()
    {
        Vector3 playerPosition = player.transform.position; // Mejora de la eficiencia
        transform.position = playerPosition.y < 0 ? new Vector3(playerPosition.x, playerPosition.y, playerPosition.z) : new Vector3(playerPosition.x, 0.8f, playerPosition.z);
    }

    private void GenerateInitialFloor()
    {
        for (int i = 0; i < 3; i++)
        {
            _zValue += 6.0f;
            GameObject newFloor = Instantiate(floor, new Vector3(_xValue, 0, _zValue), Quaternion.identity);
            if (Random.Range(0f, 1f) < probabilidadPremio)
            {
                GenerateRandomPrize(newFloor.transform.position);
            }
        }
    }

    private void GenerateRandomPrize(Vector3 floorPosition)
    {
        Vector3 obstaclePosition;
        Quaternion obstacleRotation;
        if(_lastDirection is LastDirectionEnum.Left or LastDirectionEnum.Right)
        {
            obstaclePosition = floorPosition + new Vector3(0, 0.1f, -2.5f);
            obstacleRotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            obstaclePosition = floorPosition + new Vector3(2.5f, 0.1f, 0);
            obstacleRotation = Quaternion.Euler(0, 0, 0);
        }
        Vector3 prizePosition = floorPosition + new Vector3(0, 3.50f, 0);
        Instantiate(obstaculo, obstaclePosition, obstacleRotation);
        Instantiate(premios[Random.Range(0, premios.Length)], prizePosition, Quaternion.Euler(-90, 0, 0));
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
        if (random < 0.33 && _lastDirection != LastDirectionEnum.Right)
        {
            _xValue = x + 6.0f;
            _lastDirection = LastDirectionEnum.Left;
        }
        else if (random < 0.66 && _lastDirection != LastDirectionEnum.Left)
        {
            _xValue = x - 6.0f;
            _lastDirection = LastDirectionEnum.Right;
        }
        else
        {
            _zValue = z + 6.0f;
            _lastDirection = LastDirectionEnum.Forward;
        }
    }

    IEnumerator DestroyFloor(GameObject floor)
    {
        if(PremioGenerado == 0){
            NewDirection(_xValue, _zValue);
            GameObject newFloor = Instantiate(floor, new Vector3(_xValue, 0, _zValue), Quaternion.identity);
            // Decidir aleatoriamente si generar un premio en el nuevo suelo
            if (Random.Range(0f, 1f) < probabilidadPremio)
            {
                GenerateRandomPrize(newFloor.transform.position);
                PremioGenerado = 3;
            }
        }
        else
        {
            switch (_lastDirection)
            {
                case LastDirectionEnum.Forward:
                    _zValue += 6.0f;
                    break;
                case LastDirectionEnum.Left:
                    _xValue += 6.0f;
                    break;
                case LastDirectionEnum.Right:
                    _xValue -= 6.0f;
                    break;
                default:
                    break;
            }
            GameObject newFloor = Instantiate(floor, new Vector3(_xValue, 0, _zValue), Quaternion.identity);
            PremioGenerado--;

        }
        if (floor == null)
            yield break;
        Rigidbody rb = floor.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        yield return new WaitForSeconds(2);
        Destroy(floor);
    }
}

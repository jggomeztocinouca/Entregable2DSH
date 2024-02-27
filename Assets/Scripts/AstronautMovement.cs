using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautMovement : MonoBehaviour
{
    public Camera cam;
    public GameObject floorPrefab;
    public float jumpHeight = 2.0f;
    private Vector3 offset = new Vector3(0, 10, -10);
    private float speed = 10.0f;
    private Animator anim;
    private CharacterController controller;
    private bool isJumping;
    private float yVelocity;
    private List<GameObject> activeFloors = new List<GameObject>();
    private int maxFloors = 5;
    private float lastFloorZ = -6.0f; // Inicializar con -6.0f para que el primer suelo se genere en la posición 0

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponentInChildren<Animator>();
        cam.transform.position = transform.position + offset;
        isJumping = false;
        GenerateInitialFloors();
    }

    // Genera los suelos iniciales y asegura que se añadan a la lista activeFloors
    void GenerateInitialFloors()
    {
        for (int i = 0; i < maxFloors; i++)
        {
            GenerateNewFloor();
        }
    }

    void Update()
    {
        HandleMovement();
        HandleCamera();
    }

    void HandleMovement()
    {
        Vector3 movement = speed * Time.deltaTime * transform.forward;
        yVelocity += Physics.gravity.y * Time.deltaTime;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
            transform.forward = direction;
        }

        if (controller.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
            if (isJumping)
            {
                isJumping = false;
                anim.SetInteger("AnimationPar", 3); // Animación de aterrizaje
            }

            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                anim.SetInteger("AnimationPar", 1); // Animación de inicio de salto
                yVelocity = Mathf.Sqrt(2 * jumpHeight * -Physics.gravity.y);
            }
        }

        movement.y = yVelocity;
        controller.Move(movement);
    }

    void HandleCamera()
    {
        cam.transform.position = transform.position + offset;
        cam.transform.LookAt(transform.position);
    }

    void GenerateNewFloor()
    {
        lastFloorZ += 6.0f; // Asume que cada suelo tiene una longitud de 6 unidades en Z
        GameObject newFloor = Instantiate(floorPrefab, new Vector3(0, 0, lastFloorZ), Quaternion.identity);
        activeFloors.Add(newFloor);

        // Implementa un sistema para destruir el suelo más antiguo con un retraso de 2 segundos
        if (activeFloors.Count > maxFloors)
        {
            GameObject floorToDestroy = activeFloors[0];
            activeFloors.RemoveAt(0);
            Destroy(floorToDestroy, 2f); // Destruye el suelo con un retraso de 2 segundos
        }
    }
}

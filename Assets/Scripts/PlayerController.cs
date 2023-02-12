using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // input variables
    public float moveSpeed = 1f;
    public float turnSpeed = 1.0f;
    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;

    // physics variables
    Rigidbody2D rigidbody2d;
    private float movementX;
    private float movementY;
    private float rotationX;
    private float rotationY;

    // referenced gameobjects
    public GameObject imageGemRed;
    public GameObject imageGemBlue;
    public GameObject imageGemGreen;
    public GameObject imageGhostSheet;
    public GameObject camera;

    // collectables
    public bool isGemRed = false;
    public bool isGemBlue = false;
    public bool isGemGreen = false;

    public bool isGhostSheet = false;

    // audio
    public AudioSource musicSource;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        musicSource = GetComponent<AudioSource>();
        
        // hides UI collectables
        imageGemRed.SetActive(false);
        imageGemBlue.SetActive(false);
        imageGemGreen.SetActive(false);
        imageGhostSheet.SetActive(false);
    }

    void Update()
    {
        ToggleItems();
    }

    void FixedUpdate()
    {
        // moves the player
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void OnMove(InputValue movementValue)
    {
        // receives keyboard/joystick input
        Vector2 movementVector = movementValue.Get<Vector2>();

        // sets the input into variables
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnLook(InputValue rotationValue)
    {
        // receives mouse/joystick input
        Vector2 rotationVector = rotationValue.Get<Vector2>();

        // calculates the amount of rotation
        rotationY = rotationVector.x * turnSpeed;
        rotationX += rotationVector.y * turnSpeed / 2;
        
        // clamps rotation across the x-axis
        rotationX = Mathf.Clamp(rotationX, minTurnAngle, maxTurnAngle);

        // rotates the player and camera
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + rotationY, 0);
        camera.transform.eulerAngles = new Vector3(-rotationX, transform.eulerAngles.y + rotationY, 0);
    }  

    // toggles the UI collectables
    void ToggleItems()
    {
        if (isGemRed == true)
            imageGemRed.SetActive(true);
        else 
            imageGemRed.SetActive(false);

        if (isGemBlue == true)
            imageGemBlue.SetActive(true);
        else 
            imageGemBlue.SetActive(false);

        if (isGemGreen == true)
            imageGemGreen.SetActive(true);
        else 
            imageGemGreen.SetActive(false);

        if (isGhostSheet == true)
            imageGhostSheet.SetActive(true);
        else 
            imageGhostSheet.SetActive(false);
    }
}

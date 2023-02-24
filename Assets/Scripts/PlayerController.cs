using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // health variables
    public int maxSpook = 10;
    public int spook { get { return currentSpook; } }
    int currentSpook;

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
    public GameObject GemRed;
    public GameObject GemGreen;
    public GameObject GemBlue;
    public GameObject imageGemRed;
    public GameObject imageGemBlue;
    public GameObject imageGemGreen;
    public GameObject imageGhostSheet;
    public GameObject camera;
    public GameObject mask;

    // collectables
    public bool isGemRed = false;
    public bool isGemBlue = false;
    public bool isGemGreen = false;

    public bool isGhostSheet = false;
    public bool isInvisible = false;

    // audio
    public AudioSource musicSource;

    // timer
    public float maxCount = 10;
    float count = 0;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        musicSource = GetComponent<AudioSource>();

        // sets health & timer
        currentSpook = 0;
        count = maxCount;

        //hides Gem wall Gems
        GemRed.SetActive(false);
        GemGreen.SetActive(false);
        GemBlue.SetActive(false);

        // hides UI collectables
        imageGemRed.SetActive(false);
        imageGemBlue.SetActive(false);
        imageGemGreen.SetActive(false);
        imageGhostSheet.SetActive(false);
        mask.SetActive(false);
    }

    void Update()
    {
        // rotates the player and camera
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + rotationY, 0);
        camera.transform.eulerAngles = new Vector3(-rotationX, transform.eulerAngles.y + rotationY, 0);

        ToggleItems();

        // invisibility timer
        if (isGhostSheet == true)
        {
            if (count > 0)
            {
                isGhostSheet = true;
                isInvisible = true;
                count -= Time.deltaTime;
            }
            else
            {
                isGhostSheet = false;
                isInvisible =  false;
                count = maxCount;
            }
        }
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
        rotationX += rotationVector.y * turnSpeed;

        // clamps rotation across the x-axis
        rotationX = Mathf.Clamp(rotationX, minTurnAngle, maxTurnAngle);
    }

    void OnTriggerEnter(Collider other)
    {
        // red gem collectable
        if (other.gameObject.CompareTag("Red")) // SET TAG
        {
            isGemRed = true;
            Destroy(other.gameObject);
        }
        // blue gem collectable
        if (other.gameObject.CompareTag("Blue")) // SET TAG
        {
            isGemBlue = true;
            Destroy(other.gameObject);
        }
        // green gem collectable
        if (other.gameObject.CompareTag("Green")) // SET TAG
        {
            isGemGreen = true;
            Destroy(other.gameObject);
        }
        // ghost sheet collectable
        if (other.gameObject.CompareTag("Sheet")) // SET TAG
        {
            isGhostSheet = true;
            Destroy(other.gameObject);
        }
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

    // controls health changes such as damage and healing
    public void ChangeSpook(int amount)
    {
        currentSpook = Mathf.Clamp(currentSpook + amount, 0, maxSpook);
        Debug.Log(currentSpook + "/" + maxSpook);
        mask.SetActive(true);
        UISpookBar.instance.SetValue(currentSpook / (float)maxSpook);
    }

    // places gems from inventory onto wall
    public void PlaceGems()
    {
        if (isGemBlue == true)
            GemBlue.SetActive(true);
            isGemBlue = false;

        if (isGemGreen == true)
            GemGreen.SetActive(true);
            isGemGreen = false;

        if (isGemRed == true)
            GemRed.SetActive(true);
            isGemRed = false;
    }
}

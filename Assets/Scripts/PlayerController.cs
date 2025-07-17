using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timeText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count;
    public float timer;
    public GameObject Enemy;

    public float sensX;
    public float sensY;

    public Transform orientation;
    float xRotation;
    float yRotation;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        count = 0;
        timer = 0;
        SetCountText();
        Timer();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);

            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }

    }

    void Timer()
    {
        timeText.text = "Time: " + timer.ToString();
        
    }
    /*
    public void EnableEnemy()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject taggedObject in taggedObjects)
        {
            taggedObject.enabled = true;
        }
    }
    */

    public void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        /*
        Timer();
        timer += Time.deltaTime;
        if (timer >= 10)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject taggedObject in taggedObjects)
            {
                Enemy.enabled = !Enemy.enabled;
            }
        }
        */
    }



    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            
            winTextObject.gameObject.SetActive(true );
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You lose!";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        
    }

}

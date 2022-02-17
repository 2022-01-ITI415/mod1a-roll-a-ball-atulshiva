using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour

{
    public float timeCounter;
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public bool sphereonground = true;
    
    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && sphereonground)
        {
            rb.AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);
            sphereonground = false;
        }
    }
    private void FixedUpdate()
    {

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement);

        rb.AddForce(movement * speed);

       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            sphereonground = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }
        if(other.gameObject.CompareTag("Pillar"))
        {
            other.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        print("Entering the " + other.gameObject.tag);
    }

    private void OnTriggerStay(Collider other)
    {
        print("Entering the " + other.gameObject.tag);
    }
    private void OnTriggerExit(Collider other)
    {
        print("Exiting the " + other.gameObject.tag);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
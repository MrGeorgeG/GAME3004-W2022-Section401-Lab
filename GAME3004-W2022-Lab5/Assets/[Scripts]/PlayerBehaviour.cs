using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private CharacterController controller;
    public UIController uiController;

    [Header("Movement Properties")]
    public float maxSpeed = 10.0f;
    public float grayity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;

    [Header("Ground Detection Properties")]
    public Transform groundCheck;
    public float groundRadius = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    [Header("OnScreen Controls")]
    public GameObject miniMap;
    public GameObject OnScreenControls;
    public Joystick leftJoyStick;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0.0f)
        {
            velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal") + leftJoyStick.Horizontal;
        float z = Input.GetAxis("Vertical") + leftJoyStick.Vertical;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * maxSpeed * Time.deltaTime);

        if(Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * grayity);
        }

        velocity.y += grayity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hazard"))
        {
            uiController.TakeDamage(5);
        }
    }

    public void OnJumpButton_Pressed()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * grayity);
        }
    }

    public void OnMapButton_Pressed()
    {
        miniMap.SetActive(!miniMap.activeInHierarchy);
    }
}

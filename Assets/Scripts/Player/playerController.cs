using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class playerController : MonoBehaviour
{
    [Header("Refrences")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public GameObject players;
    public Rigidbody rb;
    public GameObject bullet;
    public GameObject bulletSpawn;


    [Header("Dont Change only View")]
    [SerializeField] private float groundDrag = 10f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float bulletSpeed = 15f;
    Vector3 moveDirection;
    public LayerMask whatIsGround;
    float horizontalInputM;
    float verticalInputM;


    [Header("Movement Attribute")]
    private float playerMoveSpeed = 6.5f;
    private float playerJumpSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {

        rb = players.GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(playerObj.transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        

        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        shoot();
        playerMoveInput();
        playerMoveSet();
        speedControl();
    }

    public void FixedUpdate()
    {
        movement();
    }

    void playerMoveSet()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");

        Vector3 inputDir = orientation.forward * verInput + orientation.right * horInput;

        if (inputDir != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);

        //------------------------------------------
    }

    private void playerMoveInput()
    {
        horizontalInputM = Input.GetAxisRaw("Horizontal");
        verticalInputM = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector3(0, playerJumpSpeed, 0), ForceMode.Impulse);
        }
    }

    private void movement()
    {
        moveDirection = orientation.forward * verticalInputM + orientation.right * horizontalInputM;

        rb.AddForce(moveDirection.normalized * playerMoveSpeed * 10f, ForceMode.Force);
    }

    private void speedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > playerMoveSpeed)
        {
            Vector3 limitVel = flatVelocity.normalized * playerMoveSpeed;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
    }

    private void shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var bullets = Instantiate(bullet, bulletSpawn.transform.position, playerObj.transform.rotation);
            bullets.GetComponent<Rigidbody>().velocity = bulletSpawn.transform.forward * bulletSpeed;
            Debug.Log("Fire");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(HandleAnimation))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float groundRaycastDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rotationSpeed = 10f;
    public float moveAmount;


    [Header("Refrences")]
    private Rigidbody Rigidbody;
    public bool isGrounded;
    public Camera cam;
    private Animator anim;
    private HandleAnimation handleAnimation;


    [Header("Input")]
    public float moveInput;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        handleAnimation = GetComponent<HandleAnimation>();
        Rigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        moveAmount = Mathf.Clamp01(Mathf.Abs(moveInput));
        HandleAnimation();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        else if (isGrounded)
        {
            anim.SetBool("Jump", false);

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundRaycastDistance, groundLayer);

        moveInput = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(0, 0, moveInput);
        moveDirection *= moveSpeed * Time.fixedDeltaTime;
        Rigidbody.velocity = new Vector3(0, Rigidbody.velocity.y, moveDirection.z);

        //Jumping
       

        //Stopping player if no input
        if (isGrounded)
        {
            Rigidbody.drag = Mathf.Abs(moveInput) < 0.4 ? 4f : 0f;
        }
        else
            Rigidbody.drag = 0;
    }

    void Jump()
    {
        anim.SetBool("Jump", true);
        Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, jumpForce, 0f);
        

    }

   

    void HandleRotation()
    {
        moveInput = Input.GetAxis("Horizontal");
        if(moveInput != 0)
        {
            Vector3 targetDir = cam.transform.right * moveInput;
            targetDir.Normalize();

            if(targetDir == Vector3.zero)
            {
                targetDir = transform.forward;
            }

            Quaternion tr = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.fixedDeltaTime * moveAmount);
        }
    }

    void HandleAnimation()
    {
        if(isGrounded)
        {
            float forward = moveAmount > 0 ? (moveAmount <= 0.5f ? 0.5f : 1) : 0f;
            handleAnimation.PlayAnimationWithvalue("forward", forward, anim);
        }
    }
}

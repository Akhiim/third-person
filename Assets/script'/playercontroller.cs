using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    private CharacterController CharacterController;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private Camera followCamera;

    [SerializeField] private float rotationSpeed = 10f;

    private Vector3 playerVelocity;
    [SerializeField] private float gravityValue = -13f;

    public bool groundedPlayer;
    [SerializeField] private float jumpHeight = 1.5f;

    public Animator animator;

    public static playercontroller instance;

    private void Awake()
    {
        instance = this;
    }
   
    

    // Start is called before the first frame update
    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (checkwinner.instance.iswinner)
        {
            case true:
                animator.SetBool("victory", checkwinner.instance.iswinner);
                break;
            case false:
                Movement();
                break;
        }
       
    }
    void Movement()
    {
        groundedPlayer = CharacterController.isGrounded;

        if(CharacterController.isGrounded && playerVelocity.y < -2)
        {
            playerVelocity.y = -1;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0)
        * new Vector3(horizontalInput, 0, verticalInput);

        Vector3 movementDiraction = movementInput.normalized;

        CharacterController.Move(movementDiraction * playerSpeed * Time.deltaTime);

        if(movementDiraction != Vector3.zero)
        {
            Quaternion desireRotation = Quaternion.LookRotation(movementDiraction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation,desireRotation,rotationSpeed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityValue);
            animator.SetTrigger("jumpping");
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        CharacterController.Move(playerVelocity * Time.deltaTime);

        animator.SetFloat("speed", Mathf.Abs(movementDiraction.x) + Mathf.Abs(movementDiraction.z));
        animator.SetBool("ground", CharacterController.isGrounded);
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float runSpeed = 8f;
    public float walkSpeed = 6f;
    public float gravity = -10f;
    public float jumpHeight = 15f;
    public float groundRayDistance = 1.1f;

    [Header("Dash")]
    public float dashSpeed = 50f;
    public float dashDuration = .5f;


    private CharacterController controller;
    private Vector3 motion; // is the movement offset per frame;
    public bool isJumping;
    private float currentJumpHeight, currentSpeed;

    public GameObject skillRadius;
    private void OnDrawGizmos()
    {
        Ray groundRay = new Ray(transform.position, -transform.up);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * groundRayDistance);
    }
    private void OnValidate()
    {
        currentJumpHeight = jumpHeight;
        currentSpeed = walkSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        currentSpeed = walkSpeed;
        currentJumpHeight = jumpHeight;
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        //left shift input
        bool inputRun = Input.GetKeyDown(KeyCode.LeftShift);
        bool inputWalk = Input.GetKeyUp(KeyCode.LeftShift);
        bool inputJump = Input.GetButtonDown("Jump");
        //if we need to run
        if (inputRun)
        {
            currentSpeed = runSpeed;
        }
        if (inputWalk)
        {
            currentSpeed = walkSpeed;
        }

        // Move character motion with inputs
        Move(inputH, inputV);
        //Is the player grounded?
        if (controller.isGrounded)
        {
            motion.y = 0f;
            if (inputJump)
            {
                //Make the player jump!
                Jump(jumpHeight);

            }
            if (isJumping)
            {
                motion.y = currentJumpHeight;
                isJumping = false;
            }

        }
        //Apply gravity
        motion.y += gravity * Time.deltaTime;
        //move the controller with motion
        controller.Move(motion * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 mousePos = hit.point;
                Debug.Log("x.pos" + mousePos.x);
                Debug.Log("y.pos" + mousePos.y);
                Debug.Log("z.pos" + mousePos.z);
                transform.LookAt(mousePos, Vector3.forward);
                transform.position = new Vector3(mousePos.x, mousePos.y, mousePos.z);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = false;
            }
            else
            {
                transform.position = transform.forward;
            }
        }
    }

    //Move the character's motion in direction of input
    void Move(float inputH, float inputV)
    {
        //Generate direction from input
        Vector3 direction = new Vector3(inputH, 0f, inputV);

        //Convert local space to world space directon
        direction = transform.TransformDirection(direction);

        //check if direction exceed magnitude of 1;
        if (direction.magnitude > 1f)
        {
            // normalize it!
            direction.Normalize();

        }
        // Apply motion
        motion.x = direction.x * currentSpeed;
        motion.z = direction.z * currentSpeed;
    }
    public void Jump(float height)
    {
        isJumping = true;
        currentJumpHeight = height;
    }

    public IEnumerator SpeedBoost(float boost, float duration)
    {
        currentSpeed += boost;
        yield return new WaitForSeconds(duration);
        currentSpeed -= boost;
    }
    public void Dash(float boost)
    {
        StartCoroutine(SpeedBoost(boost, dashDuration));
    }
    public void Teleport()
    {

        

    }
}

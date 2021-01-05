using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*
     * 
     * This script controls character's movements and sets state accordingly.
     * 
     */

    [Header("Controller Setup Field")]
    private Player player;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool playerIsGrounded;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    [SerializeField] private LayerMask groundLayerMask;

    private void Start()
    {
        player = GetComponent<Player>();

        #region Character Controller Setup

        controller = gameObject.AddComponent<CharacterController>();
        controller.stepOffset = 0f;
        controller.skinWidth = 0.0001f;
        controller.center = new Vector3(0f, 0.23f, 0f);
        controller.radius = 0.19f;
        controller.height = 0;

        #endregion

    }

    private void Update()
    {
        if (Player.state == Player.State.Falling)
        {
            playerIsGrounded = false;
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
        else if (Player.state == Player.State.Freeze)
        {
            playerIsGrounded = false;
            playerVelocity.y = 0f;
        }
        else
        {
            playerIsGrounded = IsGrounded();

            Debug.Log(playerIsGrounded ? "Grounded." : "Not Grounded.");

            if (playerIsGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * player.GetPlayerSpeedRate());

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;

                Player.state = Player.State.Running;
            }
            else Player.state = Player.State.Idle;

            // Changes the height position of the player.
            if (Input.GetKeyDown(KeyCode.Space) && playerIsGrounded)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityValue);

                Player.state = Player.State.Jumping;
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

        }
    }

    // Character Controller's isGround value is unreliable. That's why we'll check ourselves if we are on ground or not.
    // Chatacter Controller's own collider messes up the calculation.
    // That's why we've added layer to the ground and checked that layer collision only.
    private bool IsGrounded()
    {
        // We look down to see if there's a collider below us.
        return Physics.Raycast(gameObject.GetComponent<Collider>().bounds.center, Vector3.down, gameObject.GetComponent<Collider>().bounds.extents.y + 0.05f, groundLayerMask);
    }
}

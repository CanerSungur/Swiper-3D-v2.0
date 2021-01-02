using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*
     * 
     * This script controls character's movements and sets state accordingly.
     * 
     */

    [Header("Controller Setup Field")]
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool playerIsGrounded;
    private float playerSpeed = 0.1f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private void Start()
    {

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
            playerIsGrounded = Grounded();

            if (playerIsGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0.1f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * playerSpeed);

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
    // Because of Character Controller's collider, it isn't calculated correctly.
    // That's why we've made our collider bigger and checked the condition reverse.
    // Meaning: if we are on ground we get false, if we jump collider will go out of the ground and can see the collider below
    // So if we jump we get true;
    private bool Grounded()
    {
        // We look down to see if there's a collider below us.
        return !Physics.Raycast(transform.position, Vector3.down, gameObject.GetComponent<Collider>().bounds.extents.y + 1f);
    }
}

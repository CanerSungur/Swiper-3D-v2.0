using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /*
     * 
     * TODO: Character Controller's isGrounded is not reliable. Can't detect jump key every frame. Change that.
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
        controller = gameObject.AddComponent<CharacterController>();
        controller.stepOffset = 0f;
        controller.skinWidth = 0.0001f;
        controller.center = new Vector3(0f, 0.23f, 0f);
        controller.radius = 0.21f;
        controller.height = 0;
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
            playerIsGrounded = controller.isGrounded;
            Debug.Log(controller.isGrounded);

            if (playerIsGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
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
}

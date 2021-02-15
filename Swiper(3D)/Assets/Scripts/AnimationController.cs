using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Player player;

    #region Animations & Variables

    private readonly string IS_JUMPING_ANIM = "Jumping";
    private readonly string IS_FALLING_ANIM = "Falling";
    private readonly string IS_IDLE_BOOL = "isIdle";
    private readonly string IS_RUNNING_BOOL = "isRunning";
    private readonly string MOVE_SPEED_FLOAT = "moveSpeed";

    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();

        player.OnGroundChange += Player_OnGroundChange;
    }

    private void Player_OnGroundChange(object sender, System.EventArgs e)
    {
        // Update animation speed when ground changes.
        animator.SetFloat(MOVE_SPEED_FLOAT, player.GetPlayerSpeedRate());
    }

    private void Update()
    {
        if (Player.state == Player.State.Idle)
        {
            animator.SetBool(IS_IDLE_BOOL, true);
            animator.SetBool(IS_RUNNING_BOOL, false);
        }
        else if (Player.state == Player.State.Running)
        {
            animator.SetBool(IS_IDLE_BOOL, false);
            animator.SetBool(IS_RUNNING_BOOL, true);
        }
        else if (Player.state == Player.State.Jumping)
        {
            animator.Play(IS_JUMPING_ANIM);
            if (animator.GetBool(IS_IDLE_BOOL))
            {
                Player.state = Player.State.Idle;
            }
            else if (animator.GetBool(IS_RUNNING_BOOL))
            {
                Player.state = Player.State.Running;
            }
        }
        else if (Player.state == Player.State.Falling || Player.state == Player.State.Freeze) animator.Play(IS_FALLING_ANIM);
    }
}

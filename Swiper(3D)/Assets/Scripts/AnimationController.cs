using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        Player.state = Player.State.Idle;
    }

    private void Update()
    {
        if (Player.state == Player.State.Idle) 
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isRunning", false);
        }
        else if (Player.state == Player.State.Running) 
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isRunning", true);
        }
        else if (Player.state == Player.State.Jumping)
        {
            animator.Play("Jumping");
            if (animator.GetBool("isIdle"))
            {
                Player.state = Player.State.Idle;
            }
            else if (animator.GetBool("isRunning"))
            {
                Player.state = Player.State.Running;
            }
        }
        else if (Player.state == Player.State.Falling || Player.state == Player.State.Freeze) animator.Play("Falling");
    }
}

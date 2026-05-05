using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public CharacterController controller;

    void Update()
    {
        if (animator == null || controller == null)
            return;

        float speed = controller.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }
}
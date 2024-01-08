using UnityEngine;

namespace FubarOps.Animation
{
    /// <summary>
    /// Controls speed of movement animation, based on rb velocity.
    /// </summary>
    public class AnimationSpeedChanger : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        Animator animator;
        Rigidbody2D rb;

        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (spriteRenderer.isVisible) // Used to prevent unnecessary velocity.magnitude calculations.
            {
                animator.SetFloat("speed", rb.velocity.magnitude * 0.3f);
            }
        }
    }
}

using UnityEngine;

namespace FubarOps.Movement
{
    /// <summary>
    /// Used to keep the sprite's global rotation at zero (independent of parent's roation) and only flip it around the x axis.
    /// </summary>
    public class KeepRotation : MonoBehaviour
    {
        float spriteFlipThreshold = 0.01f;

        void LateUpdate()
        {
            transform.rotation = Quaternion.identity;

            float dotProduct = Vector2.Dot(transform.parent.right, Vector2.right);

            if (dotProduct > spriteFlipThreshold)
            {
                foreach (SpriteRenderer spriteRenderer in transform.GetComponentsInChildren<SpriteRenderer>())
                {
                    spriteRenderer.flipX = false;
                }
            }
            else if (dotProduct < -spriteFlipThreshold)
            {
                foreach (SpriteRenderer spriteRenderer in transform.GetComponentsInChildren<SpriteRenderer>())
                {
                    spriteRenderer.flipX = true;
                }
            }

        }
    }
}

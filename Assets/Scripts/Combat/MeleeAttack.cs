using UnityEngine;

namespace FubarOps.Combat
{
    /// <summary>
    /// Allows to perform melee attacks.
    /// </summary>
    public class MeleeAttack : MonoBehaviour
    {
        [SerializeField] float damage = 5;
        [SerializeField] float attackCooldownTime = 1;
        float nextAttackTime = 0;

        void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Soldier"))
            {
                if (Time.time >= nextAttackTime && collision.gameObject.TryGetComponent<Health>(out Health targetHealth))
                {
                    nextAttackTime = Time.time + attackCooldownTime;
                    targetHealth.DealDamage(damage);
                }
            }

        }
    }
}
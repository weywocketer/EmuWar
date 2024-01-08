using System.Collections;
using UnityEngine;

namespace FubarOps.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float speed = 50;
        [SerializeField] float damage = 10;
        [SerializeField] float maxRange = 100; // Max bullet flying range in meters.

        Rigidbody2D rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            rb.velocity = transform.right * speed;
            StartCoroutine(DestroyAfterTime());
        }

        void OnCollisionEnter2D(Collision2D collision)
        {

            if (collision.gameObject.TryGetComponent<Health>(out Health targetHealth))
            {
                targetHealth.DealDamage(damage);
            }

            Destroy(gameObject);
        }

        IEnumerator DestroyAfterTime()
        {
            yield return new WaitForSeconds(maxRange / speed);

            Destroy(gameObject);
        }
    }
}
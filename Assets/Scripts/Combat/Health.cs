using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FubarOps.Combat
{
    /// <summary>
    /// Keeps track of unit's health, invokes related events.
    /// </summary>
    public class Health : MonoBehaviour
    {
        public float maxHealthPoints = 100;
        public float healthPoints;
        public bool isHuman = true; // Determines if the object will take damage from emu attacks.

        public event Action OnDeath;
        public event Action OnHealthChanged;

        void Start()
        {
            healthPoints = maxHealthPoints;
        }

        public void DealDamage(float damage)
        {
            healthPoints -= damage;
            OnHealthChanged?.Invoke();

            if (healthPoints <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        void OnDisable()
        {
            OnDeath = null;
        }
    }
}
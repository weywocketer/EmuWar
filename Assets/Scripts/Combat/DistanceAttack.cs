using System;
using System.Collections.Generic;
using UnityEngine;
using FubarOps.Constants;

namespace FubarOps.Combat
{
    /// <summary>
    /// Allows to perform distance attacks, holds list of possesed weapons.
    /// </summary>
    public class DistanceAttack : MonoBehaviour
    {
        public float attackRangeSquared = AttackRangeSquaredValues.normalRange;
        private float nextShotTime = 0;
        [SerializeField] AudioSource weaponAudioSource;
        [SerializeField] List<Weapon> weapons;
        Weapon currentWeapon;
        [SerializeField] float bulletSpawnOffset = 1.4f; // Should be higher than the sum of circle collider radii of soldier and bullet.
        public event Action<float> OnWeaponFired;
        public event Action<float> OnWeaponReload;

        void Start()
        {
            currentWeapon = weapons[0];
            transform.GetChild(0).GetChild(1).GetComponent<SpriteRenderer>().sprite = currentWeapon.weaponStats.sprite;
        }

        public bool CanFire()
        {
            return Time.time >= nextShotTime && currentWeapon.ammoInMagazine > 0;
        }

        public void ReloadCurrentWeapon()
        {
            OnWeaponReload?.Invoke(currentWeapon.weaponStats.reloadTime);
            nextShotTime = Time.time + currentWeapon.weaponStats.reloadTime;
            currentWeapon.Reload();

            weaponAudioSource.PlayOneShot(currentWeapon.weaponStats.reloadClip);
        }

        public void Fire(Vector3 targetPosition)
        {
            OnWeaponFired?.Invoke(0.3f);

            currentWeapon.ammoInMagazine--;
            nextShotTime = Time.time + currentWeapon.weaponStats.interShotTime;

            transform.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(Vector3.right, targetPosition - transform.position, Vector3.forward));
            Instantiate(currentWeapon.weaponStats.bulletPrefab, transform.position + transform.right * bulletSpawnOffset, transform.rotation);

            //weaponAudioSource.PlayOneShot(currentWeapon.weaponStats.shotClips[UnityEngine.Random.Range(0, currentWeapon.weaponStats.shotClips.Length)]);
        }

        public override string ToString()
        {
            string text = string.Empty;

            foreach (Weapon weapon in weapons)
            {
                text += $"{weapon}\n";
            }
            text = text.Remove(text.Length - 1); // Remove last '\n'

            return text;
        }
    }
}
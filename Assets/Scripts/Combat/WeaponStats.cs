using UnityEngine;

namespace FubarOps.Combat
{
    [CreateAssetMenu(fileName = "WeaponStats", menuName = "Scriptable objects/WeaponStats")]
    public class WeaponStats : ScriptableObject
    {
        public string weaponName;
        public float interShotTime;
        public int magazineCapacity;
        public float reloadTime;
        public bool automaticFire;
        public GameObject bulletPrefab;
        public AudioClip[] shotClips;
        public AudioClip reloadClip;
        public Sprite sprite;
    }
}
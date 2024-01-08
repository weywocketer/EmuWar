using System;

namespace FubarOps.Combat
{
    [Serializable]
    public class Weapon
    {
        public WeaponStats weaponStats;
        public int ammoInMagazine;
        public int ammoSpare;

        public void Reload()
        {
            int ammoToRefill = weaponStats.magazineCapacity - ammoInMagazine;

            if (ammoSpare >= ammoToRefill)
            {
                ammoInMagazine += ammoToRefill;
                ammoSpare -= ammoToRefill;
            }
            else
            {
                ammoInMagazine += ammoSpare;
                ammoSpare = 0;
            }
        }

        public override string ToString()
        {
            return $"{weaponStats.weaponName} {ammoInMagazine + ammoSpare}/{weaponStats.magazineCapacity}";
        }

    }
}
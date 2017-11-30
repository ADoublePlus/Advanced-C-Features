using UnityEngine;

namespace AbstractClasses
{
    [RequireComponent(typeof(Rigidbody2D))]

    public class Shooting : MonoBehaviour
    {
        public int weaponIndex = 0;

        private Weapon[] attachedWeapons;
        private Rigidbody2D rigid;

        void Awake ()
        {
            rigid = GetComponent<Rigidbody2D>();
        }
         
        // Use this for initialization
        void Start ()
        {
            attachedWeapons = GetComponentsInChildren<Weapon>();

            // Set the first weapon
            SwitchWeapon(weaponIndex);
        }

        // Update is called once per frame
        void Update ()
        {
            CheckFire();
            WeaponSwitching();
        }

        // Checks if the user pressed a button to fire the current weapon
        void CheckFire ()
        {
            // Set currentWeapon to attachedWeapons[weaponIndex]
            Weapon currentWeapon = attachedWeapons[weaponIndex];

            // IF user pressed down space
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Fire currentWeapon
                currentWeapon.Fire();

                // Add recoil to player from weapon's recoil
                rigid.AddForce(-transform.right * currentWeapon.recoil, ForceMode2D.Impulse);
            }
        }

        // Handles weapon switching when pressing keys
        void WeaponSwitching ()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                CycleWeapon(-1);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                CycleWeapon(1);
            }
        }

        // Cycles through weapons using amount as an index
        void CycleWeapon (int amount)
        {
            // SET desiredIndex to weaponIndex + amount
            int desiredIndex = weaponIndex + amount;

            // IF desiredIndex >= length of weapons
            if (desiredIndex >= attachedWeapons.Length)
            {
                // SET desiredIndex to zero
                desiredIndex = 0;
            }
            //ELSE IF desiredIndex < zero
            else if (desiredIndex < 0)
            {
                // SET desiredIndex to length of weapons - 1
                desiredIndex = attachedWeapons.Length - 1;
            }

            // SET weaponIndex to desiredIndex
            weaponIndex = desiredIndex;

            // SwitchWeapon() to weaponIndex
            SwitchWeapon(weaponIndex);
        }

        // Disables all other weapons in the list and returns the selected one
        Weapon SwitchWeapon (int weaponIndex)
        {
            if (weaponIndex < 0 || weaponIndex > attachedWeapons.Length)
            {
                return null;
            }

            for (int i = 0; i < attachedWeapons.Length; i++)
            {
                Weapon w = attachedWeapons[i];

                if (i == weaponIndex)
                {
                    w.gameObject.SetActive(true);
                }
                else
                {
                    w.gameObject.SetActive(false);
                }
            }

            return attachedWeapons[weaponIndex];
        }
    }
}

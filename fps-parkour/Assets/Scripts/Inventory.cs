using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject Primary;
    public GameObject Secondary;
    public GameObject Grenade;


    public bool AddWeapon(GameObject weapon)
    {
        Weapon weaponScript = weapon.GetComponent<Weapon>();

        // Check the type of weapon and assign it to the respective slot if empty
        switch (weaponScript.weapontype)
        {
            case Weapon.WeaponType.Primary:
                if (Primary == null)
                {
                    Primary = weapon;
                    return true; // Successfully added to inventory
                }
                break;

            case Weapon.WeaponType.Secondary:
                if (Secondary == null)
                {
                    Secondary = weapon;
                    return true; // Successfully added to inventory
                }
                break;

            case Weapon.WeaponType.Grenade:
                if (Grenade == null)
                {
                    Grenade = weapon;
                    return true; // Successfully added to inventory
                }
                break;
        }

        return false; // Weapon was not added to inventory (maybe because the slot was already occupied)
    }

    public void RemoveWeapon(GameObject weapon)
    {

        Weapon weaponScript = weapon.GetComponent<Weapon>();
        // Check the type of weapon and assign it to the respective slot if empty
        switch (weaponScript.weapontype)
        {
            case Weapon.WeaponType.Primary:
                if (Primary == weapon)
                {
                    Primary = null;
                }
                break;

            case Weapon.WeaponType.Secondary:
                if (Secondary == weapon)
                {
                    Secondary = null;
                }
                break;

            case Weapon.WeaponType.Grenade:
                if (Grenade == weapon)
                {
                    Grenade = null;
                }
                break;
        }
    }
}

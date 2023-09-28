using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject HandSlot; // The current weapon in hand
    public GameObject Primary;
    public GameObject Secondary;
    public GameObject Grenade;
    public GameObject Grapple; // Added Grapple variable

    // This is called when a weapon is picked up
    public bool AddWeapon(GameObject weapon)
    {
        Weapon weaponScript = weapon.GetComponent<Weapon>();

        switch (weaponScript.weapontype)
        {
            case Weapon.WeaponType.Primary:
                if (Primary == null)
                {
                    Primary = weapon;
                    EquipWeapon(weapon);
                    return true;
                }
                break;

            case Weapon.WeaponType.Secondary:
                if (Secondary == null)
                {
                    Secondary = weapon;
                    EquipWeapon(weapon);
                    return true;
                }
                break;

            case Weapon.WeaponType.Grenade:
                if (Grenade == null)
                {
                    Grenade = weapon;
                    EquipWeapon(weapon);
                    return true;
                }
                break;

            case Weapon.WeaponType.Grapple: // Added Grapple case
                if (Grapple == null)
                {
                    Grapple = weapon;
                    EquipWeapon(weapon);
                    return true;
                }
                break;
        }

        return false;
    }

    private void EquipWeapon(GameObject weapon)
    {
        if (HandSlot != null)
        {
            HandSlot.SetActive(false);
        }

        weapon.SetActive(true);
        HandSlot = weapon;
    }

    public void RemoveWeapon(GameObject weapon)
    {
        Weapon weaponScript = weapon.GetComponent<Weapon>();
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

            case Weapon.WeaponType.Grapple: // Added Grapple case
                if (Grapple == weapon)
                {
                    Grapple = null;
                }
                break;
        }

        if (HandSlot == weapon)
        {
            HandSlot = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Primary != null)
        {
            EquipWeapon(Primary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Secondary != null)
        {
            EquipWeapon(Secondary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Grenade != null)
        {
            EquipWeapon(Grenade);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && Grapple != null) // Added Grapple case
        {
            EquipWeapon(Grapple);
        }
    }
}

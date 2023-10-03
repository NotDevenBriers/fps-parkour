using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Game Objects
    public GameObject HandSlot;
    public GameObject Primary;
    public GameObject Secondary;
    public GameObject Grenade;
    public GameObject Grapple;
    public GameObject Player;

    // Transform positions
    public Transform weaponHoldPosition;
    public Transform PrimaryHolsterPosition;
    public Transform SecondaryHolsterPosition;
    public Transform GrenadeHolsterPosition;
    public Transform GrappleHolsterPosition;

    // Other variables
    public Pickup pickUpScript;
    public bool grappleable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Primary != null) EquipWeapon(Primary);
        if (Input.GetKeyDown(KeyCode.Alpha2) && Secondary != null) EquipWeapon(Secondary);
        if (Input.GetKeyDown(KeyCode.Alpha3) && Grenade != null) EquipWeapon(Grenade);
        if (Input.GetKeyDown(KeyCode.Alpha4) && Grapple != null)
        {
            EquipWeapon(Grapple);
            grappleable = true;
        }
    }

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
                HolsterWeapon(weapon, PrimaryHolsterPosition);
                return true;

            case Weapon.WeaponType.Secondary:
                if (Secondary == null)
                {
                    Secondary = weapon;
                    EquipWeapon(weapon);
                    return true;
                }
                HolsterWeapon(weapon, SecondaryHolsterPosition);
                return true;

            case Weapon.WeaponType.Grenade:
                if (Grenade == null)
                {
                    Grenade = weapon;
                    EquipWeapon(weapon);
                    return true;
                }
                HolsterWeapon(weapon, GrenadeHolsterPosition);
                return true;

            case Weapon.WeaponType.Grapple:
                if (Grapple == null)
                {
                    Grapple = weapon;
                    EquipWeapon(weapon);
                    return true;
                }
                HolsterWeapon(weapon, GrappleHolsterPosition);
                return true;
        }

        return false;
    }

    private void EquipWeapon(GameObject weapon)
    {
        grappleable = false;
        HolsterWeapon(Primary, PrimaryHolsterPosition);
        HolsterWeapon(Secondary, SecondaryHolsterPosition);
        HolsterWeapon(Grenade, GrenadeHolsterPosition);
        HolsterWeapon(Grapple, GrappleHolsterPosition);

        if (weapon != null)
        {
            weapon.transform.SetParent(weaponHoldPosition);
            weapon.SetActive(true);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;

            if (weapon == Grapple)
            {
                grappleable = true;
                Player.GetComponent<GrapplingGun>().enabled = true;
            }
            else if (Grapple != null)
            {
                Player.GetComponent<GrapplingGun>().enabled = false;
            }
        }
    }

    private void HolsterWeapon(GameObject weapon, Transform holsterPosition)
    {
        if (weapon != null)
        {
            weapon.transform.SetParent(holsterPosition);
            weapon.SetActive(false);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
        }
    }

    public void RemoveWeapon(GameObject weapon)
    {
        Weapon weaponScript = weapon.GetComponent<Weapon>();

        switch (weaponScript.weapontype)
        {
            case Weapon.WeaponType.Primary:
                if (Primary == weapon) Primary = null;
                break;

            case Weapon.WeaponType.Secondary:
                if (Secondary == weapon) Secondary = null;
                break;

            case Weapon.WeaponType.Grenade:
                if (Grenade == weapon) Grenade = null;
                break;

            case Weapon.WeaponType.Grapple:
                if (Grapple == weapon)
                {
                    Grapple = null;
                    grappleable = false;
                    Player.GetComponent<GrapplingGun>().enabled = false;
                }
                break;
        }

        if (HandSlot == weapon) HandSlot = null;
    }

    private void Start()
    {
        InitialSetup();
    }

    private void InitialSetup()
    {
        // Disable the GrapplingGun script at the start of the game
        if (Player.GetComponent<GrapplingGun>() != null)
        {
            Player.GetComponent<GrapplingGun>().enabled = false;
        }
    }

}



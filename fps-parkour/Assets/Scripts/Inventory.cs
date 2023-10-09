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
    
    //OtherVariables
    public bool grappleable;

    // Scripts
    public Pickup pickUpScript;
    private GrapplingGun grapplingGunScript;
    private SwingingDone swingingDoneScript;

    private void Awake()
    {
        if (Player != null)
        {
            grapplingGunScript = Player.GetComponent<GrapplingGun>();
            swingingDoneScript = Player.GetComponent<SwingingDone>();
        }
    }

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
        if (weapon == null) return false;

        Weapon weaponScript = weapon.GetComponent<Weapon>();
        if (weaponScript == null) return false;

        switch (weaponScript.weapontype)
        {
            case Weapon.WeaponType.Primary:
                return HandleWeaponAddition(ref Primary, weapon, PrimaryHolsterPosition);
            case Weapon.WeaponType.Secondary:
                return HandleWeaponAddition(ref Secondary, weapon, SecondaryHolsterPosition);
            case Weapon.WeaponType.Grenade:
                return HandleWeaponAddition(ref Grenade, weapon, GrenadeHolsterPosition);
            case Weapon.WeaponType.Grapple:
                return HandleWeaponAddition(ref Grapple, weapon, GrappleHolsterPosition);
            default:
                Debug.LogError("Unknown weapon type!");
                return false;
        }
    }

    private bool HandleWeaponAddition(ref GameObject weaponSlot, GameObject weapon, Transform holsterPosition)
    {
        if (weaponSlot == null)
        {
            weaponSlot = weapon;
            EquipWeapon(weapon);
            return true;
        }
        HolsterWeapon(weapon, holsterPosition);
        return true;
    }

    private void EquipWeapon(GameObject weapon)
    {
        if (weapon == HandSlot) return;

        grappleable = false;
        HolsterAllWeapons();

        if (weapon != null)
        {
            weapon.transform.SetParent(weaponHoldPosition);
            weapon.SetActive(true);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            pickUpScript.heldWeapon = weapon;
            

            if (weapon == Grapple)
            {
                grappleable = true;
                if (grapplingGunScript != null) grapplingGunScript.enabled = true;
                if (swingingDoneScript != null) swingingDoneScript.enabled = true;
            }
            else
            {
                grappleable = false;
                if (grapplingGunScript != null) grapplingGunScript.enabled = false;
                if (swingingDoneScript != null) swingingDoneScript.enabled = false;
            }
        }
    }

    private void HolsterAllWeapons()
    {
        HolsterWeapon(Primary, PrimaryHolsterPosition);
        HolsterWeapon(Secondary, SecondaryHolsterPosition);
        HolsterWeapon(Grenade, GrenadeHolsterPosition);
        HolsterWeapon(Grapple, GrappleHolsterPosition);
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
        Weapon weaponScript = weapon?.GetComponent<Weapon>();
        if (weaponScript == null) return;

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
                    if (grapplingGunScript != null) grapplingGunScript.enabled = false;
                    if (swingingDoneScript != null) swingingDoneScript.enabled = false;
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
        if (grapplingGunScript != null)
        {
            grapplingGunScript.enabled = false;
            swingingDoneScript.enabled = false;
        }
    }
}

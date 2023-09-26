using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float pickUpRange = 10f;
    public LayerMask layerMask;
    public Transform weaponHoldPosition; // Assign in inspector, where the weapon should be positioned when held
    private GameObject heldWeapon = null;

    void Update()
    {
        PickUpCheck();

        // Dropping weapon
        if (Input.GetKeyDown(KeyCode.G) && heldWeapon != null)
        {
            DropWeapon();
        }
    }

    void PickUpCheck()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Assuming 'E' is the key to pick up objects
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray pickUpChecker = Camera.main.ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;

            if (Physics.Raycast(pickUpChecker, out hit, pickUpRange, layerMask.value) && heldWeapon == null)
            {
                heldWeapon = hit.transform.gameObject;
                PickupWeapon(heldWeapon);
            }
        }
    }

    void PickupWeapon(GameObject weapon)
    {
        weapon.transform.SetParent(weaponHoldPosition);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.GetComponent<Rigidbody>().isKinematic = true;
        weapon.GetComponent<Collider>().enabled = false;
    }

    void DropWeapon()
    {
        heldWeapon.transform.SetParent(null);
        Rigidbody rb = heldWeapon.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(transform.forward * 5f, ForceMode.Impulse); // Add some force to throw the weapon
        heldWeapon.GetComponent<Collider>().enabled = true;
        heldWeapon = null;
    }
}

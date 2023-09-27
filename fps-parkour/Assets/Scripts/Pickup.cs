using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Inventory inventory; // Reference to the Inventory script
    public float pickUpRange = 10f;
    public LayerMask layerMask;
    public Transform weaponHoldPosition;
    private GameObject heldWeapon = null;

    void Start()
    {
        inventory = GetComponent<Inventory>(); // Assume Inventory script is on the same GameObject
    }

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
        if (Input.GetKeyDown(KeyCode.E)) //'E' is the key to pick up objects
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

        // Add the weapon to the inventory
        inventory.AddWeapon(weapon);
    }

    void DropWeapon()
    {
        heldWeapon.transform.SetParent(null);
        Rigidbody rb = heldWeapon.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(transform.forward * 5f, ForceMode.Impulse); // Add some force to throw the weapon
        heldWeapon.GetComponent<Collider>().enabled = true;
        inventory.RemoveWeapon(heldWeapon);
        heldWeapon = null;
    }
}

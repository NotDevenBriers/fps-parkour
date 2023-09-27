using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShoot : MonoBehaviour
{
    [Header("Shooting Parameters")]
    public float damage = 100;
    public float range = 250f;
    public LayerMask layerMask;
    public Transform HandSlot;

    void Update()
    {
        HandleInput();
    }

    // Handles Input for Shooting
    void HandleInput()
    {
        if (Input.GetButtonDown("Fire1") && HandSlot.childCount >= 1)
        {
            Shoot();
        }
    }

    // Shoot a ray from the center of the screen
    void Shoot()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        // Draw ray for debugging
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red, 1f);

        if (Physics.Raycast(ray, out hit, range, layerMask.value))
        {
            Debug.Log(hit.transform.name);

            PlayAnim playAnim = hit.transform.GetComponent<PlayAnim>();
            if (playAnim != null)
            {
                playAnim.PlayAnimation("TargetFalling");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    public enum WeaponType
    {
        Primary,
        Secondary,
        Grenade,
        Grapple
    }

    public WeaponType weapontype;

}

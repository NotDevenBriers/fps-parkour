using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    [Header("References")]
    private Animator Anim;

    void Start()
    {
        Anim = GetComponent<Animator>();
        Anim.Play("TargetStill");
    }

    // Public function to play animation
    public void PlayAnimation(string anim)
    {
        Anim.Play("TargetFalling");
    }

}
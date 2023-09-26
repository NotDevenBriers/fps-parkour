using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour

{
    private Animator Anim;
    // Start is called before the first frame update
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

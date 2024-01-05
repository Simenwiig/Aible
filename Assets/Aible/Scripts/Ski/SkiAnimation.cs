using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkiAnimation : MonoBehaviour
{
    static Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public static void SetAnimationTrigger(string anim)
    {
        animator.SetTrigger(anim);
    }

    public static void SetAnimationBool(string anim, bool value)
    {
        animator.SetBool(anim, value);
    }

    public static void PlayAnimation(string anim)
    {
        animator.Play(anim);
    }
}

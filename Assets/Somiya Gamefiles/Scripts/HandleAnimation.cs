using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleAnimation : MonoBehaviour
{
    public void PlayAnimationWithvalue(string animName, float value, Animator anim)
    {
        anim.SetFloat(animName, value, 0.2f, Time.fixedDeltaTime);
    }

    public void PlayAnimation(string animName, Animator anim)
    {
        anim.Play(animName);
    }
}

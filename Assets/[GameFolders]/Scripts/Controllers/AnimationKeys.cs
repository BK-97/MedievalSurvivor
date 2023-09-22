using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationKeys
{
    #region PlayerAnimationKeys
    public static readonly int ATTACK_BOOL = Animator.StringToHash("Attack");
    public static readonly int GET_HIT_ANIMATION = Animator.StringToHash("GetHit");
    public static readonly int SPEED = Animator.StringToHash("MoveSpeed");
    public static readonly int DIE = Animator.StringToHash("Death");
    public static readonly int WEAPON_INDEX = Animator.StringToHash("WeaponIndex");
    #endregion
}

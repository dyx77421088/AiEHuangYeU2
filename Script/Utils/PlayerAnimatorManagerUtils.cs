using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主角的动画的控制
/// </summary>
public class PlayerAnimatorManagerUtils
{
    #region 角色的动画的管理
    /// <summary>
    /// 播放角色动画的管理类
    /// </summary>
    public static void SelectAnimation(Animator animator, PlayerAnimation animation)
    {
        switch (animation)
        {
            case PlayerAnimation.Move:
                SetMoveAnimation(animator);
                break;
            case PlayerAnimation.Idle:
                SetIdleAnimation(animator);
                break;
            case PlayerAnimation.Collect:
                SetCollectAnimation(animator);
                break;
        }
    }
    /// <summary>
    /// 移动动画
    /// </summary>
    private static void SetMoveAnimation(Animator animator)
    {
        animator.SetBool("IsMove", true);
    }
    /// <summary>
    /// 设置停留动画
    /// </summary>
    private static void SetIdleAnimation(Animator animator)
    {
        animator.SetBool("IsMove", false);
        animator.SetBool("IsCollect", false);
    }

    /// <summary>
    /// 采集的动画
    /// </summary>
    /// <param name="animator"></param>
    private static void SetCollectAnimation(Animator animator)
    {
        animator.SetBool("IsCollect", true);
        animator.SetBool("IsMove", false);
    }
    #endregion

    /// <summary>
    /// 判断是否在采集动画
    /// </summary>
    public static bool IsCollect(Animator animator)
    {
        return animator.GetBool("IsCollect");
    }
}

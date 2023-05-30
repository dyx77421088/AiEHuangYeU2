using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ǵĶ����Ŀ���
/// </summary>
public class PlayerAnimatorManagerUtils
{
    #region ��ɫ�Ķ����Ĺ���
    /// <summary>
    /// ���Ž�ɫ�����Ĺ�����
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
    /// �ƶ�����
    /// </summary>
    private static void SetMoveAnimation(Animator animator)
    {
        animator.SetBool("IsMove", true);
    }
    /// <summary>
    /// ����ͣ������
    /// </summary>
    private static void SetIdleAnimation(Animator animator)
    {
        animator.SetBool("IsMove", false);
        animator.SetBool("IsCollect", false);
    }

    /// <summary>
    /// �ɼ��Ķ���
    /// </summary>
    /// <param name="animator"></param>
    private static void SetCollectAnimation(Animator animator)
    {
        animator.SetBool("IsCollect", true);
        animator.SetBool("IsMove", false);
    }
    #endregion

    /// <summary>
    /// �ж��Ƿ��ڲɼ�����
    /// </summary>
    public static bool IsCollect(Animator animator)
    {
        return animator.GetBool("IsCollect");
    }
}

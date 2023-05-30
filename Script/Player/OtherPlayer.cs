using Common;
using Common.Model;
using UnityEngine;

/// <summary>
/// ���������û��Ĺ���������������
/// </summary>
public class OtherPlayer : MonoBehaviour
{
    [HideInInspector]
    private Player otherPlayer;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayer(Player other)
    {
        this.otherPlayer = other;
    }

    public int GetId()
    {
        return otherPlayer.Id;
    }

    public bool IsLeft()
    {
        return otherPlayer.IsLeft;
    }

    /// <summary>
    /// �Ƿ���Ҫ��ת
    /// </summary>
    public bool IsZhuan ()
    {
        Debug.Log(otherPlayer.IsLast + " " + otherPlayer.IsLeft);
        return otherPlayer.IsLast != otherPlayer.IsLeft;
    }

    public void SetTitle(string text)
    {
    }

    public void SelectAnimation(PlayerAnimation animation)
    {
        PlayerAnimatorManagerUtils.SelectAnimation(animator, animation);
    }
}

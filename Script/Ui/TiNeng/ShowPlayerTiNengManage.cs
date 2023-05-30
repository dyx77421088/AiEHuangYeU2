using Common.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerTiNengManage : MonoBehaviour
{
    public Slider tili, health, food, water;
    public float tiliSec, healthSec, foodSec, waterSec;

    private Text tiliText, healthText, foodText, waterText;
    private Player player;
    private Player Player
    {
        get
        {
            return player??= PlayerInfo.Instance.player;
        }
    }
    private static ShowPlayerTiNengManage instance;
    public static ShowPlayerTiNengManage Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        tiliText = tili.transform.GetChild(2).GetComponent<Text>();
        healthText = health.transform.GetChild(2).GetComponent<Text>();
        foodText = food.transform.GetChild(2).GetComponent<Text>();
        waterText = water.transform.GetChild(2).GetComponent<Text>();

        InitPlayerTiNeng();
    }

    public void InitPlayerTiNeng()
    {
        Debug.Log("player是否为空" + Player);

        SetUi();

        // 开启协程，开始掉体能
        StartCoroutine("StrengthIE");
        StartCoroutine("ResistanceIE");
        StartCoroutine("HungerDegreeIE");
        StartCoroutine("MoistureIE");
    }

    public void SetUi()
    {
        // 体力
        tiliText.text = Player.Strength + "/" + Player.StrengthUpperLimit;
        tili.value = Player.Strength * 1.0f / Player.StrengthUpperLimit;

        // 健康
        healthText.text = Player.Resistance + "/" + Player.ResistanceUpperLimit;
        health.value = Player.Resistance * 1.0f / Player.ResistanceUpperLimit;

        // 食物
        foodText.text = Player.HungerDegree + "/" + Player.HungerDegreeUpperLimit;
        food.value = Player.HungerDegree * 1.0f / Player.HungerDegreeUpperLimit;

        // 水分
        waterText.text = Player.Moisture + "/" + Player.MoistureUpperLimit;
        water.value = Player.Moisture * 1.0f / Player.MoistureUpperLimit;
        //Debug.Log(water.value);
    }

    /// <summary>
    /// 体力
    /// </summary>
    /// <returns></returns>
    private IEnumerator StrengthIE()
    {
        while(true)
        {
            if (Player.Animation == Common.PlayerAnimation.Idle )
            {
                yield return new WaitForSeconds(1);
                Player.Strength++;
            }
            // 如果用户不是在等待状态的话就要消耗体力
            yield return new WaitUntil(() => Player.Animation != Common.PlayerAnimation.Idle);
            yield return new WaitForSeconds(tiliSec);
            Player.Strength--;
            if (Player.Strength < 0) Player.Strength = 0;
            SetUi();
        }
    }

    /// <summary>
    /// 抵抗力
    /// </summary>
    /// <returns></returns>
    private IEnumerator ResistanceIE()
    {
        while (true)
        {
            yield return new WaitForSeconds(healthSec);
            Player.Resistance--;
            if (Player.Resistance < 0) Player.Resistance = 0;
            SetUi();
        }
    }

    /// <summary>
    /// 饥饿值
    /// </summary>
    /// <returns></returns>
    private IEnumerator HungerDegreeIE()
    {
        while (true)
        {
            yield return new WaitForSeconds(foodSec);
            Player.HungerDegree--;
            if (Player.HungerDegree < 0) Player.HungerDegree = 0;
            SetUi();
        }
    }

    /// <summary>
    /// 水分
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoistureIE()
    {
        while (true)
        {
            yield return new WaitForSeconds(waterSec);
            Player.Moisture--;
            if (Player.Moisture < 0) Player.Moisture = 0;
            SetUi();
        }
    }
}

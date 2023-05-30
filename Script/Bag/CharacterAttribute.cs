using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 属性显示
/// </summary>
public class CharacterAttribute : MonoBehaviour
{
    private Text attributeText;
    private PlayerBag player;
    private static CharacterAttribute instance;
    public static CharacterAttribute Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("Character Attribute Text").GetComponent<CharacterAttribute>();
            }
            return instance;
        }
    }

    void Start()
    {
        attributeText = GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBag>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showText()
    {
        // 循环装备统计属性
        //Character.Instance
        int strength = player.BaseStrength;
        int intellect = player.BaseIntellect;
        int agility = player.BaseAgility;
        int stamina = player.BaseStamina;

        Character.Instance.SetAttribute(attributeText, strength, intellect, agility, stamina);
    }
}

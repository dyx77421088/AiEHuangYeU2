using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Item;

namespace Assets.Script.Bag.Collect
{
    /// <summary>
    /// 物品的总类
    /// </summary>
    public class Article
    {
        private int id;
        private string name;
        private string description;
        private string sprite; // 图片
        private ArticleQuality quality;
        private int capacity;

        #region GetSet
        public int Id { get => id; set => id = value; }

        public string Name { get => name; set => name = value; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get => description; set => description = value; }

        /// <summary>
        /// 图片
        /// </summary>
        public string Sprite { get => sprite; set => sprite = value; }
        /// <summary>
        /// 品质
        /// </summary>
        public ArticleQuality Quality { get => quality; set => quality = value; }
        public int Capacity { get => capacity; set => capacity = value; }
        #endregion


        /// <summary>
        /// 根据品质返回对应的颜色
        /// </summary>
        /// <returns></returns>
        public string GetQualityColor()
        {
            switch (Quality)
            {
                case ArticleQuality.Common:
                    return "#ffffff"; // 白色
                case ArticleQuality.Uncommon:
                    return "#E3D337"; // 黄色
                case ArticleQuality.Rare:
                    return "#0000ff"; // 蓝色
                case ArticleQuality.Epic:
                    return "#ff0000"; // 红色
                case ArticleQuality.Legendary:
                    return "#E54FCE"; // 粉色
                case ArticleQuality.Artifact:
                    return "#8A1898"; // 紫色
                default:
                    break;
            }
            return "white";
        }


        #region 品质枚举类
        /// <summary>
        ///Common       一般         white 白色
        ///Uncommon 不一般        lime 绿黄色
        ///Rare 稀有         navy 深蓝色
        ///Epic 史诗        magenta 品红
        ///Legendary 传说        orange 橘色
        ///Artifact 远古        red 红色
        /// </summary>
        public enum ArticleQuality
        {
            /// <summary>
            /// 一般的 白色
            /// </summary>
            Common,
            /// <summary>
            /// 不一般的 黄色
            /// </summary>
            Uncommon,
            /// <summary>
            /// 稀有的 蓝色
            /// </summary>
            Rare,
            /// <summary>
            /// 史诗 红色
            /// </summary>
            Epic,
            /// <summary>
            /// 传说 粉色
            /// </summary>
            Legendary,
            /// <summary>
            /// 远古 深紫色
            /// </summary>
            Artifact
        }
        #endregion

        public virtual string TipShow()
        {
            return "";
        }

    }

}

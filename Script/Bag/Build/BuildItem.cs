using Assets.Script.Bag.Collect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Bag.Build
{
    public class BuildItem:Article
    {
        private float collecctTime;
        private List<BuildCollect> compositeList;
        private BuildItemType type;
        private int hungerDegree; // 消耗饥饿度
        private int moisture; // 水分
        private int strength; // 体力

        #region Get Set
        /// <summary>
        /// 制作需要的时间
        /// </summary>
        public float CollecctTime { get => collecctTime; set => collecctTime = value; }
        /// <summary>
        /// 制造需要的材料
        /// </summary>
        public List<BuildCollect> CompositeList { get => compositeList; set => compositeList = value; }
        /// <summary>
        /// 制造消耗的饥饿度
        /// </summary>
        public int HungerDegree { get => hungerDegree; set => hungerDegree = value; }
        /// <summary>
        /// 制造消耗的水分
        /// </summary>
        public int Moisture { get => moisture; set => moisture = value; }
        /// <summary>
        /// 制造消耗的体力
        /// </summary>
        public int Strength { get => strength; set => strength = value; }
        /// <summary>
        /// 制作的类型，可能是可用的工具或建筑物
        /// </summary>
        public BuildItemType Type { get => type; set => type = value; }
        #endregion
        public class BuildCollect
        {
            private int id;
            private int count;

            public int Id { get => id; set => id = value; }
            public int Count { get => count; set => count = value; }
        }

        public enum BuildItemType
        {
            None,
            /// <summary>
            /// 建筑
            /// </summary>
            Build,
            /// <summary>
            /// 工具
            /// </summary>
            Tool,
            Other
        }


        public override string TipShow()
        {
            string tip = string.Format("<color={0}><size=18>{1}</size></color>\n{2}\n"
                , GetQualityColor(), Name, Description);

            string qs = "#7CFFF0"; // 青色
            StringBuilder sb = new StringBuilder();
            if (hungerDegree != 0) sb.Append("饥饿度 ").Append(hungerDegree).AppendLine();
            if (moisture != 0) sb.Append("水分 ").Append(moisture).AppendLine();
            if (strength != 0) sb.Append("体力 ").Append(strength).AppendLine();
            return tip + string.Format("<color={0}>制作消耗</color>\n{1}", qs, sb);
        }
    }
}

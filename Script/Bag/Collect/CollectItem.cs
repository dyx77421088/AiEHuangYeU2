
namespace Assets.Script.Bag.Collect
{
    /// <summary>
    /// 采集的物品的类
    /// </summary>
    public class CollectItem: Article
    {
        
        private CollectType type;
        private int count;
        private float collectTime;


        /// <summary>
        /// 物品类型，材料，食物，其它
        /// </summary>
        public CollectType Type { get => type; set => type = value; }
        
        
        /// <summary>
        /// 采集可以获得的个数
        /// </summary>
        public int Count { get => count; set => count = value; }
        /// <summary>
        /// 采集需要的时间
        /// </summary>
        public float CollectTime { get => collectTime; set => collectTime = value; }

        #region 类型枚举类
        /// <summary>
        /// 材料，食物，其它
        /// Consumable Equipment Weapon Material
        /// </summary>
        public enum CollectType
        {
            None,
            /// <summary>
            /// 材料
            /// </summary>
            Material,
            /// <summary>
            /// 食物
            /// </summary>
            Food,
            /// <summary>
            /// 其它
            /// </summary>
            Other
        }
        #endregion

        private string GetTypeStr()
        {
            switch (Type)
            {
                case CollectType.Material: return "材料";
                case CollectType.Food: return "食物";
                case CollectType.Other: return "其它";
            }
            return "未知";
        }


        public override string TipShow()
        {
            string tip = string.Format("<color={0}><size=18>{1}</size></color>\n<size=12>类型:{2}</size>\n{3}\n"
                , GetQualityColor(), Name, GetTypeStr(), Description);
            return tip;
        }
    }
}

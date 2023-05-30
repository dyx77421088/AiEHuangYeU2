using System.Text;

namespace Assets.Script.Bag.Collect
{
    /// <summary>
    /// 食品类
    /// </summary>
    public class Food:CollectItem
    {
        private int hungerDegree; // 增加饥饿度
        private int moisture; // 水分
        private int resistance; // 抵抗力

        /// <summary>
        /// 增加饥饿度
        /// </summary>
        public int HungerDegree { get => hungerDegree; set => hungerDegree = value; }
        /// <summary>
        /// 增加水分
        /// </summary>
        public int Moisture { get => moisture; set => moisture = value; }
        /// <summary>
        /// 增加抵抗力
        /// </summary>
        public int Resistance { get => resistance; set => resistance = value; }

        public override string TipShow()
        {
            string qs = "#7CFFF0"; // 青色
            string str = base.TipShow();
            StringBuilder sb = new StringBuilder();
            if (hungerDegree != 0) sb.Append("饥饿度+").Append(hungerDegree).AppendLine();
            if (moisture != 0) sb.Append("水分+").Append(moisture).AppendLine();
            if (resistance != 0) sb.Append("抵抗力+").Append(resistance).AppendLine();
            return str + string.Format("<color={0}>回复效果</color>\n{1}", qs, sb);
        }
    }
}

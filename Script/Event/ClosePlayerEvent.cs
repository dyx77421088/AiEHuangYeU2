using Common.Model;
using Common.Util;
using Common;
using ExitGames.Client.Photon;
public class ClosePlayerEvent : Event
{
    public override void OnEvent(EventData eventData)
    {
        // �н�ɫ�뿪�ˣ��Ƴ���
        string text = DictUtils.GetStringValue<byte, object>(eventData.Parameters, (byte)ParameterCode.JsonData);
        Player other = MyJsonUtils.GetObject<Player>(text);
        OtherPlayerManage.Instance.CloseOtherPlayer(other);
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VirtualCastSaveLoadTool.VirtualCastClass
{
    class Connect
    {
        private const int maxVciIdCount = 7;
        public bool isTcpConnect = false;
        public ObservableCollection<BindingClass.SelectVCITab> selectVCITabItemSource = new ObservableCollection<BindingClass.SelectVCITab>();
        public ObservableCollection<BindingClass.SelectVCIKind> selectVCIKindItemSource = new ObservableCollection<BindingClass.SelectVCIKind>();
        public Dictionary<string, Dictionary<string, ObservableCollection<BindingClass.SelectVCIContaints>>> selectVCIContaintsItemSource = new Dictionary<string, Dictionary<string, ObservableCollection<BindingClass.SelectVCIContaints>>>();

        public Connect()
        {
            for (int count = 0; count < Define.VCI_MESSAGE_KIND.Length; count++)
            {
                selectVCIKindItemSource.Add(new BindingClass.SelectVCIKind
                {
                    messageType = Define.VCI_MESSAGE_KIND[count]
                });
            }
        }

        public async Task Start(int port)
        {
            isTcpConnect = true;
            ClientWebSocket ws = new ClientWebSocket();
            Uri uri = new Uri("ws://localhost:" + port.ToString() + "/");
            //VirtualCastとつなぐとき必須
            ws.Options.AddSubProtocol("Sec-WebSocket-Accept");
            try
            {
                await ws.ConnectAsync(uri, CancellationToken.None);
                byte[] allMessageBytes = new byte[2000 * 2000 * 4 + 500];
                ArraySegment<byte> segment = new ArraySegment<byte>(allMessageBytes);
                while (true)
                {
                    var result = await ws.ReceiveAsync(allMessageBytes, CancellationToken.None);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "OK", CancellationToken.None);
                        return;
                    }

                    //メッセージの最後まで取得
                    int count = result.Count;
                    while (!result.EndOfMessage)
                    {
                        if (count >= allMessageBytes.Length)
                        {
                            await ws.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "That's too long", CancellationToken.None);
                            return;
                        }
                        segment = new ArraySegment<byte>(allMessageBytes, count, allMessageBytes.Length - count);
                        result = await ws.ReceiveAsync(segment, CancellationToken.None);
                        count += result.Count;
                    }
                    allMessageBytesToItemSource(allMessageBytes);

                    if (!isTcpConnect) {
                        await ws.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "That's too long", CancellationToken.None);
                        return;
                    }
                    Task.Delay(1);
                }
            }
            catch (WebSocketException exc)
            {
            }
            catch (NullReferenceException exc)
            {
            }
        }

        public void End()
        {
            isTcpConnect = false;
        }

        private void allMessageBytesToItemSource(byte[] allMessageBytes)
        {
            int containtsCount = 0;
            for (containtsCount = allMessageBytes.Length - 1; containtsCount >= 0; containtsCount--) if (allMessageBytes[containtsCount] != 0x00) break;
            Array.Resize(ref allMessageBytes, containtsCount + 1);
            string getAllMessageTypeASCII = Encoding.ASCII.GetString(allMessageBytes);
            string messageEndString = "CallerFile";
            int messageStartIndex = 0;
            int messageEndIndex = getAllMessageTypeASCII.IndexOf(messageEndString);
            while (messageEndIndex >= 0)
            {
                messageEndIndex += messageEndString.Length;
                int messageLength = messageEndIndex - messageStartIndex;
                byte[] messageBytes = new byte[messageLength];
                for (int byteCount = 0; byteCount < messageLength; byteCount++) messageBytes[byteCount] = allMessageBytes[messageStartIndex + byteCount];

                messageBytesToItemSource(messageBytes);
                messageStartIndex = messageEndIndex;
                messageEndIndex = getAllMessageTypeASCII.IndexOf(messageEndString, messageStartIndex);
            }
        }

        private void messageBytesToItemSource(byte[] messageBytes)
        {
            string message = System.Text.Encoding.ASCII.GetString(messageBytes);

            string unixTimeStartString = "UnixTime?";
            int unixTimeStartIndex = message.IndexOf(unixTimeStartString);
            string unixTimeEndString = "?Category";
            int unixTimeEndIndex = message.IndexOf(unixTimeEndString);
            if ((unixTimeStartIndex == -1) || (unixTimeEndIndex == -1)) return;
            unixTimeStartIndex += unixTimeStartString.Length;

            string vciNameStartString = "?Category?Item_Print?LogLevel?Debug?Item?";
            int vciNameStartIndex = message.IndexOf(vciNameStartString);
            string vciNameEndString = "?VciId?";
            int vciNameEndIndex = message.IndexOf(vciNameEndString);
            if ((vciNameStartIndex == -1) || (vciNameEndIndex == -1)) return;
            vciNameStartIndex += vciNameStartString.Length;

            int vciIdStartIndex = message.IndexOf(vciNameEndString) + vciNameEndString.Length;
            int vciIdEndIndex = vciIdStartIndex + maxVciIdCount;

            string vciContaintsStartString = "?Message?";
            int vciContaintsStartIndex = message.IndexOf(vciContaintsStartString);
            string vciContaintsEndString = "\"?CallerFile";
            int vciContaintsEndIndex = message.IndexOf(vciContaintsEndString);
            if ((vciContaintsStartIndex == -1) || (vciContaintsEndIndex == -1)) return;
            string containtsStartString = "\"";
            vciContaintsStartIndex = message.IndexOf(containtsStartString, vciContaintsStartIndex);
            vciContaintsStartIndex += containtsStartString.Length;

            string unixDate;
            string vciName = "";
            string vciID = "";
            string vciMessage = "";
            var s = message.Substring(unixTimeStartIndex, unixTimeEndIndex - unixTimeStartIndex);
            int unixTime = int.Parse(message.Substring(unixTimeStartIndex, unixTimeEndIndex - unixTimeStartIndex));
            unixDate = DateTimeOffset.FromUnixTimeSeconds(unixTime).LocalDateTime.ToString();

            int vciNameCount = vciNameEndIndex - vciNameStartIndex;
            byte[] vciNameBytes = new byte[vciNameCount];
            for (int count = 0; count < vciNameCount; count++) vciNameBytes[count] = messageBytes[vciNameStartIndex + count];
            vciName = System.Text.Encoding.UTF8.GetString(vciNameBytes);
            vciID = message.Substring(vciIdStartIndex, vciIdEndIndex - vciIdStartIndex);

            int vciContaintsCount = vciContaintsEndIndex - vciContaintsStartIndex;
            byte[] vciContaintsBytes = new byte[vciContaintsCount];
            for (int count = 0; count < vciContaintsCount; count++) vciContaintsBytes[count] = messageBytes[vciContaintsStartIndex + count];
            vciMessage = System.Text.Encoding.UTF8.GetString(vciContaintsBytes);
            string vciMessageKind = SplitVCIKindFromMessage(ref vciMessage);
            if (vciMessageKind == "") return;

            TryAddSelectVCITabItemSource(vciName, vciID);
            AddSelectVCITabItemSourceContaints(vciName + vciID, vciMessageKind, unixDate, vciMessage);
        }

        private string SplitVCIKindFromMessage(ref string vciMessage)
        {
            for (int count = 0; count < Define.VCI_MESSAGE_KIND.Length; count++)
            {
                if (vciMessage.StartsWith(Define.VCI_MESSAGE_KIND[count]))
                {
                    vciMessage = vciMessage.Substring(Define.VCI_MESSAGE_KIND[count].Length);
                    return Define.VCI_MESSAGE_KIND[count];
                }
            }
            return "";
        }

        private void TryAddSelectVCITabItemSource(string vciName, string vciID)
        {
            for (int count = 0; count < selectVCITabItemSource.Count; count++)
            {
                if ((selectVCITabItemSource[count].vciName == vciName) && (selectVCITabItemSource[count].vciID == vciID)) return;
            }
            selectVCITabItemSource.Add(new BindingClass.SelectVCITab
            {
                vciName = vciName,
                vciID = vciID
            });
            AddSelectVCIContaintsItemSource(vciName, vciID);
        }

        private void AddSelectVCIContaintsItemSource(string vciName, string vciID)
        {
            Dictionary<string, ObservableCollection<BindingClass.SelectVCIContaints>> itemSourceDictionary = new Dictionary<string, ObservableCollection<BindingClass.SelectVCIContaints>>();
            for (int count = 0; count < Define.VCI_MESSAGE_KIND.Length; count++)
            {
                ObservableCollection<BindingClass.SelectVCIContaints> itemSource = new ObservableCollection<BindingClass.SelectVCIContaints>();
                itemSourceDictionary.Add(Define.VCI_MESSAGE_KIND[count], itemSource);
            }
            selectVCIContaintsItemSource.Add(vciName + vciID, itemSourceDictionary);
        }

        private void AddSelectVCITabItemSourceContaints(string selectVCITabKey, string vciMessageKind, string unixDate, string vciMessage)
        {
            //ImageSource imageSource = CreateImageSource(vciMessage);
            Dictionary<string, ObservableCollection<BindingClass.SelectVCIContaints>> itemSourceDictionary = selectVCIContaintsItemSource[selectVCITabKey];
            ObservableCollection<BindingClass.SelectVCIContaints> itemSource = itemSourceDictionary[vciMessageKind];
            itemSource.Add(new BindingClass.SelectVCIContaints
            {
                unixDate = unixDate,
                messageType = vciMessageKind,
                messageContaints = vciMessage
            });
            while (itemSource.Count > 5) itemSource.RemoveAt(0);
        }
    }
}

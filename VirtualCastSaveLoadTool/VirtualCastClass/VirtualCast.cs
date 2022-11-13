using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace VirtualCastSaveLoadTool.VirtualCastClass
{
    class VirtualCast
    {
        private Connect virtualCastConnect = new Connect();
        private Save virtualCastSave = new Save();
        private Load virtualCastLoad = new Load();

        public void Connect(int port)
        {
            virtualCastConnect.Start(port);
        }

        public void DisConnect()
        {
            virtualCastConnect.End();
        }

        public ObservableCollection<BindingClass.SelectVCITab> GetSelectVCITabItemSource()
        {
            return virtualCastConnect.selectVCITabItemSource;
        }

        public ObservableCollection<BindingClass.SelectVCIKind> GetSelectVCIKindItemSource()
        {
            return virtualCastConnect.selectVCIKindItemSource;
        }

        public ObservableCollection<BindingClass.SelectVCIContaints> GetSelectVCIContaintsItemSource(string selectVCITabKey, string vciMessageKind)
        {
            Dictionary<string, ObservableCollection<BindingClass.SelectVCIContaints>> itemSourceDictionary = virtualCastConnect.selectVCIContaintsItemSource[selectVCITabKey];
            return itemSourceDictionary[vciMessageKind];
        }

        public void SaveVCIContaints(string vciMessageKind, string vciMessage)
        {
            for (int kind = 0; kind < Define.VCI_MESSAGE_KIND.Length; kind++)
            {
                if (Define.VCI_MESSAGE_KIND[kind] == vciMessageKind)
                {
                    if (kind == (int)Define.VCI_MESSAGE_KIND_ENUM.save) virtualCastSave.SaveContaints(vciMessage);
                    else if (kind == (int)Define.VCI_MESSAGE_KIND_ENUM.png) virtualCastSave.CreatePng(vciMessage);
                    else if (kind == (int)Define.VCI_MESSAGE_KIND_ENUM.jpg) virtualCastSave.CreateJpg(vciMessage);
                }
            }
        }

        public void LoadVCIContaints()
        {
            virtualCastLoad.LoadContaints();
        }

        public bool GetIsConnectingTcp()
        {
            return virtualCastConnect.isTcpConnect;
        }
    }
}

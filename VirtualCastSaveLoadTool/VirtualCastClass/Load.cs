using Microsoft.Win32;
using System.IO;

namespace VirtualCastSaveLoadTool.VirtualCastClass
{
    class Load
    {
        public void LoadContaints()
        {
            string saveDirectry = Define.NOW_DIRECTRY_PATH + "\\" + Define.SAVE_DIRECTRY;
            string saveKindDirectry = saveDirectry + "\\" + Define.SAVE_KIND_DIRECTRY[(int)Define.VCI_MESSAGE_KIND_ENUM.save];

            OpenFileDialog sa = new OpenFileDialog();
            sa.Title = "セーブデータ読み込み";
            sa.InitialDirectory = @saveKindDirectry;
            sa.FileName = @"vciData.save";
            sa.Filter = "セーブファイル(*.save)|*.save;";
            sa.FilterIndex = 1;
            bool result = (bool)sa.ShowDialog();
            if (result)
            {
                string fileName = sa.FileName;
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string saveData = sr.ReadToEnd();
                    string keyEndString = ",";
                    int keyEndIndex = saveData.IndexOf(keyEndString);
                    string key = saveData.Substring(0, keyEndIndex);
                    string containts = saveData.Substring(keyEndIndex+1);
                    //luaファイル位置
                    string directryUserDeskTopPass = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
                    string directryUserPass = directryUserDeskTopPass.Substring(0, directryUserDeskTopPass.LastIndexOf("\\"));
                    string vciFolderPath = directryUserPass + "\\LocalLow\\infiniteloop Co,Ltd\\VirtualCast\\EmbeddedScriptWorkspace\\LoadDataMessage\\main.lua";
                    if (Directory.Exists(vciFolderPath.Substring(0, vciFolderPath.LastIndexOf("\\"))))
                    {
                        using (StreamWriter write = new StreamWriter(vciFolderPath, false))
                        {

                            string mainString = "local avatar = vci.studio.GetLocalAvatar()" +
                                "\nlocal playerID = avatar.GetId()" +
                                "\nlocal input = {playerID = playerID, containts = \""+ containts + "\"}" +
                                "\nvci.message.Emit(\"" + key + "\", input)";
                            write.Write(mainString);
                        }
                    }
                }
            }
        }
    }
}

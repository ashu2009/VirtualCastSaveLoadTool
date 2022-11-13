using System.IO;

namespace VirtualCastSaveLoadTool.VirtualCastClass
{
    class Define
    {
        public static string NOW_DIRECTRY_PATH = Directory.GetCurrentDirectory();
        public static string SAVE_DIRECTRY = "Save";

        public enum VCI_MESSAGE_KIND_ENUM { save, png, jpg };
        public static string[] VCI_MESSAGE_KIND = { "VCI_SAVE", "VCI_PNG", "VCI_JPG" };
        public static string[] SAVE_KIND_DIRECTRY = { "SaveData", "PNG", "JPG" };
    }
}

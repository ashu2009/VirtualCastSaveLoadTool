using Microsoft.Win32;
using OpenCvSharp;
using System.IO;

namespace VirtualCastSaveLoadTool.VirtualCastClass
{
    class Save
    {
        public void SaveContaints(string vciMessage)
        {
            string saveDirectry = Define.NOW_DIRECTRY_PATH + "\\" + Define.SAVE_DIRECTRY;
            CreateDirectry(saveDirectry);
            string saveKindDirectry = saveDirectry + "\\" + Define.SAVE_KIND_DIRECTRY[(int)Define.VCI_MESSAGE_KIND_ENUM.save];
            CreateDirectry(saveKindDirectry);

            SaveFileDialog sa = new SaveFileDialog();
            sa.Title = "ファイルを保存する";
            sa.InitialDirectory = @saveKindDirectry;
            sa.FileName = @"vciData.save";
            sa.Filter = "セーブファイル(*.save)|*.save;";
            sa.FilterIndex = 1;
            bool result = (bool)sa.ShowDialog();
            if (result)
            {
                string fileName = sa.FileName;
                using (var writer = new StreamWriter(new FileStream(fileName, FileMode.Create)))
                {
                    writer.Write(vciMessage);
                }
            }
        }

        public void CreatePng(string vciMessage)
        {
            string widthString = ",width=";
            int widthIndex = vciMessage.IndexOf(widthString);
            string heighString = ",heigh=";
            int heighIndex = vciMessage.IndexOf(heighString);
            string containtsString = ",containts=";
            int containtsIndex = vciMessage.IndexOf(containtsString);
            if ((widthIndex < 0) || (heighIndex < 0) || (containtsIndex < 0)) return;
            widthIndex += widthString.Length;
            int widthLength = heighIndex - widthIndex;
            heighIndex += heighString.Length;
            int heighLength = containtsIndex - heighIndex;
            if (!int.TryParse(vciMessage.Substring(widthIndex, widthLength), out int width)) return;
            if (!int.TryParse(vciMessage.Substring(heighIndex, heighLength), out int heigh)) return;
            vciMessage = vciMessage.Substring(containtsIndex + containtsString.Length);

            Mat img = new Mat(new OpenCvSharp.Size(width, heigh), MatType.CV_8UC4, new Scalar(0, 0, 0));
            char[] vciMessageByte= vciMessage.ToCharArray();
            int count = 0;
            bool isOut = false;
            for (int y = 0; y < heigh; y++)
            {
                if (isOut) break;
                for (int x = 0; x < width; x++)
                {
                    if (isOut) break;
                    Vec4b pix = img.At<Vec4b>(y, x);
                    for (int rgbaNumber = 0; rgbaNumber < 4; rgbaNumber++)
                    {
                        if (count >= vciMessageByte.Length)
                        {
                            isOut = true;
                            break;
                        }
                        pix[rgbaNumber] = ((byte)vciMessageByte[count]);
                        count++;
                    }
                    img.Set<Vec4b>(y, x, pix);
                }
            }

            string saveDirectry = Define.NOW_DIRECTRY_PATH + "\\" + Define.SAVE_DIRECTRY;
            CreateDirectry(saveDirectry);
            string saveKindDirectry = saveDirectry + "\\" + Define.SAVE_KIND_DIRECTRY[(int)Define.VCI_MESSAGE_KIND_ENUM.png];
            CreateDirectry(saveKindDirectry);

            SaveFileDialog sa = new SaveFileDialog();
            sa.Title = "画像を保存する";
            sa.InitialDirectory = @saveKindDirectry;
            sa.FileName = @"vciPicture.png";
            sa.Filter = "png(*.png)|*.png;";
            sa.FilterIndex = 1;
            bool result = (bool)sa.ShowDialog();
            if (result)
            {
                string fileName = sa.FileName;
                Cv2.ImWrite(fileName, img);
            }
        }
        public void CreateJpg(string vciMessage)
        {
            string widthString = ",width=";
            int widthIndex = vciMessage.IndexOf(widthString);
            string heighString = ",heigh=";
            int heighIndex = vciMessage.IndexOf(heighString);
            string containtsString = ",containts=";
            int containtsIndex = vciMessage.IndexOf(containtsString);
            if ((widthIndex < 0) || (heighIndex < 0) || (containtsIndex < 0)) return;
            widthIndex += widthString.Length;
            int widthLength = heighIndex - widthIndex;
            heighIndex += heighString.Length;
            int heighLength = containtsIndex - heighIndex;
            if (!int.TryParse(vciMessage.Substring(widthIndex, widthLength), out int width)) return;
            if (!int.TryParse(vciMessage.Substring(heighIndex, heighLength), out int heigh)) return;
            vciMessage = vciMessage.Substring(containtsIndex + containtsString.Length);

            Mat img = new Mat(new OpenCvSharp.Size(width, heigh), MatType.CV_8UC4, new Scalar(0, 0, 0));
            char[] vciMessageByte = vciMessage.ToCharArray();
            int count = 0;
            bool isOut = false;
            for (int y = 0; y < heigh; y++)
            {
                if (isOut) break;
                for (int x = 0; x < width; x++)
                {
                    if (isOut) break;
                    Vec4b pix = img.At<Vec4b>(y, x);
                    for (int rgbaNumber = 0; rgbaNumber < 4; rgbaNumber++)
                    {
                        if (count >= vciMessageByte.Length)
                        {
                            isOut = true;
                            break;
                        }
                        pix[rgbaNumber] = ((byte)vciMessageByte[count]);
                        count++;
                    }
                    img.Set<Vec4b>(y, x, pix);
                }
            }

            string saveDirectry = Define.NOW_DIRECTRY_PATH + "\\" + Define.SAVE_DIRECTRY;
            CreateDirectry(saveDirectry);
            string saveKindDirectry = saveDirectry + "\\" + Define.SAVE_KIND_DIRECTRY[(int)Define.VCI_MESSAGE_KIND_ENUM.jpg];
            CreateDirectry(saveKindDirectry);

            SaveFileDialog sa = new SaveFileDialog();
            sa.Title = "画像を保存する";
            sa.InitialDirectory = @saveKindDirectry;
            sa.FileName = @"vciPicture.jpg";
            sa.Filter = "jpg(*.jpg)|*.jpg;";
            sa.FilterIndex = 1;
            bool result = (bool)sa.ShowDialog();
            if (result)
            {
                string fileName = sa.FileName;
                Cv2.ImWrite(fileName, img);
            }
        }

        private void CreateDirectry(string directryPath)
        {
            if (!Directory.Exists(directryPath)) Directory.CreateDirectory(directryPath);
        }
    }
}
using System.IO;
using System.Drawing;

namespace AAI_Texture_Redrawer
{
    class OriginalImageManager
    {
        string[] ImageNames;
        string directory;
        int curId;
        string NewDir = "insMine";

        public OriginalImageManager(TextureType t, string dir)
        {
            ImageNames = t.GetImagesPaths(dir);
            directory = dir;
        }

        public Image GetImage(int id)
        {
            if (id >= ImageNames.Length)
                id = ImageNames.Length - 1;
            else if (id < 0)
                id = 0;
            curId = id;
            return Image.FromFile(ImageNames[id]);
        }

        public Image GetNext()
        {
            return GetImage(++curId);
        }

        public Image GetPrev()
        {
            return GetImage(--curId);
        }

        public int GetCount()
        {
            return ImageNames.Length;
        }

        public void SaveTranslatedText(Image img)
        {
            if (!Directory.Exists(directory + "\\" + NewDir))
            {
                Directory.CreateDirectory(directory + "\\" + NewDir);
            }
            img.Save(directory + "\\" + NewDir + ImageNames[curId].Remove(0, directory.Length));
        }
    }
}

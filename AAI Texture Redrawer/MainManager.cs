using System;
using System.IO;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace AAI_Texture_Redrawer
{
    public class MainManager
    {
        TextureType type;
        OriginalImageManager OIM;
        ImageDrawer ID;
        static Settings stgs;

        public static string[] names;
        public static TextureType[] types;

        static MainManager()
        {
            names = new string[]{ "item_n", "item_d", "item_t", "Keyword_l", "Keyword_s", "Keyword_t", "Win_Name", "select_l", "select_s" };
            types = new TextureType[]{ new ItemNType(), new ItemDType(), new ItemTType(), new KeywordIType(), new KeywordSType(), new KeywordTType(), new WinNameType(),
            new SelectIType(), new SelectSType() };
            stgs = new Settings();
        }

        public MainManager(string originalPath, TextureType t, Graphics g)
        {
            if (!Directory.Exists(originalPath))
            {
                throw new Exception("Directory not found. Check settings to fix this.");
            }
            OIM = new OriginalImageManager(t, originalPath);
            ID = new ImageDrawer(g);
            type = t;
            Initialize();
        }

        void Initialize()
        {
            ID.OriginalImage = OIM.GetImage(0);
            ID.TemplTextImage = type.DrawText(new string[] { "", "", "" });
            ID.DrawAllImages();
        }

        public static TextureType GetType(string name)
        {
            return (TextureType)types[Array.IndexOf(names, name)];
        }

        public static string GetPath(string name)
        {
            //return (string)paths[Array.IndexOf(names, name)];
            return stgs.GetPath(name);
        }

        public int GetCount()
        {
            return OIM.GetCount();
        }

        public void SaveTranslatedText()
        {
            RunSave();
        }

        public void RunSave()
        {
            Image img = type.OrigWay(ID.TemplTextImage);
            if (img == null)
                return;
            OIM.SaveTranslatedText(img);
        }

        public void DrawImages(int index)
        {
            ID.OriginalImage = OIM.GetImage(index);
            ID.DrawAllImages();
        }

        public void DrawNext()
        {
            ID.OriginalImage = OIM.GetNext();
            ID.DrawAllImages();
        }

        public void DrawLast()
        {
            ID.OriginalImage = OIM.GetPrev();
            ID.DrawAllImages();
        }

        public void SetTranslatedText(string[] text)
        {
            ID.TemplTextImage = type.DrawText(text);
            ID.DrawAllImages();
        }

        public void SaveAllTexts(string[][] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                DrawImages(i);
                SetTranslatedText(data[i]);
                SaveTranslatedText();
            }
        }

        public void SaveAllImagesFromFile(string path)
        {
            string[] data = File.ReadAllLines(path);
            string[][] lines = type.SplitStrings(data);
            SaveAllTexts(lines);
        }

        //Settings functions

        public static string[] GetSettingsPaths()
        {
            return stgs.GetPaths();
        }

        public static void SetPath(string key, string val)
        {
            stgs.SetPath(key, val);
        }

        public static void SaveSettings()
        {
            stgs.Save();
        }

        public static void OpenSettings()
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;

namespace AAI_Texture_Redrawer
{
    public abstract class TextureType
    {
        public string TypeName;
        public string FontName;
        public float FontSize;
        public int AddLong;
        public Color TextColor;
        public Image Template;
        public StringFormat Format;
        protected int[] transform;
        protected int RowCount;

        public TextureType(string typeName, string fontName, float fontSize,
            Color textColor, Image template, StringFormat format, int add, int count)
        {
            TypeName = typeName;
            FontName = fontName;
            FontSize = fontSize;
            TextColor = textColor;
            Template = template;
            Format = format;
            AddLong = add;
            RowCount = count;
        }

        public virtual string[] GetImagesPaths(string directory)
        {
            string[] filenames = Directory.GetFiles(directory);
            List<string> list = new List<string>();
            string sh = "";
            for (int i = 0; i < filenames.Length; i++)
            {
                if (filenames[i].Remove(0, directory.Length + 1).ToUpper().IndexOf(TypeName.ToUpper()) == 0)
                {
                    list.Add(filenames[i]);
                    sh += "\n" + filenames[i];
                }
            }
            return list.ToArray();
        }

        public abstract Image AddPreEffects(Image orig);

        public abstract Image AddPostEffects(Image orig);

        public virtual Image GetTextOn(string text, int x, int y, int width, int height, Image orig)
        {
            Image res = (Image)orig.Clone();
            using (Graphics graphics = Graphics.FromImage(res))
            {
                if (TextColor.B == 17 || TextColor.R == 34)
                    using (Font font = new Font(FontName, FontSize, FontStyle.Bold))
                    {
                        graphics.DrawString(text, font, new SolidBrush(TextColor), new Rectangle(x, y, width, height), Format);
                    }
                else if (TextColor.B == 153)
                {
                    using (Font font = new Font(FontName, FontSize, FontStyle.Bold))
                    {
                        graphics.DrawString(text, font, new SolidBrush(TextColor), new Rectangle(x, y, width, height), Format);
                    }
                }
                else
                    using (Font font = new Font(FontName, FontSize))
                    {
                        graphics.DrawString(text, font, new SolidBrush(TextColor), new Rectangle(x, y, width, height), Format);
                    }
            }
            return res;
        }

        public abstract Image DrawText(string[] text);
        public Image OrigWay(Image image)
        {
            Image img = (Image)image.Clone();
            return img;
        }

        public string[][] SplitStrings(string[] data)
        {
            string[][] res = new string[data.Length / RowCount][];
            for (int i = 0; i < data.Length / RowCount; i++)
            {
                res[i] = new string[RowCount];
                for (int j = 0; j < RowCount; j++)
                {
                    res[i][j] = data[i * RowCount + j];
                }
            }
            return res;
        }
    }
}

using System.Drawing;
using AAI_Texture_Redrawer.Properties;

namespace AAI_Texture_Redrawer
{
    class KeywordTType : TextureType
    {
        private const float XScale = 1.3f;
        public KeywordTType() : base("Keyword", "Open Sans", 35f, Color.FromArgb(255, 17, 85, 153), //Ruptured balloon
            Resources.Keyword_tTempl,
            new StringFormat
            {
                LineAlignment = StringAlignment.Center,
            }, 0, 3)
        {
        }

        public override Image AddPostEffects(Image orig)
        {
            return orig;
        }

        public override Image AddPreEffects(Image orig)
        {
            return orig;
        }

        public override Image DrawText(string[] text)
        {
            Bitmap bmp = new Bitmap((int)(Template.Width * XScale), Template.Height);
            Image tmp = GetTextOn(text[0], 1, 12, bmp.Width, 50, bmp);
            tmp = GetTextOn(text[1], 1, 66, bmp.Width, 50, tmp);
            tmp = GetTextOn(text[2], 1, 120, bmp.Width, 50, tmp);
            Image res = (Image)Template.Clone();
            using (Graphics g = Graphics.FromImage(res))
            {
                g.DrawImage(tmp, new Rectangle(0, 0, Template.Width, Template.Height));
            }
            return res;
        }
    }
}

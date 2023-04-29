using System.Drawing;
using AAI_Texture_Redrawer.Properties;

namespace AAI_Texture_Redrawer
{
    class SelectSType : TextureType
    {
        private const float K = 1.55f;
        public SelectSType() : base("select", "Arial Rounded MT Pro Cyr", 41f, Color.FromArgb(255, 119, 34, 0), //Ruptured balloon
            Resources.select_sTempl,
            new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            }, 0, 1)
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
            Bitmap bmp = new Bitmap((int)(452 * K), 86);
            Image tmp = GetTextOn(text[0], 0, 0, (int)(452 * K), 86, bmp);
            Image res = (Image)Template.Clone();
            using (Graphics g = Graphics.FromImage(res))
                g.DrawImage(tmp, new Rectangle(0, 0, 452, 86));
            return res;
        }
    }
}

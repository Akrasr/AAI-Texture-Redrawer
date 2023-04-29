using System.Drawing;
using AAI_Texture_Redrawer.Properties;

namespace AAI_Texture_Redrawer
{
    class SelectIType : TextureType
    {
        private const float K = 1.2f;
        public SelectIType() : base("select", "Arial Rounded MT Pro Cyr", 39f, Color.FromArgb(255, 119, 34, 0), //Ruptured balloon
            Resources.select_lTempl,
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
            Bitmap bmp = new Bitmap((int)(712 * K), 86);
            Image tmp = GetTextOn(text[0], 0, 0, (int)(712 * K), 86, bmp);
            Image res = (Image)Template.Clone();
            using (Graphics g = Graphics.FromImage(res))
                g.DrawImage(tmp, new Rectangle(0, 0, 712, 86));
            return res;
        }
    }
}

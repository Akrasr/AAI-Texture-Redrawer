using System.Drawing;
using AAI_Texture_Redrawer.Properties;

namespace AAI_Texture_Redrawer
{
    class ItemTType : TextureType
    {
        const float K = 1.29f;
        public ItemTType() : base("item", "Open Sans", 29f, Color.FromArgb(255, 34, 0, 0), //Ruptured balloon
            Resources.item_tTempl,
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
            Bitmap bmp = new Bitmap((int)(Template.Width * K), Template.Height);
            Image tmp = GetTextOn(text[0], (int)(13 * K), 69, bmp.Width, 42, bmp);
            tmp = GetTextOn(text[1], (int)(13 * K), 123, bmp.Width, 42, tmp);
            tmp = GetTextOn(text[2], (int)(13 * K), 177, bmp.Width, 42, tmp);
            Image res = (Image)Template.Clone();
            using (Graphics g = Graphics.FromImage(res))
            {
                g.DrawImage(tmp, new Rectangle(0, 0, Template.Width, Template.Height));
            }
            return res;
        }
    }
}

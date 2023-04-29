using System.Drawing;
using AAI_Texture_Redrawer.Properties;

namespace AAI_Texture_Redrawer
{
    class ItemNType : TextureType
    {
        public ItemNType() : base("item", "Open Sans", 27f, Color.FromArgb(255, 255, 170, 17), //Ruptured balloon
            Resources.item_nTempl,
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
            return GetTextOn(text[0], -100, 0, Template.Width + 200, Template.Height, Template);
        }
    }
}

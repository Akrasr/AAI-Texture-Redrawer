using System.Drawing;
using AAI_Texture_Redrawer.Properties;

namespace AAI_Texture_Redrawer
{
    class ItemDType : TextureType
    {
        public ItemDType() : base("item", "Open Sans SemiBold", 29f, Color.FromArgb(255, 255, 255, 255), //Ruptured balloon
            Resources.item_dTempl,
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
            Image tmp = GetTextOn(text[0], 30, 22, Template.Width, 50, Template);
            tmp = GetTextOn(text[1], 30, 70, Template.Width, 50, tmp);
            return GetTextOn(text[2], 30, 118, Template.Width, 50, tmp);
        }
    }
}

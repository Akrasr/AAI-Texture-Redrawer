using System.Drawing;
using AAI_Texture_Redrawer.Properties;

namespace AAI_Texture_Redrawer
{
    class KeywordIType : TextureType
    {
        private int length;
        private float K = 0.89f;
        private int K2 = 20;
        public KeywordIType() : base("Keyword", "Open Sans SemiBold", 39f, Color.FromArgb(255, 255, 255, 255), //Color.FromArgb(255, 68, 51, 34) - для схем
            Resources.Keyword_lTempl,
            new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
            }, 0, 1)
        {
        }

        public override Image DrawText(string[] text)
        {
            return SetTextOn(text[0], -100, 0, Template.Width + 200, Template.Height, Template);
        }
        public override Image AddPostEffects(Image orig)
        {
            return orig;
        }

        public override Image AddPreEffects(Image orig)
        {
            Color MainColor = Color.FromArgb(255, 0, 34, 85);//Color.FromArgb(255, 0, 34, 85); - для логики
            return AddEffects(orig, MainColor);
        }

        public Image SetTextOn(string text, int x, int y, int width, int height, Image orig)
        {
                Image res = (Image)orig.Clone();
                Bitmap tmp = new Bitmap(width * 3, height * 3);
                for (int i = 0; i < tmp.Width; i++)
                {
                    for (int j = 0; j < tmp.Height; j++)
                    {
                        tmp.SetPixel(i, j, Color.FromArgb(0, 0, 0, 0));
                    }
                }
                using (Graphics graphics = Graphics.FromImage(tmp))
                {
                    using (Font font = new Font(FontName, FontSize * 3))
                    {
                        DrawTextWithInterval(graphics, text, font, new SolidBrush(TextColor), new Rectangle(0, 0, width * 3, height * 3), Format, -70); //-70 - kjubrf -40 cx
                    }
                }
                tmp = AddPreEffects(tmp) as Bitmap;
            Bitmap bmp = new Bitmap(width * 3, height * 3);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                graphics.DrawImage(tmp, new Rectangle(0, 0, (int)(width * 3 * K), height * 3));
            }
            tmp = bmp;
            using (Graphics graphics = Graphics.FromImage(res))
                {
                    using (Font font = new Font(FontName, FontSize))
                    {
                    int rx = (int)(width * 3 - length * K) / 6 + x - K2;
                        graphics.DrawImage(tmp, new Rectangle(rx - 1, y - 2, width, height));
                    }
                }
                return AddPostEffects(res);
        }

        public void DrawTextWithInterval(Graphics graphics, string text, Font font, SolidBrush brush, Rectangle rct, StringFormat format, int interval)
        {
            Bitmap tmp = new Bitmap(rct.Width, rct.Height);
            for (int i = 0; i < tmp.Width; i++)
            {
                for (int j = 0; j < tmp.Height; j++)
                {
                    tmp.SetPixel(i, j, Color.FromArgb(0, 0, 0, 0));
                }
            }
            int x = 0;
            int y = 10;
            using (Graphics g = Graphics.FromImage(tmp))
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == ' ')
                    {
                        x += 98 + interval;
                        continue;
                    }
                    SizeF s = g.MeasureString("" + text[i], font);
                    int len = (int)s.Width;
                    g.DrawString("" + text[i], font, brush, new Rectangle(x, 0, rct.Width - x, rct.Height));
                    x += len + interval;
                }
            length = x;
            x = (rct.Width - x) / 2;
            graphics.DrawImage(tmp, new Rectangle(0, 0, tmp.Width, tmp.Height));
        }

        public Image Crop(Image image, Rectangle rct)
        {
            Bitmap bmp = image as Bitmap;

            // Crop the image:
            Bitmap cropBmp = bmp.Clone(rct, bmp.PixelFormat);

            return cropBmp;
        }

        public Image AddEffects(Image orig, Color MainColor)
        {
            Image n = (Image)orig.Clone();
            Bitmap b = n as Bitmap;
            Bitmap res = (Bitmap)b.Clone();
            for (int x = 0; x < b.Width; x++)
            {
                for (int y = 0; y < b.Height; y++)
                {
                    Color c = b.GetPixel(x, y);
                    if (c.A != 0)
                    {
                        int[][] Checks = GetChecks(x, y, b);
                        for (int x1 = 0; x1 < Checks[0].Length; x1++)
                        {
                            bool f = false;
                            for (int y1 = 0; y1 < Checks[1].Length; y1++)
                            {
                                if (b.GetPixel(Checks[0][x1], Checks[1][y1]).A == 0)
                                {
                                    int dx = x - Checks[0][x1];
                                    int dy = y - Checks[1][y1];
                                    res.SetPixel(Checks[0][x1], Checks[1][y1], MainColor);
                                    int w = 10; //10
                                    for (int r = 2; r < w; r++)
                                        for (int z = 2; z < w; z++)
                                        {
                                            if (((dy * z) * (dy * z) + (dx * r) * (dx * r)) > w * w)
                                                break;
                                            if (x - dx * r >= 0 && x - dx * r < b.Width && y - dy * z >= 0 && y - dy * z < b.Height)
                                                if (!ColorsEqual(res.GetPixel(x - dx * r, y - dy * z), this.TextColor) && !ColorsEqual(res.GetPixel(x - dx * r, y - dy * z), MainColor))
                                                    res.SetPixel(x - dx * r, y - dy * z, MainColor);
                                        }
                                }
                            }
                        }
                    }
                }
            }
            //AntiAliasing(res, MainColor);
            return res;
        }

        public static bool ColorsEqual(Color c1, Color c2)
        {
            return c1.R == c2.R && c1.G == c2.G && c1.B == c2.B && c1.A == c2.A;
        }

        public int[][] GetChecks(int x, int y, Bitmap b)
        {
            int xs = 3;
            bool l = x - 1 < 0;
            bool r = x + 1 == b.Width;
            if (l || r)
                xs = 2;
            int ys = 3;
            bool u = y - 1 < 0;
            bool d = y + 1 == b.Height;
            if (u || d)
                ys = 2;
            int[] checkx = new int[xs];
            if (l)
            {
                checkx[0] = x;
                checkx[1] = x + 1;
            }
            else if (r)
            {
                checkx[1] = x;
                checkx[0] = x - 1;
            }
            else
            {
                checkx[0] = x - 1;
                checkx[1] = x;
                checkx[2] = x + 1;
            }
            int[] checky = new int[ys];
            if (u)
            {
                checky[0] = y;
                checky[1] = y + 1;
            }
            else if (d)
            {
                checky[1] = y;
                checky[0] = y - 1;
            }
            else
            {
                checky[0] = y - 1;
                checky[1] = y;
                checky[2] = y + 1;
            }
            return new int[][] { checkx, checky };
        }
    }
}

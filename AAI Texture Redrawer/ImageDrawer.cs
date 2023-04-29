using System.Drawing;

namespace AAI_Texture_Redrawer
{
    class ImageDrawer
    {
        const int SECTLENGTH = 2;
        private Graphics Graph;
        public Image TemplTextImage;
        public Image OriginalImage;

        public ImageDrawer() { }

        public ImageDrawer(Graphics g)
        {
            this.Graph = g;
        }

        public void DrawImage(Image img, int sector)
        {
            DrawGran();
            int wid = (int)(Graph.VisibleClipBounds.Width);
            int hei = (int)(Graph.VisibleClipBounds.Height) / SECTLENGTH;
            float mas = GetMaschtab(img);
            int imagewid = (int)((float)img.Width * mas);
            int imagehei = (int)((float)img.Height * mas);
            int x = (wid - imagewid) / 2;
            int y = (hei - imagehei) / 2 + hei * sector;
            Graph.DrawImage(img, x, y, imagewid, imagehei);
        }
        public void DrawGran()
        {
            int wid = (int)(Graph.VisibleClipBounds.Width);
            int hei = (int)(Graph.VisibleClipBounds.Height) / SECTLENGTH;
            for (int i = 0; i < SECTLENGTH; i++)
            {
                Graph.DrawRectangle(new Pen(new SolidBrush(Color.Black)), new Rectangle(0, hei * i, wid, hei));
            }
        }

        public float GetMaschtab(Image img)
        {
            int wid = (int)(Graph.VisibleClipBounds.Width);
            int hei = (int)(Graph.VisibleClipBounds.Height) / SECTLENGTH;
            float wex = (float)wid / (float)img.Width;
            float hex = (float)hei / (float)img.Height;
            return wex < hex ? wex : hex;
        }

        public void Clear()
        {
            Graph.Clear(Form1.Back);
        }

        public void DrawAllImages()
        {
            Clear();
            DrawImage(OriginalImage, 0);
            DrawImage(TemplTextImage, 1);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.IO;

namespace TitledMap
{
    public partial class Form1 : Form
    {

        List<Tile> listTileOut = new List<Tile>();
        List<Tile> listTileIn = new List<Tile>();
        List<Bitmap> listBitMap = new List<Bitmap>();
        List<GameObject> listObjectIn = new List<GameObject>();
        enum TypeGame {
            Boss_Bat,
            Boss_Medusa,
            Boss_Simon,
            Enemy_Bat,
            Enemy_Bonepilla,
            Enemy_Ghost,
            Enemy_Medusahead,
            Enemy_Merman,
            Enemy_Panther,
            Enemy_Spearguar,
            Enemy_Zombie,
            Ground_Brick,
            Ground_Fireandle,
            Ground_Firetower,
            Ground_Go_In_Castle,
            Ground_Hidden,
            Ground_Lockdoor,
            Ground_Moving_Brick,
            Ground_Next,
            Ground_Opendoor,
            Ground_Stair_Down,
            Ground_Stair_Up,
            Ground_Trap
        }
        int tileWidth, tileHeight;
        int tileColOut, tileRowOut;
        int iWidth, iHeight;
        int tileColIn, tileRowIn;
        int[,] matrixOut;
        int[,] matrixIn;
        bool first = false;
        List<GameObject> listObject = new List<GameObject>();

        Point startPoint = new Point();
        Point endPoint = new Point();

        //Bitmap currentBitmap;
        int currentType=0;

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int memcmp(IntPtr b1, IntPtr b2, long count);

        public Form1()
        {
            InitializeComponent();
            
            prepareBitmap();
            //pbGame.Image = Image.FromFile(@"C:\Users\tient_000\Desktop\5.bmp");
        }
        void prepareBitmap()
        {
            Bitmap bmp = Properties.Resources.boss_bat;
            listBitMap.Add(bmp);
            Bitmap bmp1 = Properties.Resources.boss_medusa;
            listBitMap.Add(bmp1);
            Bitmap bmp2 = Properties.Resources.simon;
            listBitMap.Add(bmp2);
            Bitmap bmp3 = Properties.Resources.enemy_bat;
            listBitMap.Add(bmp3);
            Bitmap bmp4 = Properties.Resources.enemy_bonepillar;
            listBitMap.Add(bmp4);
            Bitmap bmp5 = Properties.Resources.enemy_ghost;
            listBitMap.Add(bmp5);
            Bitmap bmp6 = Properties.Resources.enemy_medusahead;
            listBitMap.Add(bmp6);
            Bitmap bmp7 = Properties.Resources.enemy_merman;
            listBitMap.Add(bmp7);
            Bitmap bmp8 = Properties.Resources.enemy_panther;
            listBitMap.Add(bmp8);
            Bitmap bmp9 = Properties.Resources.enemy_spearguard;
            listBitMap.Add(bmp9);
            Bitmap bmp10 = Properties.Resources.enemy_zombie;
            listBitMap.Add(bmp10);
            Bitmap bmp11 = Properties.Resources.ground_brick;
            listBitMap.Add(bmp11);
            Bitmap bmp12 = Properties.Resources.ground_firecandle;
            listBitMap.Add(bmp12);
            Bitmap bmp13 = Properties.Resources.ground_firetower;
            listBitMap.Add(bmp13);
            Bitmap bmp14 = Properties.Resources.ground_go_in_castle;
            listBitMap.Add(bmp14);
            Bitmap bmp15 = Properties.Resources.ground_hidden_brick;
            listBitMap.Add(bmp15);
            Bitmap bmp16 = Properties.Resources.ground_lockdoor;
            listBitMap.Add(bmp16);
            Bitmap bmp17 = Properties.Resources.ground_moving_brick;
            listBitMap.Add(bmp17);
            Bitmap bmp18 = Properties.Resources.ground_next;
            listBitMap.Add(bmp18);
            Bitmap bmp19 = Properties.Resources.ground_opendoor;
            listBitMap.Add(bmp19);
            Bitmap bmp20 = Properties.Resources.ground_stair_down;
            listBitMap.Add(bmp20);
            Bitmap bmp21 = Properties.Resources.ground_stair_up;
            listBitMap.Add(bmp21);
            Bitmap bmp22 = Properties.Resources.ground_trap;
            listBitMap.Add(bmp22);

            Bitmap bmp23 = Properties.Resources.item_axe;
            listBitMap.Add(bmp23);
            Bitmap bmp24 = Properties.Resources.item_cross;
            listBitMap.Add(bmp24);
            Bitmap bmp25 = Properties.Resources.item_knife;
            listBitMap.Add(bmp25);
            Bitmap bmp26 = Properties.Resources.item_holy_water;
            listBitMap.Add(bmp26);
            Bitmap bmp27 = Properties.Resources.item_stop_watch;
            listBitMap.Add(bmp27);
            Bitmap bmp28 = Properties.Resources.item_morning_star;
            listBitMap.Add(bmp28);
            Bitmap bmp29 = Properties.Resources.item_double_shot;
            listBitMap.Add(bmp29);
            Bitmap bmp30 = Properties.Resources.item_small_heart;
            listBitMap.Add(bmp30);
            Bitmap bmp31 = Properties.Resources.item_big_heart;
            listBitMap.Add(bmp31);
            Bitmap bmp32 = Properties.Resources.item_money_bag;
            listBitMap.Add(bmp32);
            Bitmap bmp33 = Properties.Resources.item_roast;
            listBitMap.Add(bmp33);
            Bitmap bmp34 = Properties.Resources.item_rosary;
            listBitMap.Add(bmp34);
            Bitmap bmp35 = Properties.Resources.item_spirit_ball;
            listBitMap.Add(bmp35);
            Bitmap bmp36 = Properties.Resources.item_none;
            listBitMap.Add(bmp36);
            
        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public class Tile
        {
            int id;
            Bitmap image;
            public Tile(Bitmap image = null, int id = 0)
            {
                Image = image;
                Id = id;
            }
            public Bitmap Image
            {
                get { return image; }
                set { image = value; }
            }
            public int Id
            {
                get { return id; }
                set { id = value; }
            }

        }

        private void btnLoadPhoto_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                listTileOut.Clear();
                Image i = Image.FromFile(openFileDialog1.FileName);
                pbPhoto.Width = i.Width;
                pbPhoto.Height = i.Height;
                pbPhoto.BackgroundImage = (Bitmap)i;
                pbPhoto.Image = new Bitmap(i.Width, i.Height);
                prepareProperties(i);
            }
        }
        private void CreateOutPutTile(Image image)
        {
            matrixOut = new int[tileRowOut, tileColOut];

            for (int i = 0; i < tileRowOut; i++)
            {
                for (int j = 0; j < tileColOut; j++)
                {
                    Bitmap bmp = bmCrop(image, new Rectangle(j * tileWidth, i * tileWidth, tileWidth, tileWidth));
                    Tile t = new Tile(bmp);
                    bool exist = false;
                    for (int b = 0; b < listTileOut.Count; b++)
                    {
                        if (CompareBitmap(t.Image, listTileOut[b].Image))
                        {
                            matrixOut[i, j] = listTileOut[b].Id;
                            exist = true;
                            break;
                        }
                    }
                    if (exist) continue;
                    listTileOut.Add(t);
                    t.Id = listTileOut.Count - 1;
                    matrixOut[i, j] = t.Id;
                }
            }
        }
     
        private void ShowResult()
        {
            pbPhoto.Width = listTileOut.Count * tileWidth;
            pbPhoto.Height = tileHeight;
            pbPhoto.BackgroundImage = new Bitmap(pbPhoto.Width, pbPhoto.Height);
            int listTileCount = listTileOut.Count;

            for (int i = 0; i < listTileCount; i++)
            {
                DrawImageToImage((Bitmap)pbPhoto.BackgroundImage, listTileOut[i].Image, new Point(i * tileWidth, 0));
            }
            pbPhoto.Invalidate();
        }
        private void saveResult(string filePath)
        {
            System.IO.StreamWriter writer = new StreamWriter(filePath + "\\TileMaTrix.txt");
            StringBuilder contentSave = new StringBuilder("");
            contentSave.Append(listTileOut.Count + "\r\n");
            contentSave.Append(tileWidth + "\r\n");
            contentSave.Append(tileHeight + "\r\n");
            contentSave.Append(tileColOut + "\r\n");
            contentSave.Append(tileRowOut + "\r\n");

            for (int i = 0; i < tileRowOut; i++)
            {
                for (int j = 0; j < tileColOut; j++)
                {
                    contentSave.Append(matrixOut[i, j] + " ");
                }
                contentSave.Append("\r\n");
            }
            writer.Write(contentSave);
            writer.Close();
            pbPhoto.BackgroundImage.Save(filePath + "\\TileMap.png");
        }
        private void prepareProperties(Image i)
        {
            tileWidth = int.Parse(txtTileWidth.Text);
            tileHeight = int.Parse(txtTileHeight.Text);
            tileColOut = i.Width / tileWidth;
            tileRowOut = i.Height / tileHeight;
            
        }
       
        Bitmap bmCrop(Image src, Rectangle rectClip)
        {
            Bitmap b = new Bitmap(rectClip.Width, rectClip.Height);
            DrawImageToImage(b, (Bitmap)src, Point.Empty, rectClip);
            return b;
        }
        public bool compareMemCmp(Bitmap b1, Bitmap b2)
        {
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;

            var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format64bppArgb);
            var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format64bppArgb);

            try
            {
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;

                int stride = bd1.Stride;
                int len = stride * b1.Height;

                return memcmp(bd1scan0, bd2scan0, len) == 0;
            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }
        bool CompareBitmap(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1 == null || bmp2 == null)
                return false;
            if (object.Equals(bmp1, bmp2))
                return true;
            if (!bmp1.Size.Equals(bmp2.Size) || !bmp1.PixelFormat.Equals(bmp2.PixelFormat))
                return false;

            int bytes = bmp1.Width * bmp1.Height * (Image.GetPixelFormatSize(bmp1.PixelFormat) / 8);

            bool result = true;
            byte[] b1bytes = new byte[bytes];
            byte[] b2bytes = new byte[bytes];

            BitmapData bitmapData1 = bmp1.LockBits(new Rectangle(0, 0, bmp1.Width - 1, bmp1.Height - 1), ImageLockMode.ReadOnly, bmp1.PixelFormat);
            BitmapData bitmapData2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width - 1, bmp2.Height - 1), ImageLockMode.ReadOnly, bmp2.PixelFormat);

            Marshal.Copy(bitmapData1.Scan0, b1bytes, 0, bytes);
            Marshal.Copy(bitmapData2.Scan0, b2bytes, 0, bytes);

            for (int n = 0; n <= bytes - 1; n++)
            {
                if (b1bytes[n] != b2bytes[n])
                {
                    result = false;
                    break;
                }
            }
            bmp1.UnlockBits(bitmapData1);
            bmp2.UnlockBits(bitmapData2);

            return result;
        }
        void DrawImageToImage(Bitmap bmDrawTo, Bitmap bmDraw, Point pos)
        {
            Graphics.FromImage(bmDrawTo).DrawImage(bmDraw,
                              new Rectangle(pos.X, pos.Y, bmDrawTo.Width, bmDrawTo.Height),
                              new Rectangle(0, 0, bmDrawTo.Width, bmDrawTo.Height),
                              GraphicsUnit.Pixel);
        }
        void DrawImageToImage(Bitmap bmDrawTo, Bitmap bmDraw, Point pos, Rectangle rectClip)
        {
            Graphics.FromImage(bmDrawTo).DrawImage(bmDraw,
                              new Rectangle(pos.X, pos.Y,
                              rectClip.Width, rectClip.Height),
                              rectClip,
                              GraphicsUnit.Pixel);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            DialogResult result = saveFile.ShowDialog();
            if (result == DialogResult.OK)
            {

                if (!Directory.Exists(saveFile.FileName))
                {
                    Directory.CreateDirectory(saveFile.FileName);
                }

                saveResult(saveFile.FileName);
            }
        }

        private void btnClip_Click(object sender, EventArgs e)
        {
            Image i = pbPhoto.BackgroundImage;
            CreateOutPutTile(i);
            ShowResult();
        }

        private void btnLoadTile_Click(object sender, EventArgs e)
        {
            loadMatrix();
            loadTile();
        }

        private void loadMatrix()
        {
            DialogResult result = openFileDialog2.ShowDialog();
            openFileDialog2.Filter = "Text Documents|*.txt";
            if (result == DialogResult.OK)
            {
                StreamReader rd = new StreamReader(openFileDialog2.FileName);
                String TileCount = rd.ReadLine();
                String TileWidth = rd.ReadLine();
                String TileHeight = rd.ReadLine();
                String TileWidthCount = rd.ReadLine();
                String TileHeightCount = rd.ReadLine();

                tileColIn = int.Parse(TileWidthCount);
                tileRowIn = int.Parse(TileHeightCount);
                tileWidth = int.Parse(TileWidth);
                tileHeight = int.Parse(TileHeight);
                iHeight = tileHeight * tileRowIn;
                iWidth = tileWidth * tileColIn;

                matrixIn = new int[tileRowIn, tileColIn];
                for (int i = 0; i < tileRowIn; i++)
                {
                    String s = "";
                    s += rd.ReadLine();
                    String[] p = s.Split(' ');
                    for (int j = 0; j < p.Length - 1; j++)
                    {
                        matrixIn[i, j] = int.Parse(p[j]);
                    }
                }
                rd.Close();
            }
        }
        private void loadTile()
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                Image j = Image.FromFile(openFileDialog1.FileName);
                pbPhoto.Width = j.Width;
                pbPhoto.Height = j.Height;
                pbPhoto.BackgroundImage = (Bitmap)j;
                
                int Count = tileRowIn*tileColIn;

                for (int i = 0; i < Count; i++)
                {
                    Bitmap bmp = bmCrop((Bitmap)j, new Rectangle(i * tileWidth, 0, tileWidth, tileHeight));
                    Tile t = new Tile(bmp,i);
                    listTileIn.Add(t);
                }
                pbPhoto.Width = iWidth;
                pbPhoto.Height = iHeight;
                pbPhoto.BackgroundImage = new Bitmap(pbPhoto.Width, pbPhoto.Height);
                pbPhoto.Image = new Bitmap(pbPhoto.Width, pbPhoto.Height);
                for (int i = 0; i < tileRowIn; i++)
                {
                    for (int k = 0; k < tileColIn; k++)
                    {
                        int tamp = matrixIn[i, k];
                        DrawImageToImage((Bitmap)pbPhoto.BackgroundImage, listTileIn[tamp].Image, new Point(k*tileWidth, i* tileHeight ));
                    }
                }
               
                pbPhoto.Invalidate();

            }
        }

        private void pbPhoto_MouseClick(object sender, MouseEventArgs e)
        {
            //DrawImageToImage((Bitmap)pbPhoto.Image, listBitMap[currentType], new Point(e.X,e.Y));
            //pbPhoto.Invalidate();
            //Rectangle bound = new Rectangle(e.X,e.Y,currentBitmap.Width,currentBitmap.Height);
            //GameObject tamp = new GameObject(listObject.Count,bound,1);
            //listObject.Add(tamp);
        }
        private QuadTree _quadTreeRoot;
        private List<StringBuilder> buildQuadTree()
        {

            //if (tileRowIn != 0 && tileColIn != 0) {
            //    int maxSize = (this.tileRowIn * 16 > this.tileColIn * 16) ? this.tileRowIn * 16 : this.tileColIn * 16;
            //}
            //else
            //{
                
            //}
            int maxSize = pbPhoto.BackgroundImage.Width > pbPhoto.BackgroundImage.Height ? pbPhoto.BackgroundImage.Width : pbPhoto.BackgroundImage.Height;
            Rectangle recRoot = new Rectangle(0, 0, maxSize, maxSize);
            List<StringBuilder> listQuadNode = new List<StringBuilder>();
            _quadTreeRoot = new QuadTree(recRoot);
            _quadTreeRoot.buildQuadTree(listObject);
            _quadTreeRoot.export(_quadTreeRoot._root, listQuadNode);
            return listQuadNode;
        }

        private void btnSaveObject_Click(object sender, EventArgs e)
        {
            List<StringBuilder> listQuadNode = this.buildQuadTree();
            System.IO.TextWriter writeFile;
            string filePath = "quad.txt";
            if (!System.IO.File.Exists(filePath))
            {
                writeFile = new System.IO.StreamWriter(filePath, true);
            }
            else
            {
                System.IO.File.Delete(filePath);
                writeFile = new System.IO.StreamWriter(filePath, false);
            }
            int size = listQuadNode.Count;
            for (int i = 0; i < size; i++)
            {
                writeFile.WriteLine(listQuadNode[i]);
            }
            writeFile.Close();
            writeFile.Dispose();
            filePath = "object.txt";
            if (!System.IO.File.Exists(filePath))
            {
                writeFile = new System.IO.StreamWriter(filePath, true);
            }
            else
            {
                System.IO.File.Delete(filePath);
                writeFile = new System.IO.StreamWriter(filePath, false);
            }
            foreach (GameObject tamp in listObject)
            {
                writeFile.WriteLine(tamp.export());
            }
            writeFile.Close();
            writeFile.Dispose();
        }

        private void pbPhoto_MouseDown(object sender, MouseEventArgs e)
        {
            
            startPoint.X = e.X;
            startPoint.Y = e.Y;
            first = true;
        }

        private void pbPhoto_MouseUp(object sender, MouseEventArgs e)
        {

            if (currentType == -1)
            {
                detectBitmap(startPoint);
                return;
            }
            endPoint.X = e.X;
            endPoint.Y = e.Y;
            if(first)
                if (currentType > 10)
                {
                    DrawAndSave(listBitMap[currentType], startPoint, endPoint);
                }
                else
                {
                    DrawMoveableObject(listBitMap[currentType], startPoint, endPoint);
                }
        }
        private void DrawMoveableObject(Bitmap gObject, Point startPoint, Point endPoint)
        {
            Point draw = new Point();
            draw.X = (startPoint.X + endPoint.X - gObject.Width) / 2;
            draw.Y = (startPoint.Y + endPoint.Y-gObject.Height) / 2;
            
            int x = listBitMap[currentType].Width;
            int y =  listBitMap[currentType].Height;
           
            Bitmap Bmp = new Bitmap(x, y);
            using (Graphics gfx = Graphics.FromImage(Bmp))
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255)))
            {
                gfx.FillRectangle(brush, 0, 0, x, y);
            }
            DrawObject(Bmp, startPoint, endPoint);
            DrawImageToImage((Bitmap)pbPhoto.Image, gObject, new Point(draw.X, draw.Y));
            Point start = new Point();
            Rectangle bound = new Rectangle(draw.X, draw.Y, Math.Abs(startPoint.X - endPoint.X), Math.Abs(startPoint.Y - endPoint.Y));
            GameObject tamp = new GameObject(listObject.Count, bound, currentType,startPoint,endPoint);
            listObject.Add(tamp);
            pbPhoto.Invalidate();
        }
        private void DrawAndSave(Bitmap gObject, Point startPoint, Point endPoint)
        {
            Point start = new Point();
            start.X = startPoint.X < endPoint.X ? startPoint.X : endPoint.X;
            start.Y = startPoint.Y < endPoint.Y ? startPoint.Y : endPoint.Y;
            Rectangle bound = new Rectangle(start.X, start.Y, Math.Abs(startPoint.X - endPoint.X), Math.Abs(startPoint.Y - endPoint.Y));
            int max = Math.Abs(startPoint.X - endPoint.X) > Math.Abs(startPoint.Y - endPoint.Y) ? Math.Abs(startPoint.X - endPoint.X) : Math.Abs(startPoint.Y - endPoint.Y);

            if (bound.Width >= bound.Height)
            {
                int sum = max > gObject.Width ? max / gObject.Width : 1;
                int more = max % gObject.Width;
                int increaseHeight = bound.Height / sum;
                int increaseWidth = gObject.Width;
                if (startPoint.X != start.X)
                {
                    increaseWidth = -increaseWidth;
                    startPoint.X += increaseWidth;
                    more = -more;
                }
                if (startPoint.Y != start.Y) increaseHeight = -increaseHeight;
                for (int i = 0; i < sum; i++)
                {
                    DrawImageToImage((Bitmap)pbPhoto.Image, gObject, new Point(startPoint.X + increaseWidth * i, startPoint.Y + increaseHeight * i));
                    Rectangle boundO = new Rectangle(startPoint.X + increaseWidth * i, startPoint.Y + increaseHeight * i, gObject.Width, gObject.Height);
                    GameObject tamp = new GameObject(listObject.Count, boundO, currentType);
                    listObject.Add(tamp);
                    if (i == sum - 1)
                    {
                        if (more != 0)
                        {
                            DrawImageToImage((Bitmap)pbPhoto.Image, gObject, new Point(startPoint.X + increaseWidth * i + more, startPoint.Y + increaseHeight * i));
                            Rectangle bound1 = new Rectangle(startPoint.X + increaseWidth * i + more, startPoint.Y + increaseHeight * i, gObject.Width, gObject.Height);
                            GameObject tamp1 = new GameObject(listObject.Count, bound1, currentType);
                            listObject.Add(tamp1);
                        }
                    }
                }

            }
            if (bound.Width < bound.Height)
            {
                int sum = max > gObject.Height ? max / gObject.Height : 1;
                int more = max % gObject.Height;
                int increaseHeight = gObject.Height;
                int increaseWidth = bound.Width / sum;
                if (startPoint.X != start.X)
                {
                    increaseWidth = -increaseWidth;
                    startPoint.Y += increaseHeight;
                    more = -more;
                }
                if (startPoint.Y != start.Y) increaseHeight = -increaseHeight;
                for (int i = 0; i < sum; i++)
                {
                    DrawImageToImage((Bitmap)pbPhoto.Image, gObject, new Point(startPoint.X + increaseWidth * i, startPoint.Y + increaseHeight * i));
                    Rectangle boundO = new Rectangle(startPoint.X + increaseWidth * i, startPoint.Y + increaseHeight * i, gObject.Width, gObject.Height);
                    GameObject tamp = new GameObject(listObject.Count, boundO, currentType);
                    listObject.Add(tamp);
                    if (i == sum - 1)
                    {
                        if (more != 0)
                        {
                            DrawImageToImage((Bitmap)pbPhoto.Image, gObject, new Point(startPoint.X + increaseWidth * i, startPoint.Y + increaseHeight * i + more));
                            Rectangle bound1 = new Rectangle(startPoint.X + increaseWidth * i, startPoint.Y + increaseHeight * i + more, gObject.Width, gObject.Height);
                            GameObject tamp1 = new GameObject(listObject.Count, bound1, currentType);
                            listObject.Add(tamp1);
                        }
                    }
                }

            }
            pbPhoto.Invalidate();
            //DrawImageToImage((Bitmap)pbPhoto.Image, currentBitmap, new Point(e.X, e.Y));
        }
        private void DrawObject(Bitmap gObject, Point startPoint, Point endPoint)
        {
            Point start = new Point();
            start.X = startPoint.X < endPoint.X ? startPoint.X : endPoint.X;
            start.Y = startPoint.Y < endPoint.Y ? startPoint.Y : endPoint.Y;
            Rectangle bound = new Rectangle(start.X, start.Y, Math.Abs(startPoint.X - endPoint.X), Math.Abs(startPoint.Y - endPoint.Y));
            int max = Math.Abs(startPoint.X - endPoint.X) > Math.Abs(startPoint.Y - endPoint.Y) ? Math.Abs(startPoint.X - endPoint.X) : Math.Abs(startPoint.Y - endPoint.Y);
            
            if (bound.Width >= bound.Height)
            {
                int sum = max>gObject.Width? max / gObject.Width:1;
                int more = max % gObject.Width;
                int increaseHeight = bound.Height / sum;
                int increaseWidth = gObject.Width;
                if (startPoint.X != start.X)
                {
                    increaseWidth = -increaseWidth;
                    startPoint.X += increaseWidth;
                    more = -more;
                }
                if (startPoint.Y != start.Y) increaseHeight = -increaseHeight;
                for (int i = 0; i < sum; i++)
                {
                    DrawImageToImage((Bitmap)pbPhoto.Image, gObject, new Point(startPoint.X + increaseWidth * i, startPoint.Y + increaseHeight * i));

                    if (i == sum - 1)
                    {
                        if (more != 0)
                        {
                            DrawImageToImage((Bitmap)pbPhoto.Image, gObject, new Point(startPoint.X + increaseWidth * i+ more, startPoint.Y + increaseHeight * i));
                        }
                    }
                }
                
            }
            if (bound.Width < bound.Height)
            {
                int sum =max>gObject.Height? max / gObject.Height:1;
                int more = max % gObject.Height;
                int increaseHeight = gObject.Height;
                int increaseWidth = bound.Width / sum;
                if (startPoint.X != start.X)
                {
                    increaseWidth = -increaseWidth;
                    startPoint.Y += increaseHeight;
                    more = -more;
                }
                if (startPoint.Y != start.Y) increaseHeight = -increaseHeight;
                for (int i = 0; i < sum; i++)
                {
                    DrawImageToImage((Bitmap)pbPhoto.Image, gObject, new Point(startPoint.X + increaseWidth * i, startPoint.Y + increaseHeight * i));
                    
                    if (i == sum - 1)
                    {
                        if (more != 0)
                        {
                            DrawImageToImage((Bitmap)pbPhoto.Image, gObject, new Point(startPoint.X + increaseWidth * i, startPoint.Y + increaseHeight * i + more));
                        }
                    }
                }
                
            }
            pbPhoto.Invalidate();
            //DrawImageToImage((Bitmap)pbPhoto.Image, currentBitmap, new Point(e.X, e.Y));
        }

        private void ground_brick_Click(object sender, EventArgs e)
        {
            currentType = 11;
        }

        private void ground_hidden_brick_Click(object sender, EventArgs e)
        {
            currentType = 15;
        }

        private void ground_firecandle_Click(object sender, EventArgs e)
        {
            currentType = 12;
        }

        private void ground_stair_up_Click(object sender, EventArgs e)
        {
            currentType = 21;
        }

        private void ground_stair_down_Click(object sender, EventArgs e)
        {
            currentType = 20;
        }

        private void ground_trap_Click(object sender, EventArgs e)
        {
            currentType = 22;
        }

        private void ground_go_in_castle_Click(object sender, EventArgs e)
        {
            currentType = 14;
        }

        private void ground_firetower_Click(object sender, EventArgs e)
        {
            currentType = 13;
        }

        private void ground_moving_brick_Click(object sender, EventArgs e)
        {
            currentType = 17;
        }

        private void ground_lockdoor_Click(object sender, EventArgs e)
        {
            currentType = 16;
        }

        private void ground_opendoor_Click(object sender, EventArgs e)
        {
            currentType = 19;
        }

        private void enemy_zombie_Click(object sender, EventArgs e)
        {
            currentType = 10;
        }

        private void enemy_merman_Click(object sender, EventArgs e)
        {
            currentType = 7;
        }

        private void enemy_spearguard_Click(object sender, EventArgs e)
        {
            currentType = 9;
        }

        private void enemy_bonepillar_Click(object sender, EventArgs e)
        {
            currentType = 10;
        }

        private void enemy_bat_Click(object sender, EventArgs e)
        {
            currentType = 3;
        }

        private void enemy_ghost_Click(object sender, EventArgs e)
        {
            currentType = 5;
        }

        private void enemy_medusahead_Click(object sender, EventArgs e)
        {
            currentType = 6;
        }

        private void enemy_panther_Click(object sender, EventArgs e)
        {
            currentType = 8;
        }

        private void boss_bat_Click(object sender, EventArgs e)
        {
            currentType = 0;
        }

        private void boss_medusa_Click(object sender, EventArgs e)
        {
            currentType = 1;
        }

      

        private string exportObject()
        {
            string result = "";
            foreach (GameObject tamp in listObject)
            {
                result += tamp.export();
            }
            return result;
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            currentType = -1;
        }

        private void pbPhoto_Click(object sender, EventArgs e)
        {
          
        }

        

        private void detectBitmap(Point pCor)
        {
            
            for(int i = listObject.Count-1;i>=0;i--){
                if(listObject[i]._bounds.Contains(pCor)){
                    Bitmap tamp = listBitMap[listObject[i].type];
                    //tamp.MakeTransparent();
                    //using (Graphics g = Graphics.FromImage((Bitmap)pbPhoto.BackgroundImage))
                    // {
                    //     g.Clip = new Region(listObject[i]._bounds);
                    //     g.Clear(Color.FromArgb(0, Color.White));
                    //     pbPhoto.Invalidate();
                    // }
                    Bitmap clone = new Bitmap(pbPhoto.Image);
                    //DrawImageToImage(clone, tamp, new Point(listObject[i]._bounds.X, listObject[i]._bounds.Y));
                    for (int y = 0; y < tamp.Height; ++y)
                    {
                        for (int x = 0; x < tamp.Width; ++x)
                        {
                            clone.SetPixel(listObject[i]._bounds.X + x, listObject[i]._bounds.Y + y, Color.Transparent);
                        }
                    }
                    //clone.MakeTransparent();
                    pbPhoto.Image=clone;

                    listObject.RemoveAt(i);
                    pbPhoto.Invalidate();
                }
                
            }
        }

        private void item_axe_Click(object sender, EventArgs e)
        {
            currentType = 23;
        }

        private void item_cross_Click(object sender, EventArgs e)
        {
            currentType = 24;
        }

        private void item_knife_Click(object sender, EventArgs e)
        {
            currentType = 25;
        }

        private void item_holy_water_Click(object sender, EventArgs e)
        {
            currentType = 26;
        }

        private void item_stop_watch_Click(object sender, EventArgs e)
        {
            currentType = 27;
        }

        private void item_morning_star_Click(object sender, EventArgs e)
        {
            currentType = 28;
        }

        private void item_double_shot_Click(object sender, EventArgs e)
        {
            currentType = 29;
        }

        private void item_small_heart_Click(object sender, EventArgs e)
        {
            currentType = 30;
        }

        private void item_big_heart_Click(object sender, EventArgs e)
        {
            currentType = 31;
        }

        private void item_money_bag_Click(object sender, EventArgs e)
        {
            currentType = 32;
        }

        private void item_roast_Click(object sender, EventArgs e)
        {
            currentType = 33;
        }

        private void item_rosary_Click(object sender, EventArgs e)
        {
            currentType = 34;
        }

        private void item_spirit_ball_Click(object sender, EventArgs e)
        {
            currentType = 35;
        }

        private void item_none_Click(object sender, EventArgs e)
        {
            currentType = 36;
        }

        private void btnLoadObject_Click(object sender, EventArgs e)
        {
            string startupPath = Environment.CurrentDirectory;
            string[] lines = System.IO.File.ReadAllLines(startupPath+@"\object.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] obj = lines[i].Split(' ');
                Rectangle tamp = new Rectangle(int.Parse(obj[2]), int.Parse(obj[3]), int.Parse(obj[4]), int.Parse(obj[5]));
                GameObject tampG = new GameObject(int.Parse(obj[0]), tamp, int.Parse(obj[1]), new Point(int.Parse(obj[6]), int.Parse(obj[7])), new Point(int.Parse(obj[8]), int.Parse(obj[9])));
                listObjectIn.Add(tampG);
            }

            foreach (GameObject tamp in listObjectIn)
            {
                if (tamp.type > 10)
                {
                    DrawImageToImage((Bitmap)pbPhoto.Image, listBitMap[tamp.type], new Point(tamp._bounds.X, tamp._bounds.Y));
                }
                else
                {
                    Bitmap Bmp = new Bitmap(listBitMap[tamp.type].Width, listBitMap[tamp.type].Height);
                    using (Graphics gfx = Graphics.FromImage(Bmp))
                    using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255)))
                    {
                        gfx.FillRectangle(brush, 0, 0, listBitMap[tamp.type].Width, listBitMap[tamp.type].Height);
                    }
                    DrawObject(Bmp, tamp.pointStart, tamp.pointEnd);
                    DrawImageToImage((Bitmap)pbPhoto.Image, listBitMap[tamp.type], new Point(tamp._bounds.X, tamp._bounds.Y));
                }
            }
        }
        private void loadObjec()
        {
            
        }
        
    }
}

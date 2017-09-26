/// <summary>
/// 类说明：验证码
/// </summary>
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace JDI.Utility.VerifyImage
{
   public class VerifyImage
    {
        private static Random rand = new Random();
        private static Font[] fonts ={
                                        new Font("Times New Roman",16+Next(4),FontStyle.Bold),
                                        new Font("Georgia",16+Next(4),FontStyle.Bold),
                                        new Font("Arial",16+Next(4),FontStyle.Bold),
                                        new Font("Comic Sans MS",16+Next(4),FontStyle.Bold)
                                     };

        #region GenerateCheckCode
        /// <summary>
        /// 验证码生成器
        /// </summary>
        /// <param name="codeCount">验证码个数</param>
        /// <returns></returns>
        private static string GenerateCheckCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,J,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            StringBuilder checkCode = new StringBuilder();
            for (int i = 0; i < codeCount; i++)
            {
                int temp = Next(allCharArray.Length - 1);
                checkCode.Append(allCharArray[temp]);
            }
            return checkCode.ToString();
        }
        #endregion

        #region GenerateImage
        /// <summary>
        /// 验证图片生成器
        /// </summary>
        /// <param name="codeCount">验证码个数</param>
        /// <param name="checkCode">输出验证码</param>
        /// <returns></returns>
        public static VerifyImageInfo GenerateImage(int codeCount, out string checkCode, string bgcolor)
        {
            VerifyImageInfo vif = new VerifyImageInfo();
            checkCode = GenerateCheckCode(codeCount);
            int iwidth = codeCount * 25;
            Bitmap image = new Bitmap(iwidth, 30);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(ToColor(bgcolor));
                g.SmoothingMode = SmoothingMode.HighSpeed;
                Pen arcPen = new Pen(Color.FromArgb(Next(50), Next(50), Next(50)), 1);
                for (int i = 0; i < 4; i++)
                {
                    g.DrawArc(arcPen, Next(20) - 10, Next(20) - 10, Next(iwidth) + 10, Next(30) + 10, Next(-90, 90), Next(-180, 180));
                }

                using (Bitmap charbmp = new Bitmap(30, 30))
                {
                    using (Graphics charg = Graphics.FromImage(charbmp))
                    {
                        using (Matrix m = new Matrix())
                        {
                            float offsetx = -20;
                            SolidBrush blackBrush = new SolidBrush(Color.Black);
                            for (int j = 0; j < checkCode.Length; j++)
                            {
                                m.Reset();
                                m.RotateAt(Next(50) - 25, new PointF(Next(3) + 7, Next(3) + 7));
                                charg.Clear(Color.Transparent);
                                charg.Transform = m;
                                charg.DrawString(checkCode[j].ToString(), fonts[Next(fonts.Length - 1)], blackBrush, new PointF(0, 0));
                                charg.ResetTransform();
                                offsetx += 20 + Next(5);
                                PointF drawPoint = new PointF(offsetx, 2);
                                g.DrawImage(charbmp, drawPoint);
                            }
                            vif.Image = image;
                            SetPageNoCache();
                        }
                    }
                }
            }
            return vif;
        }
        #endregion

        #region SetPageNoCache
        /// <summary>
        /// 设置缓存
        /// </summary>
        private static void SetPageNoCache()
        {
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Expires = -1;
            HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.Cache.SetNoStore();
        }
        #endregion

        #region 随机数生成器
        /// <summary>
        /// 随机数生成器
        /// </summary>
        /// <param name="max">最大随机数</param>
        /// <returns></returns>
        public static int Next(int max)
        {
            return rand.Next(max);
        }

        /// <summary>
        /// 随机数生成器
        /// </summary>
        /// <param name="min">最小随机数</param>
        /// <param name="max">最大随机数</param>
        /// <returns></returns>
        public static int Next(int min, int max)
        {
            return rand.Next(min, max);
        }

        public static Color ToColor(string strColor)
        {
            int red, green, blue = 0;
            char[] rgb;
            strColor = strColor.TrimStart('#');
            strColor = Regex.Replace(strColor.ToLower(), "[g-zG-Z]", "");
            switch (strColor.Length)
            {
                case 3:
                    rgb = strColor.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[0].ToString(), 16);
                    green = Convert.ToInt32(rgb[1].ToString() + rgb[1].ToString(), 16);
                    blue = Convert.ToInt32(rgb[2].ToString() + rgb[2].ToString(), 16);
                    return Color.FromArgb(red, green, blue);
                case 6:
                    rgb = strColor.ToCharArray();
                    red = Convert.ToInt32(rgb[0].ToString() + rgb[1].ToString(), 16);
                    green = Convert.ToInt32(rgb[2].ToString() + rgb[3].ToString(), 16);
                    blue = Convert.ToInt32(rgb[4].ToString() + rgb[5].ToString(), 16);
                    return Color.FromArgb(red, green, blue);
                default:
                    return Color.FromName(strColor);
            }
        }
        #endregion
    }

   /// <summary>
   /// 验证图片信息
   /// </summary>
   public class VerifyImageInfo
   {
       private Bitmap image;
       private string contentType = "image/pjpeg";
       private ImageFormat imageFormat = ImageFormat.Jpeg;

       /// <summary>
       /// 输出图片
       /// </summary>
       public Bitmap Image
       {
           get { return image; }
           set { image = value; }
       }

       /// <summary>
       /// 输出图片类型
       /// </summary>
       public string ContentType
       {
           get { return contentType; }
           set { contentType = value; }
       }

       /// <summary>
       /// 图片格式
       /// </summary>
       public ImageFormat ImageFormat
       {
           get { return imageFormat; }
           set { imageFormat = value; }
       }
   }
}

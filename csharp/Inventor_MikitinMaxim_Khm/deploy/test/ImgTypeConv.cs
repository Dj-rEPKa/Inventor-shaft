using System;
using System.Collections.Generic;
using Inventor;
using System.Drawing;
using System.Windows.Forms;

namespace InvAddIn
{
    class ImgTypeConv : AxHost
    {
        private ImgTypeConv() : base(String.Empty) {}

        private static stdole.IPictureDisp img_to_picture(Image img)
        {
            return (stdole.IPictureDisp)GetIPictureDispFromPicture(img);
        }

        private static stdole.IPictureDisp ico_to_picture(Icon ico)
        {
            return Img_to_Picture(ico.ToBitmap());
        }

        public static stdole.IPictureDisp Img_to_Picture(Image img)
        {
            return img_to_picture(img);
        }

        public static stdole.IPictureDisp Ico_to_Picture(Icon ico)
        {
            return ico_to_picture(ico);
        }
        
    }
}

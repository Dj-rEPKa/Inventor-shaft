using Inventor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace InvAddIn
{
    class var_es
    {
        public static Inventor.Application InventorApp;

        private static string ClientId;
        public static string _ClientId
        {
            get
            {
                if (!String.IsNullOrEmpty(ClientId))
                    return var_es.ClientId;
                else
                    throw new Exception("Yet, there is no value here!");
            }
            set { var_es.ClientId = value; }
        }


        public static List<Section> list;
        public static List<Section> _list;
        public static List<chamf> chamfer_list;
        public static List<SketchLine> lines;
        public static List<Feature> feature_list;

        public static void Get_addInClassId(Type t)
        {
            GuidAttribute gAttr = (GuidAttribute)GuidAttribute.GetCustomAttribute(t, typeof(GuidAttribute));
            ClientId = "{" + gAttr.Value + "}";
        }

        private static AssemblyDocument Doc;
        public static AssemblyDocument _Doc
        {
            get
            {
                return var_es.Doc;
            }

            set { var_es.Doc = value; }
        }

        // public static AssemblyDocument assemb_doc;
        public static PartDocument part_doc;
        public static PartComponentDefinition part_doc_def;

        // public static string path;

        public static void attach_active_doc()
        {
            Doc = (AssemblyDocument)var_es.InventorApp.ActiveDocument;
            if (String.IsNullOrEmpty(InventorApp.ActiveDocument.FullDocumentName))
                throw new Exception();

        }


        private static Bitmap[] imgs;
        public static Bitmap[] _imgs
        {
            get
            {
                if (imgs.Length != 0)
                {
                    return imgs;
                }
                else
                    throw new Exception("Yet, there is no images here!");
            }
            set { imgs = value; }
        }

        public static string[] chamfer_right_text = { "no feature", "Fillet", "Chamfer", "Ring", "Groove", "Undefined" };
        public static string[] chamfer_left_text = { "no feature", "Fillet", "Chamfer", "Ring", "Groove", "Undefined" };
        public static string[] section_text = { "Cylynder", "Conus", "Polygon" };
        public static string[] side_text = { "Measure from the left edge", "Measure from the right edge" };

        #region Combo Tree   


        public static string[] text_left = { "no feature", "Fillet", "Chamfer" };
        public static string[] text_mid = { "Cylynder", "Conus", "Polygon" };
        public static string[] text_right = { "no feature", "Fillet", "Chamfer" };
        public static string[] text_last = { "no feature", "Ring", "Groove", "Undefined" };



        #endregion
        public static void img_find()
        {
            IList<Bitmap> imgs_list = new List<Bitmap>();
            try
            {
                string[] fileEntries = Directory.GetFiles(@"imgs\", "*.bmp");

                foreach (string fileName in fileEntries)
                {
                    imgs_list.Add(new Bitmap(@"" + fileName));
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
            imgs = new Bitmap[imgs_list.Count];
            imgs_list.CopyTo(imgs, 0);
        }

        public static Feature feature;
    }

    public class DATA
    {
        string _nick;
        public string Name
        {
            get { return _nick; }
            set { _nick = value; }
        }

        double _value;
        public double Size
        {
            get { return _value; }
            set { _value = value; }
        }

        string _info;
        public string Description
        {
            get { return _info; }
            set { _info = value; }
        }
    }


}

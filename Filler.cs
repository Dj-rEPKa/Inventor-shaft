using System;
using System.Windows.Forms;
using Inventor;

namespace InvAddIn
{
    public partial class Filler : Form
    {
        
        public Filler(ref ListView listView1, char side, Section figure, int index)
        {
            InitializeComponent();
            Side = side;
            Figure = figure;
            lv = listView1;
            ID = index;
        }

        private ListView lv;
        private int ID;
        private char Side;
        private Section Figure;

        private void Filler_Load(object sender, EventArgs e)
        {
            Name = "Fillet";
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Side == 'r')
                    lv.Items[ID].SubItems[2].Text = "Fillet";
                else
                    lv.Items[ID].SubItems[0].Text = "Fillet";

                if (!String.IsNullOrEmpty(textBox1.Text.ToString()))
                {
                    ID += 1;
                    ID *= 2;
                    if (Side == 'l')
                    {
                        ID -= 2;
                        var_es.features_list.RemoveAt(ID);
                        fill filler = new fill(Convert.ToDouble(textBox1.Text), Side);
                        var_es.features_list.Insert(ID,filler);
                        if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                            addInForm.Del();
                        addInForm.Shaft();
                    }
                    else
                    {
                        ID -= 1;
                        var_es.features_list.RemoveAt(ID);
                        fill filler = new fill(Convert.ToDouble(textBox1.Text), Side);
                        var_es.features_list.Insert(ID, filler);
                        if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                            addInForm.Del();
                        addInForm.Shaft();
                    }
                    Close();
                }
            }
            catch
            { }
        }
    }
}

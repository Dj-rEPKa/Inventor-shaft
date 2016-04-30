using System;
using System.Windows.Forms;
using Inventor;

namespace InvAddIn
{
    public partial class chamfer : Form
    {
        public chamfer(ref ListView listView1, char side,  Section figure, int index)
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

        private string type = "Distance";

        private void chamfer_Load(object sender, EventArgs e)
        {
            assemble();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void assemble()
        {

            FormBorderStyle = FormBorderStyle.FixedToolWindow;

            Name = "Chamfer";
            button3.BackgroundImage = var_es._imgs[3];
            button4.BackgroundImage = var_es._imgs[1];
            button5.BackgroundImage = var_es._imgs[2];
            label2.Visible = false;
            textBox2.Visible = false;
            label3.Visible = false;
            textBox3.Visible = false;
            button3.Select();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            textBox2.Visible = false;
            label3.Visible = false;
            textBox3.Visible = false;
            label1.Text = "Distance";

            type = "Distance";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            textBox2.Visible = true;

            type = "Distance and Angle";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            textBox2.Visible = false;
            label3.Visible = true;
            textBox3.Visible = true;
            label1.Text += " 1";

            type = "Distance and Distance";
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (Side == 'r')
                lv.Items[ID].SubItems[2].Text = "Chamfer";
            else
                lv.Items[ID].SubItems[0].Text = "Chamfer";
            switch (type)
            {
                case ("Distance"):
                    try
                    {
                        ID += 1;
                        ID *= 2;
                        if (Side == 'l')
                        {
                            ID -= 2;
                            var_es.features_list.RemoveAt(ID);
                            chamf distance_chamfer = new chamf(Convert.ToDouble(textBox1.Text), Side);
                            var_es.features_list.Insert(ID, distance_chamfer);
                            if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                                addInForm.Del();
                            addInForm.Shaft();
                        }
                        else
                        {
                            ID -= 1;
                            var_es.features_list.RemoveAt(ID);
                            chamf distance_chamfer = new chamf(Convert.ToDouble(textBox1.Text), Side);
                            var_es.features_list.Insert(ID, distance_chamfer);
                            if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                                addInForm.Del();
                            addInForm.Shaft();
                        }
                        Close();
                    }
                    catch
                    { }
                    break;
                case ("Distance and Angle"):
                    try
                    {
                        ID += 1;
                        ID *= 2;
                        if (Side == 'l')
                        {
                            ID -= 2;
                            var_es.features_list.RemoveAt(ID);
                            Angle_chamf angle_chamfer = new Angle_chamf(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Side);
                            var_es.features_list.Insert(ID, angle_chamfer);
                            if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                                addInForm.Del();
                            addInForm.Shaft();
                        }
                        else
                        {
                            ID -= 1;
                            var_es.features_list.RemoveAt(ID);
                            Angle_chamf angle_chamfer = new Angle_chamf(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Side);
                            var_es.features_list.Insert(ID, angle_chamfer);
                            if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                                addInForm.Del();
                            addInForm.Shaft();
                        }
                        Close();
                    }
                    catch
                    { }
                    break;
                case ("Distance and Distance"):
                    try
                    {
                        ID += 1;
                        ID *= 2;
                        if (Side == 'l')
                        {
                            ID -= 2;
                            var_es.features_list.RemoveAt(ID);
                            Distance_chamf two_distance_chamfer = new Distance_chamf(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox3.Text), Side);
                            var_es.features_list.Insert(ID, two_distance_chamfer);
                            if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                                addInForm.Del();
                            addInForm.Shaft();
                        }
                        else
                        {
                            ID -= 1;
                            var_es.features_list.RemoveAt(ID);
                            Distance_chamf two_distance_chamfer = new Distance_chamf(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox3.Text), Side);
                            var_es.features_list.Insert(ID, two_distance_chamfer);
                            if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                                addInForm.Del();
                            addInForm.Shaft();
                        }
                        Close();
                    }
                    catch
                    { }
                    break;
            }
        }
    }
}

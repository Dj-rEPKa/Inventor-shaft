using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Inventor;

namespace InvAddIn
{
    public partial class Conus : Form
    {
        public Conus(ref ListView listView1)
        {
            InitializeComponent();

            lv = listView1;
            change = false;
        }

        public Conus(ref ListView listView1, int index)
        {
            InitializeComponent();

            lv = listView1;
            ID = index;
            change = true;
        }

        private List<DATA> data;
        private ListView lv;
        private int ID;
        private bool change;

        private void Conus_Load(object sender, EventArgs e)
        {
            Name = "Conus";
            assemble();
        }
        
        private void assemble()
        {
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Navy;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.Name = "Dimensions";
            dataGridView1.MultiSelect = false;

            data = new List<DATA>();

            data.AddRange(new DATA[] {
                new DATA { Name = "D1", Size = 4, Description = "First Diameter" },
                new DATA { Name = "D2", Size = 2, Description = "Second Diameter" },
                new DATA { Name = "L", Size = 4, Description = "Section length" } }
            );

            BindingSource srs = new BindingSource { DataSource = data };
            dataGridView1.DataSource = srs;
            dataGridView1.Columns[0].Width = 52;
            dataGridView1.Columns[1].Width = 52;
            dataGridView1.Columns[2].Width = 93;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            pictureBox1.Image = var_es._imgs[4];
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (data[0].Size != data[1].Size)
            {
                Create();
            }
            else
                MessageBox.Show("You are trying to make cylinder, change one of two diameters");
        }

        private void Create()
        {
            if (!change)
            {
                Con_ conus = new Con_(Convert.ToDouble(data[2].Size), Convert.ToDouble(data[0].Size), Convert.ToDouble(data[1].Size));
                var_es.list.Add(conus);
                var_es.features_list.Add(new Create() as chamf);
                var_es.features_list.Add(new Create() as chamf);
                if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                    addInForm.Del();
                addInForm.Shaft();
                lv.Items.Add("no feature");
                lv.Items[lv.Items.Count - 1].SubItems.Add("Conus");
                lv.Items[lv.Items.Count - 1].SubItems.Add("no feature");
                Close();
            }
            else
            {
                lv.Items.RemoveAt(ID);
                var_es.list.RemoveAt(ID);
                var id = ID;
                id += 1;
                id *= 2;
                id -= 2;
                var_es.features_list.RemoveAt(ID);
                id -= 1;
                var_es.features_list.RemoveAt(ID);
                Con_ conus = new Con_(Convert.ToDouble(data[2].Size), Convert.ToDouble(data[0].Size), Convert.ToDouble(data[1].Size));
                var_es.list.Insert(ID, conus);
                var_es.features_list.Insert(ID, new Create() as chamf);
                var_es.features_list.Insert(ID, new Create() as chamf);
                if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                    addInForm.Del();
                addInForm.Shaft();
                lv.Items.Insert(ID, "no feature");
                lv.Items[ID].SubItems.Add("Conus");
                lv.Items[ID].SubItems.Add("no feature");
                Close();
            }
        }
    }
}

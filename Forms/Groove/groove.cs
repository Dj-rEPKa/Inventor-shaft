
using ButtonTree;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace InvAddIn
{
    public partial class groove : Form
    {
        public groove(int index)
        {
            InitializeComponent();
            ID = index;
            Side = 'l';
            change = false;
            canceled = false;
        }

        public groove(int index, int position, char side)
        {
            InitializeComponent();
            ID = index;
            change = true;
            canceled = false;
            Position = position;
            Side = side;
        }

        private int ID;
        private char Side;
        internal ButtonNode NODE;
        private int Position;
        private bool change;
        private string ratio;
        internal static bool canceled = false;
        internal static int position;

        private void Groove_Load(object sender, System.EventArgs e)
        {
            Name = "Groove";
            assemble();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            canceled = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ratio = "Groove " + data[3].Size + "x" + data[4].Size;
            if (!change)
            {
                determination();
                addInForm.NODES[ID].AddChild(NODE);
                addInForm.nodes.Add(NODE);
                position = addInForm.nodes.Count - 1;
            }
            else addInForm.nodes[Position].TextLabel.Text = ratio;
            Create();
        }

        private System.Collections.Generic.List<DATA> data;

        private void assemble()
        {
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Navy;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.Name = "Dimensions";
            dataGridView1.MultiSelect = false;

            data = new System.Collections.Generic.List<DATA>();

            var diam = var_es._list[ID].Radius * 2;
            if (!change)
            {
                data.AddRange(new DATA[] {
            new DATA { Name = "D", Size = diam, Description = "Figure diameter" },
            new DATA { Name = "L", Size = var_es._list[ID].Length, Description = "Section length" },
            new DATA { Name = "x", Size = 1, Description = "Distance" },
            new DATA { Name = "R", Size = 0.5, Description = "Radius" },
            new DATA { Name = "H", Size = diam-diam*0.9, Description = "Depth" },});
            }
            else
            {
                var feature = var_es.feature_list[addInForm.nodes[Position].FeaturePosition] as Groove;
                data.AddRange(new DATA[] {
            new DATA { Name = "D", Size = diam, Description = "Figure diameter" },
            new DATA { Name = "L", Size = var_es._list[ID].Length, Description = "Section length" },
            new DATA { Name = "x", Size = feature.Distance, Description = "Distance" },
            new DATA { Name = "R", Size = feature.Radius, Description = "Radius" },
            new DATA { Name = "H", Size = feature.Depth, Description = "Depth" },});
            }


            BindingSource srs = new BindingSource { DataSource = data };
            dataGridView1.DataSource = srs;
            dataGridView1.Columns[0].Width = 52;
            dataGridView1.Columns[1].Width = 52;
            dataGridView1.Columns[2].Width = 93;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Rows[0].ReadOnly = true;
            dataGridView1.Rows[1].ReadOnly = true;
            pictureBox1.Image = var_es._imgs[16];
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            comboBox1.Items.AddRange(var_es.side_text);
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void Create()
        {
            if (!change)
            {
                Groove groove = new Groove();
                var_es.feature_list.Add(groove);
                var_es.feature_list[var_es.feature_list.Count - 1] = new Groove(Convert.ToDouble(data[2].Size), Convert.ToDouble(data[3].Size), Convert.ToDouble(data[4].Size), Side, ID, position, NODE.NodePosition);
                NODE.FeaturePosition = var_es.feature_list.Count - 1;
                addInForm.Revolve();
                Close();
            }
            else
            {

                Groove groove = new Groove(Convert.ToDouble(data[2].Size), Convert.ToDouble(data[3].Size), Convert.ToDouble(data[4].Size), Side, ID, Position);
                var_es.feature_list[addInForm.nodes[Position].FeaturePosition] = groove;
                addInForm.Revolve();
                Close();
            }


        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            if (!change)
            {
                if (comboBox1.SelectedIndex == 0)
                    Side = 'l';
                else
                    Side = 'r';
            }
            else
            {
                Side = var_es.feature_list[Position].Side;
            }
        }

        private void determination()
        {
            NODE = new ButtonNode();

            NODE.TextLabel.Text = ratio;
            NODE.FeatureImg.Image = var_es._imgs[15];

            NODE.CustomizeButton.Click += new EventHandler(Node_CustomizeButton_Click);
            NODE.DeleteNodeButton.Click += new EventHandler(Node_DeleteNodeButton_Click);
        }

        private void Node_DeleteNodeButton_Click(object sender, EventArgs e)
        {
            try
            {
                addInForm.Delete_child(ID, NODE.NodePosition, position);
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void Node_CustomizeButton_Click(object sender, EventArgs e)
        {
            groove groove_form = new groove(ID, position, Side);
            groove_form.Owner = this;
            groove_form.ShowDialog();
        }
    }
}

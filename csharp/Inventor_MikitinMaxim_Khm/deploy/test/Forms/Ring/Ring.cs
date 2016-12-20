using ButtonTree;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace InvAddIn
{
    public partial class Ring : Form
    {
        public Ring(int index)
        {
            InitializeComponent();
            Side = 'l';
            ID = index;
            change = false;
            canceled = false;
        }

        public Ring(int index, int position, char side)
        {
            InitializeComponent();
            ID = index;
            change = true;
            Side = side;
            canceled = false;
            Position = position;
        }

        private int ID;
        private char Side;
        internal ButtonNode NODE;
        private int Position;
        private bool change;
        private string ratio;
        internal static bool canceled = false;
        internal static int node_position; //addInForm.node poistion
        private System.Collections.Generic.List<DATA> data;


        private void Ring_Load(object sender, System.EventArgs e)
        {
            Name = "Retaining ring";
            assemble();
        }


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
            new DATA { Name = "M", Size = 0.5, Description = "Width" },
            new DATA { Name = "D1", Size = diam-diam*0.1, Description = "Diameter" },});
            }
            else
            {
                var feature = var_es.feature_list[addInForm.nodes[Position].FeaturePosition] as ring;
                data.AddRange(new DATA[] {
            new DATA { Name = "D", Size = diam, Description = "Figure diameter" },
            new DATA { Name = "L", Size = var_es._list[ID].Length, Description = "Section length" },
            new DATA { Name = "x", Size = feature.Distance, Description = "Distance" },
            new DATA { Name = "M", Size = feature.Width, Description = "Width" },
            new DATA { Name = "D1", Size = feature.Radius * 2, Description = "Diameter" },});
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
            pictureBox1.Image = var_es._imgs[17];
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            comboBox1.Items.AddRange(var_es.side_text);
            comboBox1.SelectedItem = comboBox1.Items[0];
        }


        private void button2_Click(object sender, System.EventArgs e)
        {
            ratio = "Retailing ring " + data[3].Size + "x" + data[4].Size;
            if (!change)
            {
                determination();
                addInForm.NODES[ID].AddChild(NODE);
                addInForm.nodes.Add(NODE);
                node_position = addInForm.nodes.Count - 1;
            }
            else addInForm.nodes[Position].TextLabel.Text = ratio;

            Create();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            canceled = true;
            Close();
        }

        
        private void Create()
        {
            if (!change)
            {
                ring _ring = new ring();
                var_es.feature_list.Add(_ring);
                var_es.feature_list[var_es.feature_list.Count - 1] = new ring(Convert.ToDouble(data[2].Size), Convert.ToDouble(data[4].Size), Convert.ToDouble(data[3].Size), Side, ID, node_position, NODE.NodePosition); //Node.NodePostiton - created node position in parrent list 
                NODE.FeaturePosition = var_es.feature_list.Count - 1;
                addInForm.Revolve();
                Close();
            }
            else
            {
                ring _ring = new ring(Convert.ToDouble(data[2].Size), Convert.ToDouble(data[4].Size), Convert.ToDouble(data[3].Size), Side, ID, Position);
                var_es.feature_list[addInForm.nodes[Position].FeaturePosition] = _ring;
                addInForm.Revolve();
                Close();
            }
        }

        private void determination()
        {
            NODE = new ButtonNode();

            NODE.TextLabel.Text = ratio;
            NODE.FeatureImg.Image = var_es._imgs[14];

            NODE.CustomizeButton.Click += new EventHandler(Node_CustomizeButton_Click);
            NODE.DeleteNodeButton.Click += new EventHandler(Node_DeleteNodeButton_Click);
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

        private void Node_DeleteNodeButton_Click(object sender, EventArgs e)
        {
            addInForm.Delete_child(ID, NODE.NodePosition, node_position);
        }

        private void Node_CustomizeButton_Click(object sender, EventArgs e)
        {
            Ring _ring = new Ring(ID, node_position, Side);
            _ring.Owner = this;
            _ring.ShowDialog();
        }
    }
}
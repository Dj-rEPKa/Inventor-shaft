using System;
using System.Windows.Forms;
using Inventor;
using ButtonTree;

namespace InvAddIn
{
    public partial class Filler : Form
    {
        
        public Filler( char side,  int index)
        {
            InitializeComponent();
            Side = side;
            ID = index;
            change = false;
            canceled = false;
        }

        public Filler(char side,  int index, int position)
        {
            InitializeComponent();
            Side = side;
            ID = index;
            change = true;
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
        internal static int position;

        private void Filler_Load(object sender, EventArgs e)
        {
            Name = "Fillet";
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            if(change)
            {
                var chamfer = var_es.chamfer_list[Position];
                textBox1.Text = "" + chamfer.Radius;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            canceled = true;
            Close();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            ratio = "fillet " + Convert.ToDouble(textBox1.Text);
            if (Side == 'l')
            {
                ratio = "Left " + ratio;
            }
            else
            {
                ratio = "Right " + ratio;
            }
            if (!change)
            {
                determination();
                addInForm.NODES[ID].AddChild(NODE);
                addInForm.nodes.Add(NODE);
                position = addInForm.nodes.Count - 1;
            }
            else addInForm.nodes[var_es.chamfer_list[Position].Node_Position].TextLabel.Text = ratio;

            Create();
        }

        private void Create()
        {
            try
            {
                Position = (ID + 1) * 2;
                if (!String.IsNullOrEmpty(textBox1.Text.ToString()))
                {

                    if (Side == 'l')
                    {
                        Position -= 2;
                        fill filler;
                        try { filler = new fill(Convert.ToDouble(textBox1.Text), Side, Position, NODE.NodePosition); }
                        catch { filler = new fill(Convert.ToDouble(textBox1.Text), Side, Position); }
                        var_es.chamfer_list[Position] = filler;
                        if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                            addInForm.Del();
                        addInForm.Revolve();
                    }
                    else
                    {
                        Position -= 1;
                        fill filler;
                        try { filler = new fill(Convert.ToDouble(textBox1.Text), Side, Position, NODE.NodePosition); }
                        catch { filler = new fill(Convert.ToDouble(textBox1.Text), Side, Position); }
                        var_es.chamfer_list[Position] = filler;
                        if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                            addInForm.Del();
                        addInForm.Revolve();
                    }
                    Close();
                }
            }
            catch
            { }
        }


        private void determination()
        {
            NODE = new ButtonNode();

            NODE.TextLabel.Text = ratio;
            NODE.FeatureImg.Image = var_es._imgs[8];

            NODE.CustomizeButton.Click += new EventHandler(Node_CustomizeButton_Click);
            NODE.DeleteNodeButton.Click += new EventHandler(Node_DeleteNodeButton_Click);
        }

        private void Node_DeleteNodeButton_Click(object sender, EventArgs e)
        {
            try
            {
                addInForm.Delete_chamfer(ID, var_es.chamfer_list[Position].Node_Position, var_es.chamfer_list[Position].Position);
                var_es.chamfer_list[Position] = new chamf();
                addInForm.Revolve();
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        private void Node_CustomizeButton_Click(object sender, EventArgs e)
        {
            Filler filler_from = new Filler(Side, ID, Position);
            filler_from.Owner = this;
            filler_from.ShowDialog();
        }
    }
}

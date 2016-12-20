using System;
using System.Windows.Forms;
using Inventor;
using ButtonTree;

namespace InvAddIn
{
    public partial class chamfer : Form
    {
        public chamfer(char side, int index)
        {
            InitializeComponent();
            Side = side;
            ID = index;
            change = false;
            canceled = false;
            type = "Distance";
        }

        public chamfer(char side, int index, string Type, int pos)
        {
            InitializeComponent();
            Side = side;
            Position = pos;
            ID = index;
            change = true;
            canceled = false;
            type = Type;
        }

        internal Action deleg;
        private int ID;
        private char Side;
        internal ButtonNode NODE;
        private int Position;
        private bool change;
        private string ratio;
        internal static bool canceled = false;
        internal static int position;

        private string type;

        private void chamfer_Load(object sender, EventArgs e)
        {
            assemble();
        }

        //Cancel Button
        private void button1_Click(object sender, EventArgs e)
        {
            canceled = true;
            Close();
        }

        private void assemble()
        {

            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Chamfer";
            button3.BackgroundImage = var_es._imgs[3];
            button4.BackgroundImage = var_es._imgs[1];
            button5.BackgroundImage = var_es._imgs[2];

            if (!change)
            {
                label2.Visible = false;
                textBox2.Visible = false;
                label3.Visible = false;
                textBox3.Visible = false;
                button3.Select();
            }
            else
            {
                switch (type)
                {
                    case ("Distance"):
                        {
                            var chamfer = var_es.chamfer_list[Position];
                            label2.Visible = false;
                            textBox2.Visible = false;
                            label3.Visible = false;
                            textBox3.Visible = false;
                            textBox1.Text = "" + chamfer.Distance;
                            button3.Select();
                        }
                        break;
                    case ("Distance and Angle"):
                        {
                            var chamfer = var_es.chamfer_list[Position] as Angle_chamf;
                            label2.Visible = true;
                            textBox2.Visible = true;
                            label3.Visible = false;
                            textBox3.Visible = false;
                            textBox1.Text = "" + chamfer.Distance;
                            textBox2.Text = "" + chamfer.Angle;
                            button4.Select();
                        }
                        break;
                    case ("Distance and Distance"):
                        {
                            var chamfer = var_es.chamfer_list[Position] as Distance_chamf;
                            label2.Visible = false;
                            textBox2.Visible = false;
                            label3.Visible = true;
                            textBox3.Visible = true;
                            label1.Text += " 1";
                            textBox1.Text = "" + chamfer.Distance;
                            textBox3.Text = "" + chamfer.Second_Distance;
                            button5.Select();
                        }
                        break;
                }
            }
        }

        //Distance chamf
        private void button3_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            textBox2.Visible = false;
            label3.Visible = false;
            textBox3.Visible = false;
            label1.Text = "Distance";

            type = "Distance";
        }

        //Distance & angle chamf
        private void button4_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            textBox2.Visible = true;
            label3.Visible = false;
            textBox3.Visible = false;
            label1.Text = "Distance";

            type = "Distance and Angle";
        }

        //Two Distance chamf
        private void button5_Click(object sender, EventArgs e)
        {
            label2.Visible = false;
            textBox2.Visible = false;
            label3.Visible = true;
            textBox3.Visible = true;
            label1.Text += " 1";

            type = "Distance and Distance";
        }
        
        //Ok Button
        private void button2_Click(object sender, EventArgs e)
        {
            if (Side == 'l')
            {
                ratio = "Left ";
            }
            else
            {
                ratio = "Right ";
            }
            switch (type)
            {
                case ("Distance"):
                    ratio += " distance chamfer " + textBox1.Text;
                    break;
                case ("Distance and Angle"):
                    ratio += " distance and angle chamfer " + textBox1.Text + "x" + textBox2.Text + "deg";
                    break;
                case ("Distance and Distance"):
                    ratio += " two distance chamfer " + textBox1.Text + "x" + textBox3.Text;
                    break;
            }

            if (!change)
            {
                determination();
                addInForm.NODES[ID].AddChild(NODE);
                addInForm.nodes.Add(NODE);
                position = addInForm.nodes.Count - 1;
            }
            else
            {
                addInForm.nodes[var_es.chamfer_list[Position].Node_Position].TextLabel.Text = ratio;
                addInForm.nodes[var_es.chamfer_list[Position].Node_Position].CustomizeButton.Click += new EventHandler(Delegate_Method);
                deleg = Node_CustomizeButton_Click;
                
            }

            Create();
        }
       
        private void Create()
        {
            Position = (ID + 1) * 2;
            switch (type)
            {
                case ("Distance"):
                    try
                    {
                        if (Side == 'l')
                        {
                            Position -= 2;
                            chamf distance_chamfer;
                            try { distance_chamfer = new chamf(Convert.ToDouble(textBox1.Text), Side, Position, NODE.NodePosition); }
                            catch { distance_chamfer = new chamf(Convert.ToDouble(textBox1.Text), Side, Position); }
                            var_es.chamfer_list[Position] = distance_chamfer;
                            if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                                addInForm.Del();
                            addInForm.Revolve();
                        }
                        else
                        {
                            Position -= 1;
                            chamf distance_chamfer;
                            try { distance_chamfer = new chamf(Convert.ToDouble(textBox1.Text), Side, Position, NODE.NodePosition); }
                            catch { distance_chamfer = new chamf(Convert.ToDouble(textBox1.Text), Side, Position); }
                            var_es.chamfer_list[Position] = distance_chamfer;
                            if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                                addInForm.Del();
                            addInForm.Revolve();
                        }
                        Close();
                    }
                    catch (Exception e1)
                    { MessageBox.Show(e1.ToString()); }
                    break;
                case ("Distance and Angle"):
                    try
                    {
                        if (Side == 'l')
                        {
                            Position -= 2;
                            Angle_chamf angle_chamfer;
                            try { angle_chamfer = new Angle_chamf(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Side, Position, NODE.NodePosition); }
                            catch { angle_chamfer = new Angle_chamf(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Side, Position); }
                            var_es.chamfer_list[Position] = angle_chamfer;
                            if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                                addInForm.Del();
                            addInForm.Revolve();
                        }
                        else
                        {
                            Position -= 1;
                            Angle_chamf angle_chamfer;
                            try { angle_chamfer = new Angle_chamf(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Side, Position, NODE.NodePosition); }
                            catch { angle_chamfer = new Angle_chamf(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Side, Position); }
                            var_es.chamfer_list[Position] = angle_chamfer;
                            if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                                addInForm.Del();
                            addInForm.Revolve();
                        }
                        Close();
                    }
                    catch
                    { }
                    break;
                case ("Distance and Distance"):
                    try
                    {
                        if (Side == 'l')
                        {
                            Position -= 2;
                            Distance_chamf two_distance_chamfer;
                            try { two_distance_chamfer = new Distance_chamf(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox3.Text), Side, Position, NODE.NodePosition); }
                            catch { two_distance_chamfer = new Distance_chamf(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox3.Text), Side, Position, NODE.NodePosition); }
                            var_es.chamfer_list[Position] = two_distance_chamfer;
                            if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                                addInForm.Del();
                            addInForm.Revolve();
                        }
                        else
                        {
                            Position -= 1;
                            Distance_chamf two_distance_chamfer;
                            try { two_distance_chamfer = new Distance_chamf(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox3.Text), Side, Position, NODE.NodePosition); }
                            catch { two_distance_chamfer = new Distance_chamf(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox3.Text), Side, Position, NODE.NodePosition); }
                            var_es.chamfer_list[Position] = two_distance_chamfer;
                            if (var_es.part_doc_def.Features.ExtrudeFeatures.Count != 0)
                                addInForm.Del();
                            addInForm.Revolve();
                        }
                        Close();
                    }
                    catch
                    { }
                    break;
            }
        }


        private void determination()
        {
            NODE = new ButtonNode();

            NODE.TextLabel.Text = ratio;
            NODE.FeatureImg.Image = var_es._imgs[0];

            NODE.DeleteNodeButton.Click += new EventHandler(Node_DeleteNodeButton_Click);

            NODE.CustomizeButton.Click += new EventHandler(Delegate_Method);
            deleg = Node_CustomizeButton_Click;
        }

        private void Node_CustomizeButton_Click()
        {
            DeleteDeleg();
            chamfer chamfer_from = new chamfer(Side, ID, type, Position);
            chamfer_from.Owner = this;
            chamfer_from.ShowDialog();
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

            }
        }

        /*private void Node_CustomizeButton_Click(object sender, EventArgs e)
        {
            chamfer chamfer_from = new chamfer(Side, ID , type, Position);
            chamfer_from.Owner = this;
            chamfer_from.ShowDialog();
        }*/

        private void Delegate_Method(object sender, EventArgs e)
        {
            deleg?.Invoke();
        }

        public void DeleteDeleg()
        {
            deleg = null;
        }
    }
}

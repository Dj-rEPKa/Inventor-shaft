using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ButtonTree;

namespace InvAddIn
{
    public partial class Polyon : Form
    {
        public Polyon()
        {
            InitializeComponent();

            change = false;
            canceled = false;
        }

        public Polyon(int index)
        {
            InitializeComponent();

            ID = index;
            change = true;
            canceled = false;
        }

        private List<DATA> data;
        private int ID;
        private bool change;
        internal static bool canceled;
        private string ratio;
        internal ButtonNode NODE;

        private void button1_Click(object sender, EventArgs e)
        {
            canceled = true;
            Close();
        }

        private void Polyon_Load(object sender, EventArgs e)
        {
            Name = "Polygon";
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
            if (!change)
            {
                data.AddRange(new DATA[] {
                //new DATA { Name = "D out", Size = 3, Description = "Diameter" },
                //new DATA { Name = "D in", Size = 4, Description = "Diameter inscribed" },
                new DATA { Name = "L", Size = 4, Description = "Section length" },
                new DATA { Name = "N", Size = 6, Description = "Number of edges" },
                //new DATA { Name = "α", Size = 0, Description = "Section angle" },
                new DATA { Name = "D", Size = 4, Description = "Diameter" } });
            }
            else
            {
                data.AddRange(new DATA[] {
                //new DATA { Name = "D out", Size = 3, Description = "Diameter" },
                //new DATA { Name = "D in", Size = 4, Description = "Diameter inscribed" },
                new DATA { Name = "L", Size = var_es._list[ID].Length, Description = "Section length" },
                new DATA { Name = "N", Size = var_es._list[ID].Number_of_Edges, Description = "Number of edges" },
                //new DATA { Name = "α", Size = 0, Description = "Section angle" },
                new DATA { Name = "D", Size = var_es._list[ID].Radius * 2, Description = "Diameter" } });
            }

            BindingSource srs = new BindingSource { DataSource = data };
            dataGridView1.DataSource = srs;
            dataGridView1.Columns[0].Width = 52;
            dataGridView1.Columns[1].Width = 52;
            dataGridView1.Columns[2].Width = 93;
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            pictureBox1.Image = var_es._imgs[10];
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ratio = "Polyon  " + data[2].Size + "x" + data[0].Size + ":" + data[1].Size + "edges";
            if (!change)
            {
                determination(ratio);
                addInForm.NODES.Add(NODE);
            }
            else
            {
            }
            Create();
        }

        private void Create()
        {
            if (!change)
            {
                Pol polygon = new Pol(Convert.ToDouble(data[0].Size), Convert.ToDouble(data[2].Size), Convert.ToInt32(data[1].Size), true, var_es._list.Count == 0 ? true : false, var_es._list.Count);
                var_es._list.Add(polygon);

                var_es.chamfer_list.Add(new chamf());
                var_es.chamfer_list.Add(new chamf());

                addInForm.Revolve();

                Close();
            }
            else
            {/*
                var_es._list.RemoveAt(ID);
                var id = ID;
                id += 1;
                id *= 2;
                id -= 2;
                var_es.chamfer_list.RemoveAt(ID);
                id -= 1;
                var_es.chamfer_list.RemoveAt(ID);
                */
                var_es._list[ID] = new Pol(Convert.ToDouble(data[0].Size), Convert.ToDouble(data[2].Size), Convert.ToInt32(data[1].Size), true, var_es._list.Count == 0 ? true : false, var_es._list.Count);
                /*var_es._list.Insert(ID, polygon);

                var_es.chamfer_list.Insert(ID, new chamf());
                var_es.chamfer_list.Insert(ID, new chamf());*/

                addInForm.Revolve();

                Close();
            }
        }


        private void determination(string Ratio)
        {
            NODE = new ButtonNode();

            string[] text_left = { "no feature", "Fillet", "Chamfer" };
            string[] text_mid = { "Cylynder", "Conus", "Polygon" };
            string[] text_right = { "no feature", "Fillet", "Chamfer" };
            string[] text_last = { "no feature", "Ring", "Groove", "Undefined" };

            NODE.combo_left_chamf.DropDownStyle = ComboBoxStyle.DropDownList;
            NODE.combo_left_chamf.DrawMode = DrawMode.OwnerDrawFixed;
            NODE.combo_left_chamf.DataSource = text_left;
            NODE.combo_left_chamf.DrawItem += new DrawItemEventHandler(DrawItem_left);
            NODE.combo_left_chamf.DropDownClosed += new EventHandler(combo_left_DropDownClosed);

            NODE.comb_mid_figure.DropDownStyle = ComboBoxStyle.DropDownList;
            NODE.comb_mid_figure.DrawMode = DrawMode.OwnerDrawFixed;
            NODE.comb_mid_figure.DataSource = text_mid;
            NODE.comb_mid_figure.DrawItem += new DrawItemEventHandler(DrawItem_mid);
            NODE.comb_mid_figure.DropDownClosed += new EventHandler(combo_mid_DropDownClosed);


            NODE.comb_right_chamf.DropDownStyle = ComboBoxStyle.DropDownList;
            NODE.comb_right_chamf.DrawMode = DrawMode.OwnerDrawFixed;
            NODE.comb_right_chamf.DataSource = text_right;
            NODE.comb_right_chamf.DrawItem += new DrawItemEventHandler(DrawItem_right);
            NODE.comb_right_chamf.DropDownClosed += new EventHandler(combo_right_DropDownClosed);

            NODE.comb_last_feauter.DropDownStyle = ComboBoxStyle.DropDownList;
            NODE.comb_last_feauter.DrawMode = DrawMode.OwnerDrawFixed;
            NODE.comb_last_feauter.DataSource = text_last;
            NODE.comb_last_feauter.DrawItem += new DrawItemEventHandler(DrawItem_last);
            NODE.comb_last_feauter.DropDownClosed += new EventHandler(combo_last_DropDownClosed);

            NODE.FigureName.Text = ratio;

            NODE.CustomizeButton.Click += new EventHandler(Node_CustomizeButton_Click);
            NODE.DeleteNodeButton.Click += new EventHandler(Node_DeleteNodeButton_Click);


        }


        #region Draw Items
        private void DrawItem_left(object sender, DrawItemEventArgs e)
        {
            try
            {
                var i = 0;
                //для выбора нужного изображения из массива
                switch (e.Index)
                {
                    case 0:
                        i = 9;
                        break;
                    case 1:
                        i = 8;
                        break;
                    case 2:
                        i = 0;
                        break;
                }

                e.DrawBackground();
                //отрисовка изображения в combobox
                e.Graphics.DrawImage(var_es._imgs[i], new RectangleF(e.Bounds.X + 2, e.Bounds.Y + 2, 24, 24));

                //подсказка

                var text = NODE.combo_left_chamf.GetItemText(NODE.combo_left_chamf.Items[e.Index]);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    addInForm.toolTip1.Show(text, NODE.combo_left_chamf, e.Bounds.Right, e.Bounds.Bottom);
                }
                else
                {
                    addInForm.toolTip1.Hide(NODE.combo_left_chamf);
                }
                e.DrawFocusRectangle();

            }
            catch (Exception e2)
            { MessageBox.Show(e2.ToString()); }
        }

        private void DrawItem_mid(object sender, DrawItemEventArgs e)
        {
            try
            {
                var i = 0;
                //для выбора нужного изображения из массива
                switch (e.Index)
                {
                    case 0:
                        i = 7;
                        break;
                    case 1:
                        i = 5;
                        break;
                    case 2:
                        i = 11;
                        break;
                }

                e.DrawBackground();
                //отрисовка изображения в combobox
                e.Graphics.DrawImage(var_es._imgs[i], new RectangleF(e.Bounds.X + 2, e.Bounds.Y + 2, 24, 24));

                //подсказка

                var text = NODE.comb_mid_figure.GetItemText(NODE.comb_mid_figure.Items[e.Index]);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    addInForm.toolTip1.Show(text, NODE.comb_mid_figure, e.Bounds.Right, e.Bounds.Bottom);
                }
                else
                {
                    addInForm.toolTip1.Hide(NODE.comb_mid_figure);
                }
                e.DrawFocusRectangle();

            }
            catch (Exception e2)
            { MessageBox.Show(e2.ToString()); }
        }

        private void DrawItem_right(object sender, DrawItemEventArgs e)
        {
            try
            {
                var i = 0;
                //для выбора нужного изображения из массива
                switch (e.Index)
                {
                    case 0:
                        i = 9;
                        break;
                    case 1:
                        i = 8;
                        break;
                    case 2:
                        i = 0;
                        break;
                }

                e.DrawBackground();
                //отрисовка изображения в combobox
                e.Graphics.DrawImage(var_es._imgs[i], new RectangleF(e.Bounds.X + 2, e.Bounds.Y + 2, 24, 24));

                //подсказка

                var text = NODE.comb_right_chamf.GetItemText(NODE.comb_right_chamf.Items[e.Index]);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    addInForm.toolTip1.Show(text, NODE.comb_right_chamf, e.Bounds.Right, e.Bounds.Bottom);
                }
                else
                {
                    addInForm.toolTip1.Hide(NODE.comb_right_chamf);
                }
                e.DrawFocusRectangle();

            }
            catch (Exception e2)
            { MessageBox.Show(e2.ToString()); }
        }

        private void DrawItem_last(object sender, DrawItemEventArgs e)
        {
            try
            {
                var i = 0;
                //для выбора нужного изображения из массива
                switch (e.Index)
                {
                    case 0:
                        i = 9;
                        break;
                    case 1:
                        i = 14;
                        break;
                    case 2:
                        i = 13;
                        break;
                    case 3:
                        i = 15;
                        break;
                }

                e.DrawBackground();
                //отрисовка изображения в combobox
                e.Graphics.DrawImage(var_es._imgs[i], new RectangleF(e.Bounds.X + 2, e.Bounds.Y + 2, 24, 24));

                //подсказка

                var text
                    = NODE.comb_last_feauter.GetItemText(NODE.comb_last_feauter.Items[e.Index]);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    addInForm.toolTip1.Show(text, NODE.comb_last_feauter, e.Bounds.Right, e.Bounds.Bottom);
                }
                else
                {
                    addInForm.toolTip1.Hide(NODE.comb_last_feauter);
                }
                e.DrawFocusRectangle();

            }
            catch (Exception e2)
            { MessageBox.Show(e2.ToString()); }
        }
        #endregion


        private void combo_left_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (NODE.combo_left_chamf.SelectedItem.ToString() == "no feature")
                {
                    
                }
                if (NODE.combo_left_chamf.SelectedItem.ToString() == "Fillet")
                {
                    try
                    {
                        int id = (NODE.NodePosition + 1) * 2;
                        id -= 2;
                        if (!var_es.chamfer_list[id].IsNullorEmpty())
                        {
                            addInForm.NODES[ID].RemoveAt(var_es.chamfer_list[id].Node_Position);
                            addInForm.nodes.RemoveAt(var_es.chamfer_list[id].Node_Position);
                            addInForm.buttonTree1.ReLoad();
                            var_es.chamfer_list[id] = new chamf();
                        }
                    }
                    catch { }
                    Filler filler_form = new Filler('l', NODE.NodePosition);
                    filler_form.Owner = this;
                    filler_form.ShowDialog();
                    if (!Filler.canceled)
                    {
                        addInForm.buttonTree1.ReLoad();
                        addInForm.NODES[addInForm.NODES.Count - 1].combo_left_chamf.SelectedItem = "Fillet";
                    }
                }
                if (NODE.combo_left_chamf.SelectedItem.ToString() == "Chamfer")
                {
                    try
                    {
                        int id = (NODE.NodePosition + 1) * 2;
                        id -= 2;
                        if (!var_es.chamfer_list[id].IsNullorEmpty())
                        {
                            addInForm.NODES[ID].RemoveAt(var_es.chamfer_list[id].Node_Position);
                            addInForm.nodes.RemoveAt(var_es.chamfer_list[id].Node_Position);
                            addInForm.buttonTree1.ReLoad();
                            var_es.chamfer_list[id] = new chamf();
                        }
                    }
                    catch { }
                    chamfer chamf_form = new chamfer('l', NODE.NodePosition);
                    chamf_form.Owner = this;
                    chamf_form.ShowDialog();
                    if (!chamfer.canceled)
                    {
                        addInForm.buttonTree1.ReLoad();
                        addInForm.NODES[addInForm.NODES.Count - 1].combo_left_chamf.SelectedItem = "Chamfer";
                    }
                }
            }
            catch (Exception) { MessageBox.Show("You have to change somethig"); }
        }

        private void combo_mid_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (NODE.comb_mid_figure.SelectedItem.ToString() == "Conus")
                {
                    Conus con_form = new Conus(NODE.NodePosition);
                    con_form.Owner = this;
                    con_form.ShowDialog();
                }
                if (NODE.comb_mid_figure.SelectedItem.ToString() == "Polygon")
                {
                    Polyon pol_form = new Polyon(NODE.NodePosition);
                    pol_form.Owner = this;
                    pol_form.ShowDialog();
                }
                if (NODE.comb_mid_figure.SelectedItem.ToString() == "Cylynder")
                {
                    Cylinder cyl_form = new Cylinder(NODE.NodePosition);
                    cyl_form.Owner = this;
                    cyl_form.ShowDialog();

                }

            }
            catch (Exception) { MessageBox.Show("You have to change somethig"); }
        }

        private void combo_right_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (NODE.comb_right_chamf.SelectedItem.ToString() == "no feature")
                {
                    
                }
                if (NODE.comb_right_chamf.SelectedItem.ToString() == "Fillet")
                {
                    try
                    {
                        int id = (NODE.NodePosition + 1) * 2;
                        id -= 1;
                        if (!var_es.chamfer_list[id].IsNullorEmpty())
                        {
                            addInForm.NODES[ID].RemoveAt(var_es.chamfer_list[id].Node_Position);
                            addInForm.nodes.RemoveAt(var_es.chamfer_list[id].Node_Position);
                            addInForm.buttonTree1.ReLoad();
                            var_es.chamfer_list[id] = new chamf();
                        }
                    }
                    catch { }
                    Filler filler_from = new Filler('r', NODE.NodePosition);
                    filler_from.Owner = this;
                    filler_from.ShowDialog();
                    if (!Filler.canceled)
                    {
                        addInForm.buttonTree1.ReLoad();
                        addInForm.NODES[addInForm.NODES.Count - 1].comb_right_chamf.SelectedItem = "Fillet";
                    }
                }
                if (NODE.comb_right_chamf.SelectedItem.ToString() == "Chamfer")
                {
                    try
                    {
                        int id = (NODE.NodePosition + 1) * 2;
                        id -= 1;
                        if (!var_es.chamfer_list[id].IsNullorEmpty())
                        {
                            addInForm.NODES[ID].RemoveAt(var_es.chamfer_list[id].Node_Position);
                            addInForm.nodes.RemoveAt(var_es.chamfer_list[id].Node_Position);
                            addInForm.buttonTree1.ReLoad();
                            var_es.chamfer_list[id] = new chamf();
                        }
                    }
                    catch { }
                    chamfer chamf_foorm = new chamfer('r', NODE.NodePosition);
                    chamf_foorm.Owner = this;
                    chamf_foorm.ShowDialog();
                    if (!chamfer.canceled)
                    {
                        addInForm.buttonTree1.ReLoad();
                        addInForm.NODES[addInForm.NODES.Count - 1].comb_right_chamf.SelectedItem = "Chamfer";
                    }
                }
            }
            catch (Exception) { MessageBox.Show("You have to change somethig"); }
        }

        private void combo_last_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (NODE.comb_last_feauter.SelectedItem.ToString() == "no feature")
                {

                }
                else if (NODE.comb_last_feauter.SelectedItem.ToString() == "Ring")
                {
                    /*
                    Ring ring_form = new Ring(0);
                    ring_form.Owner = this;
                    ring_form.ShowDialog();*/
                }
                else if (NODE.comb_last_feauter.SelectedItem.ToString() == "Groove")
                {
                   /*
                    groove groove_form = new groove(0);
                    groove_form.Owner = this;
                    groove_form.ShowDialog();*/
                }
                else if (NODE.comb_last_feauter.SelectedItem.ToString() == "Undefined")
                {
                    MessageBox.Show("Undefined");
                }
            }
            catch (Exception) { MessageBox.Show("You have to change somethig"); }
        }



        private void Node_DeleteNodeButton_Click(object sender, EventArgs e)
        {
            var_es._list.RemoveAt(NODE.NodePosition);
            addInForm.NODES.RemoveAt(NODE.NodePosition);
            addInForm.Delete(NODE.NodePosition);
        }

        private void Node_CustomizeButton_Click(object sender, EventArgs e)
        {
            Polyon pol_form = new Polyon(NODE.NodePosition);
            pol_form.Owner = this;
            pol_form.ShowDialog();
        }
    }
}

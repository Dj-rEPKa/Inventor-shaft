using System;
using System.Drawing;
using System.Windows.Forms;
using Inventor;


namespace InvAddIn
{
    public partial class addInForm : Form
    {
        private int section_index = 0;

        public addInForm()
        {
            InitializeComponent();
            //поиск изображений
            var_es.img_find();
            
            //формируем почву для вала, создаём новый assembly document и part document
            assemble();

            //формируем наш список елементов
            assemble_list_view();

        }

        private Section section = null;
        
        AssemblyDocument assemb_doc;

        public string path;

        private void addInForm_Load(object sender, EventArgs e)
        {
            #region Comboboxs definiton

            right_edge_camf.DropDownStyle = ComboBoxStyle.DropDownList;
            right_edge_camf.DrawMode = DrawMode.OwnerDrawFixed;
            right_edge_camf.ItemHeight = 26;
            right_edge_camf.DataSource = var_es.chamfer_right_text;
            right_edge_camf.DrawItem += new DrawItemEventHandler(right_edge_camf_DrawItem);

            left_edge_chamf.DropDownStyle = ComboBoxStyle.DropDownList;
            left_edge_chamf.DrawMode = DrawMode.OwnerDrawFixed;
            left_edge_chamf.ItemHeight = 26;
            left_edge_chamf.DataSource = var_es.chamfer_left_text;
            left_edge_chamf.DrawItem += new DrawItemEventHandler(left_edge_chamf_DrawItem);

            section_type.DropDownStyle = ComboBoxStyle.DropDownList;
            section_type.DrawMode = DrawMode.OwnerDrawFixed;
            section_type.ItemHeight = 26;
            section_type.DataSource = var_es.section_text;
            section_type.DrawItem += new DrawItemEventHandler(section_type_DrawItem);

            #endregion
            button6.BackgroundImage = var_es._imgs[12];
            var_es.part_doc_def = var_es.part_doc.ComponentDefinition;
        }

        #region ComboBox.DrawItem

        //рисование произвольных елементов комбобокс 
        private void right_edge_camf_DrawItem(object sender, DrawItemEventArgs e)
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

                var text = this.right_edge_camf.GetItemText(right_edge_camf.Items[e.Index]);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    toolTip1.Show(text, right_edge_camf, e.Bounds.Right, e.Bounds.Bottom);
                }
                else
                {
                    this.toolTip1.Hide(right_edge_camf);
                }
                e.DrawFocusRectangle();

            }
            catch (Exception e2)
            { MessageBox.Show(e2.ToString()); }
        }

        private void section_type_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                var i = 0;
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
                e.Graphics.DrawImage(var_es._imgs[i], new RectangleF(e.Bounds.X + 2, e.Bounds.Y + 2, 24, 24));

                var text = this.section_type.GetItemText(section_type.Items[e.Index]);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    toolTip1.Show(text, section_type, e.Bounds.Right, e.Bounds.Bottom);
                }
                else
                {
                    this.toolTip1.Hide(section_type);
                }
                e.DrawFocusRectangle();
            }
            catch (Exception e2)
            { MessageBox.Show(e2.ToString()); }
        }

        private void left_edge_chamf_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                var i = 0;
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
                e.Graphics.DrawImage(var_es._imgs[i], new RectangleF(e.Bounds.X + 2, e.Bounds.Y + 2, 24, 24));

                var text = this.left_edge_chamf.GetItemText(left_edge_chamf.Items[e.Index]);

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    toolTip1.Show(text, left_edge_chamf, e.Bounds.Right, e.Bounds.Bottom);
                }
                else
                {
                    this.toolTip1.Hide(left_edge_chamf);
                }
                e.DrawFocusRectangle();

            }
            catch (Exception e2)
            { MessageBox.Show(e2.ToString()); }
        }

        #endregion

        private void assemble()
        {
            try
            {
                path = var_es._Doc.FullFileName.Remove(var_es._Doc.FullFileName.Length - 4);

                //создаём новый AssemblyDocument
                assemb_doc = (AssemblyDocument)var_es.InventorApp.Documents.Add(DocumentTypeEnum.kAssemblyDocumentObject, var_es.InventorApp.FileManager.GetTemplateFile(DocumentTypeEnum.kAssemblyDocumentObject), false);
                AssemblyComponentDefinition assemb_doc_Comp = assemb_doc.ComponentDefinition;

                //сохраняем созданий AssemblyDocument
                assemb_doc.SaveAs(path + @"/Shaft.iam", true);

                //Создаём матрицу
                TransientGeometry TG = var_es.InventorApp.TransientGeometry;
                Matrix matrix = TG.CreateMatrix();

                //привязываем наш AssemblyDocument в браузере к AssemblyDocument, который открыл пользователь
                var_es.part_doc = (PartDocument)var_es.InventorApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject, var_es.InventorApp.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject), true);
                PartComponentDefinition doc_def = var_es.part_doc.ComponentDefinition;
                
            }
            catch (Exception e3)
            {
                MessageBox.Show(e3.ToString());
            }
        }
        

        protected static PartComponentDefinition partDef;
        protected static TransientGeometry tg;
        protected static PlanarSketch sketch;
        protected static EdgeCollection Edges;
        protected static Face S_Face = null;
        protected static Face E_Face = null;
        
        //function to draw shaft with all features
        public static void Shaft()
        {
            partDef = var_es.part_doc.ComponentDefinition;
            tg = var_es.InventorApp.TransientGeometry;
            sketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
            Edges = default(EdgeCollection);
            Edges = var_es.InventorApp.TransientObjects.CreateEdgeCollection();
            S_Face = null;
            E_Face = null;
            int i = 0, j = 1;

            foreach (var part in var_es.list)
            {

                S_Face = null;
                E_Face = null;
                var obj = part;
                if (obj != null)
                    obj.Create_BR(tg, ref sketch, Edges, ref S_Face, ref E_Face, ref partDef);
                try
                {
                    S_Face = obj.Start_face;
                    E_Face = obj.End_face;
                    Edges = var_es.InventorApp.TransientObjects.CreateEdgeCollection();
                    if (var_es.features_list[i] != null)
                    { 
                        Create feature = var_es.features_list[i];
                        feature.Create_BR(tg, ref sketch, Edges, ref S_Face, ref E_Face, ref partDef);
                    }
                    Edges = var_es.InventorApp.TransientObjects.CreateEdgeCollection();
                    S_Face = obj.Start_face;
                    E_Face = obj.End_face;
                    if (var_es.features_list[j] != null)
                    {
                        Create feature = var_es.features_list[j];
                        feature.Create_BR(tg, ref sketch, Edges, ref S_Face, ref E_Face, ref partDef);
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());

                }
                i += 2;
                j += 2;
            }
    }

        #region Обработчики событий ComboBox

        private void section_type_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (section_type.SelectedItem.ToString() == "Conus")
                {
                    Conus con_form = new Conus(ref listView1, section_index);
                    con_form.Owner = this;
                    con_form.ShowDialog();
                }
                if (section_type.SelectedItem.ToString() == "Polygon")
                {
                    Polyon pol_form = new Polyon(ref listView1, section_index);
                    pol_form.Owner = this;
                    pol_form.ShowDialog();
                }
                if (section_type.SelectedItem.ToString() == "Cylynder")
                {
                    Cylinder cyl_form = new Cylinder(ref listView1, section_index);
                    cyl_form.Owner = this;
                    cyl_form.ShowDialog();
                }

            }
            catch (Exception) { MessageBox.Show("You have to change somethig"); }
        }

        private void left_edge_chamf_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                if (left_edge_chamf.SelectedItem.ToString() == "no feature")
                {

                }
                if (left_edge_chamf.SelectedItem.ToString() == "Fillet")
                {
                    Filler filler_from = new Filler(ref listView1, 'l', section, section_index);
                    filler_from.Owner = this;
                    filler_from.ShowDialog();
                }
                if (left_edge_chamf.SelectedItem.ToString() == "Chamfer")
                {
                    chamfer chamf_foorm = new chamfer(ref listView1, 'l', section, section_index);
                    chamf_foorm.Owner = this;
                    chamf_foorm.ShowDialog();
                }

            }
            catch (Exception) { MessageBox.Show("You have to change somethig"); }
        }

        private void right_edge_camf_DropDownClosed(object sender, EventArgs e)
        {
            
            try
            {
                if (right_edge_camf.SelectedItem.ToString() == "no feature")
                {

                }
                else if (right_edge_camf.SelectedItem.ToString() == "Fillet")
                {
                    Filler filler_from = new Filler(ref listView1, 'r', section, section_index);
                    filler_from.Owner = this;
                    filler_from.ShowDialog();
                }
                else if (right_edge_camf.SelectedItem.ToString() == "Chamfer")
                {
                    chamfer chamf_foorm = new chamfer(ref listView1, 'r', section, section_index);
                    chamf_foorm.Owner = this;
                    chamf_foorm.ShowDialog();
                }

            }
            catch (Exception) { MessageBox.Show("You have to change somethig"); }
        }

        #endregion
        //ok button
        private void button2_Click(object sender, EventArgs e)
        {
            var_es.part_doc.SaveAs(path + @"/Shaft.ipt", true);
            TransientGeometry TG = var_es.InventorApp.TransientGeometry;
            Matrix matrix = TG.CreateMatrix();
            ComponentOccurrence Shaft1 = var_es._Doc.ComponentDefinition.Occurrences.Add(path + @"/Shaft.ipt", matrix);
            //ComponentOccurrence Shaft = var_es._Doc.ComponentDefinition.Occurrences.Add(path + @"/Shaft.iam", matrix);
            var_es.part_doc.Close(true);
        }
        
        //cancel button
        private void button3_Click(object sender, EventArgs e)
        {
            var_es.part_doc.Close(true);
            assemb_doc.Close(true);
            Close();
        }
        //add_polyon
        private void button1_Click(object sender, EventArgs e)
        {
            Polyon pol_form = new Polyon(ref listView1);
            pol_form.Owner = this;
            pol_form.ShowDialog();
        }
        //add_conus
        private void button4_Click(object sender, EventArgs e)
        {
            Conus con_form = new Conus(ref listView1);
            con_form.Owner = this;
            con_form.ShowDialog();
        }
        //add_Cylinder
        private void button5_Click(object sender, EventArgs e)
        {
            Cylinder cyl_form = new Cylinder(ref listView1);
            cyl_form.Owner = this;
            cyl_form.ShowDialog();
        }

        #region List_View

        private void assemble_list_view()
        {
            ColumnHeader header = new ColumnHeader();

            header.Text = "left chamfer";
            header.Name = "col0";
            header.Width = 70;
            listView1.Columns.Add(header);

            header = new ColumnHeader();
            header.Text = "Section";
            header.Name = "col1";
            listView1.Columns.Add(header);

            header = new ColumnHeader();
            header.Text = "riht chamfer";
            header.Name = "col2";
            header.Width = 70;
            listView1.Columns.Add(header);

            listView1.Scrollable = true;
            listView1.View = System.Windows.Forms.View.Details;
            listView1.GridLines = false;
            
            add_base_objects();
        }

        //creating and adding base fiures and features to them
        private void add_base_objects()
        {
            double dist = 0.2;
            double ang = 25;
            
            chamf distance_chamfer;
            Angle_chamf angle_chamfer;
            Distance_chamf two_distance_chamfer;
            fill filler;
            
            listView1.Items.Add("Chamfer");
            listView1.Items[listView1.Items.Count - 1].SubItems.Add("Polgyon");
            listView1.Items[listView1.Items.Count - 1].SubItems.Add("Chamfer");
            var_es.list.Add(new Pol(4, 3, 8, true));
            
            listView1.Items.Add("Fillet");
            listView1.Items[listView1.Items.Count - 1].SubItems.Add("Cylinder");
            listView1.Items[listView1.Items.Count - 1].SubItems.Add("Chamfer");
            var_es.list.Add(new Cyl(4, 3));
            
            listView1.Items.Add("Chamfer");
            listView1.Items[listView1.Items.Count - 1].SubItems.Add("Conus");
            listView1.Items[listView1.Items.Count - 1].SubItems.Add("Fillet");
            var_es.list.Add(new Con_(5, 7, 4));
            
            listView1.Items.Add("Fillet");
            listView1.Items[listView1.Items.Count - 1].SubItems.Add("Cylinder");
            listView1.Items[listView1.Items.Count - 1].SubItems.Add("Fillet");
            var_es.list.Add(new Cyl(6,3));
            
            //для первой секции
            distance_chamfer = new chamf(dist, 'l');
            var_es.features_list.Add(distance_chamfer);
            angle_chamfer = new Angle_chamf(dist, ang, 'r');
            var_es.features_list.Add(angle_chamfer);
            //для второй секции
            filler = new fill(dist + 0.1, 'l');
            var_es.features_list.Add(filler);
            two_distance_chamfer = new Distance_chamf(dist + 0.1, dist + 0.2, 'r');
            var_es.features_list.Add(two_distance_chamfer);
            //для третей секции
            Distance_chamf two_distance_chamfe = new Distance_chamf(dist + 0.2, dist + 0.3, 'l');
            var_es.features_list.Add(two_distance_chamfe);
            fill fille = new fill(dist + 0.1, 'r');
            var_es.features_list.Add(fille);
            //для четвертой секции
            fill fill = new fill(dist + 0.2, 'l');
            var_es.features_list.Add(fill);
            fill fil = new fill(dist + 0.3, 'r');
            var_es.features_list.Add(fil);

            Shaft();
        }

        //запрещаем пользователю изменять размер колонок
        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.Cancel = true;
            e.NewWidth = listView1.Columns[e.ColumnIndex].Width;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection collection = listView1.SelectedItems;
            foreach (ListViewItem item in collection)
            {
                section_index = item.Index;
                section = var_es.list[section_index];
            }
        }

        #endregion

        private void button6_Click(object sender, EventArgs e)
        {
            listView1.Items[section_index].Remove();
            var ID = section_index; 
            ID += 1;
            ID *= 2;
            ID -= 1;
            var_es.features_list.RemoveAt(ID);
            ID -= 1;
            var_es.features_list.RemoveAt(ID);
            Del(section_index);
            Shaft();
        }

        public static void Del(int index)
        {
            var_es.list.RemoveAt(index);
            partDef.Features.ExtrudeFeatures[1].Delete();
        }

        public static void Del()
        {
            partDef.Features.ExtrudeFeatures[1].Delete();
        }
    }
}
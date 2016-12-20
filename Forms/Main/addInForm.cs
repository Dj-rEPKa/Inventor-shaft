using System;
using System.Drawing;
using System.Windows.Forms;
using Inventor;
using ButtonTree;
using System.Collections.Generic;

namespace InvAddIn
{
    internal partial class addInForm : Form
    {

        //From variables
        #region variables

        internal static List<ButtonNode> NODES;
        internal static List<ButtonNode> nodes;

        public addInForm form;
        
        AssemblyDocument assemb_doc;

        public string path;

        protected static PartComponentDefinition partDef;
        protected static TransientGeometry tg;
        protected static PlanarSketch sketch;
        protected static EdgeCollection Edges;
        protected static Face S_Face = null;
        protected static Face E_Face = null;
        protected static List<Face> Side_F = null;
        #endregion


        //Constructor
        public addInForm()
        {
            InitializeComponent();
            //поиск изображений
            var_es.img_find();

            NODES = new List<ButtonNode>();
            nodes = new List<ButtonNode>();

            //формируем почву для вала, создаём новый assembly document и part document
            assemble();

            var_es.part_doc_def = var_es.part_doc.ComponentDefinition;

            var_es.list = new List<Section>();
            var_es.chamfer_list = new List<chamf>();

            var_es._list = new List<Section>();

        }


        //when form load do next:
        private void addInForm_Load(object sender, EventArgs e)
        {
            buttonTree1.IndicatorOnImage = var_es._imgs[19];
            buttonTree1.IndicatorOffImage = var_es._imgs[20];
            buttonTree1.DeleteBTNImage = var_es._imgs[12];
            buttonTree1.CustomizeBTNImage = var_es._imgs[18];
            buttonTree1.delToolTip = "Delete";
            buttonTree1.customToolTip = "Customize";

            var_es.feature_list = new List<Feature>();

            form = this;

            try
            {
                if (partDef.Features.ExtendFeatures.Count != 0)
                {
                    Del();
                }

                var sketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
                sketch.Name = "Features";
            }
            catch { }
        }


        //when form close do next:
        private void addInForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                var_es.part_doc.Close(true);
                assemb_doc.Close(true);
                System.IO.Directory.Delete(path, true);
            }
            catch { }
        }  //Main form closing


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
            Side_F = null;
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
                    if (var_es.chamfer_list.Count != 0)
                    {
                        S_Face = obj.Start_face;
                        E_Face = obj.End_face;
                        if (part is Pol)
                            Side_F = obj.Side_faces;

                        Edges = var_es.InventorApp.TransientObjects.CreateEdgeCollection();
                        if (var_es.chamfer_list[i] != null)
                        {
                            Create feature = var_es.chamfer_list[i];
                            feature.Create_BR(tg, ref sketch, Edges, ref S_Face, ref E_Face, ref partDef);
                        }
                        Edges = var_es.InventorApp.TransientObjects.CreateEdgeCollection();
                        S_Face = obj.Start_face;
                        E_Face = obj.End_face;
                        if (var_es.chamfer_list[j] != null)
                        {
                            Create feature = var_es.chamfer_list[j];
                            feature.Create_BR(tg, ref sketch, Edges, ref S_Face, ref E_Face, ref partDef);
                        }
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
        

        //Main form Buttons
        #region Buttons
        //ok button
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var_es.part_doc.SaveAs(path + @"/Shaft.ipt", true);
                TransientGeometry TG = var_es.InventorApp.TransientGeometry;
                Matrix matrix = TG.CreateMatrix();
                ComponentOccurrence Shaft1 = var_es._Doc.ComponentDefinition.Occurrences.Add(path + @"/Shaft.ipt", matrix);
                //ComponentOccurrence Shaft = var_es._Doc.ComponentDefinition.Occurrences.Add(path + @"/Shaft.iam", matrix);
                var_es.part_doc.Close(true);
                Close();
            }
            catch { }
        }
        
        //cancel button
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        //add_Polyon
        private void button1_Click(object sender, EventArgs e)
        {
            Polyon pol_form = new Polyon();
            pol_form.Owner = this;
            pol_form.ShowDialog();

            if (!Polyon.canceled)
            {
                buttonTree1.Add(NODES[NODES.Count - 1]);
                NODES[NODES.Count - 1].comb_mid_figure.SelectedItem = "Polygon";
            }
        }

        //add_Conus
        private void button4_Click(object sender, EventArgs e)
        {
            Conus con_form = new Conus();
            con_form.Owner = this;
            con_form.ShowDialog();

            if (!Conus.canceled)
            {
                buttonTree1.Add(NODES[NODES.Count - 1]);
                NODES[NODES.Count - 1].comb_mid_figure.SelectedItem = "Conus";
            }
        }

        //add_Cylinder
        private void button5_Click(object sender, EventArgs e)
        {
            Cylinder cyl_form = new Cylinder();
            cyl_form.Owner = this;
            cyl_form.ShowDialog();

            if (!Cylinder.canceled)
            {
                buttonTree1.Add(NODES[NODES.Count - 1]);
            }
        }
        #endregion 


        //creating and adding base fiures and features to them
        private void add_base_objects()
        {

            double dist = 0.2;
            double ang = 25;
            
            chamf distance_chamfer;
            Angle_chamf angle_chamfer;
            Distance_chamf two_distance_chamfer;
            fill filler;
            
            //для первой секции
            Revolve();
        }


        //variety of delete functions
        #region Delete options    
        public static void Del(int index)
        {
            var_es._list.RemoveAt(index);
            partDef.Features.RevolveFeatures[1].Delete();
            
        }

        public static void Del()
        {
            partDef.Features.RevolveFeatures[1].Delete();
        }

        public static void Delete()
        {
            foreach (RevolveFeature f in partDef.Features.RevolveFeatures)
            {
                f.Delete();
            }
        }

        public static void Delete(int ID)
        {
            Delete();
            buttonTree1.RemoveAt(ID);
            buttonTree1.ReLoad();
            Revolve();
            
        }

        public static void Delete_child(int ID, int id, int position)
        {
            Delete();
            var_es.feature_list.RemoveAt(addInForm.nodes[position].FeaturePosition);
            nodes.RemoveAt(position);
            NODES[ID].RemoveAt(id);
            buttonTree1.ReLoad();
            Revolve();
        }

        public static void Delete_chamfer(int ID, int id, int position)
        {
            Delete();
            nodes.RemoveAt(position);
            NODES[ID].RemoveAt(id);
            buttonTree1.ReLoad();
        }

        #endregion


        //creating shaft
        internal static void Revolve()
        {
            Profile profile;
            try
            {
                var_es.lines = new List<SketchLine>();

                if (var_es.part_doc_def.Features.RevolveFeatures.Count != 0)
                {
                    //partDef.Features.RevolveFeatures[1].Delete();
                    foreach (RevolveFeature s in var_es.part_doc_def.Features.RevolveFeatures)
                    {
                        s.Delete();
                    }
                }

                if (var_es.part_doc_def.Sketches.Count != 0)
                {
                    foreach(Sketch s in var_es.part_doc_def.Sketches)
                    {
                        s.Delete();
                    }
                }

                partDef = var_es.part_doc.ComponentDefinition;

                tg = var_es.InventorApp.TransientGeometry;
                sketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);

                if (var_es.lines.Count > 0)
                {
                    var_es.lines.RemoveAt(var_es.lines.Count - 1);
                    sketch.SketchLines[sketch.SketchLines.Count].Delete();
                }


                foreach (Section obj in var_es._list)
                {
                    obj.DrawGeom(tg, ref sketch, ref var_es.lines);
                }


                var_es.lines.Add(sketch.SketchLines.AddByTwoPoints(var_es.lines[0].StartSketchPoint, var_es.lines[var_es.lines.Count - 1].EndSketchPoint));
                var_es.lines[var_es.lines.Count - 1].Centerline = true;

                profile = sketch.Profiles.AddForSolid();

                RevolveFeature ex = partDef.Features.RevolveFeatures.AddFull(profile, var_es.lines[var_es.lines.Count - 1], PartFeatureOperationEnum.kNewBodyOperation);
            


            //assinging all faces to each section
            Assign_all_faces();

            Assign_All_Faces();

            }
            catch { }


            //Chamfers and Fillets creation

            Chamfer_creation();

            #region Polygon creation
            try
            {
                //polyon creation by cutting out unneeded parts
                foreach (Section obj in var_es._list)
                {
                    if (obj.GetType().ToString() == "InvAddIn.Pol")
                    {
                        PlanarSketch sketch;
                        //getting sketch
                        try
                        {
                            sketch = partDef.Sketches.Add(obj.Start_face);
                        }
                        catch
                        {
                            sketch = partDef.Sketches.Add(obj.End_face);
                        }
                        
                        sketch.Name = "Pol" + obj.Position;
                        
                        //creatin points for sketchline, this line is polygon`s side, wich determines what should be cut off
                        Point2d first = tg.CreatePoint2d(), second = tg.CreatePoint2d();
                        var k = (360 / obj.Number_of_Edges) / 2;
                        first.X = Math.Cos(k * Math.PI / 180) * var_es._list[obj.Position].Radius;
                        first.Y = Math.Sin(k * Math.PI / 180) * var_es._list[obj.Position].Radius;
                        second.X = first.X;
                        second.Y = first.Y * -1;

                        sketch.SketchLines.AddByTwoPoints(first, second);
                        //Arc to connect start and end point of polygon`s side , and mid-point is point on the the outside circle
                        sketch.SketchArcs.AddByThreePoints(sketch.SketchLines[1].StartSketchPoint, tg.CreatePoint2d(var_es._list[obj.Position].Radius), sketch.SketchLines[1].EndSketchPoint);
                        profile = sketch.Profiles.AddForSolid();
                        
                        //adding extrude definition
                        ExtrudeDefinition extrude = partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kCutOperation);
                        extrude.SetDistanceExtent(var_es._list[obj.Position].Length, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
                        ExtrudeFeature extrudeFeature = partDef.Features.ExtrudeFeatures.Add(extrude);
                        
                        //creating circle array of features to make polygon 
                        ObjectCollection OBJ_colection = var_es.InventorApp.TransientObjects.CreateObjectCollection();
                        OBJ_colection.Add(partDef.Features.ExtrudeFeatures[partDef.Features.ExtrudeFeatures.Count] as Object);
                        partDef.Features.CircularPatternFeatures.Add(OBJ_colection, partDef.WorkAxes[1], false, obj.Number_of_Edges + " ul", 360 + " deg", true, PatternComputeTypeEnum.kIdenticalCompute);
                    }
                }
                
            }
            catch (Exception e5)
            {
                MessageBox.Show(e5.ToString());
            }
            #endregion

            //All features creating

            try
            {
                Feature();
            }
            catch (Exception e113) { MessageBox.Show(e113.ToString()); }

            
        }

        internal static FaceCollection qwe;
        internal static FaceCollection sidefaces;

        
        internal static void Assign_all_faces()
        {
            
            sidefaces = var_es.InventorApp.TransientObjects.CreateFaceCollection();
            try
            {
                int i = -1;
                foreach (Face s in partDef.SurfaceBodies[1].Faces)
                {
                    if (s.SurfaceType == SurfaceTypeEnum.kConeSurface || s.SurfaceType == SurfaceTypeEnum.kCylinderSurface)
                    {
                        i++;
                        
                        var_es._list[i].Side_face = (Face)s;
                    }
                    else
                    {
                        
                    }
                }
                //MessageBox.Show(sidefaces.Count.ToString());
                /*foreach (Section s in var_es._list)
                {
                    s.Side_face = (Face)sidefaces[sidefaces.Count];
                    sidefaces.Remove(sidefaces.Count);
                }*/
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }

        internal static void Assign_All_Faces()
        {
            qwe = var_es.InventorApp.TransientObjects.CreateFaceCollection();
            foreach (Face s in partDef.SurfaceBodies[1].Faces)
            {
                try
                {
                    if (s.SurfaceType == SurfaceTypeEnum.kPlaneSurface)
                    {
                        qwe.Add(s);
                    }
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.ToString());
                }
            }
            //MessageBox.Show(qwe.Count.ToString());
            try
            {

                var_es._list[0].Start_face = qwe[qwe.Count] as Face;
                qwe.Remove(qwe.Count);

                for (int i = 0; i < var_es._list.Count; i++)
                {
                    if (!var_es._list[i].Same_as_next)
                    {
                        var_es._list[i].End_face = (Face)qwe[1];
                        if (i < var_es._list.Count - 1)
                            var_es._list[i + 1].Start_face = (Face)qwe[1];
                        qwe.Remove(1);
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }


        //creating chamfers
        private static void Chamfer_creation()
        {
            try
            {
                int i = 0;
                int j = 0;
                foreach (var f in var_es.chamfer_list)
                {
                    Edges = var_es.InventorApp.TransientObjects.CreateEdgeCollection();
                    if (!f.IsNullorEmpty())
                    {
                        f.Create_BR(tg, ref sketch, Edges, ref var_es._list[j].Side_face, ref var_es._list[j].Side_face, ref partDef);
                    }


                    i++;
                    j = i / 2;
                }
            }
            catch (Exception e21)
            {
                MessageBox.Show(e21.ToString());
            }
        }


        //creating other features
        private static void Feature()
        {
            foreach (Feature f in var_es.feature_list)
            {
                try
                {
                    PlanarSketch sketch = partDef.Sketches.Add(partDef.WorkPlanes[3]);
                    f.DrawGeom(tg, ref sketch, ref var_es.lines);
                    Profile prof = sketch.Profiles.AddForSolid();
                    RevolveFeature F = partDef.Features.RevolveFeatures.AddFull(prof, partDef.WorkAxes[1], PartFeatureOperationEnum.kCutOperation);
                }
                catch (Exception e1) { MessageBox.Show(e1.ToString()); }
            }
        }
    }
}
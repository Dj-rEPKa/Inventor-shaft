using System;
using Inventor;
using System.Collections.Generic;

namespace InvAddIn
{
    internal class Con_ : Section
    {

        
        protected double Angle;

        public Con_()
        {
            Radius = 0;
            Second_Radius = 0;
            Angle = 0;
        }

        public Con_(double length, double diametr, double second_diametr)
        {
            Radius = 0.5 * diametr;
            Second_Radius = 0.5 * second_diametr;
            Length = length;
            try
            {
                var a = Math.Abs(Radius - Second_Radius);
                var c = Math.Sqrt(a * a + Math.Pow(Length,2));
                var ang = 90 - Math.Acos((a * a + c * c - Math.Pow(Length, 2)) / (2 * a * c)) * 180 / Math.PI;
                if (diametr > second_diametr)
                    Angle = -ang;
                else
                {
                    Angle = ang;
                }
            }
            catch
            {
                Angle = 0;
            }
        }

        public Con_(double length, double diametr, double second_diametr, bool first, int position)
        {
            Radius = 0.5 * diametr;
            Second_Radius = 0.5 * second_diametr;
            Length = length;
            First = first;
            Position = position;
        }

        internal override void Create_BR(TransientGeometry TG, ref PlanarSketch sketch, EdgeCollection eColl, ref Face B_face, ref Face E_face, ref PartComponentDefinition partDef)
        {
            sketch.SketchCircles.AddByCenterRadius(TG.CreatePoint2d(), Radius);
            Profile profile = sketch.Profiles.AddForSolid();
            ExtrudeDefinition extrude = partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kNewBodyOperation);
            extrude.SetDistanceExtent(Length, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            extrude.TaperAngle = Angle + " deg";
            ExtrudeFeature extrudeFeature = partDef.Features.ExtrudeFeatures.Add(extrude);
            sketch = partDef.Sketches.Add(extrudeFeature.EndFaces[1]);
            Start_face = extrudeFeature.StartFaces[1];
            End_face = extrudeFeature.EndFaces[1];
            Same_as_next = false;
        }

        internal override void DrawGeom(TransientGeometry TG, ref PlanarSketch sketch, ref List<SketchLine> lines)
        {
            try
            {
                if (!First)
                {
                    if (Radius == var_es._list[Position - 1].Radius)
                        var_es._list[Position - 1].Same_as_next = true;

                    lines.RemoveAt(lines.Count - 1);
                    sketch.SketchLines[sketch.SketchLines.Count].Delete();
                    if (Radius == var_es._list[Position - 1].Radius)
                    {
                        SideLine = sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(lines[lines.Count - 1].EndSketchPoint.Geometry.X + Length, Second_Radius));
                        lines.Add(SideLine);
                        lines.Add(sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(lines[lines.Count - 1].EndSketchPoint.Geometry.X, 0)));
                    }
                    else
                    {
                        lines.Add(sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(lines[lines.Count - 1].EndSketchPoint.Geometry.X, Radius)));
                        SideLine = sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(lines[lines.Count - 1].EndSketchPoint.Geometry.X + Length, Second_Radius));
                        lines.Add(SideLine);
                        lines.Add(sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(lines[lines.Count - 1].EndSketchPoint.Geometry.X, 0)));
                    }
                }
                else if (First)
                {
                    lines.Add(sketch.SketchLines.AddByTwoPoints(TG.CreatePoint2d(), TG.CreatePoint2d(0, Radius)));
                    SideLine = sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(Length, Second_Radius));
                    lines.Add(SideLine);
                    lines.Add(sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(Length)));
                }
            }
            catch (System.Exception e1)
            {
                System.Windows.Forms.MessageBox.Show(e1.ToString());
            }
        }
    }
}

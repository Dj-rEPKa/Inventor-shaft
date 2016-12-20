using System.Collections.Generic;
using Inventor;

namespace InvAddIn
{
    internal class Cyl : Section
    {

        internal Feature feature;

        public Cyl()
        {
            Radius = 0; //delete
        }

        public Cyl(double length, double diametr)
        {
            Radius = 0.5 * diametr;
            Length = length;
            First = false;
        }

        internal override void Create_BR(TransientGeometry TG, ref PlanarSketch sketch, EdgeCollection eColl, ref Face B_face, ref Face E_face, ref PartComponentDefinition partDef)
        {
            sketch.SketchCircles.AddByCenterRadius(TG.CreatePoint2d(), Radius);
            Profile profile = sketch.Profiles.AddForSolid();
            ExtrudeDefinition extrude = partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kNewBodyOperation);
            extrude.SetDistanceExtent(Length, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature extrudeFeature = partDef.Features.ExtrudeFeatures.Add(extrude);
            sketch = partDef.Sketches.Add(extrudeFeature.EndFaces[1]);
            Start_face = extrudeFeature.StartFaces[1];
            End_face = extrudeFeature.EndFaces[1];
            Side_face = extrudeFeature.SideFaces[1];
        }

        public Cyl(double length, double diametr, bool first, int position)
        {
            Radius = 0.5 * diametr;
            Length = length;
            First = first;
            Position = position;
            Same_as_next = false;
        }

        public Cyl(double length, double diametr, bool first, int position, Feature _feature)
        {
            Radius = 0.5 * diametr;
            Length = length;
            First = first;
            Position = position;
            Same_as_next = false;
            feature = _feature;
        }

        internal override void DrawGeom(TransientGeometry TG, ref PlanarSketch sketch, ref List<SketchLine> lines)
        {
            try
            {
                if(!First)
                {
                    if (Radius == var_es._list[Position - 1].Radius)
                        var_es._list[Position - 1].Same_as_next = true;

                    lines.RemoveAt(lines.Count - 1);
                    sketch.SketchLines[sketch.SketchLines.Count].Delete();
                    if (Radius == var_es._list[Position - 1].Radius)
                    {
                        SideLine = sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(lines[lines.Count - 1].EndSketchPoint.Geometry.X + Length, Radius));
                        lines.Add(SideLine);
                        lines.Add(sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(lines[lines.Count - 1].EndSketchPoint.Geometry.X, 0)));
                    }
                    else
                    {
                        lines.Add(sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(lines[lines.Count - 1].EndSketchPoint.Geometry.X, Radius)));
                        SideLine = sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(lines[lines.Count - 1].EndSketchPoint.Geometry.X + Length, Radius));
                        lines.Add(SideLine);
                        lines.Add(sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(lines[lines.Count - 1].EndSketchPoint.Geometry.X, 0)));
                    }
                }
                else if (First)
                {
                    lines.Add(sketch.SketchLines.AddByTwoPoints(TG.CreatePoint2d(), TG.CreatePoint2d(0, Radius)));
                    SideLine = sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(Length, Radius));
                    lines.Add(SideLine);
                    lines.Add(sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(Length)));
                }
            }
            catch(System.Exception e1)
            {
                System.Windows.Forms.MessageBox.Show(e1.ToString());
            }
        }

    }
}

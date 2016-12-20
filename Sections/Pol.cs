using System;
using Inventor;
using System.Collections.Generic;

namespace InvAddIn
{
    internal class Pol: Section
    {
        protected bool is_Inscribed;

        public Pol()
        {
            Number_of_Edges = 0;
            is_Inscribed = true;
        }

        public Pol(double length, double diametr, int edges, bool inscr )
        {
            Radius = 0.5 * diametr;
            Length = length;
            Number_of_Edges = edges;
            is_Inscribed = inscr;
        }

        public Pol (double length, double diametr, int edges, bool inscr, bool first, int position)
        {
            Radius = 0.5 * diametr;
            Length = length;
            Number_of_Edges = edges;
            is_Inscribed = inscr;
            First = first;
            Position = position;
            Same_as_next = false;
        }


        internal override void Create_BR(TransientGeometry TG, ref PlanarSketch sketch, EdgeCollection eColl, ref Face B_face, ref Face E_face, ref PartComponentDefinition partDef)
        {
            sketch.SketchLines.AddAsPolygon(Number_of_Edges, TG.CreatePoint2d(), TG.CreatePoint2d(Radius), is_Inscribed);
            Profile profile = sketch.Profiles.AddForSolid();
            ExtrudeDefinition extrude = partDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kNewBodyOperation);
            extrude.SetDistanceExtent(Length, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature extrudeFeature = partDef.Features.ExtrudeFeatures.Add(extrude);
            sketch = partDef.Sketches.Add(extrudeFeature.EndFaces[1]);
            Start_face = extrudeFeature.StartFaces[1];
            End_face = extrudeFeature.EndFaces[1];
            foreach (Face side in extrudeFeature.SideFaces)
                Side_faces.Add(side);
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
                    if (var_es._list[Position].Radius == var_es._list[Position - 1].Radius)
                    {
                        SideLine = sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(lines[lines.Count - 1].EndSketchPoint.Geometry.X + Length, Radius));
                        lines.Add(SideLine);
                        lines.Add(sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(lines[lines.Count - 1].EndSketchPoint.Geometry.X, 0)));
                    }
                    else
                    {
                        lines.Add(sketch.SketchLines.AddByTwoPoints(lines[lines.Count - 1].EndSketchPoint, TG.CreatePoint2d(lines[lines.Count - 1].EndSketchPoint.Geometry.X, var_es._list[Position].Radius)));
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
            catch { }
        }
    }
}   

using System;
using Inventor;

namespace InvAddIn
{
    internal class Con_ : Section
    {

        protected double Second_Radius;
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
        }
    }
}

using Inventor;

namespace InvAddIn
{
    internal class Cyl : Section
    {

        public Cyl()
        {
            Radius = 0;
        }

        public Cyl(double length, double diametr)
        {
            Radius = 0.5 * diametr;
            Length = length;
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
        }
    }
}

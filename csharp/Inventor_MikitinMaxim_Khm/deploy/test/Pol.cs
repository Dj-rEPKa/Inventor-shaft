using Inventor;

namespace InvAddIn
{
    internal class Pol: Section
    {
        protected int Number_of_Edges;
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
        }
    }
}

using Inventor;

namespace InvAddIn
{
    public class fill : chamf
    {

        internal fill()
        {
            Radius = 0;
            Side = 'r';
        }

        internal fill(double radius, char side)
        {
            Radius = radius;
            Side = side;
        }

        internal override void Create_BR(TransientGeometry TG, ref PlanarSketch sketch, EdgeCollection eColl, ref Face B_face, ref Face E_face, ref PartComponentDefinition partDef)
        {
            FilletFeature fillet_Feature;
            switch (Side)
            {
                case ('r'):
                    foreach (Edge e in B_face.Edges)
                        eColl.Add(e);
                    fillet_Feature = partDef.Features.FilletFeatures.AddSimple(eColl, Radius);
                    break;
                case ('l'):
                    foreach (Edge e in E_face.Edges)
                        eColl.Add(e);
                    fillet_Feature = partDef.Features.FilletFeatures.AddSimple(eColl, Radius);
                    break;
            }

        }
    }
}

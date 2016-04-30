using Inventor;

namespace InvAddIn
{
    public class chamf: Create
    {
        protected double Distance;
        protected char Side;

        internal chamf()
        {
            Distance = 0;
            Side = 'r';
        } 

        internal chamf(double distance, char side)
        {
            Distance = distance;
            Side = side;
        }


        internal override void Create_BR(TransientGeometry TG, ref PlanarSketch sketch, EdgeCollection eColl, ref Face B_face, ref Face E_face, ref PartComponentDefinition partDef)
        {
            ChamferFeature chamf_Feature;
            switch (Side)
            {
                case ('r'):
                    foreach (Edge e in B_face.Edges)
                        eColl.Add(e);
                    chamf_Feature = partDef.Features.ChamferFeatures.AddUsingDistance(eColl, Distance);
                    break;
                case ('l'):
                    foreach (Edge e in E_face.Edges)
                        eColl.Add(e);
                    chamf_Feature = partDef.Features.ChamferFeatures.AddUsingDistance(eColl, Distance);
                    break;
            }
            
        }
        
    }
}

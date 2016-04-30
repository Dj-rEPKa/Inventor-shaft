using Inventor;

namespace InvAddIn
{
    class Distance_chamf: chamf
    {
        protected double Second_Distance;

        public Distance_chamf()
        {
            Distance = 0;
            Second_Distance = 0;
            Side = 'r';
        }

        public Distance_chamf(double distance1, double distance2, char side)
        {
            Distance = distance1;
            Second_Distance = distance2;
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
                    chamf_Feature = partDef.Features.ChamferFeatures.AddUsingTwoDistances(eColl, B_face, Distance, Second_Distance);
                    break;
                case ('l'):
                    foreach (Edge e in E_face.Edges)
                        eColl.Add(e);
                    chamf_Feature = partDef.Features.ChamferFeatures.AddUsingTwoDistances(eColl, E_face, Distance, Second_Distance);
                    break;
            }

        }
    }
}

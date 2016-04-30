using Inventor;

namespace InvAddIn
{
    class Angle_chamf : chamf
    {
        protected double Angle;

        public Angle_chamf()
        {
            Distance = 0;
            Angle = 0;
            Side = 'r';
        }

        public Angle_chamf(double distance, double angle, char side)
        {
            Distance = distance;
            Angle = angle;
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
                    chamf_Feature = partDef.Features.ChamferFeatures.AddUsingDistanceAndAngle(eColl, B_face, Distance, Angle + "deg");
                    break;
                case ('l'):
                    foreach (Edge e in E_face.Edges)
                        eColl.Add(e);
                    chamf_Feature = partDef.Features.ChamferFeatures.AddUsingDistanceAndAngle(eColl, E_face, Distance, Angle + "deg");
                    break;
            }

        }
    }
}
//idCode 3544911567
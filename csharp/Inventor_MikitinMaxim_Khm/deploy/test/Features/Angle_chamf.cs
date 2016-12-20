using Inventor;
using System.Windows.Forms;

namespace InvAddIn
{
    class Angle_chamf : chamf
    {
        internal double Angle;

        public Angle_chamf()
        {
            Distance = 0;
            Angle = 0;
            Side = 'r';
            empty = true;
        }

        public Angle_chamf(double distance, double angle, char side, int position, int node_position)
        {
            Distance = distance;
            Angle = angle;
            Side = side;
            empty = false;
            Position = position;
            Node_Position = node_position;
        }

        public Angle_chamf(double distance, double angle, char side, int position)
        {
            Distance = distance;
            Angle = angle;
            Side = side;
            empty = false;
            Position = position;
        }

        internal override void Create_BR(TransientGeometry TG, ref PlanarSketch sketch, EdgeCollection eColl, ref Face B_face, ref Face E_face, ref PartComponentDefinition partDef)
        {
            ChamferFeature chamf_Feature;
            switch (Side)
            {
                case ('r'):
                    
                    try
                    {
                        eColl.Add(B_face.Edges[1]);
                    }
                    catch
                    {
                        MessageBox.Show("Catch r");
                        //eColl.Add(B_face.Edges[2]);
                    }
                    chamf_Feature = partDef.Features.ChamferFeatures.AddUsingDistanceAndAngle(eColl, B_face, Distance, Angle + "deg");
                    break;
                case ('l'):
                    try
                    {
                        eColl.Add(E_face.Edges[2]);
                    }
                    catch
                    {
                        MessageBox.Show("Catch l");
                        //eColl.Add(E_face.Edges[1]);
                    }
                    chamf_Feature = partDef.Features.ChamferFeatures.AddUsingDistanceAndAngle(eColl, E_face, Distance, Angle + "deg");
                    break;
            }

        }
    }
}
//idCode 3544911567
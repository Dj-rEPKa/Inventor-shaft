using Inventor;
using System.Windows.Forms;

namespace InvAddIn
{
    class Distance_chamf: chamf
    {
        internal double Second_Distance;

        public Distance_chamf()
        {
            Distance = 0;
            Second_Distance = 0;
            Side = 'r';
            empty = true;
        }

        public Distance_chamf(double distance1, double distance2, char side, int position, int node_position)
        {
            Distance = distance1;
            Second_Distance = distance2;
            Side = side;
            empty = false;
            Position = position;
            Node_Position = node_position;
        }

        public Distance_chamf(double distance1, double distance2, char side, int position)
        {
            Distance = distance1;
            Second_Distance = distance2;
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
                    chamf_Feature = partDef.Features.ChamferFeatures.AddUsingTwoDistances(eColl, B_face, Distance, Second_Distance);
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
                    chamf_Feature = partDef.Features.ChamferFeatures.AddUsingTwoDistances(eColl, E_face, Distance, Second_Distance);
                    break;
            }

        }
    }
}

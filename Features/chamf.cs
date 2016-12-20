using Inventor;
using System;
using System.Windows.Forms;

namespace InvAddIn
{
    public class chamf: Feature
    {

        internal chamf()
        {
            Distance = 0;
            Side = 'r';
            empty = true;
        } 

        internal chamf(double distance, char side, int position, int node_position)
        {
            Distance = distance;
            Side = side;
            empty = false;
            Position = position;
            Node_Position = node_position;
        }

        internal chamf(double distance, char side, int position)
        {
            Distance = distance;
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
                    chamf_Feature = partDef.Features.ChamferFeatures.AddUsingDistance(eColl, Distance);
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
                    chamf_Feature = partDef.Features.ChamferFeatures.AddUsingDistance(eColl, Distance);
                    break;
            }
            
        }
        
    }
}

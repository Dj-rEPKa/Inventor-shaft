using Inventor;

namespace InvAddIn
{
    public class fill : chamf
    {

        internal fill()
        {
            Radius = 0;
            Side = 'r';
            empty = true;
        }

        internal fill(double radius, char side, int position, int node_position)
        {
            Radius = radius;
            Side = side;
            empty = false;
            Position = position;
            Node_Position = node_position;
        }

        internal fill(double radius, char side, int position)
        {
            Radius = radius;
            Side = side;
            empty = false;
            Position = position;
        }

        internal override void Create_BR(TransientGeometry TG, ref PlanarSketch sketch, EdgeCollection eColl, ref Face B_face, ref Face E_face, ref PartComponentDefinition partDef)
        {
            FilletFeature fillet_Feature;
            switch (Side)
            {
                case ('r'):
                    /*foreach (Edge e in B_face.Edges)
                        eColl.Add(e);*/
                    try
                    {
                        //MessageBox.Show("2 - r");
                        eColl.Add(B_face.Edges[1]);
                    }
                    catch
                    {
                        //MessageBox.Show("1 - r");
                        eColl.Add(B_face.Edges[2]);
                    }
                    fillet_Feature = partDef.Features.FilletFeatures.AddSimple(eColl, Radius);
                    break;
                case ('l'):
                    /*foreach (Edge e in E_face.Edges)
                        eColl.Add(e);*/
                    try
                    {
                        //MessageBox.Show("2 - l");
                        eColl.Add(E_face.Edges[2]);
                    }
                    catch
                    {
                        //MessageBox.Show("1 - l");
                        eColl.Add(E_face.Edges[1]);
                    }
                    fillet_Feature = partDef.Features.FilletFeatures.AddSimple(eColl, Radius);
                    break;
            }

        }
    }
}

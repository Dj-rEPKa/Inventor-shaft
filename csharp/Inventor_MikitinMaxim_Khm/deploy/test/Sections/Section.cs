using Inventor;

namespace InvAddIn
{
    public class Section : Create
    {
        internal Face Start_face;
        internal Face End_face;
        internal Face Side_face;
        
        internal bool First;
        internal double Length;
        internal int Number_of_Edges;
        internal SketchPoint StartPoint;
        internal System.Collections.Generic.List<Face> Side_faces = new System.Collections.Generic.List<Face>();
        internal int Position;
        internal double Second_Radius;
        internal bool Same_as_next;

        internal Section()
        {

        }

        internal Section(double length, double diametr)
        {
            Length = length;
            Radius = 0.5 * diametr;
        }
    }
}

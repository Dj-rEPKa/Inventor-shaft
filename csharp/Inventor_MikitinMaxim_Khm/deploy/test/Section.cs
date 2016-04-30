using Inventor;

namespace InvAddIn
{
    public class Section : Create
    {
        internal Face Start_face;
        internal Face End_face;
        internal System.Collections.Generic.List<Face> Side_faces = new System.Collections.Generic.List<Face>();

        protected double Length;

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

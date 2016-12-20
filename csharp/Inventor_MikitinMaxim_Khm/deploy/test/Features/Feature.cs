using Inventor;

namespace InvAddIn

{
    public class Feature: Create
    {
        internal double Distance;
        internal char Side;
        internal int index;
        internal double _length;
        internal double Width; //ring
        internal double Depth; //groove
        internal int Position;
        internal int Node_Position;

        internal bool empty = true;

        internal Feature()
        {
            Radius = 0;
            Distance = 0;
            
        }

        internal virtual bool IsNullorEmpty()
        {
            return empty ? true : false;
        }
    }
}

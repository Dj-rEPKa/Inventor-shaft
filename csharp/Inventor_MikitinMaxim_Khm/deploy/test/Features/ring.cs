using System;
using System.Collections.Generic;
using Inventor;
using System.Windows.Forms;

namespace InvAddIn
{
    public class ring : Feature
    {
        internal ring()
        {
            empty = true;
            Radius = 0;
            Distance = 0;
        }

        internal ring(double distance, double diametr, double width, char side, int ID, int position, int node_position)
        {
            Distance = distance;
            Radius = 0.5 * diametr;
            Width = width;
            Side = side;
            index = ID;
            empty = false;
            Position = position;
            Node_Position = node_position;
        }

        internal ring(double distance, double diametr, double width, char side, int ID, int position)
        {
            Distance = distance;
            Radius = 0.5 * diametr;
            Width = width;
            Side = side;
            index = ID;
            empty = false;
            Position = position;
        }


        internal override void DrawGeom(TransientGeometry TG, ref PlanarSketch sketch, ref List<SketchLine> lines)
        {
            try
            {
                _length = 0;
                for (int i = 0; i < index; i++)
                {
                    _length += var_es._list[i].Length;
                }
                
                switch (Side)
                {
                    case ('r'):
                        _length += var_es._list[index].Length;
                        sketch.SketchLines.AddAsTwoPointRectangle(TG.CreatePoint2d(_length - Distance - Width, Radius), TG.CreatePoint2d(_length - Distance, var_es._list[index].Radius));
                        break;
                    case ('l'):
                        sketch.SketchLines.AddAsTwoPointRectangle(TG.CreatePoint2d(_length + Distance, Radius), TG.CreatePoint2d(_length + Distance + Width, var_es._list[index].Radius));
                        break;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.ToString());
            }
        }
    }
}

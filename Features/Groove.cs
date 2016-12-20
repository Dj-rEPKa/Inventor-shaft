using System;
using System.Collections.Generic;
using Inventor;

namespace InvAddIn
{
    public class Groove : Feature
    {
        internal double Hord;
        internal Groove()
        {
            empty = true;
            Radius = 0;
            Distance = 0;
        }

        internal Groove(double distance, double radius, double depth, char side, int ID, int position, int node_position)
        {
            Distance = distance;
            Radius = radius;
            Side = side;
            Depth = depth;
            index = ID;
            empty = false;
            Position = position;
            Node_Position = node_position;
        }

        internal Groove(double distance, double radius, double depth, char side, int ID, int position)
        {
            Distance = distance;
            Radius = radius;
            Side = side;
            Depth = depth;
            index = ID;
            empty = false;
            Position = position;
        }

        internal override void DrawGeom(TransientGeometry TG, ref PlanarSketch sketch, ref List<SketchLine> lines)
        {
            Hord = 2 * Math.Sqrt(Depth * (2 * Radius - Depth));
            //(2 * Math.Sqrt(Depth * (2 * Radius - Depth))) to calculate hord distance
            var length = Distance + Hord;

            _length = 0;
            for (int i = 0; i < index; i++)
            {
                _length += var_es._list[i].Length;
            } 

            switch (Side)
            {
                case ('r'):
                    _length += var_es._list[index].Length;
                    var arc = sketch.SketchArcs.AddByThreePoints(TG.CreatePoint2d(_length - Distance + 0.5 * Hord, var_es._list[index].Radius), TG.CreatePoint2d(_length - Distance, var_es._list[index].Radius - Depth), TG.CreatePoint2d(_length - Distance - 0.5 * Hord, var_es._list[index].Radius));
                    sketch.SketchLines.AddByTwoPoints(arc.EndSketchPoint, arc.StartSketchPoint);
                    break;
                case ('l'):
                    arc = sketch.SketchArcs.AddByThreePoints(TG.CreatePoint2d(_length + Distance - 0.5 * Hord, var_es._list[index].Radius), TG.CreatePoint2d(_length + Distance, var_es._list[index].Radius - Depth), TG.CreatePoint2d(_length + Distance + 0.5 * Hord, var_es._list[index].Radius));
                    sketch.SketchLines.AddByTwoPoints(arc.EndSketchPoint, arc.StartSketchPoint);
                    break;
            }
        }

    }
}

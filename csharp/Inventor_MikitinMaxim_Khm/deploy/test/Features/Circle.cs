using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventor;
using System.Windows.Forms;

namespace InvAddIn
{
    public class Circle : Feature
    {
        internal Circle ()
        {
            empty = true;
            Radius = 0;
            Distance = 0;
        }

        internal Circle(double distance, double diametr, double width, char side, SketchLine side_line)
        {
            Distance = distance;
            Radius = 0.5 * diametr;
            Width = width;
            Side = side;
            SideLine = side_line;
            empty = false;
        }

        internal override void DrawGeom(TransientGeometry TG, ref PlanarSketch sketch, ref List<SketchLine> lines)
        {
            var pos = lines.FindIndex(x => x == SideLine);
            lines.RemoveAt(pos);
            pos += 1;
            sketch.SketchLines[pos].Delete();
            var length = Distance + Width;

            switch (Side)
            {
                case ('r'):
                    var point = sketch.SketchLines[pos - 1].EndSketchPoint;
                    var line = sketch.SketchLines.AddByTwoPoints(point, TG.CreatePoint2d(point.Geometry.X + Distance, point.Geometry.Y));
                    var line0 = sketch.SketchLines.AddByTwoPoints(line.EndSketchPoint, TG.CreatePoint2d(line.EndSketchPoint.Geometry.X, Radius));
                    var line1 = sketch.SketchLines.AddByTwoPoints(line0.EndSketchPoint, TG.CreatePoint2d(line0.EndSketchPoint.Geometry.X + Width, line0.EndSketchPoint.Geometry.Y));
                    var line2 = sketch.SketchLines.AddByTwoPoints(line1.EndSketchPoint, TG.CreatePoint2d(line.EndSketchPoint.Geometry.X + Width, line.EndSketchPoint.Geometry.Y));
                    sketch.SketchLines.AddByTwoPoints(line2.EndSketchPoint, sketch.SketchLines[pos].StartSketchPoint);
                    break;
                case ('l'):
                    point = sketch.SketchLines[pos].StartSketchPoint;
                    line = sketch.SketchLines.AddByTwoPoints(sketch.SketchLines[pos - 1].EndSketchPoint, TG.CreatePoint2d(point.Geometry.X - length, point.Geometry.Y));
                    line0 = sketch.SketchLines.AddByTwoPoints(line.EndSketchPoint, TG.CreatePoint2d(line.EndSketchPoint.Geometry.X, Radius));
                    line1 = sketch.SketchLines.AddByTwoPoints(line0.EndSketchPoint, TG.CreatePoint2d(line0.EndSketchPoint.Geometry.X + Width, line0.EndSketchPoint.Geometry.Y));
                    line2 = sketch.SketchLines.AddByTwoPoints(line1.EndSketchPoint, TG.CreatePoint2d(line.EndSketchPoint.Geometry.X + Width, line.EndSketchPoint.Geometry.Y));
                    sketch.SketchLines.AddByTwoPoints(line2.EndSketchPoint, point);
                    break;
            }
        }
    }
}

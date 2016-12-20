using Inventor;
using System.Collections.Generic;

namespace InvAddIn
{
    public class Create
    {

        internal double Radius;
        internal SketchLine SideLine;
        //метод для рисования елемента который будут переопределять все дочерние классы
        internal virtual void Create_BR(TransientGeometry TG,ref PlanarSketch sketch, EdgeCollection eColl, ref Face B_face, ref Face E_face, ref PartComponentDefinition partDef )
        {
        }
        
        internal virtual void DrawGeom(TransientGeometry TG, ref PlanarSketch sketch, ref List<SketchLine> lines)
        {
        }

    }
}

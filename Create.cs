using Inventor;

namespace InvAddIn
{
    public class Create
    {

        protected double Radius;
        //метод для рисования елемента который будут переопределять все дочерние классы
        internal virtual void Create_BR(TransientGeometry TG,ref PlanarSketch sketch, EdgeCollection eColl, ref Face B_face, ref Face E_face, ref PartComponentDefinition partDef )
        {
        }
        
    }
}

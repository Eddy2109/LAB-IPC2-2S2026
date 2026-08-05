namespace IntroProyecto1
{
    public abstract class Robot
    {
        public string nombre;
        public int capacidad;

        public Robot(string nombre)
        {
            this.capacidad = 0;
            this.nombre = nombre;
        }
      
        public abstract void Mover(int capacidadUnidadMilitar); // metodo difertente para un recue o un fighter
        public abstract void RestarCapacidad(int capacidadUnidadMilitar); // metodo solo aplica a los fighters
    }
}
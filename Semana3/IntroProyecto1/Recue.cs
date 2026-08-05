namespace IntroProyecto1
{
    public class Recue : Robot
    {
        public Recue() : base("Recue")
        {
            this.capacidad = 0; // El robot Recue no tiene capacidad de carga
        }
        public override void Mover(int capacidadUnidadMilitar)
        {
            // El robot Recue se mueve evitando a las unidades militares
        }

        public override void RestarCapacidad(int capacidadUnidadMilitar)
        {
            // Este método no aplica para el robot Recue, por lo que no se implementa.
            
        }
    }
}
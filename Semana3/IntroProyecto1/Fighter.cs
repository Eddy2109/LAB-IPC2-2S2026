namespace IntroProyecto1
{
    public class Fighter : Robot
    {
        public Fighter(int capacidadInicial) : base("Fighter")
        {
            this.capacidad = capacidadInicial;
        }
        public override void Mover(int capacidadUnidadMilitar)
        {
            if(this.capacidad > capacidadUnidadMilitar)
            {
                // se le resta
                Console.WriteLine("El robot Fighter se está moviendo.");
            }
            else
            {
                Console.WriteLine("El robot Fighter no puede vencer a la unidad militar, mision fallida.");
            }
        }

        public override void RestarCapacidad(int capacidadUnidadMilitar)
        {
            this.capacidad -= capacidadUnidadMilitar;
            
        }
    }
}
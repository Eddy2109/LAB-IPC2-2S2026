namespace Semana2
{
    public class carro : vehiculo
    {
        public string color;

        public carro(string marca, string modelo, string color) : base(marca, modelo)
        {
            this.color = color;
        }

        // POLIMORFISMO
        public override void Arrancar()
        {
            Console.WriteLine($"[{marca} {modelo}] Gira la llave... ¡BRUM BRUM! (Ruido de motor V8)");
        }
    }
}
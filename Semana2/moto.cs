namespace Semana2
{
    public class moto : vehiculo
    {
        public moto(string marca, string modelo) : base(marca, modelo) { }

        // 4. POLIMORFISMO (Parte 2: La implementación)
        public override void Arrancar()
        {
            Console.WriteLine($"[{marca} {modelo}] Patada al pedal... ¡RATATA-TA! (Ruido agudo)");
        }
    }
}
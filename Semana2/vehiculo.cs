namespace Semana2
{
    public abstract class vehiculo
    {
        public string marca;
        public string modelo;
        
        // ENCAPSULAMIENTO
        private int gasolina;

        public vehiculo(string marca, string modelo)
        {
            this.marca = marca;
            this.modelo = modelo;
            this.gasolina = 0;
        }

        public void CargarGasolina(int cantidad)
        {
            gasolina += cantidad;
            Console.WriteLine($"Se cargaron {cantidad} litros de gasolina. Total: {gasolina} litros.");
        }

        public void verGasolina()
        {
            Console.WriteLine($"Gasolina actual: {gasolina} litros.");
        }

        // POLIMORFISMO
        public abstract void Arrancar();
    }
}
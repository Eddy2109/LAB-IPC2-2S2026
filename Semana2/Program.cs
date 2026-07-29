namespace Semana2;

class Program
{
    static void Main(string[] args)
    {
        carro miCarro = new carro("Toyota", "Corolla", "Rojo");
        moto miMoto = new moto("Honda", "CBR500R");

        // metodos heredados
        miCarro.CargarGasolina(40);
        miCarro.verGasolina();

        miMoto.CargarGasolina(15);
        miMoto.verGasolina();

        // POLIMORFISMO
        Console.WriteLine("\nArrancando los vehículos:");
        miCarro.Arrancar();
        miMoto.Arrancar();
    }
}

namespace IntroProyecto1
{
    public class ListaRobot
    {

        public class NodoRobot
        {
            public Robot robot;
            public NodoRobot? siguiente;
            
            public NodoRobot(Robot r)
            {
                robot = r;
                siguiente = null;
            }

        }
        private NodoRobot? cabeza;
        private NodoRobot? cola;

        public ListaRobot()
        {
            cabeza = null;
            cola = null;
        }

        public void AgregarRobot(Robot robot)
        {
            NodoRobot nuevoNodo = new NodoRobot(robot);
            if (cabeza == null)
            {
                cabeza = nuevoNodo;
                cola = nuevoNodo;
            }
            else
            {
                cola.siguiente = nuevoNodo;
                cola = nuevoNodo;
            }
        }
        

        public void MostrarRobots()
        {
            NodoRobot actual = cabeza;
            while (actual != null)
            {
                Console.WriteLine($"Robot: {actual.robot.GetType().Name}, Capacidad: {actual.robot.capacidad}");
                actual = actual.siguiente;
            }
        }
    }
}
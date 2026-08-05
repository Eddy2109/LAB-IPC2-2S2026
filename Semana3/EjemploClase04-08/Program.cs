using System.Xml; //XmlDocument
using System.Xml.Linq; //XDocument
using System.Xml.XPath;


namespace Ejemplo3
{
    class Producto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Stock { get; set; }

        public Producto siguiente;
    }

    class ListaProductos
    {
        public Producto primero { get; set; }
        public Producto ultimo { get; set; }

        public ListaProductos()
        {
            this.primero = null;
            this.ultimo = null;
        }

        public void agregarProducto(string id, string nombre, string stock)
        {
            Producto nuevoProducto = new Producto { Id = id, Nombre = nombre, Stock = stock };
            if (primero == null)
            {
                primero = nuevoProducto;
                ultimo = nuevoProducto;
            }
            else
            {
                ultimo.siguiente = nuevoProducto;
                ultimo = nuevoProducto;
            }
        }

        public void mostrarProductos()
        {
            Producto actual = primero;
            while (actual != null)
            {
                Console.WriteLine($"ID: {actual.Id}, Nombre: {actual.Nombre}, Stock: {actual.Stock}");
                actual = actual.siguiente;
            }
        }

        public void modificarStock(string id, string nuevoStock)
        {
            Producto actual = primero;
            while (actual != null)
            {
                if (actual.Id == id)
                {
                    actual.Stock = nuevoStock;
                    Console.WriteLine($"Producto {actual.Nombre} actualizado con nuevo stock: {actual.Stock}");
                    return;
                }
                actual = actual.siguiente;
            }
            Console.WriteLine("Producto no encontrado.");
        }
    }

    class Program
    {
        
        static void Main(String[] args)
        {
            // menu
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Leer XML con XDocument");
            Console.WriteLine("2. Leer XML con XmlDocument");
            Console.WriteLine("3. Leer XML con XPathNavigator");
            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    Xml_Con_XDocument("inventario.xml");
                    break;
                case "2":
                    Xml_Con_XmlDocument("inventario.xml");
                    break;
                case "3":
                    Xml_Con_XpathNavigator("inventario.xml");
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        // usando XDocument
        public static void Xml_Con_XDocument(string rutaArchivo)
        {
            ListaProductos lista = new ListaProductos();

            //leer el archivo XML
            XDocument doc = XDocument.Load(rutaArchivo);

            // guardar los productos en la lista enlazada
            foreach (var producto in doc.Descendants("Producto"))
            {
                string id = producto.Attribute("id").Value;
                string nombre = producto.Element("Nombre").Value;
                string stock = producto.Element("Stock").Value;
                lista.agregarProducto(id, nombre, stock);
            }

            // mostramos los productos guardados en la lista enlazada
            lista.mostrarProductos();

            // liberar memoria ocupada por el archivo
            doc = null;

            // hacer modificaciones 
            Console.WriteLine("Escriba el ID del producto a modificar:");
            string idModificar = Console.ReadLine();
            Console.WriteLine("Escriba el nuevo stock:");
            string nuevoStock = Console.ReadLine();
            lista.modificarStock(idModificar, nuevoStock);

            // agregar otro producto
            Console.WriteLine("Escriba el ID del nuevo producto:");
            string idNuevo = Console.ReadLine();
            Console.WriteLine("Escriba el nombre del nuevo producto:");
            string nombreNuevo = Console.ReadLine();
            Console.WriteLine("Escriba el stock del nuevo producto:");
            string stockNuevo = Console.ReadLine();
            lista.agregarProducto(idNuevo, nombreNuevo, stockNuevo);

            lista.mostrarProductos();

            // escribir el nuevo stock en el otro archivo xml
            XDocument docNuevo = new XDocument(new XElement("Inventario"));
            Producto actual = lista.primero;
            while (actual != null)
            {
                docNuevo.Root.Add(new XElement("Producto",
                    new XAttribute("id", actual.Id),
                    new XElement("Nombre", actual.Nombre),
                    new XElement("Stock", actual.Stock)));
                actual = actual.siguiente;
            }
            docNuevo.Save("nuevoInventario1.xml");
        }

        // usando XmlDocument        
        public static void Xml_Con_XmlDocument(string rutaArchivo)
        {
            ListaProductos lista2 = new ListaProductos();

            // cargar el archivo XML con XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(rutaArchivo);

            // leer los productos y guardarlos en la lista enlazada
            XmlNodeList productos = xmlDoc.GetElementsByTagName("Producto");

            foreach (XmlNode producto in productos)
            {
                string id = producto.Attributes["id"].Value;
                string nombre = producto["Nombre"].InnerText;
                string stock = producto["Stock"].InnerText;
                lista2.agregarProducto(id, nombre, stock);
            }

            // mostrar los productos guardados en la lista enlazada
            lista2.mostrarProductos();

            // agregar otro producto
            Console.WriteLine("Escriba el ID del nuevo producto:");
            string idNuevo = Console.ReadLine();
            Console.WriteLine("Escriba el nombre del nuevo producto:");
            string nombreNuevo = Console.ReadLine();
            Console.WriteLine("Escriba el stock del nuevo producto:");
            string stockNuevo = Console.ReadLine();
            lista2.agregarProducto(idNuevo, nombreNuevo, stockNuevo);

            // escribir el nuevo stock en el otro archivo xml
            XmlDocument nuevoDoc = new XmlDocument();
            XmlElement root = nuevoDoc.CreateElement("Inventario");
            Producto actual = lista2.primero;
            while (actual != null)
            {
                XmlElement productoElement = nuevoDoc.CreateElement("Producto");
                productoElement.SetAttribute("id", actual.Id);
                XmlElement nombreElement = nuevoDoc.CreateElement("Nombre");
                nombreElement.InnerText = actual.Nombre;
                XmlElement stockElement = nuevoDoc.CreateElement("Stock");
                stockElement.InnerText = actual.Stock;
                productoElement.AppendChild(nombreElement);
                productoElement.AppendChild(stockElement);
                root.AppendChild(productoElement);
                actual = actual.siguiente;
            }
            nuevoDoc.AppendChild(root);
            nuevoDoc.Save("nuevoInventario2.xml");
        }

        // usando XpathNavigator
        public static void Xml_Con_XpathNavigator(string rutaArchivo)
        {
            ListaProductos lista3 = new ListaProductos();

            // cargar el archivo XML con XPathNavigator
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(rutaArchivo);
            XPathNavigator navigator = xmlDoc.CreateNavigator();

            // leer los productos y guardarlos en la lista enlazada
            XPathNodeIterator productos = navigator.Select("/Inventario/Producto");
            while (productos.MoveNext())
            {
                string id = productos.Current.GetAttribute("id", "");
                string nombre = productos.Current.SelectSingleNode("Nombre").Value;
                string stock = productos.Current.SelectSingleNode("Stock").Value;
                lista3.agregarProducto(id, nombre, stock);
            }

            // mostrar los productos guardados en la lista enlazada
            lista3.mostrarProductos();

             // agregar otro producto
            Console.WriteLine("Escriba el ID del nuevo producto:");
            string idNuevo = Console.ReadLine();
            Console.WriteLine("Escriba el nombre del nuevo producto:");
            string nombreNuevo = Console.ReadLine();
            Console.WriteLine("Escriba el stock del nuevo producto:");
            string stockNuevo = Console.ReadLine();
            lista3.agregarProducto(idNuevo, nombreNuevo, stockNuevo);

            // escribir el nuevo stock en el otro archivo xml
            XmlDocument nuevoDoc = new XmlDocument();
            XmlElement root = nuevoDoc.CreateElement("Inventario");
            Producto actual = lista3.primero;
            while (actual != null)
            {
                XmlElement productoElement = nuevoDoc.CreateElement("Producto");
                productoElement.SetAttribute("id", actual.Id);
                XmlElement nombreElement = nuevoDoc.CreateElement("Nombre");
                nombreElement.InnerText = actual.Nombre;
                XmlElement stockElement = nuevoDoc.CreateElement("Stock");
                stockElement.InnerText = actual.Stock;
                productoElement.AppendChild(nombreElement);
                productoElement.AppendChild(stockElement);
                root.AppendChild(productoElement);
                actual = actual.siguiente;
            }
            nuevoDoc.AppendChild(root);
            nuevoDoc.Save("nuevoInventario3.xml");
        }
    }
}

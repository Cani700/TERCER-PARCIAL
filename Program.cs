using System;
using MySql.Data.MySqlClient;

namespace tercerparcial

{
    class Program
    {
        static string connectionString = "server=localhost;database=tercerp;user=root;password='';"; 
        MySqlConnection conectado = new MySqlConnection(connectionString);
        static void Main(string[] args)
        {
            Console.WriteLine("Conexión exitosa a la base de datos.");
            while (true)
            {
                Console.WriteLine("Elige una opción:");
                Console.WriteLine("1 - Trabajar con Clientes");
                Console.WriteLine("2 - Trabajar con Facturas");
                Console.WriteLine("3 - Trabajar con Sucursales");
                Console.WriteLine("4 - Salir");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        MenuClientes();
                        break;
                    case "2":
                        MenuFacturas();
                        break;
                    case "3":
                        MenuSucursales();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        static void MenuClientes()
        {
            while (true)
            {
                Console.WriteLine("Elige una opción:");
                Console.WriteLine("1 - Insertar Datos");
                Console.WriteLine("2 - Listar Datos");
                Console.WriteLine("3 - Modificar Datos");
                Console.WriteLine("4 - Eliminar Datos");
                Console.WriteLine("0 - Volver atrás");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        InsertarCliente();
                        break;
                    case "2":
                        ListarClientes();
                        break;
                    case "3":
                        ModificarCliente();
                        break;
                    case "4":
                        EliminarCliente();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        static void InsertarCliente()
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(nombre) || nombre.Length < 3)
            {
                Console.WriteLine("El nombre es obligatorio y debe tener al menos 3 caracteres.");
                Console.Write("Nombre: ");
                nombre = Console.ReadLine();
            }

            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(apellido) || apellido.Length < 3)
            {
                Console.WriteLine("El apellido es obligatorio y debe tener al menos 3 caracteres.");
                Console.Write("Apellido: ");
                apellido = Console.ReadLine();
            }

            Console.Write("Email: ");
            string email = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Clientes (Nombre, Apellido, Email) VALUES (@Nombre, @Apellido, @Email)";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellido", apellido);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Cliente insertado con éxito.");
        }

        static void ListarClientes()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Clientes";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ClienteId: {reader["ClienteId"]}, Nombre: {reader["Nombre"]}, Apellido: {reader["Apellido"]}, Email: {reader["Email"]}");
                    }
                }
            }
        }

        static void ModificarCliente()
        {
            Console.Write("ID del Cliente a modificar: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nuevo Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Nuevo Apellido: ");
            string apellido = Console.ReadLine();
            Console.Write("Nuevo Email: ");
            string email = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Clientes SET Nombre = @Nombre, Apellido = @Apellido, Email = @Email WHERE ClienteId = @ClienteId";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Apellido", apellido);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@ClienteId", id);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Cliente modificado con éxito.");
        }

        static void EliminarCliente()
        {
            Console.Write("ID del Cliente a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Clientes WHERE ClienteId = @ClienteId";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ClienteId", id);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Cliente eliminado con éxito.");
        }

        static void MenuFacturas()
        {
            while (true)
            {
                Console.WriteLine("Elige una opción:");
                Console.WriteLine("1 - Insertar Datos");
                Console.WriteLine("2 - Listar Datos");
                Console.WriteLine("3 - Modificar Datos");
                Console.WriteLine("4 - Eliminar Datos");
                Console.WriteLine("0 - Volver atrás");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        InsertarFactura();
                        break;
                    case "2":
                        ListarFacturas();
                        break;
                    case "3":
                        ModificarFactura();
                        break;
                    case "4":
                        EliminarFactura();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        static void InsertarFactura()
        {
            Console.Write("ID del Cliente: ");
            int clienteID = int.Parse(Console.ReadLine());
            Console.Write("Fecha (YYYY-MM-DD): ");
            string fecha = Console.ReadLine();
            Console.Write("Monto: ");
            decimal monto = decimal.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Facturas (ClienteID, Fecha, Monto) VALUES (@ClienteID, @Fecha, @Monto)";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ClienteID", clienteID);
                    cmd.Parameters.AddWithValue("@Fecha", fecha);
                    cmd.Parameters.AddWithValue("@Monto", monto);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Factura insertada con éxito.");
        }

        static void ListarFacturas()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Facturas";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"FacturaId: {reader["FacturaId"]}, ClienteID: {reader["ClienteID"]}, Fecha: {reader["Fecha"]}, Monto: {reader["Monto"]}");
                    }
                }
            }
        }

        static void ModificarFactura()
        {
            Console.Write("ID de la Factura a modificar: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nuevo ClienteID: ");
            int clienteID = int.Parse(Console.ReadLine());
            Console.Write("Nueva Fecha (YYYY-MM-DD): ");
            string fecha = Console.ReadLine();
            Console.Write("Nuevo Monto: ");
            decimal monto = decimal.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Facturas SET ClienteID = @ClienteID, Fecha = @Fecha, Monto = @Monto WHERE FacturaId = @FacturaId";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ClienteID", clienteID);
                    cmd.Parameters.AddWithValue("@Fecha", fecha);
                    cmd.Parameters.AddWithValue("@Monto", monto);
                    cmd.Parameters.AddWithValue("@FacturaId", id);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Factura modificada con éxito.");
        }

        static void EliminarFactura()
        {
            Console.Write("ID de la Factura a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Facturas WHERE FacturaId = @FacturaId";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@FacturaId", id);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Factura eliminada con éxito.");
        }

        static void MenuSucursales()
        {
            while (true)
            {
                Console.WriteLine("Elige una opción:");
                Console.WriteLine("1 - Insertar Datos");
                Console.WriteLine("2 - Listar Datos");
                Console.WriteLine("3 - Modificar Datos");
                Console.WriteLine("4 - Eliminar Datos");
                Console.WriteLine("0 - Volver atrás");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        InsertarSucursal();
                        break;
                    case "2":
                        ListarSucursales();
                        break;
                    case "3":
                        ModificarSucursal();
                        break;
                    case "4":
                        EliminarSucursal();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
        }

        static void InsertarSucursal()
        {
            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Ubicación: ");
            string ubicacion = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Sucursales (Nombre, Ubicacion) VALUES (@Nombre, @Ubicacion)";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Ubicacion", ubicacion);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Sucursal insertada con éxito.");
        }

        static void ListarSucursales()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Sucursales";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"SucursalId: {reader["SucursalId"]}, Nombre: {reader["Nombre"]}, Ubicación: {reader["Ubicacion"]}");
                    }
                }
            }
        }

        static void ModificarSucursal()
        {
            Console.Write("ID de la Sucursal a modificar: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nuevo Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Nueva Ubicación: ");
            string ubicacion = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Sucursales SET Nombre = @Nombre, Ubicacion = @Ubicacion WHERE SucursalId = @SucursalId";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Ubicacion", ubicacion);
                    cmd.Parameters.AddWithValue("@SucursalId", id);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Sucursal modificada con éxito.");
        }

        static void EliminarSucursal()
        {
            Console.Write("ID de la Sucursal a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Sucursales WHERE SucursalId = @SucursalId";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SucursalId", id);
                    cmd.ExecuteNonQuery();
                }
            }
            Console.WriteLine("Sucursal eliminada con éxito.");
        }
    }
}


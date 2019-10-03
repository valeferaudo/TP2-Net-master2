using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities;
using Business.Logic;

namespace UI.Consola
{
    public class Usuarios
    {
        private Business.Logic.UsuarioLogic _UsuarioNegocio;
        public Business.Logic.UsuarioLogic UsuarioNegocio
        {
            get { return _UsuarioNegocio; }
            set { _UsuarioNegocio = value; }
        }
        public Usuarios()
        {
            this.UsuarioNegocio = new Business.Logic.UsuarioLogic();
        }
        public void Menu()
        {
            int opc = 0;
            while (opc != 6)
            {
                Muestraopciones();
                opc = Convert.ToInt32(System.Console.ReadLine());
                switch (opc)
                {
                    case 1:
                        {
                            ListadoGeneral();
                        }
                        break;
                    case 2:
                        {
                            Consultar();
                        }
                        break;
                    case 3:
                        {

                            Agregar();

                        }
                        break;
                    case 4:
                        {
                            Modificar();
                        }
                        break;
                    case 5:
                        {
                            Eliminar();
                        }
                        break;
                    default:
                        {

                        }
                        break;
                }
                Console.ReadKey();
            }
        }
        public void Muestraopciones()
        {
            System.Console.Clear();
            System.Console.WriteLine("Intento de Sysacad trucho v0.0000000000....0001");
            System.Console.WriteLine("===========Menu principal ==============");
            System.Console.WriteLine("1. Listado General");
            System.Console.WriteLine("2. Consulta");
            System.Console.WriteLine("3. Agregar");
            System.Console.WriteLine("4. Modificar");
            System.Console.WriteLine("5. Eliminar");
            System.Console.WriteLine("6. Salir");
            System.Console.WriteLine("========================================");
            System.Console.WriteLine("Elija una opcion :");

        }
        public void ListadoGeneral()
        {
            Console.Clear();
            Usuarios us = new Usuarios();
            foreach (Usuario usr in us.UsuarioNegocio.GetAll())
            {
                MostrarDatos(usr);
            }
        }
        public void MostrarDatos(Usuario usr)
        {
            Console.WriteLine("Usuario : {0}", usr.ID);
            Console.WriteLine("\t\tNombre : {0}", usr.Nombre);
            Console.WriteLine("\t\tApellido: {0}", usr.Apellido);
            Console.WriteLine("\t\tNombre de Usuario: {0}", usr.NombreUsuario);
            Console.WriteLine("\t\tClave: {0}", usr.Clave);
            Console.WriteLine("\t\tEmail: {0}", usr.EMail);
            Console.WriteLine("\t\tHabilitado: {0}", usr.Habilitado);
            Console.WriteLine();
        }
        public void Consultar()
        {
            try
            {
                Console.Clear();
                Console.Write("Ingrese el ID del usuario a consultar: ");
                int ID = int.Parse(Console.ReadLine());
                this.MostrarDatos(UsuarioNegocio.GetOne(ID));
            }
            catch (FormatException fe)
            {
                Console.WriteLine("El ID debe ser un entero");
            }
            catch (Exception e)
            {
                Console.WriteLine("No existe el usuario");

            }
            finally
            {
                Console.WriteLine("Presione una tecla para continuar");
            }
        }
        public void Modificar()
        {
            try
            {
                Console.Clear();
                Console.Write("Ingrese el ID de usuario a modificar : ");
                int ID = int.Parse(Console.ReadLine());
                Usuario usuario = UsuarioNegocio.GetOne(ID);
                Console.Write("Ingrese nombre : ");
                usuario.Nombre = Console.ReadLine();
                Console.Write("Ingrese apellido : ");
                usuario.Apellido = Console.ReadLine();
                Console.Write("Ingrese Email : ");
                usuario.EMail = Console.ReadLine();
                Console.Write("Ingrese nombre de usuario : ");
                usuario.NombreUsuario = Console.ReadLine();
                Console.Write("Ingrese clave : ");
                usuario.Clave = Console.ReadLine();
                Console.Write("Ingrese Habilitacion de Usuario (1-Si/otro-No) : ");
                usuario.Habilitado = (Console.ReadLine() == "1");
                usuario.State = BusinessEntity.States.Modified;
                UsuarioNegocio.Save(usuario);
            }
            catch (FormatException fe)
            {
                Console.WriteLine(" ");
                Console.WriteLine("La ID debe ser un numero entero ");


            }
            catch (Exception e)
            {
                Console.WriteLine(" ");
                Console.WriteLine("El usuario no existe ");

            }
            finally
            {
                Console.WriteLine("Ingrese una tecla para continuar.");
            }

        }
        public void Agregar()
        {
            Usuario usuario = new Usuario();
            Console.Clear();
            Console.Write("Ingresar Nombre :");
            usuario.Nombre = Console.ReadLine();
            Console.Write("Ingresar apellido : ");
            usuario.Apellido = Console.ReadLine();
            Console.Write("Ingresar nombre de usuario : ");
            usuario.NombreUsuario = Console.ReadLine();
            Console.Write("Ingresar clave : ");
            usuario.Clave = Console.ReadLine();
            Console.Write("Ingresar email : ");
            usuario.EMail = Console.ReadLine();
            Console.Write("Ingrese Habilitacion de Usuario (1-Si/otro-No) : ");
            usuario.Habilitado = (Console.ReadLine() == "1");
            usuario.State = BusinessEntity.States.New;
            UsuarioNegocio.Save(usuario);
            Console.WriteLine();
            Console.WriteLine("ID: {0}",usuario.ID);

        }
        public void Eliminar()
        {
            try
            {
                Console.Clear();
                Console.Write("Ingrese el ID de usuario a eliminar: ");
                int ID = int.Parse(Console.ReadLine());
                UsuarioNegocio.Delete(ID);
            }
            catch (FormatException fe)
            {
                Console.WriteLine(" ");
                Console.WriteLine("La ID debe ser un numero entero ");


            }
            catch (Exception e)
            {
                Console.WriteLine(" ");
                Console.WriteLine("El usuario no existe ");

            }
            finally
            {
                Console.WriteLine("Ingrese una tecla para continuar.");
            }
        }
    }
}

using AngularNetcoreEmployeesTable.Models;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace AngularNetcoreEmployeesTable.Data
{
    public class EmpleadoData
    {
        private readonly string connection;

        public EmpleadoData(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Empleado>> GetAll()
        {
            List<Empleado> lista = new List<Empleado>();
            try
            {
                using (var con = new SqlConnection(connection))
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("sp_listaEmpleados", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lista.Add(new Empleado
                            {
                                IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                                NombreCompleto = reader["NombreCompleto"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Sueldo = Convert.ToDecimal(reader["Sueldo"]),
                                FechaContrato = reader["FechaContrato"].ToString()
                            });
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return lista;
        }

        public async Task<Empleado> GetById(int Id)
        {
            Empleado empleado = new Empleado();
            try
            {
                using (var con = new SqlConnection(connection))
                {
                    await con.OpenAsync();
                    SqlCommand cmd = new SqlCommand("sp_obtenerEmpleado", con);
                    cmd.Parameters.AddWithValue("@IdEmpleado", Id);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            empleado = new Empleado
                            {
                                IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                                NombreCompleto = reader["NombreCompleto"].ToString(),
                                Correo = reader["Correo"].ToString(),
                                Sueldo = Convert.ToDecimal(reader["Sueldo"]),
                                FechaContrato = reader["FechaContrato"].ToString()
                            };
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return empleado;
        }


        public async Task<bool> Post(Empleado objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(connection))
            {

                SqlCommand cmd = new SqlCommand("sp_crearEmpleado", con);
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);
                cmd.Parameters.AddWithValue("@Sueldo", objeto.Sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", objeto.FechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }





        public async Task<bool> Put(Empleado objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("sp_editarEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", objeto.IdEmpleado);
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);
                cmd.Parameters.AddWithValue("@Sueldo", objeto.Sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", objeto.FechaContrato);

                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            };

            return respuesta;
        }

        public async Task<bool> Delete(int Id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(connection))
            {
                SqlCommand cmd = new SqlCommand("sp_eliminarEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;

                }
                catch
                {
                    respuesta = false;
                }

            }

            return respuesta;
        }

    }
}
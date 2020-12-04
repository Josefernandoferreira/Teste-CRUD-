using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Dapper;

namespace CRUD.Models
{
    public class ClienteModel
    {        
        public int IdCliente { get; set; }

        [Required(ErrorMessage ="Informe seu Nome!")]
        public string NomeCliente { get; set; }

        [Required(ErrorMessage = "Informe seu Email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe seu CPF!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Escolha o Produto!")]
        public int IdProduto { get; set; }

        static string strConnectionString = "User Id=sa;Password=123456;Server=Localhost;Database=crud;";

        public static IEnumerable<ClienteModel> ListarClientes()
        {
            List<ClienteModel> listarClientes = new List<ClienteModel>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                listarClientes = con.Query<ClienteModel>("ListarClientes").ToList();
            }
           
            return listarClientes;
        }

        public static ClienteModel ListarClientePorId(int? id)
        {
            ClienteModel clienteModel = new ClienteModel();
            if (id == null)
                return clienteModel;

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@IdCliente", id);
                clienteModel = con.Query<ClienteModel>("ListarClientePorId", parameter, commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
                        
            return clienteModel;
        }

        public static int AdicionarCliente(ClienteModel clienteModel)
        {
            int rowAffected = 0;
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@NomeCliente", clienteModel.NomeCliente);
                parameters.Add("@Email", clienteModel.Email);
                parameters.Add("@CPF", clienteModel.CPF);
                parameters.Add("@IdProduto", clienteModel.IdProduto);
                try
                {
                    rowAffected = con.Execute("InserirCliente", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (SqlException e)
                {
                    return 0;
                    //throw e;
                }
            }
           
            return rowAffected;
        }

        public static int EditaCliente(ClienteModel clienteModel)
        {
            int rowAffected = 0;

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@IdCliente", clienteModel.IdCliente);
                parameters.Add("@NomeCliente", clienteModel.NomeCliente);
                parameters.Add("@Email", clienteModel.Email);
                parameters.Add("@CPF", clienteModel.CPF);
                parameters.Add("@IdProduto", clienteModel.IdProduto);
                rowAffected=  con.Execute("AtualizaCLiente",parameters,commandType:CommandType.StoredProcedure);
            }
                        
            return rowAffected;
        }

        public static int DeletarCliente(int id)
        {
            int rowAffected = 0;
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@IdCliente",id);
                rowAffected = con.Execute("DeletaCliente",parameters,commandType:CommandType.StoredProcedure);

            }
                       
            return rowAffected;
        }
    }
}

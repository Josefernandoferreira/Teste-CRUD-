using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Dapper;

namespace CRUD.Models
{
    public class ProdutoModel
    {        
        public int IdProduto { get; set; }

        [Required(ErrorMessage ="Informe o Nome do Produto!")]
        public string NomeProduto { get; set; }

        [Required(ErrorMessage = "Informe o Status do Produto!")]
        public string StatusProd { get; set; }



        static string strConnectionString = "User Id=sa;Password=123456;Server=Localhost;Database=crud;";


        public static IEnumerable<ProdutoModel> ListarProduto()
        {
            List<ProdutoModel> listarProduto = new List<ProdutoModel>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                listarProduto = con.Query<ProdutoModel>("ListarProdutos").ToList();
            }
           
            return listarProduto;
        }

        public static ProdutoModel ListarProdutoPorId(int? id)
        {
            ProdutoModel produtoModel = new ProdutoModel();
            if (id == null)
                return produtoModel;

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@IdProduto", id);
                produtoModel = con.Query<ProdutoModel>("ListarProdutoPorId", parameter, commandType:CommandType.StoredProcedure).FirstOrDefault();
            }
                        
            return produtoModel;
        }

        public static int AdicionarProduto(ProdutoModel produtoModel)
        {
            int rowAffected = 0;
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@NomeProduto", produtoModel.NomeProduto);
                parameters.Add("@StatusProd", produtoModel.StatusProd);
                
                rowAffected= con.Execute("InserirProduto",parameters,commandType:CommandType.StoredProcedure);
            }
           
            return rowAffected;
        }

        public static int EditaProduto(ProdutoModel produtoModel)
        {
            int rowAffected = 0;

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@IdProduto", produtoModel.IdProduto);
                parameters.Add("@NomeProduto", produtoModel.NomeProduto);
                parameters.Add("@StatusProd", produtoModel.StatusProd);
  
                rowAffected = con.Execute("AtualizaProduto", parameters, commandType: CommandType.StoredProcedure);
            }

            return rowAffected;
        }

        public static int DeletarProduto(int id)
        {
            int rowAffected = 0;
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@IdProduto",id);

                try
                {
                    rowAffected = con.Execute("DeletaProduto", parameters, commandType: CommandType.StoredProcedure);
                }catch(SqlException e)
                {
                    return 0;
                    throw (e);
                } 
            }
                       
            return rowAffected;
        }
    }
}

using RegistroCidades.Interface;
using RegistroCidades.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroCidades.Data
{
    public class RegiaoDAO : IDataStore<Regiao>
    {
        private ConnectionManager ConnectionManager = new ConnectionManager();
        private string sql;
        public void Add(Regiao obj)
        {
            sql = $"INSERT INTO Regiao ([Descricao],[UF]) VALUES ('{obj.Descricao}','{obj.UF});";
            ConnectionManager.ExecuteNonQuery(sql);
            sql = string.Empty;
        }
        public void Delete(Regiao obj)
        {
            sql = $"DELETE FROM Regiao WHERE ID ={obj.ID}; ";
            ConnectionManager.ExecuteNonQuery(sql);
            sql = string.Empty;
        }
        public List<Regiao> GetAll()
        {
            sql = $"SELECT * FROM Regiao;";
            List<Regiao> regioes = new List<Regiao>();
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    regioes.Add(Converter(reader));
                }
                reader.Close();
                con.Close();
            }
            sql = string.Empty;
            return regioes;
        }
        public Regiao GetById(int id)
        {
            Regiao item;
            sql = $"SELECT * FROM Regiao WHERE ID = {id};";
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                item = Converter(reader);
                reader.Close();
                con.Close();
            }
            sql = string.Empty;
            return item;
        }
        public Regiao Converter(SqlDataReader sqlDataReader)
        {
            return new Regiao
            {
                ID = Convert.ToInt32(sqlDataReader["ID"]),
                Descricao = sqlDataReader["Descricao"].ToString(),
                UF = Convert.ToInt32(sqlDataReader["UF"]) 
            };
        }
    }
}

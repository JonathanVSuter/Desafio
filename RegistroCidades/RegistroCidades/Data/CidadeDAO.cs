using RegistroCidades.Interface;
using RegistroCidades.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroCidades.Data
{
    public class CidadeDAO : IDataStore<Cidade>
    {
        private ConnectionManager ConnectionManager = new ConnectionManager();
        private string sql;

        public void Add(Cidade obj)
        {
            sql = $"INSERT INTO Cidade ([CodIBGE],[Nome],[Latitude],[Longitude],[UF],[Regiao]) VALUES ({obj.CodIBGE},'{obj.Nome}','{obj.Latitude}','{obj.Longitude}',{obj.UF},{obj.Regiao});";
            ConnectionManager.ExecuteNonQuery(sql);
            sql = string.Empty;
        }
        public void Delete(Cidade obj)
        {
            sql = $"DELETE FROM Cidade WHERE CodIBGE ={obj.CodIBGE}; ";
            ConnectionManager.ExecuteNonQuery(sql);
            sql = string.Empty;
        }
        public List<Cidade> GetAll()
        {
            sql = $"SELECT * FROM Cidade;";
            List<Cidade> cidades = new List<Cidade>();
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    cidades.Add(Converter(reader));
                }
                reader.Close();
                con.Close();
            }
            sql = string.Empty;
            return cidades;
        }
        public Cidade GetById(int id)
        {
            Cidade item;
            sql = $"SELECT * FROM Cidade WHERE CodIBGE = {id};";
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
        public Cidade Converter(SqlDataReader sqlDataReader)
        {
            return new Cidade
            {
                CodIBGE = Convert.ToInt32(sqlDataReader["CodIBGE"]),
                Nome = sqlDataReader["Nome"].ToString(),
                Latitude = sqlDataReader["Latitude"].ToString(),
                Longitude = sqlDataReader["Longitude"].ToString(),
                Regiao = Convert.ToInt32(sqlDataReader["Regiao"]),
                UF = Convert.ToInt32(sqlDataReader["UF"])
            };
        }
    }
}

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
            sql = $"INSERT INTO Cidade ([CodIBGE],[Nome],[Latitude],[Longitude],[UF],[Regiao]) VALUES ({obj.CodIBGE},'{obj.Nome}','{obj.Latitude}','{obj.Longitude}',{obj.UF.ID},{obj.Regiao.ID});";
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
            sql = $"SELECT UF.ID, UF.Sigla,Regiao.ID, Regiao.Descricao, Regiao.UF, Cidade.CodIBGE," +
                $"Cidade.Nome,Cidade.Latitude,Cidade.Longitude,Cidade.Regiao,Cidade.UF, Cidade.Regiao,Cidade.UF " +
                $"FROM (([UF] INNER JOIN [Regiao] ON [Regiao].UF = [UF].ID) INNER JOIN [Cidade] ON [Regiao].ID = [Cidade].Regiao);";
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
            Cidade item = new Cidade();
            sql = $"SELECT UF.ID, UF.Sigla,Regiao.ID, Regiao.Descricao, Regiao.UF, " +
                $"Cidade.CodIBGE,Cidade.Nome,Cidade.Latitude,Cidade.Longitude,Cidade.Regiao," +
                $"Cidade.UF, Cidade.Regiao  " +
                $"FROM (([UF] INNER JOIN [Regiao] ON [Regiao].UF = [UF].ID) INNER JOIN [Cidade] ON [Regiao].ID = [Cidade].Regiao) WHERE Cidade.CodIBGE={id};";
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    item=(Converter(reader));
                }
                reader.Close();
                con.Close();
            }
            sql = string.Empty;
            return item;
        }
        public void Update(int id,Cidade obj) 
        {
            sql = $"UPDATE Cidade SET [Nome]='{obj.Nome}',[Latitude]='{obj.Latitude}',[Longitude]='{obj.Longitude}',[UF]={obj.UF.ID},[Regiao]={obj.Regiao.ID} WHERE [CodIBGE] = {id};";
            ConnectionManager.ExecuteNonQuery(sql);
        }
        public Cidade Converter(SqlDataReader sqlDataReader)
        {
            return new Cidade
            {
                CodIBGE = Convert.ToInt32(sqlDataReader[5]),
                Nome = sqlDataReader[6].ToString(),
                Latitude = sqlDataReader[7].ToString(),
                Longitude = sqlDataReader[8].ToString(),
                Regiao =
                {
                    Descricao = sqlDataReader[3].ToString(),
                    ID = Convert.ToInt32(sqlDataReader[2]),
                    UF = Convert.ToInt32(sqlDataReader[4])
                },
                UF =
                {
                    ID = Convert.ToInt32(sqlDataReader[0]),
                    Sigla = sqlDataReader[1].ToString()
                }
            };
        }
    }
}

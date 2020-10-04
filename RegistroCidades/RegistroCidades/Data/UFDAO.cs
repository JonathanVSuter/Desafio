using RegistroCidades.Interface;
using RegistroCidades.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RegistroCidades.Data
{
    public class UFDAO : IDataStore<UF>
    {
        private ConnectionManager ConnectionManager = new ConnectionManager();
        private string sql;
        //operações simplificadas, ainda poderia melhorar usando SQLParameter: https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlparameter?view=netframework-4.7.2
        public void Add(UF obj)
        {
            sql = $"INSERT INTO UF ([Sigla]) VALUES ('{obj.Sigla}');";
            ConnectionManager.ExecuteNonQuery(sql);
            sql = string.Empty;
        }
        public void Delete(UF obj)
        {
            sql = $"DELETE FROM UF WHERE ID ={obj.ID}; ";
            ConnectionManager.ExecuteNonQuery(sql);
            sql = string.Empty;
        }
        public List<UF> GetAll()
        {
            sql = $"SELECT * FROM UF;";
            List<UF> uFs = new List<UF>();
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while(reader.Read())
                {
                    uFs.Add(Converter(reader));
                }
                reader.Close();
                con.Close();
            }
            sql = string.Empty;
            return uFs;    
        }
        public UF GetById(int id)
        {
            List<UF> lista = new List<UF>();
            sql = $"SELECT * FROM UF WHERE ID = {id};";
            using (SqlConnection con = ConnectionManager.GetConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while(reader.Read())
                {                    
                     lista.Add(Converter(reader));
                }
                sql = string.Empty;
                reader.Close();
                con.Close();
                return lista.FirstOrDefault();
            }
            
            
        }
        //simplificado: poderia utilizar reflexão e generics para converter os objetos
        public UF Converter(SqlDataReader sqlDataReader)
        {
            return new UF
            {
                ID = Convert.ToInt32(sqlDataReader["ID"]),
                Sigla = sqlDataReader["Sigla"].ToString()
            };
        }
    }
}

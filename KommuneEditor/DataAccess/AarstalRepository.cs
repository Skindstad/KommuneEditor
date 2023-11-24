﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using KommuneEditor.Model;

namespace KommuneEditor.DataAccess
{
    internal class AarstalRepository : Repository, IEnumerable<Aarstal>
    {
        private List<Aarstal> list = new List<Aarstal>();

        public IEnumerator<Aarstal> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Search(string year)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Aarstal WHERE Years LIKE @Year", connection);
                command.Parameters.Add(CreateParam("@Year", year + "%", SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read()) list.Add(new Aarstal(reader[0].ToString()));
                OnChanged(DbOperation.SELECT, DbModeltype.Aarstal);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Year repositiory: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        public void Add(string year)
        {
            string error = "";
            Aarstal aarstal = new Aarstal(year);
            if (aarstal.IsValid)
            {
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Zipcodes (Years) VALUES (@Year)", connection);
                    command.Parameters.Add(CreateParam("@Year", year, SqlDbType.NVarChar));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        list.Add(aarstal);
                        list.Sort();
                        OnChanged(DbOperation.INSERT, DbModeltype.Aarstal);
                        return;
                    }
                    error = string.Format("{0} could not be inserted in the database", year);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
            }
            else error = "Illegal value for year";
            throw new DbException("Error in year repositiory: " + error);
        }

        public void Update(string oldYear, string newYear)
        {
            string error = "";
            if (oldYear.Length > 0 && newYear.Length > 0)
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Aarstal SET Years = @NewYear WHERE Years = @OldYear", connection);
                    command.Parameters.Add(CreateParam("@OldYear", oldYear, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@NewYear", newYear, SqlDbType.NVarChar));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        for (int i = 0; i < list.Count; ++i)
                            if (list[i].Year.Equals(oldYear))
                            {
                                list[i].Year = newYear;
                                break;
                            }
                        OnChanged(DbOperation.UPDATE, DbModeltype.Aarstal);
                        return;
                    }
                    error = string.Format("Aarstal could not be update {0} => {1}", oldYear, newYear);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
            }
            else error = "Illegal value for city";
            throw new DbException("Error in Zipcode repositiory: " + error);
        }

        public void Remove(string year)
        {
            string error = "";
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Aarstal WHERE Years = @Year", connection);
                command.Parameters.Add(CreateParam("@Year", year, SqlDbType.NVarChar));
                connection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    list.Remove(new Aarstal(year));
                    OnChanged(DbOperation.DELETE, DbModeltype.Aarstal);
                    return;
                }
                error = string.Format("Zipcode {0} could not be deleted", year);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            throw new DbException("Error in Zipcode repositiory: " + error);
        }

        public static string GetYear(string year)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand command = new SqlCommand("SELECT Year FROM Aarstal WHERE Years = @Year", connection);
                SqlParameter param = new SqlParameter("@Year", SqlDbType.NVarChar);
                param.Value = year;
                command.Parameters.Add(param);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) return reader[0].ToString();
            }
            catch
            {
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            return "";
        }
    }
}
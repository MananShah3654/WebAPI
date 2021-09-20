///-----------------------------------------------------------------
///   Namespace:      <Class Namespace>
///   Class:          ClsSqlHelper
///   Description:    
///   Author:         Rajan Arora                    Date: 01-01-2018
///   Notes:          
///   Revision History:
///   Name:           Date:        Description:
///   Rajan Arora     09-07-2018   Fetch Connection String according to the Environment
///-----------------------------------------------------------------
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Web_API.Common
{
    public class ClsSqlHelper : IDisposable
    {
        #region Variables

        public string ErrorMessage;
        public string ErrorDescription;

        private SqlConnection _objSqlConnection;
        private SqlCommand _objSqlCommand;
        private SqlParameter _objSqlParameter;
        private SqlDataAdapter _objSqlDataAdapter;

        #endregion

        #region Methods

        public void Dispose()
        {
            if (_objSqlConnection != null)
            {
                _objSqlConnection.Dispose();
                _objSqlConnection = null;
            }
            if (_objSqlCommand != null)
            {
                _objSqlCommand.Dispose();
                _objSqlCommand = null;
            }
            if (_objSqlDataAdapter != null)
            {
                _objSqlDataAdapter.Dispose();
                _objSqlDataAdapter = null;
            }
        }

        public string GetConnectionString()
        {
            //string environment = ConfigurationManager.AppSettings["Environment"];

            //string connectionString = ConfigurationManager.ConnectionStrings[environment]
            //    .ConnectionString;
           //string connectionString = @"Data Source=192.168.1.9\Mantra2017;Initial Catalog=GSL;User ID=sa;Password=Mantra@123 providerName=System.Data.SqlClient";
             string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

            if (connectionString != null){
                return connectionString;
            }
            else
            {
                throw new NullReferenceException("Warning!! Getting the NULL ConnectionString");
            }
        }

        public int ExecuteNonQuery(object[] parameterObjects, string storedProcedureName)
        {
            try
            {
                using (_objSqlConnection = new SqlConnection(GetConnectionString()))
                {
                    _objSqlConnection.Open();

                    var parameterObjectsLength = parameterObjects.Length;

                    var storedProcedureparameterObjects = new object[parameterObjectsLength];
                    using (_objSqlCommand = new SqlCommand())
                    {
                        _objSqlCommand.CommandText = storedProcedureName;
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;
                        _objSqlCommand.Connection = _objSqlConnection;

                        SqlCommandBuilder.DeriveParameters(_objSqlCommand);

                        for (var i = 0; i < _objSqlCommand.Parameters.Count - 1; i++)
                        {
                            storedProcedureparameterObjects[i] = _objSqlCommand.Parameters[i + 1].ParameterName;
                        }

                        _objSqlCommand = new SqlCommand(storedProcedureName, _objSqlConnection);
                        for (var i = 0; i < parameterObjects.Length; i++)
                        {
                            _objSqlParameter = new SqlParameter
                            {
                                ParameterName = storedProcedureparameterObjects[i].ToString(),
                                Value = parameterObjects[i].ToString()
                            };
                            _objSqlCommand.Parameters.Add(_objSqlParameter);
                        }
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;


                        return _objSqlCommand.ExecuteNonQuery();
                    }
                }
            }
            //catch (Exception)
            //{

            //    throw;
            //}
            finally
            {
                Dispose();
            }
        }

        public long ExecuteScalar(object[] parameterObjects, string storedProcedureName)
        {
            try
            {
                using (_objSqlConnection = new SqlConnection(GetConnectionString()))
                {
                    _objSqlConnection.Open();
                    int parameterObjectsLength = parameterObjects.Length;

                    object[] storedProcedureparameterObjects = new object[parameterObjectsLength];
                    using (_objSqlCommand = new SqlCommand())
                    {
                        _objSqlCommand.CommandText = storedProcedureName;
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;
                        _objSqlCommand.Connection = _objSqlConnection;

                        SqlCommandBuilder.DeriveParameters(_objSqlCommand);

                        for (int i = 0; i < _objSqlCommand.Parameters.Count - 1; i++)
                        {
                            storedProcedureparameterObjects[i] = _objSqlCommand.Parameters[i + 1].ParameterName;
                        }

                        _objSqlCommand = new SqlCommand(storedProcedureName, _objSqlConnection);
                        for (int i = 0; i < parameterObjects.Length; i++)
                        {
                            _objSqlParameter = new SqlParameter
                            {
                                ParameterName = storedProcedureparameterObjects[i].ToString(),
                                Value = parameterObjects[i].ToString()
                            };
                            _objSqlCommand.Parameters.Add(_objSqlParameter);
                        }
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;
                        return Convert.ToInt64(_objSqlCommand.ExecuteScalar());
                    }
                }
            }
            //catch (Exception)
            //{

            //    throw;
            //}
            finally
            {
                Dispose();
            }
        }

        public DataSet GetDataSet(object[] parameterObjects, string storedProcedureName)
        {
            try
            {
                DataSet objDataSet = new DataSet();
                using (_objSqlConnection = new SqlConnection(GetConnectionString()))
                {
                    _objSqlConnection.Open();
                    int parameterObjectsLength = parameterObjects.Length;

                    object[] storedProcedureparameterObjects = new object[parameterObjectsLength];
                    using (_objSqlCommand = new SqlCommand())
                    {
                        _objSqlCommand.CommandText = storedProcedureName;
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;
                        _objSqlCommand.Connection = _objSqlConnection;

                        SqlCommandBuilder.DeriveParameters(_objSqlCommand);

                        for (int i = 0; i < _objSqlCommand.Parameters.Count - 1; i++)
                        {
                            storedProcedureparameterObjects[i] = _objSqlCommand.Parameters[i + 1].ParameterName;
                        }

                        _objSqlCommand = new SqlCommand(storedProcedureName, _objSqlConnection);
                        for (int i = 0; i < parameterObjects.Length; i++)
                        {
                            _objSqlParameter = new SqlParameter
                            {
                                ParameterName = storedProcedureparameterObjects[i].ToString(),
                                Value = parameterObjects[i].ToString()
                            };
                            _objSqlCommand.Parameters.Add(_objSqlParameter);
                        }
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;

                        using (_objSqlDataAdapter = new SqlDataAdapter(_objSqlCommand))
                        {
                            _objSqlDataAdapter.Fill(objDataSet);
                        }

                        return objDataSet;
                    }
                }
            }
            //catch (Exception)
            //{

            //    throw;
            //}
            finally
            {
                Dispose();
            }
        }

        public DataSet GetDataSetAddWithValue(object[] parameterObjects, string storedProcedureName)
        {
            try
            {
                DataSet objDataSet = new DataSet();
                using (_objSqlConnection = new SqlConnection(GetConnectionString()))
                {
                    _objSqlConnection.Open();
                    int parameterObjectsLength = parameterObjects.Length;

                    object[] storedProcedureparameterObjects = new object[parameterObjectsLength];
                    using (_objSqlCommand = new SqlCommand())
                    {
                        _objSqlCommand.CommandText = storedProcedureName;
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;
                        _objSqlCommand.Connection = _objSqlConnection;

                        SqlCommandBuilder.DeriveParameters(_objSqlCommand);

                        for (int i = 0; i < _objSqlCommand.Parameters.Count - 1; i++)
                        {
                            storedProcedureparameterObjects[i] = _objSqlCommand.Parameters[i + 1].ParameterName;
                        }

                        _objSqlCommand = new SqlCommand(storedProcedureName, _objSqlConnection);
                        for (var i = 0; i < parameterObjects.Length; i++)
                        {
                            _objSqlParameter = new SqlParameter();

                            //_objSqlParameter.ParameterName = storedProcedureparameterObjects[i].ToString();
                            //_objSqlParameter.Value = parameterObjects[i].ToString();
                            //_objSqlCommand.Parameters.AddWithValue(_objSqlParameter);                          

                            _objSqlCommand.Parameters.AddWithValue(storedProcedureparameterObjects[i].ToString(),
                                parameterObjects[i]);
                        }
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;

                        using (_objSqlDataAdapter = new SqlDataAdapter(_objSqlCommand))
                        {
                            _objSqlDataAdapter.Fill(objDataSet);
                        }

                        return objDataSet;
                    }
                }
            }
            //catch (Exception)
            //{

            //    throw;
            //}
            finally
            {
                Dispose();
            }
        }

        public int ExecuteNonQueryAddWithValue(object[] parameterObjects, string storedProcedureName)
        {
            try
            {
                using (_objSqlConnection = new SqlConnection(GetConnectionString()))
                {
                    _objSqlConnection.Open();

                    var parameterObjectsLength = parameterObjects.Length;

                    var storedProcedureparameterObjects = new object[parameterObjectsLength];
                    using (_objSqlCommand = new SqlCommand())
                    {
                        _objSqlCommand.CommandText = storedProcedureName;
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;
                        _objSqlCommand.Connection = _objSqlConnection;

                        SqlCommandBuilder.DeriveParameters(_objSqlCommand);

                        for (var i = 0; i < _objSqlCommand.Parameters.Count - 1; i++)
                        {
                            storedProcedureparameterObjects[i] = _objSqlCommand.Parameters[i + 1].ParameterName;
                        }

                        _objSqlCommand = new SqlCommand(storedProcedureName, _objSqlConnection);
                        for (var i = 0; i < parameterObjects.Length; i++)
                        {
                            _objSqlParameter = new SqlParameter();

                            //_objSqlParameter.ParameterName = storedProcedureparameterObjects[i].ToString();
                            //_objSqlParameter.Value = parameterObjects[i].ToString();
                            //_objSqlCommand.Parameters.AddWithValue(_objSqlParameter);                          

                            _objSqlCommand.Parameters.AddWithValue(storedProcedureparameterObjects[i].ToString(),
                                parameterObjects[i]);
                        }
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;


                        return _objSqlCommand.ExecuteNonQuery();
                    }
                }
            }
            //catch (Exception)
            //{

            //    throw;
            //}
            finally
            {
                Dispose();
            }
        }

        public long ExecuteScalerAddWithValue(object[] parameterObjects, string storedProcedureName)
        {
            try
            {
                using (_objSqlConnection = new SqlConnection(GetConnectionString()))
                {
                    _objSqlConnection.Open();

                    var parameterObjectsLength = parameterObjects.Length;

                    var storedProcedureparameterObjects = new object[parameterObjectsLength];
                    using (_objSqlCommand = new SqlCommand())
                    {
                        _objSqlCommand.CommandText = storedProcedureName;
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;
                        _objSqlCommand.Connection = _objSqlConnection;

                        SqlCommandBuilder.DeriveParameters(_objSqlCommand);

                        for (var i = 0; i < _objSqlCommand.Parameters.Count - 1; i++)
                        {
                            storedProcedureparameterObjects[i] = _objSqlCommand.Parameters[i + 1].ParameterName;
                        }

                        _objSqlCommand = new SqlCommand(storedProcedureName, _objSqlConnection);
                        for (var i = 0; i < parameterObjects.Length; i++)
                        {
                            _objSqlParameter = new SqlParameter();

                            //_objSqlParameter.ParameterName = storedProcedureparameterObjects[i].ToString();
                            //_objSqlParameter.Value = parameterObjects[i].ToString();
                            //_objSqlCommand.Parameters.Add(_objSqlParameter);

                            _objSqlCommand.Parameters.AddWithValue(storedProcedureparameterObjects[i].ToString(),
                                parameterObjects[i]);
                        }
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;


                        return Convert.ToInt64(_objSqlCommand.ExecuteScalar());
                    }
                }
            }
            //catch (Exception)
            //{

            //    throw;
            //}
            finally
            {
                Dispose();
            }
        }

        public string ExecuteNonQueryWithRetValStr(object[] parameterObjects, string storedProcedureName)
        {
            try
            {
                using (_objSqlConnection = new SqlConnection(GetConnectionString()))
                {
                    //if (objSqlConnection.State == ConnectionState.Closed) OpenConnection(objSqlConnection);
                    _objSqlConnection.Open();

                    int parameterObjectsLength = parameterObjects.Length;

                    object[] storedProcedureparameterObjects = new object[parameterObjectsLength];
                    using (_objSqlCommand = new SqlCommand())
                    {
                        _objSqlCommand.CommandText = storedProcedureName;
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;
                        _objSqlCommand.Connection = _objSqlConnection;

                        SqlCommandBuilder.DeriveParameters(_objSqlCommand);

                        for (int i = 0; i < _objSqlCommand.Parameters.Count - 3; i++)
                        {
                            storedProcedureparameterObjects[i] = _objSqlCommand.Parameters[i + 1].ParameterName;
                        }

                        _objSqlCommand = new SqlCommand(storedProcedureName, _objSqlConnection);
                        for (int i = 0; i < parameterObjects.Length; i++)
                        {
                            _objSqlParameter = new SqlParameter();
                            _objSqlParameter.ParameterName = storedProcedureparameterObjects[i].ToString();
                            _objSqlParameter.Value = parameterObjects[i];
                            _objSqlCommand.Parameters.Add(_objSqlParameter);

                        }
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;
                        //SqlParameter returnParameter = _objSqlCommand.Parameters.Add("@RetVal", SqlDbType.Int);
                        //returnParameter.Direction = ParameterDirection.ReturnValue;

                        SqlParameter p1 = new SqlParameter("@RetVal", SqlDbType.Int);
                        p1.Direction = ParameterDirection.Output;
                        _objSqlCommand.Parameters.Add(p1);

                        //_objSqlCommand.CommandType = CommandType.StoredProcedure;
                        //SqlParameter returnParameter1 = _objSqlCommand.Parameters.Add("@RetValEmpID", SqlDbType.Text);
                        //returnParameter1.Direction = ParameterDirection.ReturnValue;

                        SqlParameter p2 = new SqlParameter("@RetValEmpID", SqlDbType.VarChar, 100);
                        p2.Direction = ParameterDirection.Output;
                        _objSqlCommand.Parameters.Add(p2);

                        int m = _objSqlCommand.ExecuteNonQuery();
                        String retid = (String)p2.Value;
                        return retid;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public int ExecuteNonQueryWithRetVal(object[] parameterObjects, string storedProcedureName)
        {
            try
            {
                using (_objSqlConnection = new SqlConnection(GetConnectionString()))
                {
                    //if (objSqlConnection.State == ConnectionState.Closed) OpenConnection(objSqlConnection);
                    _objSqlConnection.Open();

                    int parameterObjectsLength = parameterObjects.Length;

                    object[] storedProcedureparameterObjects = new object[parameterObjectsLength];
                    using (_objSqlCommand = new SqlCommand())
                    {
                        _objSqlCommand.CommandText = storedProcedureName;
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;
                        _objSqlCommand.Connection = _objSqlConnection;

                        SqlCommandBuilder.DeriveParameters(_objSqlCommand);

                        for (int i = 0; i < _objSqlCommand.Parameters.Count - 1; i++)
                        {
                            storedProcedureparameterObjects[i] = _objSqlCommand.Parameters[i + 1].ParameterName;
                        }

                        _objSqlCommand = new SqlCommand(storedProcedureName, _objSqlConnection);
                        for (int i = 0; i < parameterObjects.Length; i++)
                        {
                            _objSqlParameter = new SqlParameter();
                            _objSqlParameter.ParameterName = storedProcedureparameterObjects[i].ToString();
                            _objSqlParameter.Value = parameterObjects[i];
                            _objSqlCommand.Parameters.Add(_objSqlParameter);

                        }
                        _objSqlCommand.CommandType = CommandType.StoredProcedure;
                        SqlParameter returnParameter = _objSqlCommand.Parameters.Add("RetVal", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        Int32 m = _objSqlCommand.ExecuteNonQuery();
                        Int32 retid = (Int32)returnParameter.Value;
                        return retid;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
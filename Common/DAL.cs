using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace Web_API.Common
{
	public class DAL
	{
		public string errorMessage;
		public string errorDescription;
		SqlConnection con = new SqlConnection();
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;

		public bool OpenConnection(SqlConnection Conn)
		{
			//String connStr = System.Configuration.ConfigurationManager.AppSettings["SqlConnStr"];
            String connStr = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
			errorMessage = "";
			errorDescription = "";
			if (Conn == null)
			{
				errorMessage = "Connection Object doesn't exists. Internal Error";
				return false;
			}
			if (Conn.State != ConnectionState.Open)
			{
				try
				{
					Conn.ConnectionString = connStr;
					Conn.Open();
					return true;
				}
				catch (Exception objE)
				{
					throw;
					//errorMessage = "<font Color=RED>Exception Occured :</Font>  Message= " + objE.Message.ToString() + ". Method= " + objE.TargetSite.Name.ToString();
					//errorDescription = " StackTrace : " + objE.StackTrace.ToString() + " Source = " + objE.Source.ToString();
					//return false;
				}
			}
			else
			{
				return true;
			}
		}
		public bool CloseConnection(SqlConnection Conn)
		{
			errorMessage = "";
			errorDescription = "";

			if (Conn.State == ConnectionState.Closed)
			{
				return true;
			}
			else
			{
				try
				{
					Conn.Close();
					return true;
				}
				catch (SqlException objE)
				{
					throw;
					//errorMessage = "<font Color=RED>Exception Occured :</Font>  Message= " + objE.Message.ToString() + ". Method= " + objE.TargetSite.Name.ToString();
					//errorDescription = " StackTrace : " + objE.StackTrace.ToString() + " Source = " + objE.Source.ToString();
					//return false;
				}
			}
		}
        private SqlConnection objSqlConnection;
        private SqlCommand objSqlCommand;
        private SqlParameter objSqlParameter;


        public Int32 SP_ExecuteNonQueryRetVal(object[] parameterObjects, string storedProcedureName)
        {
            try
            {
                using (objSqlConnection = new SqlConnection(connectionString))
                {
                    //if (objSqlConnection.State == ConnectionState.Closed) OpenConnection(objSqlConnection);
                    objSqlConnection.Open();

                    int parameterObjectsLength = parameterObjects.Length;

                    object[] storedProcedureparameterObjects = new object[parameterObjectsLength];
                    using (objSqlCommand = new SqlCommand())
                    {
                        objSqlCommand.CommandText = storedProcedureName;
                        objSqlCommand.CommandType = CommandType.StoredProcedure;
                        objSqlCommand.Connection = objSqlConnection;

                        SqlCommandBuilder.DeriveParameters(objSqlCommand);

                        for (int i = 0; i < objSqlCommand.Parameters.Count - 2; i++)
                        {
                            //storedProcedureparameterObjects[i] = objSqlCommand.Parameters[i + 1].ParameterName;
                            storedProcedureparameterObjects[i] = objSqlCommand.Parameters[i + 1].ParameterName;
                        }

                        objSqlCommand = new SqlCommand(storedProcedureName, objSqlConnection);
                        for (int i = 0; i < parameterObjects.Length; i++)
                        {
                            objSqlParameter = new SqlParameter();
                            objSqlParameter.ParameterName = storedProcedureparameterObjects[i].ToString();
                            objSqlParameter.Value = parameterObjects[i];
                            objSqlCommand.Parameters.Add(objSqlParameter);

                        }
                        objSqlCommand.CommandType = CommandType.StoredProcedure;
                        SqlParameter returnParameter = objSqlCommand.Parameters.Add("RetVal", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.Output;
                        Int32 m = objSqlCommand.ExecuteNonQuery();
                        Int32 retid = (Int32)returnParameter.Value;
                        return retid;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("String or binary data would be truncated"))
                {
                    throw new Exception("Data would be truncated. please enter proper value");
                }
                throw ex;
            }
        }

		public bool ExecuteStoredProcedureCM(string storedProc, string[,] Params)
		{
			DataSet dsList = new DataSet();
			SqlCommand dbCommand = new SqlCommand();
			SqlTransaction tran;
			Int32 rowsAffected;

			if (con.State != ConnectionState.Open) OpenConnection(con);

			dbCommand.Connection = con;
			dbCommand.CommandType = CommandType.StoredProcedure;
			dbCommand.CommandText = storedProc;
			for (int p = 0; p < (Params.Length) / 2; p++)
			{
				SqlParameter sqlParam = dbCommand.Parameters.AddWithValue(Params[p, 0], Params[p, 1]);
			}

			tran = con.BeginTransaction();
			dbCommand.Transaction = tran;
			try
			{
				rowsAffected = dbCommand.ExecuteNonQuery();
				if (rowsAffected > 0)
				{
					tran.Commit();
					return true;
				}
				else
				{
					tran.Rollback();
					return false;
				}
			}
			catch (Exception objE)
			{
				//if (objE.Message.Contains("Violation of PRIMARY KEY constraint"))
				//{
				//	return true;
				//}
				tran.Rollback();
				throw;
				//LogManager.WriteErrorLog(storedProc + " :- " + objE.Message.ToString());
				//LogManager.WriteErrorLog(storedProc + " :- " + objE.InnerException.ToString());
				//errorMessage = "<B><font Color=RED>Exception Occured :</Font></B>  Message= " + objE.Message.ToString() + ". Method= " + objE.TargetSite.Name.ToString();
				//errorDescription = " <B>StackTrace :</B> " + objE.StackTrace.ToString() + " Source = " + objE.Source.ToString();

				//return false;
			}
			finally
			{
				if (con.State == ConnectionState.Open) CloseConnection(con);
			}


		}

		public DataSet ExecuteStoredProcedureDS(string StoredProceduredName, string tableName)
		{
			errorMessage = "";
			errorDescription = "";

			DataSet ds = new DataSet();
			//Boolean blnState= OpenConnection(Conn);
			//if (blnState == false)
			//    return null ;
			try
			{
				if (con.State == ConnectionState.Closed) OpenConnection(con);
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = con;
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.CommandText = StoredProceduredName;
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds, tableName);
				return ds;
			}
			catch (Exception objE)
			{
				throw;
				//errorMessage = "<font Color=RED>Exception Occured :</Font>  Message= " + objE.Message.ToString() + ". Method= " + objE.TargetSite.Name.ToString();
				//errorDescription = " StackTrace : " + objE.StackTrace.ToString() + " Source = " + objE.Source.ToString();
				//ds = null;
				//return ds;
			}
			finally
			{
				if (con.State == ConnectionState.Open) CloseConnection(con);
			}
		}

		public DataSet ExecuteStoredProcedureDS(string StoredProceduredName, string[,] Params)
		{
			//LogManager.WriteErrorLog("ds");
			DataSet dsList = new DataSet();
			SqlCommand dbCommand = new SqlCommand();
			//LogManager.WriteErrorLog("ds mm");
			if (con.State == ConnectionState.Closed) OpenConnection(con);
			dbCommand.Connection = con;
			dbCommand.CommandTimeout = 600;
			dbCommand.CommandType = CommandType.StoredProcedure;
			dbCommand.CommandText = StoredProceduredName;
			//LogManager.WriteErrorLog("ds 1");
			for (int p = 0; p < (Params.Length) / 2; p++)
			{
				SqlParameter sqlParam = dbCommand.Parameters.AddWithValue(Params[p, 0], Params[p, 1]);
			}
			//LogManager.WriteErrorLog("ds 2");
			try
			{

				SqlDataAdapter da = new SqlDataAdapter(dbCommand);
				//LogManager.WriteErrorLog("ds 3");
				da.Fill(dsList);
				return dsList;
			}

			catch (Exception objE)
			{
				//LogManager.WriteErrorLog("SayHello_CNT 3" + objE.Message);
				//throw new Exception(ex.Message);
				throw;
				//errorMessage = "<font Color=RED>Exception Occured :</Font>  Message= " + objE.Message.ToString() + ". Method= " + objE.TargetSite.Name.ToString();
				//errorDescription = " StackTrace : " + objE.StackTrace.ToString() + " Source = " + objE.Source.ToString();
				//return null;
			}
			finally
			{
				if (con.State == ConnectionState.Open) CloseConnection(con);
			}
		}

		public string SelectScalar(string storedProc, string[,] Params)
		{
			string v = "0";
			SqlCommand dbCommand = new SqlCommand();
			SqlTransaction tran;
			if (con.State == ConnectionState.Closed) OpenConnection(con);
			dbCommand.Connection = con;
			dbCommand.CommandType = CommandType.StoredProcedure;
			dbCommand.CommandText = storedProc;
			for (int p = 0; p < (Params.Length) / 2; p++)
			{
				SqlParameter sqlParam = dbCommand.Parameters.AddWithValue(Params[p, 0], Params[p, 1]);
			}

			tran = con.BeginTransaction();
			dbCommand.Transaction = tran;
			try
			{
				v = Convert.ToString(dbCommand.ExecuteScalar());
				if (v != "0")
				{
					tran.Commit();
				}
				else
				{
					tran.Rollback();
				}
			}
			catch (Exception objE)
			{
				tran.Rollback();
				throw;
				//errorMessage = "<B><font Color=RED>Exception Occured :</Font></B>  Message= " + objE.Message.ToString() + ". Method= " + objE.TargetSite.Name.ToString();
				//errorDescription = " <B>StackTrace :</B> " + objE.StackTrace.ToString() + " Source = " + objE.Source.ToString();
			}
			finally
			{
				if (con.State == ConnectionState.Open) CloseConnection(con);
			}

			return v;
		}

		public int ExecuteStoredProcedureCMRetVal(string storedProc, string[,] Params)
		{
			DataSet dsList = new DataSet();
			SqlCommand dbCommand = new SqlCommand();
			SqlTransaction tran;
			Int32 rowsAffected;
			if (con.State == ConnectionState.Closed) OpenConnection(con);
			dbCommand.Connection = con;
			dbCommand.CommandType = CommandType.StoredProcedure;
			dbCommand.CommandText = storedProc;
			for (int p = 0; p < (Params.Length) / 2; p++)
			{
				SqlParameter sqlParam = dbCommand.Parameters.AddWithValue(Params[p, 0], Params[p, 1]);
			}

			tran = con.BeginTransaction();
			dbCommand.Transaction = tran;
			try
			{
				SqlParameter returnParameter = dbCommand.Parameters.Add("RetVal", SqlDbType.Int);
				returnParameter.Direction = ParameterDirection.Output;
				rowsAffected = dbCommand.ExecuteNonQuery();
				int retid = (int)returnParameter.Value;
				if (retid > 0)
				{
					tran.Commit();
					return retid;
				}
				else
				{
					tran.Rollback();
					return retid;
				}
			}
			catch (Exception objE)
			{
				tran.Rollback();
				throw;
				//if (objE.ToString().Contains("Cannot insert duplicate key in object 'dbo.TblBkpDmpTerminalData'"))
				//{
				//	errorMessage = "Success";
				//	errorDescription = "Success";
				//	return 1;
				//}
				//else
				//{
				//	errorMessage = "<B><font Color=RED>Exception Occured :</Font></B>  Message= " + objE.Message.ToString() + ". Method= " + objE.TargetSite.Name.ToString();
				//	errorDescription = " <B>StackTrace :</B> " + objE.StackTrace.ToString() + " Source = " + objE.Source.ToString();
				//	return 0;
				//}
			}
			finally
			{
				if (con.State == ConnectionState.Open) CloseConnection(con);
			}


		}


        //public int ExecuteStoredProcedureClassRetVal(string storedProc, ClsReqUploadTransaction objReq)
        //{
        //    DataSet dsList = new DataSet();

        //    SqlTransaction tran;
        //    Int32 rowsAffected;
        //    if (con.State == ConnectionState.Closed) OpenConnection(con);
        //    tran = con.BeginTransaction();
        //    try
        //    {
        //        if (objReq.UploadTransactionTimes != null)
        //        {
        //            if (objReq.UploadTransactionTimes.Length > 0)
        //            {
        //                for (int i = 0; i < objReq.UploadTransactionTimes.Length; i++)
        //                {
        //                    SqlCommand dbCommand = new SqlCommand();
        //                    dbCommand.Connection = con;
        //                    dbCommand.CommandType = CommandType.StoredProcedure;
        //                    dbCommand.CommandText = storedProc;
        //                    dbCommand.Transaction = tran;

        //                    SqlParameter sqlParam = new SqlParameter();
        //                    SqlParameter returnParameter = new SqlParameter();
        //                    returnParameter = dbCommand.Parameters.Add("RetVal", SqlDbType.Int);
        //                    returnParameter.Direction = ParameterDirection.Output;

        //                    string[,] Params = new string[,]
        //                        {
        //                             {"@CardNo",Convert.ToString(objReq.CardNo)},
        //                             {"@IMEI",Convert.ToString(objReq.IMEI)},
        //                             {"@EmpID",Convert.ToString(objReq.EmpID)},
        //                             {"@In_Out_time",Convert.ToString(objReq.In_Out_time)},
        //                             {"@TokenNo",Convert.ToString(objReq.TokenNo)},
        //                             {"@MealType",Convert.ToString(objReq.MealType)},
        //                             {"@CardType",Convert.ToString(objReq.CardType)},
        //                             {"@CanteenCode",Convert.ToString(objReq.CanteenCode)},
        //                             {"@OrderID",Convert.ToString(objReq.OrderID)},
        //                             {"@ItemCode",Convert.ToString(objReq.UploadTransactionTimes[i].ItemCode)},
        //                             {"@ItemPrice",Convert.ToString(objReq.UploadTransactionTimes[i].ItemPrice)},
        //                             {"@Quantity",Convert.ToString(objReq.UploadTransactionTimes[i].Quantity)},
        //                             {"@TotalPrice",Convert.ToString(objReq.UploadTransactionTimes[i].TotalPrice)},                   
        //                             {"@ItemDiscount",Convert.ToString(objReq.UploadTransactionTimes[i].ItemDiscount)},
        //                            {"@ItemTax",Convert.ToString(objReq.UploadTransactionTimes[i].ItemTax)},				 
        //                             {"@GrandTotalPrice",Convert.ToString(objReq.GrandTotalPrice)},                   
        //                             {"@EntryBy",Convert.ToString(objReq.EntryBy)},                    
        //                             {"@AuthType",Convert.ToString(objReq.AuthType)},      
        //                             {"@IsForGuest",Convert.ToString(objReq.IsForGuest)},
        //                             {"@GuestName",Convert.ToString(objReq.GuestName)},
        //                             {"@TotalDiscount",Convert.ToString(objReq.TotalDiscount)},
        //                            {"@TotalTax",Convert.ToString(objReq.TotalTax)},
        //                        };
        //                    for (int p = 0; p < (Params.Length) / 2; p++)
        //                    {
        //                        sqlParam = dbCommand.Parameters.AddWithValue(Params[p, 0], Params[p, 1]);
        //                    }


        //                    try
        //                    {

        //                        rowsAffected = dbCommand.ExecuteNonQuery();
        //                        int retid = (int)returnParameter.Value;
        //                        if (retid > 0)
        //                        {
        //                            //tran.Commit();
        //                            //return retid;
        //                        }
        //                        else
        //                        {
        //                            tran.Rollback();
        //                            return retid;
        //                        }
        //                    }
        //                    catch (Exception objE1)
        //                    {
        //                        throw;
        //                        //if (objE1.ToString().Contains("Cannot insert duplicate key in object 'dbo.TblBkpDmpTerminalData'"))
        //                        //{

        //                        //}
        //                        //else
        //                        //{
        //                        //	tran.Rollback();
        //                        //	errorMessage = "<B><font Color=RED>Exception Occured :</Font></B>  Message= " + objE1.Message.ToString() + ". Method= " + objE1.TargetSite.Name.ToString();
        //                        //	errorDescription = " <B>StackTrace :</B> " + objE1.StackTrace.ToString() + " Source = " + objE1.Source.ToString();
        //                        //	return 0;
        //                        //}
        //                    }
        //                }
        //                tran.Commit();
        //                return 1;
        //            }
        //            else
        //            {
        //                tran.Rollback();
        //                return 0;
        //            }
        //        }
        //        else
        //        {
        //            tran.Rollback();
        //            return 0;
        //        }

        //    }

        //    catch (Exception objE)
        //    {
        //        ////tran.Rollback();
        //        throw;
        //        if (objE.ToString().Contains("Cannot insert duplicate key in object 'dbo.TblBkpDmpTerminalData'"))
        //        {
        //            errorMessage = "Success";
        //            errorDescription = "Success";
        //            return 1;
        //        }
        //        else
        //        {
        //            tran.Rollback();
        //            errorMessage = "<B><font Color=RED>Exception Occured :</Font></B>  Message= " + objE.Message.ToString() + ". Method= " + objE.TargetSite.Name.ToString();
        //            errorDescription = " <B>StackTrace :</B> " + objE.StackTrace.ToString() + " Source = " + objE.Source.ToString();
        //            return 0;
        //        }
        //    }
        //    finally
        //    {
        //        if (con.State == ConnectionState.Open) CloseConnection(con);
        //    }


        //}

		public int ExecuteNonQueryAddWithValue(object[] parameterObjects, string storedProcedureName)
		{
			try
			{
				//String connStr = System.Configuration.ConfigurationManager.AppSettings["SqlConnStr"];
				String connStr = ConfigurationManager.ConnectionStrings["SqlConnStr"].ConnectionString;
				SqlCommand _objSqlCommand = new SqlCommand();
				SqlParameter _objSqlParameter = new SqlParameter();

				using (con = new SqlConnection(connStr))
				{
					con.Open();

					var parameterObjectsLength = parameterObjects.Length;

					var storedProcedureparameterObjects = new object[parameterObjectsLength];
					var storedProcedureparameterObjectsDataType = new object[parameterObjectsLength];
					using (_objSqlCommand = new SqlCommand())
					{
						_objSqlCommand.CommandText = storedProcedureName;
						_objSqlCommand.CommandType = CommandType.StoredProcedure;
						_objSqlCommand.Connection = con;

						SqlCommandBuilder.DeriveParameters(_objSqlCommand);

						for (var i = 0; i < _objSqlCommand.Parameters.Count - 2; i++)
						{
							storedProcedureparameterObjects[i] = _objSqlCommand.Parameters[i + 1].ParameterName;
							storedProcedureparameterObjectsDataType[i] = _objSqlCommand.Parameters[i + 1].SqlDbType;
						}

						_objSqlCommand = new SqlCommand(storedProcedureName, con);
						for (var i = 0; i < parameterObjects.Length; i++)
						{
							_objSqlParameter = new SqlParameter();

							//_objSqlParameter.ParameterName = storedProcedureparameterObjects[i].ToString();
							//_objSqlParameter.Value = parameterObjects[i].ToString();
							//_objSqlCommand.Parameters.AddWithValue(_objSqlParameter);                          
							if (parameterObjects[i] == null)
							{
								SqlParameter imageParameter = new SqlParameter();
								imageParameter.ParameterName = storedProcedureparameterObjects[i].ToString();
								imageParameter.SqlDbType = (SqlDbType)storedProcedureparameterObjectsDataType[i];
								imageParameter.Value = DBNull.Value;
								_objSqlCommand.Parameters.Add(imageParameter);
							}
							else
							{
								_objSqlCommand.Parameters.AddWithValue(storedProcedureparameterObjects[i].ToString(),
								    parameterObjects[i]);
							}

						}
						_objSqlCommand.CommandType = CommandType.StoredProcedure;

						SqlParameter returnParameter = _objSqlCommand.Parameters.Add("RetVal", SqlDbType.Int);
						returnParameter.Direction = ParameterDirection.Output;

						_objSqlCommand.ExecuteNonQuery();
						int retid = (int)returnParameter.Value;
						return retid;
					}
				}
			}
			catch (Exception objE)
			{
				throw;
				//errorMessage = "<B><font Color=RED>Exception Occured :</Font></B>  Message= " + objE.Message.ToString() + ". Method= " + objE.TargetSite.Name.ToString();
				//errorDescription = " <B>StackTrace :</B> " + objE.StackTrace.ToString() + " Source = " + objE.Source.ToString();
				//return 0;
			}
			finally
			{
				if (con.State == ConnectionState.Open) CloseConnection(con);
			}
		}

		public int ExecuteNonQueryAddWithValueDblParameter(object[] parameterObjects, string storedProcedureName, out string EmpID)
		{
			try
			{
				//String connStr = System.Configuration.ConfigurationManager.AppSettings["SqlConnStr"];
				String connStr = ConfigurationManager.ConnectionStrings["SqlConnStr"].ConnectionString;
				SqlCommand _objSqlCommand = new SqlCommand();
				SqlParameter _objSqlParameter = new SqlParameter();

				using (con = new SqlConnection(connStr))
				{
					con.Open();

					var parameterObjectsLength = parameterObjects.Length;

					var storedProcedureparameterObjects = new object[parameterObjectsLength];
					var storedProcedureparameterObjectsDataType = new object[parameterObjectsLength];
					using (_objSqlCommand = new SqlCommand())
					{
						_objSqlCommand.CommandText = storedProcedureName;
						_objSqlCommand.CommandType = CommandType.StoredProcedure;
						_objSqlCommand.Connection = con;

						SqlCommandBuilder.DeriveParameters(_objSqlCommand);

						for (var i = 0; i < _objSqlCommand.Parameters.Count - 3; i++)
						{
							storedProcedureparameterObjects[i] = _objSqlCommand.Parameters[i + 1].ParameterName;
							storedProcedureparameterObjectsDataType[i] = _objSqlCommand.Parameters[i + 1].SqlDbType;
						}

						_objSqlCommand = new SqlCommand(storedProcedureName, con);
						for (var i = 0; i < parameterObjects.Length; i++)
						{
							_objSqlParameter = new SqlParameter();

							//_objSqlParameter.ParameterName = storedProcedureparameterObjects[i].ToString();
							//_objSqlParameter.Value = parameterObjects[i].ToString();
							//_objSqlCommand.Parameters.AddWithValue(_objSqlParameter);                          
							if (parameterObjects[i] == null)
							{
								SqlParameter imageParameter = new SqlParameter();
								imageParameter.ParameterName = storedProcedureparameterObjects[i].ToString();
								imageParameter.SqlDbType = (SqlDbType)storedProcedureparameterObjectsDataType[i];
								imageParameter.Value = DBNull.Value;
								_objSqlCommand.Parameters.Add(imageParameter);
							}
							else
							{
								_objSqlCommand.Parameters.AddWithValue(storedProcedureparameterObjects[i].ToString(),
								    parameterObjects[i]);
							}

						}
						_objSqlCommand.CommandType = CommandType.StoredProcedure;

						SqlParameter returnParameter = _objSqlCommand.Parameters.Add("RetVal", SqlDbType.Int);
						returnParameter.Direction = ParameterDirection.Output;

						SqlParameter returnParameter1 = _objSqlCommand.Parameters.Add("retValEmpID", SqlDbType.VarChar, 50);
						returnParameter1.Direction = ParameterDirection.Output;

						_objSqlCommand.ExecuteNonQuery();
						int retid = (int)returnParameter.Value;
						EmpID = (string)returnParameter1.Value;

						return retid;
					}
				}
			}
			catch (Exception objE)
			{
				throw;
				//errorMessage = "<B><font Color=RED>Exception Occured :</Font></B>  Message= " + objE.Message.ToString() + ". Method= " + objE.TargetSite.Name.ToString();
				//errorDescription = " <B>StackTrace :</B> " + objE.StackTrace.ToString() + " Source = " + objE.Source.ToString();
				//return 0;
			}
			finally
			{
				if (con.State == ConnectionState.Open) CloseConnection(con);
			}
		}

		public int ExecuteNonQueryAddWithValue1(object[] parameterObjects, string storedProcedureName)
		{
			try
			{
				//	String connStr = System.Configuration.ConfigurationManager.AppSettings["SqlConnStr"];
				String connStr = ConfigurationManager.ConnectionStrings["SqlConnStr"].ConnectionString;
				SqlCommand _objSqlCommand = new SqlCommand();
				SqlParameter _objSqlParameter = new SqlParameter();

				using (con = new SqlConnection(connStr))
				{
					con.Open();

					var parameterObjectsLength = parameterObjects.Length;

					var storedProcedureparameterObjects = new object[parameterObjectsLength];
					var storedProcedureparameterObjectsDataType = new object[parameterObjectsLength];
					using (_objSqlCommand = new SqlCommand())
					{
						_objSqlCommand.CommandText = storedProcedureName;
						_objSqlCommand.CommandType = CommandType.StoredProcedure;
						_objSqlCommand.Connection = con;

						SqlCommandBuilder.DeriveParameters(_objSqlCommand);

						for (var i = 0; i < _objSqlCommand.Parameters.Count - 1; i++)
						{
							storedProcedureparameterObjects[i] = _objSqlCommand.Parameters[i + 1].ParameterName;
							storedProcedureparameterObjectsDataType[i] = _objSqlCommand.Parameters[i + 1].SqlDbType;
						}

						_objSqlCommand = new SqlCommand(storedProcedureName, con);
						for (var i = 0; i < parameterObjects.Length; i++)
						{
							_objSqlParameter = new SqlParameter();

							//_objSqlParameter.ParameterName = storedProcedureparameterObjects[i].ToString();
							//_objSqlParameter.Value = parameterObjects[i].ToString();
							//_objSqlCommand.Parameters.AddWithValue(_objSqlParameter);                          
							if (parameterObjects[i] == null)
							{
								SqlParameter imageParameter = new SqlParameter();
								imageParameter.ParameterName = storedProcedureparameterObjects[i].ToString();
								imageParameter.SqlDbType = (SqlDbType)storedProcedureparameterObjectsDataType[i];
								imageParameter.Value = DBNull.Value;
								_objSqlCommand.Parameters.Add(imageParameter);
							}
							else
							{
								_objSqlCommand.Parameters.AddWithValue(storedProcedureparameterObjects[i].ToString(),
								    parameterObjects[i]);
							}

						}
						_objSqlCommand.CommandType = CommandType.StoredProcedure;

						//SqlParameter returnParameter = _objSqlCommand.Parameters.Add("RetVal", SqlDbType.Int);
						//returnParameter.Direction = ParameterDirection.Output;

						int retid = _objSqlCommand.ExecuteNonQuery();
						//	int retid = (int)returnParameter.Value;
						return retid;
					}
				}
			}
			catch (Exception objE)
			{
				throw;
				//errorMessage = "<B><font Color=RED>Exception Occured :</Font></B>  Message= " + objE.Message.ToString() + ". Method= " + objE.TargetSite.Name.ToString();
				//errorDescription = " <B>StackTrace :</B> " + objE.StackTrace.ToString() + " Source = " + objE.Source.ToString();
				//return 0;
			}
			finally
			{
				if (con.State == ConnectionState.Open) CloseConnection(con);
			}
		}

		public string ExecuteStoredProcedureCMRetValString(string storedProc, string[,] Params)
		{
			DataSet dsList = new DataSet();
			SqlCommand dbCommand = new SqlCommand();
			SqlTransaction tran;
			Int32 rowsAffected;
			if (con.State == ConnectionState.Closed) OpenConnection(con);
			dbCommand.Connection = con;
			dbCommand.CommandType = CommandType.StoredProcedure;
			dbCommand.CommandText = storedProc;
			for (int p = 0; p < (Params.Length) / 2; p++)
			{
				SqlParameter sqlParam = dbCommand.Parameters.AddWithValue(Params[p, 0], Params[p, 1]);
			}

			tran = con.BeginTransaction();
			dbCommand.Transaction = tran;
			try
			{
				SqlParameter returnParameter = dbCommand.Parameters.Add("@RetVal", SqlDbType.Text);
				returnParameter.Direction = ParameterDirection.ReturnValue;
				rowsAffected = dbCommand.ExecuteNonQuery();
				string retid = (string)returnParameter.Value;
				if (rowsAffected > 0)
				{
					tran.Commit();
					return retid;
				}
				else
				{
					tran.Rollback();
					return retid;
				}
			}
			catch (Exception objE)
			{
				tran.Rollback();
				throw;
				//errorMessage = "<B><font Color=RED>Exception Occured :</Font></B>  Message= " + objE.Message.ToString() + ". Method= " + objE.TargetSite.Name.ToString();
				//errorDescription = " <B>StackTrace :</B> " + objE.StackTrace.ToString() + " Source = " + objE.Source.ToString();
				//return "0";
			}
			finally
			{
				if (con.State == ConnectionState.Open) CloseConnection(con);
			}


		}

        //public int ExecuteStoredProcedureCurrentStockClassRetVal(string storedProc, ClsReqCurrentItemQuantity objReq, ref ClsResCurrentItemQuantity objRes)
        //{
        //    DataSet dsList = new DataSet();

        //    SqlTransaction tran;
        //    Int32 rowsAffected;
        //    if (con.State == ConnectionState.Closed) OpenConnection(con);


        //    tran = con.BeginTransaction();


        //    try
        //    {
        //        if (objReq.CurrentItemQuantityTimes != null)
        //        {
        //            if (objReq.CurrentItemQuantityTimes.Length > 0)
        //            {
        //                objRes.ResCurrentItemQuantityTimes = new ClsResCurrentItemQuantityTimes[objReq.CurrentItemQuantityTimes.Length];
        //                for (int i = 0; i < objReq.CurrentItemQuantityTimes.Length; i++)
        //                {
        //                    SqlCommand dbCommand = new SqlCommand();
        //                    dbCommand.Connection = con;
        //                    dbCommand.CommandType = CommandType.StoredProcedure;
        //                    dbCommand.CommandText = storedProc;
        //                    dbCommand.Transaction = tran;

        //                    SqlParameter sqlParam = new SqlParameter();
        //                    SqlParameter returnParameter = new SqlParameter();
        //                    returnParameter = dbCommand.Parameters.Add("RetVal", SqlDbType.Int);
        //                    returnParameter.Direction = ParameterDirection.Output;

        //                    string[,] Params = new string[,]
        //                 {
        //                     {"@MastCode",Convert.ToString(objReq.CurrentItemQuantityTimes[i].MastCode)},
        //                     {"@ItemCode",Convert.ToString(objReq.CurrentItemQuantityTimes[i].ItemCode)},
        //                        {"@Quantity",Convert.ToString(objReq.CurrentItemQuantityTimes[i].Quantity)},
        //                        {"@CanteenCode",Convert.ToString(objReq.CurrentItemQuantityTimes[i].CanteenCode)},
        //                        {"@EntryBy",Convert.ToString(objReq.CurrentItemQuantityTimes[i].EntryBy)},
        //                        {"@EntryDate",Convert.ToString(objReq.CurrentItemQuantityTimes[i].EntryDate)},
        //                        {"@IsActive",Convert.ToString(objReq.CurrentItemQuantityTimes[i].IsActive)},
        //                        {"@DataSource",Convert.ToString(objReq.CurrentItemQuantityTimes[i].DataSource)},
        //                        {"@IMEI",Convert.ToString(objReq.CurrentItemQuantityTimes[i].IMEI)},
        //                 };
        //                    for (int p = 0; p < (Params.Length) / 2; p++)
        //                    {
        //                        sqlParam = dbCommand.Parameters.AddWithValue(Params[p, 0], Params[p, 1]);
        //                    }


        //                    try
        //                    {

        //                        rowsAffected = dbCommand.ExecuteNonQuery();
        //                        int retid = (int)returnParameter.Value;
        //                        if (retid > 0)
        //                        {
        //                            ClsResCurrentItemQuantityTimes objInnerCls = new ClsResCurrentItemQuantityTimes();
        //                            objInnerCls.MastCode = Convert.ToString(retid);
        //                            objInnerCls.ItemCode = objReq.CurrentItemQuantityTimes[i].ItemCode;
        //                            objInnerCls.Quantity = objReq.CurrentItemQuantityTimes[i].Quantity;
        //                            objInnerCls.CanteenCode = objReq.CurrentItemQuantityTimes[i].CanteenCode;
        //                            objInnerCls.EntryBy = objReq.CurrentItemQuantityTimes[i].EntryBy;
        //                            objInnerCls.EntryDate = objReq.CurrentItemQuantityTimes[i].EntryDate;
        //                            objInnerCls.IsActive = objReq.CurrentItemQuantityTimes[i].IsActive;
        //                            objInnerCls.DataSource = objReq.CurrentItemQuantityTimes[i].DataSource;
        //                            objRes.ResCurrentItemQuantityTimes[i] = objInnerCls;
        //                            //tran.Commit();
        //                            //return retid;
        //                        }
        //                        else
        //                        {
        //                            tran.Rollback();
        //                            objRes = null;
        //                            return 0;
        //                        }
        //                    }
        //                    catch (Exception objE1)
        //                    {
        //                        tran.Rollback();
        //                        throw;
        //                        //errorMessage = "<B><font Color=RED>Exception Occured :</Font></B>  Message= " + objE1.Message.ToString() + ". Method= " + objE1.TargetSite.Name.ToString();
        //                        //errorDescription = " <B>StackTrace :</B> " + objE1.StackTrace.ToString() + " Source = " + objE1.Source.ToString();
        //                        //objRes = null;
        //                        //return 0;
        //                    }
        //                }
        //                tran.Commit();
        //                return 1;
        //            }
        //            else
        //            {
        //                tran.Rollback();
        //                objRes = null;
        //                return 0;
        //            }
        //        }
        //        else
        //        {
        //            tran.Rollback();
        //            objRes = null;
        //            return 0;
        //        }
        //    }

        //    catch (Exception objE)
        //    {
        //        ////tran.Rollback();
        //        ////if (objE.ToString().Contains("Cannot insert duplicate key in object 'dbo.TblBkpDmpTerminalData'"))
        //        ////{
        //        ////    errorMessage = "Success";
        //        ////    errorDescription = "Success";
        //        ////    return 1;
        //        ////}
        //        ////else
        //        ////{
        //        tran.Rollback();
        //        throw;
        //        //errorMessage = "<B><font Color=RED>Exception Occured :</Font></B>  Message= " + objE.Message.ToString() + ". Method= " + objE.TargetSite.Name.ToString();
        //        //errorDescription = " <B>StackTrace :</B> " + objE.StackTrace.ToString() + " Source = " + objE.Source.ToString();
        //        //objRes = null;
        //        //return 0;

        //        ////}
        //    }
        //    finally
        //    {
        //        if (con.State == ConnectionState.Open) CloseConnection(con);
        //    }


        //}

     
	}
}
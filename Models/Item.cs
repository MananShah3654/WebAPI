using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Web_API.Common;
using Web_API.Request;
using Web_API.Response;

namespace Web_API.Models
{
    public class Item
    {
        DAL dl = new DAL();
        ClsSqlHelper objSqlHelper = new ClsSqlHelper();

            public ClsSyncItemMasterResponse SyncItemMaster(ClsSyncItemMasterRequest clsSyncItemMasterRequest)
            {
                DataSet ds = new DataSet();
                ClsSyncItemMasterResponse objClsSyncItemMasterResponse = new ClsSyncItemMasterResponse();


                object[] parameter = new object[]{
					    clsSyncItemMasterRequest.SyncDate,
                        clsSyncItemMasterRequest.ItemMastCode,
                        clsSyncItemMasterRequest.IMEI,
                        clsSyncItemMasterRequest.UserCode,
                       clsSyncItemMasterRequest.Company,
                       clsSyncItemMasterRequest.CanteenCode
                };

                ds = objSqlHelper.GetDataSetAddWithValue(parameter, "GetAll_TblItemMaster_device_CMS");


                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            objClsSyncItemMasterResponse.ErrorCode = Convert.ToString(ds.Tables[0].Rows[0]["ErrorCode"]);
                            objClsSyncItemMasterResponse.ErrorMessage = Convert.ToString(ds.Tables[0].Rows[0]["ErrorMessage"]);

                            ClsSyncItemMasterData[] ObjRes_List = new ClsSyncItemMasterData[ds.Tables[0].Rows.Count];
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ClsSyncItemMasterData objRes1 = new ClsSyncItemMasterData();

                                objRes1.MastCode = ds.Tables[0].Rows[i]["MastCode"].ToString();
                                objRes1.ItemCode = ds.Tables[0].Rows[i]["ItemCode"].ToString();
                                objRes1.MealCode = ds.Tables[0].Rows[i]["MealCode"].ToString();
                                objRes1.MastName = ds.Tables[0].Rows[i]["MastName"].ToString();
                                objRes1.ItemPrice = ds.Tables[0].Rows[i]["ItemPrice"].ToString();
                                objRes1.IsActive = ds.Tables[0].Rows[i]["IsActive"].ToString();
                                objRes1.CanteenCode = ds.Tables[0].Rows[i]["CanteenCode"].ToString();
                                objRes1.MinimumStock = ds.Tables[0].Rows[i]["MinStock"].ToString();
                                objRes1.IsQuantity = ds.Tables[0].Rows[i]["IsQuantity"].ToString();

                                ObjRes_List[i] = objRes1;
                          
                            }
                            objClsSyncItemMasterResponse.SyncItemMasterData = ObjRes_List;
                        }
                        else
                        {
                            objClsSyncItemMasterResponse.SyncItemMasterData = null;
                        }
                    }
                    else
                    {
                        objClsSyncItemMasterResponse.ErrorCode = "1003";
                        objClsSyncItemMasterResponse.ErrorMessage = "DATASET TABLE COUNT NOT FOUND";
                    }
                }
                else
                {
                    objClsSyncItemMasterResponse.ErrorCode = "1003";
                    objClsSyncItemMasterResponse.ErrorMessage = "DATASET NULL";
                }

                return objClsSyncItemMasterResponse;
            }
    }
}
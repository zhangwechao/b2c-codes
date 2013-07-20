//--------------------------------------------------
// 网商宝商城免费开源版 V1.0.110909
// 本程序仅用于学习和研究，不得作为商业用途。
// 如需进行商城运营，请与我公司联系购买商业版本。
//
// 东莞市捷联科技有限公司
// 网址：www.128.com.cn
// QQ：1316108492
// 电话：400-678-1128
//--------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using com.eshop.www.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
//51aspx.com 下载 http://www.51aspx.com
namespace com.eshop.www.DAL
{
    public class JobInformationDAO
    {
        public bool Add(JobInformation jobInformation)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into job_information(job,description,number,effective_date,create_date) values (@job,@description,@number,@effective_date,default)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@job", DbType.String, jobInformation.Job);
            database.AddInParameter(cmd, "@description", DbType.String, jobInformation.Description);
            database.AddInParameter(cmd, "@number", DbType.String, jobInformation.Number);
            database.AddInParameter(cmd, "@effective_date", DbType.String, jobInformation.EffectiveDate);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from job_information where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(JobInformation jobInformation)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update job_information set job=@job,description=@description,number=@number,effective_date=@effective_date where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@job", DbType.String, jobInformation.Job);
            database.AddInParameter(cmd, "@description", DbType.String, jobInformation.Description);
            database.AddInParameter(cmd, "@number", DbType.String, jobInformation.Number);
            database.AddInParameter(cmd, "@effective_date", DbType.String, jobInformation.EffectiveDate);
            database.AddInParameter(cmd, "@Id", DbType.Int32, jobInformation.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public JobInformation GetEntity(int Id)
        {
            JobInformation jobInformation = new JobInformation();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select job,description,number,effective_date,create_date from job_information where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    jobInformation.Id = Id;
                    jobInformation.Job = reader["job"].ToString();
                    jobInformation.Description = reader["description"].ToString();
                    jobInformation.Number = reader["number"].ToString();
                    jobInformation.EffectiveDate = reader["effective_date"].ToString();
                    jobInformation.CreateDate = DateTime.Parse(reader["create_date"].ToString());
                }
            }
            return jobInformation;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "job_information");
            database.AddInParameter(cmd, "@FieldList", DbType.String, fieldList);
            database.AddInParameter(cmd, "@PageSize", DbType.Int32, pageSize);
            database.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageIndex);
            database.AddInParameter(cmd, "@OrderField", DbType.String, orderField);
            database.AddInParameter(cmd, "@OrderType", DbType.Boolean, orderBy);
            database.AddInParameter(cmd, "@Where", DbType.String, where);
            database.AddOutParameter(cmd, "@RecordCount", DbType.Int32, 4);
            database.AddOutParameter(cmd, "@PageCount", DbType.Int32, 4);

            DataSet ds = database.ExecuteDataSet(cmd);
            int recordCount = Convert.ToInt32(database.GetParameterValue(cmd, "@RecordCount"));
            int pageCount = Convert.ToInt32(database.GetParameterValue(cmd, "@PageCount"));
            table.Table = ds.Tables[0];
            table.PageSize = pageSize;
            table.PageIndex = pageIndex;
            table.PageCount = pageCount;
            table.RecordCount = recordCount;
            return table;
        }
        public int GetRecordCount(string where)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select count(Id) recordCount from job_information where " + where;
            DbCommand cmd = database.GetSqlStringCommand(sql);
            string record = database.ExecuteScalar(cmd).ToString();
            if (record.Length > 0)
                return int.Parse(database.ExecuteScalar(cmd).ToString());
            else
                return 0;
        }
    }
}

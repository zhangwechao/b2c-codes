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

namespace com.eshop.www.DAL
{
    public class ResumeDAO
    {
        public bool Add(Resume resume)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "insert into resume(user_name,image,sex,cardID,email,nation,birthday,politic_status,address,degree,health,is_marry,height,school,profession,graduate_date,language,computer,job_id,good_at,contact,tel,contact_add,postcode,require_pay,training,intro,gain,other) "
                + " values (@user_name,@image,@sex,@cardID,@email,@nation,@birthday,@politic_status,@address,@degree,@health,@is_marry,@height,@school,@profession,@graduate_date,@language,@computer,@job_id,@good_at,@contact,@tel,@contact_add,@postcode,@require_pay,@training,@intro,@gain,@other)";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@user_name", DbType.String, resume.UserName);
            database.AddInParameter(cmd, "@image", DbType.String, resume.Image);
            database.AddInParameter(cmd, "@sex", DbType.Boolean, resume.Sex);
            database.AddInParameter(cmd, "@cardID", DbType.String, resume.CardID);
            database.AddInParameter(cmd, "@email", DbType.String, resume.Email);
            database.AddInParameter(cmd, "@nation", DbType.String, resume.Nation);
            database.AddInParameter(cmd, "@birthday", DbType.DateTime, resume.Birthday);
            database.AddInParameter(cmd, "@politic_status", DbType.String, resume.PoliticStatus);
            database.AddInParameter(cmd, "@address", DbType.String, resume.Address);
            database.AddInParameter(cmd, "@degree", DbType.String, resume.Degree);
            database.AddInParameter(cmd, "@health", DbType.String, resume.Health);
            database.AddInParameter(cmd, "@is_marry", DbType.Boolean, resume.IsMarry);
            database.AddInParameter(cmd, "@height", DbType.Int32, resume.Height);
            database.AddInParameter(cmd, "@school", DbType.String, resume.School);
            database.AddInParameter(cmd, "@profession", DbType.String, resume.Profession);
            database.AddInParameter(cmd, "@graduate_date", DbType.DateTime, resume.GraduateDate);
            database.AddInParameter(cmd, "@language", DbType.String, resume.Language);
            database.AddInParameter(cmd, "@computer", DbType.String, resume.Computer);
            database.AddInParameter(cmd, "@job_id", DbType.Int32, resume.JobId);
            database.AddInParameter(cmd, "@good_at", DbType.String, resume.GoodAt);
            database.AddInParameter(cmd, "@contact", DbType.String, resume.Contact);
            database.AddInParameter(cmd, "@tel", DbType.String, resume.Tel);
            database.AddInParameter(cmd, "@contact_add", DbType.String, resume.ContactAdd);
            database.AddInParameter(cmd, "@postcode", DbType.String, resume.Postcode);
            database.AddInParameter(cmd, "@require_pay", DbType.String, resume.RequirePay);
            database.AddInParameter(cmd, "@training", DbType.String, resume.Training);
            database.AddInParameter(cmd, "@intro", DbType.String, resume.Intro);
            database.AddInParameter(cmd, "@gain", DbType.String, resume.Gain);
            database.AddInParameter(cmd, "@other", DbType.String, resume.Other);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Delete(int Id)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "delete from resume where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public bool Update(Resume resume)
        {
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "update resume set user_name=@user_name,image=@image,sex=@sex,cardID=@cardID,email=@email,nation=@nation,birthday=@birthday,politic_status=@politic_status,address=@address,degree=@degree,health=@health,is_marry=@is_marry,height=@height,school=@school,"+
                " profession=@profession,graduate_date=@graduate_date,language=@language,computer=@computer,good_at=@good_at,contact=@contact,tel=@tel,contact_add=@contact_add,postcode=@postcode,require_pay=@require_pay,training=@training,intro=@intro,gain=@gain,other=@other where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@user_name", DbType.String, resume.UserName);
            database.AddInParameter(cmd, "@image", DbType.String, resume.Image);
            database.AddInParameter(cmd, "@sex", DbType.Boolean, resume.Sex);
            database.AddInParameter(cmd, "@cardID", DbType.String, resume.CardID);
            database.AddInParameter(cmd, "@email", DbType.String, resume.Email);
            database.AddInParameter(cmd, "@nation", DbType.String, resume.Nation);
            database.AddInParameter(cmd, "@birthday", DbType.DateTime, resume.Birthday);
            database.AddInParameter(cmd, "@politic_status", DbType.String, resume.PoliticStatus);
            database.AddInParameter(cmd, "@address", DbType.String, resume.Address);
            database.AddInParameter(cmd, "@degree", DbType.String, resume.Degree);
            database.AddInParameter(cmd, "@health", DbType.String, resume.Health);
            database.AddInParameter(cmd, "@is_marry", DbType.Boolean, resume.IsMarry);
            database.AddInParameter(cmd, "@height", DbType.Int32, resume.Height);
            database.AddInParameter(cmd, "@school", DbType.String, resume.School);
            database.AddInParameter(cmd, "@profession", DbType.String, resume.Profession);
            database.AddInParameter(cmd, "@graduate_date", DbType.DateTime, resume.GraduateDate);
            database.AddInParameter(cmd, "@language", DbType.String, resume.Language);
            database.AddInParameter(cmd, "@computer", DbType.String, resume.Computer);
            database.AddInParameter(cmd, "@job_id", DbType.Int32, resume.JobId);
            database.AddInParameter(cmd, "@good_at", DbType.String, resume.GoodAt);
            database.AddInParameter(cmd, "@contact", DbType.String, resume.Contact);
            database.AddInParameter(cmd, "@tel", DbType.String, resume.Tel);
            database.AddInParameter(cmd, "@contact_add", DbType.String, resume.ContactAdd);
            database.AddInParameter(cmd, "@postcode", DbType.String, resume.Postcode);
            database.AddInParameter(cmd, "@require_pay", DbType.String, resume.RequirePay);
            database.AddInParameter(cmd, "@training", DbType.String, resume.Training);
            database.AddInParameter(cmd, "@intro", DbType.String, resume.Intro);
            database.AddInParameter(cmd, "@gain", DbType.String, resume.Gain);
            database.AddInParameter(cmd, "@other", DbType.String, resume.Other);
            database.AddInParameter(cmd, "@Id", DbType.Int32, resume.Id);
            int row = database.ExecuteNonQuery(cmd);
            return row > 0;
        }

        public Resume GetEntity(int Id)
        {
            Resume resume = new Resume();
            Database database = DatabaseFactory.CreateDatabase();
            string sql = "select user_name,image,sex,cardID,email,nation,birthday,politic_status,address,degree,health,is_marry,height,school,profession,graduate_date,language,computer,job_id,good_at,contact,tel,contact_add,postcode,require_pay,training,intro,gain,other from resume where Id=@Id";
            DbCommand cmd = database.GetSqlStringCommand(sql);
            database.AddInParameter(cmd, "@Id", DbType.Int32, Id);
            using (IDataReader reader = database.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    resume.Id = Id;
                    resume.Address = reader["address"].ToString();
                    if(reader["birthday"].ToString().Length>0)
                        resume.Birthday = DateTime.Parse(reader["birthday"].ToString());
                    resume.CardID = reader["cardID"].ToString();
                    resume.Computer = reader["computer"].ToString();
                    resume.Contact = reader["contact"].ToString();
                    resume.ContactAdd = reader["contact_add"].ToString();
                    resume.Degree = reader["alt"].ToString();
                    resume.Email = reader["email"].ToString();
                    resume.Gain = reader["gain"].ToString();
                    resume.GoodAt = reader["good_at"].ToString();
                    if (reader["graduate_date"].ToString().Length>0)
                        resume.GraduateDate = DateTime.Parse(reader["graduate_date"].ToString());
                    resume.Health = reader["health"].ToString();
                    resume.Height = int.Parse(reader["height"].ToString());
                    resume.Image = reader["image"].ToString();
                    resume.Intro = reader["intro"].ToString();
                    resume.IsMarry = bool.Parse(reader["is_marry"].ToString());
                    resume.JobId = int.Parse(reader["job_id"].ToString());
                    resume.Language = reader["language"].ToString();
                    resume.Nation = reader["nation"].ToString();
                    resume.Other = reader["other"].ToString();
                    resume.PoliticStatus = reader["politic_status"].ToString();
                    resume.Postcode = reader["postcode"].ToString();
                    resume.Profession = reader["profession"].ToString();
                    resume.RequirePay = reader["require_pay"].ToString();
                    resume.School = reader["school"].ToString();
                    resume.Sex = bool.Parse(reader["sex"].ToString());
                    resume.Tel = reader["tel"].ToString();
                    resume.Training = reader["training"].ToString();
                    resume.UserName = reader["user_name"].ToString();
                }
            }
            return resume;
        }

        public DataRecordTable GetList(string fieldList, string orderField, bool orderBy, int pageIndex, int pageSize, string where)
        {
            DataRecordTable table = new DataRecordTable();
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand cmd = database.GetStoredProcCommand("proc_getpagedata");
            database.AddInParameter(cmd, "@TableName", DbType.String, "resume");
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
    }
}

using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tampines_CMS.Domain;
using Tampines_CMS.Persistence.DBConnectionFactory;
using Tampines_CMS.Persistence.Interface;

namespace Tampines_CMS.Persistence.Implementation
{
    public class OurTownDao: IOurTownDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public OurTownDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion

        public OurTown GetOurTown()
        {
            OurTown ourTown = null;


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetOurTown]";
                ourTown = conn.Query<OurTown>(SQL, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return ourTown;
        }

        public Int64 Save(OurTown HB)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (HB.GUID != Guid.Empty)
            {
                param.Add("@Guid", HB.GUID, dbType: DbType.Guid);
            }
            param.Add("@ImageGUID1", HB.ImageGUID1, dbType: DbType.String);
            param.Add("@ImageName1", HB.ImageName1, dbType: DbType.String);
            param.Add("@ImageExtension1", HB.ImageExtension1, dbType: DbType.String);
            param.Add("@ImageAltTag1", HB.ImageAltTag1, dbType: DbType.String);

            param.Add("@ImageGUID2", HB.ImageGUID2, dbType: DbType.String);
            param.Add("@ImageName2", HB.ImageName2, dbType: DbType.String);
            param.Add("@ImageExtension2", HB.ImageExtension2, dbType: DbType.String);
            param.Add("@ImageAltTag2", HB.ImageAltTag2, dbType: DbType.String);

            param.Add("@ImageGUID3", HB.ImageGUID3, dbType: DbType.String);
            param.Add("@ImageName3", HB.ImageName3, dbType: DbType.String);
            param.Add("@ImageExtension3", HB.ImageExtension3, dbType: DbType.String);
            param.Add("@ImageAltTag3", HB.ImageAltTag3, dbType: DbType.String);

            param.Add("@Content", HB.Content, dbType: DbType.String);
            param.Add("@SystemIP", HB.SystemIp, dbType: DbType.String);
            param.Add("@UserGuid", HB.UserId, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_UpdateOurTown]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }

        public TownMapPDF GetTownMapPDF()
        {
            TownMapPDF ourTown = null;


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetTownMapPDF]";
                ourTown = conn.Query<TownMapPDF>(SQL, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return ourTown;
        }

        public Int64 SavePDF(TownMapPDF HB)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (HB.GUID != Guid.Empty)
            {
                param.Add("@Guid", HB.GUID, dbType: DbType.Guid);
            }
            param.Add("@PDFFileGUID", HB.PDFFileGUID, dbType: DbType.String);
            param.Add("@PDFFileName", HB.PDFFileName, dbType: DbType.String);
            param.Add("@PDFFileExtension", HB.PDFFileExtension, dbType: DbType.String);
   
            param.Add("@SystemIP", HB.SystemIp, dbType: DbType.String);
            param.Add("@UserGuid", HB.UserId, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_UpdateTownMapPDF]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }
    }
}

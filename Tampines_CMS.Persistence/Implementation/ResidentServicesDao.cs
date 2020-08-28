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
    public class ResidentServicesDao : IResidentServicesDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public ResidentServicesDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion


        public BulkyItemRemovalServices GetBulkyItemRemovalServices()
        {
            BulkyItemRemovalServices services = null;


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetBulkyItemRemovalServices]";
                services = conn.Query<BulkyItemRemovalServices>(SQL, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return services;
        }

        public Int64 Save(BulkyItemRemovalServices HB)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (HB.GUID != Guid.Empty)
            {
                param.Add("@Guid", HB.GUID, dbType: DbType.Guid);
            }
            param.Add("@Content", HB.Content, dbType: DbType.String);
            param.Add("@SystemIP", HB.SystemIp, dbType: DbType.String);
            param.Add("@UserGuid", HB.UserId, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_UpdateBulkyItemRemovalServices]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }

        IList<Accordion> IResidentServicesDao.GetAccordions()
        {
            IList<Accordion> result = null;
            DynamicParameters param = new DynamicParameters();
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetAccordions]";
                result = conn.Query<Accordion>(sql: SQL, param: param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return result;
        }

        public int DeleteAccordion(Accordion A, Guid UserGUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();

            if (A.GUID != Guid.Empty)
            {
                param.Add("@GUID", A.GUID, dbType: DbType.Guid);
            }

            param.Add("@AdminUserId", UserGUID, dbType: DbType.Guid);
            param.Add("@SystemIP", A.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteAccordion]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }

        public Accordion SaveAccordion(Accordion A, Guid UserGUID)
        {
            Accordion result = new Accordion();
            DynamicParameters param = new DynamicParameters();
            if (A.GUID != Guid.Empty)
            {
                param.Add("@Guid", A.GUID, dbType: DbType.Guid);
            }
            param.Add("@Title", A.Title, dbType: DbType.String);
            param.Add("@ShortDescription", A.ShortDescription, dbType: DbType.String);
            param.Add("@SystemIP", A.SystemIp, dbType: DbType.String);
            param.Add("@@AdminUserId", UserGUID, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveAccordion]";
                result = conn.Query<Accordion>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }

        public Accordion GetAccordionByGuid(Guid GUID)
        {
            Accordion Accordion = null;
            DynamicParameters param = new DynamicParameters();
            if (GUID != Guid.Empty)
            {
                param.Add("@GUID", GUID, dbType: DbType.Guid);
            }
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetAccordionByGuid]";
                var results = conn.QueryMultiple(
                   sql: SQL,
                   param: param,
                   commandType: CommandType.StoredProcedure);
                if (results != null)
                {
                    Accordion = results.Read<Accordion>().FirstOrDefault();
                    if (Accordion != null)
                    {
                        Accordion.AccordionPdfS = results.Read<AccordionPdf>().ToList();
                    }
                }
                conn.Close();
            }
            return Accordion;
        }

        public int SaveAccordionPdf(AccordionPdf A, Guid UserGUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();
            if (A.GUID != Guid.Empty)
            {
                param.Add("@Guid", A.GUID, dbType: DbType.Guid);
            }
            if (A.AccordionGuid != Guid.Empty)
            {
                param.Add("@AccordionGuid", A.AccordionGuid, dbType: DbType.Guid);
            }
            param.Add("@PdfTitle", A.PdfTitle, dbType: DbType.String);
            param.Add("@FileName", A.FileName, dbType: DbType.String);
            param.Add("@FileGUID", A.FileGUID, dbType: DbType.String);
            param.Add("@Extension", A.Extension, dbType: DbType.String);
            param.Add("@SystemIP", A.SystemIp, dbType: DbType.String);
            param.Add("@@AdminUserId", UserGUID, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveAccordionPdf]";
                result = conn.Query<int>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }

        public int DeleteAccordionPdf(AccordionPdf A, Guid UserGUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();

            if (A.GUID != Guid.Empty)
            {
                param.Add("@GUID", A.GUID, dbType: DbType.Guid);
            }

            param.Add("@AdminUserId", UserGUID, dbType: DbType.Guid);
            param.Add("@SystemIP", A.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteAccordionPdf]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }

        public AccordionPdf GetAccordionPdfByGuid(Guid GUID)
        {
            AccordionPdf result = new AccordionPdf();
            DynamicParameters param = new DynamicParameters();
            if (GUID != Guid.Empty)
            {
                param.Add("@GUID", GUID, dbType: DbType.Guid);
            }
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetAccordionPdfByGuid]";
                result = conn.Query<AccordionPdf>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return result;
        }

        public int PublishAccordionToLive(Guid UserGUID, string SystemIp)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();
            param.Add("@AdminUserId", UserGUID, dbType: DbType.Guid);
            param.Add("@SystemIP", SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_PublishAccordionToLive]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }

        public IList<Accordion> GetPublishedAccordions()
        {
            IList<Accordion> Accordions = null;
            DynamicParameters param = new DynamicParameters();
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetPublishedAccordions]";
                var results = conn.QueryMultiple(
                   sql: SQL,
                   param: param,
                   commandType: CommandType.StoredProcedure);
                if (results != null)
                {
                    Accordions = results.Read<Accordion>().ToList();
                    List<AccordionPdf> accordionPdfs = results.Read<AccordionPdf>().ToList();
                    foreach (Accordion A in Accordions)
                    {
                        A.AccordionPdfS = accordionPdfs.Where(S => S.AccordionId == A.Id).ToList();
                    }
                }
                conn.Close();
            }
            return Accordions;
        }

        public int CheckAccordionTitleIsValid(string Title, Guid GUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Title", Title.Trim(), dbType: DbType.String);

            if (GUID != Guid.Empty)
            {
                param.Add("@GUID", GUID, dbType: DbType.Guid);
            }

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_CheckAccordionTitleIsValid]";
                result = conn.Query<int>(sql: SQL, param: param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }
    }
}

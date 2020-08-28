


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
    public  class ContactsDao:IContactsDao
    {

        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public ContactsDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion



        public Int64 SaveLinkContacts(LinksContacts LC)
        {

            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (LC.GUID != Guid.Empty)
                param.Add("@Guid", LC.GUID, dbType: DbType.Guid);

            param.Add("@Organisation", LC.Organisation, dbType: DbType.String);
            param.Add("@Issue", LC.Issue, dbType: DbType.String);
            param.Add("@Contact", LC.Contact, dbType: DbType.String);
            param.Add("@SystemIP", LC.SystemIp, dbType: DbType.String);
            param.Add("@UserId", LC.UserId, dbType: DbType.Guid);


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveLinksContacts]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }



        public List<LinksContacts> Get(int PageIndex = 1, int PageSize = 10)
        {
            List<LinksContacts> linksContacts = new List<LinksContacts>();
            DynamicParameters param = new DynamicParameters();
          
            param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
            param.Add("@PageSize", PageSize, dbType: DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetLinksContacts]";
                linksContacts = conn.Query<LinksContacts>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return linksContacts;
        }


        public LinksContacts GetEditLinkContacts(Guid GUID)
        {
            LinksContacts linksContacts = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Guid", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEditLinksContactByGuid]";
                linksContacts = conn.Query<LinksContacts>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return linksContacts;
        }



        public Int64 DeleteLinksContact(Guid GUID, Guid UserId, string SystemIp)
        {
            Int64 result = 0;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            param.Add("@UserId", UserId, dbType: DbType.Guid);
            param.Add("@SystemIp", SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteLinksConatct]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }




        public IList<LinksContacts> GetResourcesSorting(/*Guid GUID*/)
        {
            IList<LinksContacts> resources = null;

            DynamicParameters param = new DynamicParameters();
           // param.Add("@Guid", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetLinksContactsSorting]";
                resources = conn.Query<LinksContacts>(SQL, param, commandType: CommandType.StoredProcedure).ToList<LinksContacts>();
                conn.Close();
            }
            return resources;
        }


        public Int32 UpdateResourcesSorting(List<Sorting> sort, Guid UserId, string SystemIp)
        {
            Int32 result = 0;
            DataTable SortingTbl = new DataTable("SortingTbl");
            SortingTbl.Columns.Add("ReOrder", typeof(Int64));
            SortingTbl.Columns.Add("GUID", typeof(Guid));

            if (sort != null && sort.Count() > 0)
            {
                sort.ForEach(d => { { SortingTbl.Rows.Add(d.ReOrder, d.GUID); } });
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("@SortingTbl", SortingTbl.AsTableValuedParameter());
            param.Add("@UserId", UserId, dbType: DbType.Guid);
            param.Add("@SystemIp", SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_UpdateLinksContactsSorting]";
                result = conn.Query<Int32>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }


        public TownMapPDF GetPDPAPDF()
        {
            TownMapPDF ourTown = null;


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetPDPAPDF]";
                ourTown = conn.Query<TownMapPDF>(SQL, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return ourTown;
        }

        public Int64 SavePDPAPDF(TownMapPDF HB)
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
                string SQL = @"[USP_UpdatePDPAPDF]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }


        public TownMapPDF GetWhistleBlowingPolicyPDF()
        {
            TownMapPDF ourTown = null;


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetWhistleBlowingPolicyPDF]";
                ourTown = conn.Query<TownMapPDF>(SQL, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return ourTown;
        }

        public Int64 SaveWhistleBlowingPolicyPDF(TownMapPDF HB)
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
                string SQL = @"[USP_UpdateWhistleBlowingPolicyPDF]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }





        public Int64 SaveFAQ(FAQ fAQ)
        {

            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (fAQ.GUID != Guid.Empty)
                param.Add("@Guid", fAQ.GUID, dbType: DbType.Guid);

            param.Add("@Content", fAQ.Content, dbType: DbType.String);
            param.Add("@Question", fAQ.Question, dbType: DbType.String);
            
            param.Add("@SystemIP", fAQ.SystemIp, dbType: DbType.String);
            param.Add("@UserGuid", fAQ.UserId, dbType: DbType.Guid);


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveFAQ]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }



        public IList<FAQ> GetFAQ(int PageIndex = 1, int PageSize = 10)
        {
            IList<FAQ> fAQs = null;
            DynamicParameters param = new DynamicParameters();

            param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
            param.Add("@PageSize", PageSize, dbType: DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetFAQS]";
                fAQs = conn.Query<FAQ>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return fAQs;
        }


        public FAQ GetFAQByGuid(Guid GUID)
        {
            FAQ fAQ = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Guid", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetFAQByGuid]";
                fAQ = conn.Query<FAQ>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return fAQ;
        }


    }
}

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
    public class AboutUsDao : IAboutUsDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public AboutUsDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion

        public AboutUsIntroduction GetAboutUsIntroduction()
        {
            AboutUsIntroduction aboutUsIntroduction = null;


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetAboutUsIntroduction]";
                aboutUsIntroduction = conn.Query<AboutUsIntroduction>(SQL, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return aboutUsIntroduction;
        }

        public Int64 Save(AboutUsIntroduction HB)
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
                string SQL = @"[USP_UpdateAboutUsIntroduction]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }


        public TownMapPDF GetOrganisationChartPDF()
        {
            TownMapPDF ourTown = null;


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetOrganisationChartPDF]";
                ourTown = conn.Query<TownMapPDF>(SQL, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return ourTown;
        }

        public Int64 SaveOrganisationChartPDF(TownMapPDF HB)
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
                string SQL = @"[USP_UpdateOrganisationChartPDF]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }


        public TownMapPDF GetMasterPlanPDF()
        {
            TownMapPDF ourTown = null;


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetMasterPlanPDF]";
                ourTown = conn.Query<TownMapPDF>(SQL, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return ourTown;
        }

        public Int64 SaveMasterPlanPDF(TownMapPDF HB)
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
                string SQL = @"[USP_UpdateMasterPlanPDF]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }



        public List<OurMps> Get()
        {
            List<OurMps> OurMps = new List<OurMps>();
            DynamicParameters param = new DynamicParameters();

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetOurMps]";
                OurMps = conn.Query<OurMps>(SQL, param, commandType: CommandType.StoredProcedure).ToList();
                conn.Close();
            }
            return OurMps;
        }


        public Int64 SaveOurMps(OurMps LC)
        {

            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (LC.GUID != Guid.Empty)
                param.Add("@Guid", LC.GUID, dbType: DbType.Guid);

            param.Add("@Name", LC.Name, dbType: DbType.String);
            param.Add("@ImageGUID", LC.ImageGUID, dbType: DbType.String);
            param.Add("@ImageName", LC.ImageName, dbType: DbType.String);
            param.Add("@ImageExtension", LC.ImageExtension, dbType: DbType.String);
            param.Add("@ImageAltTag", LC.ImageAltTag, dbType: DbType.String);

            param.Add("@FacebookLink", LC.FacebookLink, dbType: DbType.String);
            param.Add("@InstagramLink", LC.InstagramLink, dbType: DbType.String);

            param.Add("@Designation", LC.Designation, dbType: DbType.String);
            param.Add("@Division", LC.Division, dbType: DbType.String);
            param.Add("@MeetThePeople", LC.MeetThePeople, dbType: DbType.String);
            param.Add("@CommunityClub", LC.CommunityClub, dbType: DbType.String);

            param.Add("@SystemIP", LC.SystemIp, dbType: DbType.String);
            param.Add("@UserId", LC.UserId, dbType: DbType.Guid);


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveOurMps]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }






        public OurMps GetOurMpsByGuid(Guid GUID)
        {
            OurMps OurMps = null;
            DynamicParameters param = new DynamicParameters();
            param.Add("@Guid", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEditOurMpsByGuid]";
                OurMps = conn.Query<OurMps>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return OurMps;
        }


        public Int64 DeleteOurMps(Guid GUID, Guid UserId, string SystemIp)
        {
            Int64 result = 0;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            param.Add("@UserId", UserId, dbType: DbType.Guid);
            param.Add("@SystemIp", SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteOurMps]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }



        public IList<OurMps> GetOurMpsSorting(/*Guid GUID*/)
        {
            IList<OurMps> OurMps = null;

            DynamicParameters param = new DynamicParameters();
            // param.Add("@Guid", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetOurMpsSorting]";
                OurMps = conn.Query<OurMps>(SQL, param, commandType: CommandType.StoredProcedure).ToList<OurMps>();
                conn.Close();
            }
            return OurMps;
        }


        public Int32 UpdateOurMpsSorting(List<Sorting> sort, Guid UserId, string SystemIp)
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
                string SQL = @"[USP_UpdateOurMpsSorting]";
                result = conn.Query<Int32>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }



        public OurPublicationsViewModel GetOurPublications()
        {

            OurPublicationsViewModel our = new OurPublicationsViewModel();
            DynamicParameters param = new DynamicParameters();


            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetAnnualReports]";
                var result = conn.QueryMultiple(SQL, param, commandType: CommandType.StoredProcedure);
                if (result != null)
                {

                    our.AnnualReports = result.Read<AnnualReports>().ToList();
                    our.PressReleases = result.Read<PressRelease>().ToList();


                }

                conn.Close();
            }
            return our;
        }


        public long Save(AnnualReports AU, Guid UserGUID)
        {
            long result = 0;
            DynamicParameters param = new DynamicParameters();

            if (AU.GUID != Guid.Empty)
            {
                param.Add("@GUID", AU.GUID, dbType: DbType.Guid);
            }
            param.Add("@Title", AU.Title, dbType: DbType.String);
            param.Add("@Year", AU.Year, dbType: DbType.String);
            param.Add("@ImageGUID", AU.ImageGUID, dbType: DbType.String);
            param.Add("@ImageName", AU.ImageName, dbType: DbType.String);
            param.Add("@ImageExtension", AU.ImageExtension, dbType: DbType.String);
            //  param.Add("@ImageAltTag", AU.ImageAltTag, dbType: DbType.String);
            param.Add("@PDFGUID", AU.PDFGUID, dbType: DbType.String);
            param.Add("@PDFName", AU.PDFName, dbType: DbType.String);
            param.Add("@PDFExtension", AU.PDFExtension, dbType: DbType.String);
            param.Add("@PDFFileSize", AU.PDFFileSize, dbType: DbType.String);
            //  param.Add("@AdminUserId", UserGUID, dbType: DbType.Guid);
            param.Add("@SystemIP", AU.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveAnnualReport]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }

        public int Delete(AnnualReports AU, Guid UserGUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();

            if (AU.GUID != Guid.Empty)
            {
                param.Add("@GUID", AU.GUID, dbType: DbType.Guid);
            }

            param.Add("@AdminUserId", UserGUID, dbType: DbType.Guid);
            param.Add("@SystemIP", AU.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteAnnualReport_Test]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }


        public AnnualReports GetbyGuid(Guid GUID)
        {
            AnnualReports result = null;
            DynamicParameters param = new DynamicParameters();

            if (GUID != Guid.Empty)
            {
                param.Add("@GUID", GUID, dbType: DbType.Guid);
            }

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetAnnualReportByGuid]";
                result = conn.Query<AnnualReports>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return result;
        }


        public long SavePressRelease(PressRelease PR, Guid UserGUID)
        {
            long result = 0;
            DynamicParameters param = new DynamicParameters();

            if (PR.GUID != Guid.Empty)
            {
                param.Add("@GUID", PR.GUID, dbType: DbType.Guid);
            }
            param.Add("@Title", PR.Title, dbType: DbType.String);
            param.Add("@PressDate", PR.PressDate, dbType: DbType.DateTime);

            param.Add("@PDFGUID", PR.PDFGUID, dbType: DbType.String);
            param.Add("@PDFName", PR.PDFName, dbType: DbType.String);
            param.Add("@PDFExtension", PR.PDFExtension, dbType: DbType.String);

            param.Add("@SystemIP", PR.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SavePressRelease]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }


        public int DeletePress(PressRelease PR, Guid UserGUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();

            if (PR.GUID != Guid.Empty)
            {
                param.Add("@GUID", PR.GUID, dbType: DbType.Guid);
            }

            param.Add("@AdminUserId", UserGUID, dbType: DbType.Guid);
            param.Add("@SystemIP", PR.SystemIp, dbType: DbType.String);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeletePressRelease]";
                result = conn.Execute(sql: SQL, param: param, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return result;
        }



        public PressRelease GetbyGuidPress(Guid GUID)
        {
            PressRelease result = null;
            DynamicParameters param = new DynamicParameters();

            if (GUID != Guid.Empty)
            {
                param.Add("@GUID", GUID, dbType: DbType.Guid);
            }

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetPressReleaseByGuid]";
                result = conn.Query<PressRelease>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            return result;
        }



    }
}

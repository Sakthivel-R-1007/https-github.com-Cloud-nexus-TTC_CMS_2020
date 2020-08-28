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
    public class BannerDao : IBannerDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public BannerDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion

        public ViewBannerModel GetViewBanner()
        {
            ViewBannerModel viewBanner = new ViewBannerModel();

            DynamicParameters param = new DynamicParameters();

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetViewBanners]";
                var contents = conn.QueryMultiple(SQL, param, commandType: CommandType.StoredProcedure);
                if (contents != null)
                {

                    viewBanner.HomeBanner = contents.Read<HomeBanner>().ToList();
                    viewBanner.AboutUsBanner = contents.Read<AboutUsBanner>().ToList();
                    viewBanner.OurTownBanner = contents.Read<OurTownBanner>().ToList();
                    viewBanner.ResidentServicesBanner = contents.Read<ResidentServicesBanner>().ToList();
                    viewBanner.TEMPOBanner = contents.Read<TEMPOBanner>().ToList();
                    viewBanner.EventBanner = contents.Read<EventBanner>().ToList();
                    viewBanner.ContactUsBanner = contents.Read<ContactUsBanner>().ToList();

                }

            }
            return viewBanner;

        }

        public Int64 SaveHomeBanner(HomeBanner HB)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (HB.GUID != Guid.Empty)
            {
                param.Add("@Guid", HB.GUID, dbType: DbType.Guid);
            }
            param.Add("@Header1", HB.Header1, dbType: DbType.String);
            param.Add("@Header2", HB.Header2, dbType: DbType.String);
            param.Add("@Header3", HB.Header3, dbType: DbType.String);
            param.Add("@ReadMore", HB.ReadMore, dbType: DbType.String);
            param.Add("@LinkTarget", HB.LinkTarget, dbType: DbType.String);

            if (!string.IsNullOrEmpty(HB.ImageGUID))
            {
                param.Add("@ImageGUID", HB.ImageGUID, dbType: DbType.String);
                param.Add("@ImageName", HB.ImageName, dbType: DbType.String);
                param.Add("@ImageExtension", HB.ImageExtension, dbType: DbType.String);
            }

           
            param.Add("@SystemIP", HB.SystemIp, dbType: DbType.String);
            param.Add("@UserGuid", HB.UserId, dbType: DbType.Guid);
           
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveHomeBanner]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }

        public HomeBanner GetHomeBannerByGuid(Guid GUID)
        {
            HomeBanner homeBanner = null;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetHomeBannerByGuid]";
                homeBanner = conn.Query<HomeBanner>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return homeBanner;
        }

        public Int64 DeleteHomeBanner(Guid GUID, Guid UserId, string SystemIp)
        {
            Int64 result = 0;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            param.Add("@UserId", UserId, dbType: DbType.Guid);
            param.Add("@SystemIp", SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteHomeBanner]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }

        public IList<HomeBanner> GetHomeBanners()
        {
            IList<HomeBanner> P = null;
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetHomeBanners]";
                P = conn.Query<HomeBanner>(SQL, commandType: CommandType.StoredProcedure).ToList<HomeBanner>();
            }
            return P;
        }

        public Int32 UpdateHomeBannerSorting(List<Sorting> sort, Guid UserId, string SystemIp)
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
                string SQL = @"[USP_UpdateHomeBannerSorting]";
                result = conn.Query<Int32>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }


        public Int64 UpdateAboutUsBanner(AboutUsBanner HB)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (HB.GUID != Guid.Empty)
            {
                param.Add("@Guid", HB.GUID, dbType: DbType.Guid);
            }
          

            if (!string.IsNullOrEmpty(HB.ImageGUID))
            {
                param.Add("@ImageGUID", HB.ImageGUID, dbType: DbType.String);
                param.Add("@ImageName", HB.ImageName, dbType: DbType.String);
                param.Add("@ImageExtension", HB.ImageExtension, dbType: DbType.String);
            }


            param.Add("@SystemIP", HB.SystemIp, dbType: DbType.String);
            param.Add("@UserGuid", HB.UserId, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_UpdateAboutUsBanner]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }

        public AboutUsBanner GetAboutUsBannerByGuid(Guid GUID)
        {
            AboutUsBanner aboutUsBanner = null;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetAboutUsByGuid]";
                aboutUsBanner = conn.Query<AboutUsBanner>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return aboutUsBanner;
        }

        public Int64 UpdateOurTownBanner(OurTownBanner HB)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (HB.GUID != Guid.Empty)
            {
                param.Add("@Guid", HB.GUID, dbType: DbType.Guid);
            }


            if (!string.IsNullOrEmpty(HB.ImageGUID))
            {
                param.Add("@ImageGUID", HB.ImageGUID, dbType: DbType.String);
                param.Add("@ImageName", HB.ImageName, dbType: DbType.String);
                param.Add("@ImageExtension", HB.ImageExtension, dbType: DbType.String);
            }


            param.Add("@SystemIP", HB.SystemIp, dbType: DbType.String);
            param.Add("@UserGuid", HB.UserId, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_UpdateOurTownBanner]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }

        public OurTownBanner GetOurTownBannerByGuid(Guid GUID)
        {
            OurTownBanner aboutUsBanner = null;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetOurTownByGuid]";
                aboutUsBanner = conn.Query<OurTownBanner>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return aboutUsBanner;
        }

        public Int64 UpdateResidentServicesBanner(ResidentServicesBanner HB)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (HB.GUID != Guid.Empty)
            {
                param.Add("@Guid", HB.GUID, dbType: DbType.Guid);
            }


            if (!string.IsNullOrEmpty(HB.ImageGUID))
            {
                param.Add("@ImageGUID", HB.ImageGUID, dbType: DbType.String);
                param.Add("@ImageName", HB.ImageName, dbType: DbType.String);
                param.Add("@ImageExtension", HB.ImageExtension, dbType: DbType.String);
            }


            param.Add("@SystemIP", HB.SystemIp, dbType: DbType.String);
            param.Add("@UserGuid", HB.UserId, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_UpdateResidentServicesBanner]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }

        public ResidentServicesBanner GetResidentServicesBannerByGuid(Guid GUID)
        {
            ResidentServicesBanner residentServices = null;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetResidentServicesBannerByGuid]";
                residentServices = conn.Query<ResidentServicesBanner>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return residentServices;
        }


        public Int64 UpdateTEMPOBanner(TEMPOBanner HB)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (HB.GUID != Guid.Empty)
            {
                param.Add("@Guid", HB.GUID, dbType: DbType.Guid);
            }


            if (!string.IsNullOrEmpty(HB.ImageGUID))
            {
                param.Add("@ImageGUID", HB.ImageGUID, dbType: DbType.String);
                param.Add("@ImageName", HB.ImageName, dbType: DbType.String);
                param.Add("@ImageExtension", HB.ImageExtension, dbType: DbType.String);
            }


            param.Add("@SystemIP", HB.SystemIp, dbType: DbType.String);
            param.Add("@UserGuid", HB.UserId, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_UpdateTEMPOBannerBanner]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }

        public TEMPOBanner GetTEMPOBannerByGuid(Guid GUID)
        {
            TEMPOBanner tEMPOBanner = null;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetTEMPOBannerByGuid]";
                tEMPOBanner = conn.Query<TEMPOBanner>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return tEMPOBanner;
        }


        public Int64 UpdateEventBanner(EventBanner HB)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (HB.GUID != Guid.Empty)
            {
                param.Add("@Guid", HB.GUID, dbType: DbType.Guid);
            }


            if (!string.IsNullOrEmpty(HB.ImageGUID))
            {
                param.Add("@ImageGUID", HB.ImageGUID, dbType: DbType.String);
                param.Add("@ImageName", HB.ImageName, dbType: DbType.String);
                param.Add("@ImageExtension", HB.ImageExtension, dbType: DbType.String);
            }


            param.Add("@SystemIP", HB.SystemIp, dbType: DbType.String);
            param.Add("@UserGuid", HB.UserId, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_UpdateEventBanner]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }

        public EventBanner GetEventBannerByGuid(Guid GUID)
        {
            EventBanner eventBanner = null;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEventBannerByGuid]";
                eventBanner = conn.Query<EventBanner>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return eventBanner;
        }


        public Int64 UpdateContactUsBanner(ContactUsBanner HB)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (HB.GUID != Guid.Empty)
            {
                param.Add("@Guid", HB.GUID, dbType: DbType.Guid);
            }


            if (!string.IsNullOrEmpty(HB.ImageGUID))
            {
                param.Add("@ImageGUID", HB.ImageGUID, dbType: DbType.String);
                param.Add("@ImageName", HB.ImageName, dbType: DbType.String);
                param.Add("@ImageExtension", HB.ImageExtension, dbType: DbType.String);
            }


            param.Add("@SystemIP", HB.SystemIp, dbType: DbType.String);
            param.Add("@UserGuid", HB.UserId, dbType: DbType.Guid);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_UpdateContactUsBanner]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }

            return result;
        }

        public ContactUsBanner GetContactUsBannerByGuid(Guid GUID)
        {
            ContactUsBanner contactUsBanner = null;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetContactUsBannerByGuid]";
                contactUsBanner = conn.Query<ContactUsBanner>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return contactUsBanner;
        }
    }
}

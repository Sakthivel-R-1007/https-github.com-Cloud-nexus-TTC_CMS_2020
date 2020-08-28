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
    public class EventsDao : IEventsDao
    {
        #region Private variables and constructors

        protected readonly IDbConnectionFactory factory;

        public EventsDao()
        {
            this.factory = new DbConnectionFactory("DefaultDb");
        }

        #endregion


        public Int64 SaveEvents(Events NL)
        {
            Int64 result = 0;
            DynamicParameters param = new DynamicParameters();
            if (NL.GUID != Guid.Empty)
                param.Add("@Guid", NL.GUID, dbType: DbType.Guid);

            param.Add("@Title", NL.Title, dbType: DbType.String);

            param.Add("@EventDate", NL.EventDate, dbType: DbType.DateTime);

            param.Add("@ImageGUID", NL.ImageGUID, dbType: DbType.String);
            param.Add("@ImageName", NL.ImageName, dbType: DbType.String);
            param.Add("@ImageExtension", NL.ImageExtension, dbType: DbType.String);

            DataTable EventsTbl = new DataTable("EventsTbl");
            EventsTbl.Columns.Add("With", typeof(string));
            EventsTbl.Columns.Add("Venue", typeof(string));
            EventsTbl.Columns.Add("Time", typeof(string));

            if (NL.EventSection != null && NL.EventSection.Count() > 0)
            {
                NL.EventSection.ForEach(d =>
                {
                    {
                        EventsTbl.Rows.Add(d.With, d.Venue, d.Time);
                    }
                });
            }

            param.Add("@EventsTbl", EventsTbl.AsTableValuedParameter());
            param.Add("@FacebookLink", NL.FacebookLink, dbType: DbType.String);
            param.Add("@InstagramLink", NL.InstagramLink, dbType: DbType.String);
            param.Add("@SystemIP", NL.SystemIp, dbType: DbType.String);
            param.Add("@UserId", NL.UserId, dbType: DbType.Guid);



            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_SaveEvents]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }


        public IList<Events> Get(int PageIndex = 1, int PageSize = 10)
        {
            IList<Events> Events = null;
            List<EventSection> EventSection = new List<EventSection>();
            DynamicParameters param = new DynamicParameters();

            param.Add("@PageIndex", PageIndex, dbType: DbType.Int32);
            param.Add("@PageSize", PageSize, dbType: DbType.Int32);

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEvents]";
                var result = conn.QueryMultiple(SQL, param, commandType: CommandType.StoredProcedure);
                if (result != null)
                {

                    Events = result.Read<Events>().ToList();
                    EventSection = result.Read<EventSection>().ToList();

                    if (EventSection != null && Events != null && EventSection.Count > 0 && Events.Count > 0)
                    {



                        for (int i = 0; i < Events.Count(); i++)
                        {


                            Events[i].EventSection = EventSection.Where(E => E.EventsId == Events[i].Id).ToList();


                        }
                    }

                }
                conn.Close();
            }
            return Events;
        }



        public Events GetEventByGuid(Guid GUID)
        {
            Events events = new Events();
            DynamicParameters param = new DynamicParameters();
            param.Add("@Guid", GUID, dbType: DbType.Guid);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_GetEventsByGuid]";
                var result = conn.QueryMultiple(SQL, param, commandType: CommandType.StoredProcedure);

                if (result != null)
                {

                    events = result.Read<Events>().FirstOrDefault();
                    events.EventSection = result.Read<EventSection>().ToList();

                }
                conn.Close();
            }
            return events;
        }




        public Int64 DeleteEvents(Guid GUID, Guid UserId, string SystemIp)
        {
            Int64 result = 0;

            DynamicParameters param = new DynamicParameters();
            param.Add("@GUID", GUID, dbType: DbType.Guid);
            param.Add("@UserId", UserId, dbType: DbType.Guid);
            param.Add("@SystemIp", SystemIp, dbType: DbType.String);
            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_DeleteEvents]";
                result = conn.Query<Int64>(SQL, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;
        }



        public int CheckEventTitleName(string EventTitle, Guid GUID)
        {
            int result = 0;
            DynamicParameters param = new DynamicParameters();
            param.Add("@EventTitle", EventTitle.Trim(), dbType: DbType.String);

            if (GUID != Guid.Empty)
            {
                param.Add("@GUID", GUID, dbType: DbType.Guid);
            }

            using (IDbConnection conn = factory.GetConnection())
            {
                conn.Open();
                string SQL = @"[USP_CheckEventTitle]";
                result = conn.Query<int>(sql: SQL, param: param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                conn.Close();
            }
            return result;

        }


    }
}

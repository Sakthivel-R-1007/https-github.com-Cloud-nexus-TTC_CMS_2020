
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tampines_CMS.Domain;

namespace Tampines_CMS.Persistence.Interface
{
     public  interface IContactsDao
    {

        Int64 SaveLinkContacts(LinksContacts LC);
        List<LinksContacts> Get(int PageIndex = 1, int PageSize = 10);
        LinksContacts GetEditLinkContacts(Guid GUID);
        Int64 DeleteLinksContact(Guid GUID, Guid UserId, string SystemIp);
 
        IList<LinksContacts> GetResourcesSorting();

        Int32 UpdateResourcesSorting(List<Sorting> sort, Guid UserId, string SystemIp);



        TownMapPDF GetPDPAPDF();

        Int64 SavePDPAPDF(TownMapPDF townMap);


        TownMapPDF GetWhistleBlowingPolicyPDF();

        Int64 SaveWhistleBlowingPolicyPDF(TownMapPDF townMap);


        IList<FAQ> GetFAQ(int PageIndex = 1, int PageSize = 10);
        FAQ GetFAQByGuid(Guid GUID);

        Int64 SaveFAQ(FAQ fAQ);



    }

}

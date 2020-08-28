using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tampines_CMS.Domain;

namespace Tampines_CMS.Persistence.Interface
{
    public interface IResidentServicesDao
    {
        BulkyItemRemovalServices GetBulkyItemRemovalServices();
        Int64 Save(BulkyItemRemovalServices services);
        IList<Accordion> GetAccordions();
        int DeleteAccordion(Accordion a, Guid guid);
        Accordion SaveAccordion(Accordion a, Guid guid);
        Accordion GetAccordionByGuid(Guid guid);
        int SaveAccordionPdf(AccordionPdf a, Guid guid);
        int DeleteAccordionPdf(AccordionPdf a, Guid guid);
        AccordionPdf GetAccordionPdfByGuid(Guid guid);
        int PublishAccordionToLive(Guid guid, string systemIp);
        IList<Accordion> GetPublishedAccordions();
        int CheckAccordionTitleIsValid(string titleName, Guid gUID);
    }
}

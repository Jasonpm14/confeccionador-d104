using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confeccionador_D_104.Model
{
    public class DeclarationConfigDataModel
    {

        string personId;

        DateTime startDate;

        DateTime endDate;

        public string PersonId { get => personId; set => personId = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }

        public DeclarationConfigDataModel(string id, int year, int month)
        {

            this.PersonId = id;

            this.StartDate = new DateTime(year, month, 1);

            this.EndDate =  new DateTime(year, month,
                DateTime.DaysInMonth(year, month));

        }

        public bool IsDateInCurrentPeriod(DateTime date)
        {
            if(date >= this.startDate && date <= endDate)
                return true;
            else 
                return false;
        }

        public bool IsInvoiceForDeclarator(string Id)
        {
            if (Id.Equals(this.personId))
                return true;
            else
                return false;
                    
        }

        
    }
}

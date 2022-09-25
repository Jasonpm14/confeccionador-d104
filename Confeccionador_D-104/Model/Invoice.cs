using System;
using System.Collections.Generic;
using System.Linq;

namespace Confeccionador_D_104.Model
{
    public class Invoice
    {
        private string invoiceNumber;
        private string emisorId;        
        private string emisorName;
        private string customerId;
        private string customerName;
        private string emisorEmail;
        private string customerEmail;

        private DateTime date;

        private List<InvoiceDetail> detailLines;

        public string InvoiceNumber { get => invoiceNumber; set => invoiceNumber = value; }
        public string EmisorId { get => emisorId; set => emisorId = value; }
        public string EmisorName { get => emisorName; set => emisorName = value; }
        public string CustomerId { get => customerId; set => customerId = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public string EmisorEmail { get => emisorEmail; set => emisorEmail = value; }
        public string CustomerEmail { get => customerEmail; set => customerEmail = value; }
        public DateTime Date { get => date; set => date = value; }
        public List<InvoiceDetail> DetailLines { get => detailLines; set => detailLines = value; }

        public Invoice( string invoiceNumber, string emisorId, string emisorName,
                        string customerId, string customerName, string emisorEmail,
                        string customerEmail, DateTime date, List<InvoiceDetail> detailLines)
        {
            this.invoiceNumber = invoiceNumber;
            this.emisorId = emisorId;
            this.emisorName = emisorName;
            this.customerId = customerId;
            this.customerName = customerName;
            this.emisorEmail = emisorEmail;
            this.customerEmail = customerEmail;
            this.date = date;
            this.detailLines = detailLines;
        }

        public Invoice()
        {
            this.invoiceNumber = "";
            this.emisorId = "";
            this.emisorName = "";
            this.customerId = "";
            this.customerName = "";
            this.emisorEmail = "";
            this.customerEmail = "";
            this.date = new DateTime();
            this.detailLines = new List<InvoiceDetail>();
        }


        public bool isForCurrentDeclarator(string id) 
        {
            if (id.Equals(emisorId))
                return true;
            else
                return false;
        }
    }
}

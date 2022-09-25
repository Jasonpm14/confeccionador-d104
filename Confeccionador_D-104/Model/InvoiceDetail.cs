using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confeccionador_D_104.Model
{
    public class InvoiceDetail
    {
        private string code;
        private string type;
        private string description;

        private float taxRate;
        private float subtotal;
        private float taxTotal;
        

        /// <summary>
        /// Ministerio de hacienda goods and services CABYS code
        /// </summary>
        public string Code { get => code; set => code = value; }

        /// <summary>
        /// Indicates if detail line corresponds to good or service
        /// </summary>
        public string Type { get => type; set => type = value; }

        /// <summary>
        /// Line detail description
        /// </summary>
        public string Description { get => description; set => description = value; }

        /// <summary>
        /// Line subtotal
        /// </summary>
        public float Subtotal { get => subtotal; set => subtotal = value; }

        /// <summary>
        /// Tax rate percentage
        /// </summary>
        public float TaxRate { get => taxRate; set => taxRate = value; }

        /// <summary>
        /// Result of subtotal by tax rate
        /// </summary>
        public float TaxTotal { get => taxTotal; set => taxTotal = value; }

        public InvoiceDetail()
        {
            code = "";
            type = "";
            description = "";
            subtotal = 0;
            taxRate = 0;
            taxTotal = 0;
        }

        public InvoiceDetail(string _code, string _type, string _description, int _taxRate, float _subtotal, float _taxTotal)
        {
            code = _code;
            type = _type;
            description = _description;
            taxRate = _taxRate;
            subtotal = _subtotal;
            taxTotal = _taxTotal;
        }
    }
}

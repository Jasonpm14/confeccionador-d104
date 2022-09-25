using Confeccionador_D_104.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confeccionador_D_104.Helpers
{
    static class GlobalData
    {
        public static readonly string[] MONTHS = new string[] 
        { 
            "Enero", 
            "Febrero",
            "Marzo",
            "Abril",
            "Mayo",
            "Junio",
            "Julio",
            "Agosto",
            "Setiembre",
            "Octubre",
            "Noviembre",
            "Diciembre"
        };

        public static List<Invoice> INCOME_INVOICES = new List<Invoice>();

        public static List<Invoice> EXPENSE_INVOICES = new List<Invoice>();

        public static List<string> CABYS_CODES_LIST = new List<string>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Confeccionador_D_104.Model
{
    public  class CabysModel
    {

        private string codigo;

        private string[] categorias;

        private string descripcion;

        private int impuesto;

        public string Codigo { get => codigo; set => codigo = value; }
        public string[] Categorias { get => categorias; set => categorias = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int Impuesto { get => impuesto; set => impuesto = value; }

        public CabysModel(string _codigo, string[] _categorias, string _descripcion, int _impuesto)
        {
            codigo = _codigo;
            categorias = _categorias;
            descripcion = _descripcion;
            impuesto = _impuesto;
        }
    }
}

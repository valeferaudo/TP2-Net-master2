using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public class DocenteCurso : BusinessEntity
    {
        private int _IDCurso;
        public int IDCurso
        {
            get { return _IDCurso; }
            set { _IDCurso = value; }
        }
        private int _IDDocente;
        public int IDDocente
        {
            get { return _IDDocente; }
            set { _IDDocente = value; }
        }
        private TiposCargos _Cargo;
        public TiposCargos Cargo
        {
            get { return _Cargo; }
            set { _Cargo = value; }
        }
        public enum TiposCargos
        {
            JefeCatedra = 1 ,
            Auxiliar
        }
        private String _DescripcionDocente;
        public String DescripcionDocente
        {
            get { return _DescripcionDocente; }
            set { _DescripcionDocente = value; }
        }
        private String _DescripcionCurso;
        public String DescripcionCurso
        {
            get { return _DescripcionCurso; }
            set { _DescripcionCurso = value; }
        }
    }
}

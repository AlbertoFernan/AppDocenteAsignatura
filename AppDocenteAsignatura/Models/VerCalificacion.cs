using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocenteAsignatura.Models
{
    public class VerCalificacion
    {
        public int IdAsignatura { get; set; }

        public int IdAlumno { get; set; }

        public int IdDocente { get; set; }

        public int IdPeriodo { get; set; }
    }
}

using AppDocenteAsignatura.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDocenteAsignatura.Services
{
    public class DocenteAsignService
    {
        HttpClient cliente = new HttpClient
        {
            BaseAddress = new Uri("https://docenteprimaria.sistemas19.com/")
        };

        public async Task<List<Grupos>> GetGrupos(int id)
        {
            List<Grupos> Grupos = null;

            var response = await cliente.GetAsync("api/Docente/grupos/"+id);

            if (response.IsSuccessStatusCode) //status= 200 ok
            {
                var json = await response.Content.ReadAsStringAsync();
                Grupos = JsonConvert.DeserializeObject<List<Grupos>>(json);
            }

            if (Grupos != null)
            {
                return Grupos;
            }
            else
            {
                return new List<Grupos>();
            }
        }

        public async Task<List<Alumnos>> GetAlumnos(int id)
        {
            List<Alumnos> Alumnos = null;

            var response = await cliente.GetAsync("api/Docente/alumnos/" + id);

            if (response.IsSuccessStatusCode) //status= 200 ok
            {
                var json = await response.Content.ReadAsStringAsync();
                Alumnos = JsonConvert.DeserializeObject<List<Alumnos>>(json);
            }

            if (Alumnos != null)
            {
                return Alumnos;
            }
            else
            {
                return new List<Alumnos>();
            }
        }


    }
}

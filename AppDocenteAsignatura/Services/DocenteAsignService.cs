using AppDocenteAsignatura.Models;
using Microsoft.VisualBasic;
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

        public async Task<Docente> Login(LoginUser login)
        {
            //Validar
           Docente docente = null;

            var json = JsonConvert.SerializeObject(login);
            var response = await cliente.PostAsync("api/Docente/usuario", new StringContent(json, Encoding.UTF8,
                "application/json"));

            var json2 = await response.Content.ReadAsStringAsync();
            docente = JsonConvert.DeserializeObject<Docente>(json2);
             
            

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //BadRequest
            {
                return null;
            }

            return docente;
        }



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

        public async Task<List<Evaluaciones>> VerCalifs(VerCalificacion calif)
        {
            //Validar
            List<Evaluaciones> calificaciones = null;

            var json = JsonConvert.SerializeObject(calif);
            var response = await cliente.PostAsync("api/Docente/calificaciones", new StringContent(json, Encoding.UTF8,
                "application/json"));

            var json2 = await response.Content.ReadAsStringAsync();
             calificaciones = JsonConvert.DeserializeObject<List<Evaluaciones>>(json2);
        

            //if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //BadRequest
            //{
            //    var errores = await response.Content.ReadAsStringAsync();
            //    LanzarErrorJson(errores);
            //    return false;
            //}
            return calificaciones;
        }


        public async Task<bool> SubirCalif(Calificacion calif)
        {
            //Validar
          

            var json = JsonConvert.SerializeObject(calif);
            var response = await cliente.PostAsync("api/Docente/calificar", new StringContent(json, Encoding.UTF8,
                "application/json"));

        


            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //BadRequest
            {
                var errores = await response.Content.ReadAsStringAsync();
              
                return false;
            }
            return true;
        }


    }
}

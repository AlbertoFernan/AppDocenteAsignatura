using AppDocenteAsignatura.Models;
using AppDocenteAsignatura.Services;
using AppDocenteAsignatura.Views;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppDocenteAsignatura.ViewModels
{
    public class DocenteAsignaturaViewModel:INotifyPropertyChanged
    {

        MainView mainView;
        AlumnosView alumnosView;
        CalificacionesView  calificacionesView;
        public List<Grupos> GruposDelDocente { get; set; }

        public List<Alumnos> AlumnosDelGrupo { get; set; }

        public List<Calificacion> CalificacionesAlumno { get; set; }
        public ICommand LoginCommand { get; set; }

        public ICommand VerAlumnosCommand { get; set; }

        public ICommand VerCalificacionesCommand { get; set; }

        public DocenteAsignService service;

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginUser loginUser { get; set; }

        public Docente docente { get; set; }

        public DocenteAsignaturaViewModel()
        {
            LoginCommand = new Command(IniciarSesionAsync);
            VerAlumnosCommand = new Command<int>(VerAlumnos);

            VerCalificacionesCommand = new Command<int>(VerCalificacioness);
            service = new DocenteAsignService();

            loginUser = new LoginUser();
            docente = new Docente();

        }

        private async void VerCalificacioness(int id)
        {
            VerCalificacion calif = new VerCalificacion();
            calif.IdAlumno = id;
            calif.IdDocente = docente.Id;
            calif.IdPeriodo =(int)docente.Periodo;
            calif.IdAsignatura = docente.IdAsigantura;

            CalificacionesAlumno = await service.VerCalifs(calif);

            calificacionesView = new CalificacionesView() { BindingContext = this };
            Application.Current.MainPage = calificacionesView;
        }

        private async void VerAlumnos(int id)
        {

            AlumnosDelGrupo = await service.GetAlumnos(id); 

            alumnosView = new AlumnosView() { BindingContext = this };
            Application.Current.MainPage = alumnosView;
        }

        private async void IniciarSesionAsync()
        {


            docente = await service.Login(loginUser);
            if (docente != null)
            {
                GruposDelDocente = await service.GetGrupos(docente.Id);
                mainView = new MainView() { BindingContext = this };

                Application.Current.MainPage = mainView;

            }


       


        }
    }
}

using AppDocenteAsignatura.Models;
using AppDocenteAsignatura.Services;
using AppDocenteAsignatura.Views;
using CommunityToolkit.Maui.Views;
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
        public List<int> Unidades { get; set; }
        public List<Evaluaciones> EvaluacionesAlumno { get; set; }
        public ICommand LoginCommand { get; set; }

        public ICommand VerAlumnosCommand { get; set; }

        public ICommand VerCalificacionesCommand { get; set; }

        public ICommand RegresarCommand { get; set; }

        public ICommand AgregarCalificacionCommand { get; set; }

        public DocenteAsignService service;

        public event PropertyChangedEventHandler PropertyChanged;

        public LoginUser loginUser { get; set; }

        public Docente docente { get; set; }

        public DocenteAsignaturaViewModel()
        {
            Unidades=new List<int>();
            Unidades.Add(1);

            Unidades.Add(2);

            Unidades.Add(3);
            LoginCommand = new Command(IniciarSesionAsync);
            VerAlumnosCommand = new Command<int>(VerAlumnos);

            VerCalificacionesCommand = new Command<int>(VerCalificacioness);

            AgregarCalificacionCommand = new Command(AggCalifAsync);

            RegresarCommand = new Command(Regresar);
            service = new DocenteAsignService();

            loginUser = new LoginUser();
            docente = new Docente();

        }

        private void AggCalifAsync(object obj)
        {
            Calificacion nuevacalif = new Calificacion();
          Application.Current.MainPage.ShowPopup(new PopupEvaluacion());
            //string calif = await Application.Current.MainPage.DisplayPromptAsync("Calificación", "Ingrese una calificacion", initialValue: "1", maxLength: 2, keyboard: Keyboard.Numeric);


        }

        private async void Regresar()
        {
            await Application.Current.MainPage.Navigation.PopAsync(); 
        }

        private async void VerCalificacioness(int id)
        {
            VerCalificacion calif = new VerCalificacion();
            calif.IdAlumno = id;
            calif.IdDocente = docente.Id;
            calif.IdPeriodo =(int)docente.Periodo;
            calif.IdAsignatura = docente.IdAsigantura;

            EvaluacionesAlumno = await service.VerCalifs(calif);

            calificacionesView = new CalificacionesView() { BindingContext = this };
         

            await Application.Current.MainPage.Navigation.PushAsync(calificacionesView);
        }

        private async void VerAlumnos(int id)
        {

            AlumnosDelGrupo = await service.GetAlumnos(id); 

            alumnosView = new AlumnosView() { BindingContext = this };
            Application.Current.MainPage = new NavigationPage(alumnosView);
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

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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppDocenteAsignatura.ViewModels
{
    public class DocenteAsignaturaViewModel:INotifyPropertyChanged
    {

        MainView mainView;
        AlumnosView alumnosView;
        CalificacionesView  calificacionesView;
        PopupEvaluacion popup;
        public List<Grupos> GruposDelDocente { get; set; }

        public Calificacion nuevacalif  { get; set; }
        public string Error { get; set; }
        public Asignatura materia { get; set; }
        public VerCalificacion calif { get; set; }
        public List<Alumnos> AlumnosDelGrupo { get; set; }
        public List<int> Unidades { get; set; }
        public List<Evaluaciones> EvaluacionesAlumno { get; set; }
        public ICommand LoginCommand { get; set; }

        public ICommand VerAlumnosCommand { get; set; }

        public ICommand VerCalificacionesCommand { get; set; }

        public ICommand RegresarCommand { get; set; }

        public ICommand AgregarCalificacionCommand { get; set; }

        public ICommand EnviarCalificacionCommand { get; set; }

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
            EnviarCalificacionCommand = new Command(EnviarAsync);

            RegresarCommand = new Command(Regresar);
            service = new DocenteAsignService();

            loginUser = new LoginUser();
            docente = new Docente();

        }

        private async void EnviarAsync()
        {
            if (nuevacalif.Calificacion1>10|| nuevacalif.Calificacion1 < 5)
            {
                Error= "Ingrese una calificación valida";
                Actualizar(nameof(Error));

            }
            else
            {
                await service.SubirCalif(nuevacalif);
                EvaluacionesAlumno = await service.VerCalifs(calif);
                popup.Close();
                Actualizar(nameof(EvaluacionesAlumno));
            }
           

        }

        private void AggCalifAsync(object obj)
        {
            popup = new PopupEvaluacion();
            nuevacalif = new Calificacion();
            nuevacalif.IdDocente = docente.Id;
            nuevacalif.IdPeriodo = (int)docente.Periodo;
            nuevacalif.IdAsignatura = docente.IdAsigantura;
            nuevacalif.IdAlumno = calif.IdAlumno;
            nuevacalif.Unidad = 1;
            nuevacalif.Calificacion1 = 0;
          Application.Current.MainPage.ShowPopup(popup);
            //string calif = await Application.Current.MainPage.DisplayPromptAsync("Calificación", "Ingrese una calificacion", initialValue: "1", maxLength: 2, keyboard: Keyboard.Numeric);


        }

        private async void Regresar()
        {
            await Application.Current.MainPage.Navigation.PopAsync(); 
        }

        private async void VerCalificacioness(int id)
        {
            calif = new VerCalificacion();
            calif.IdAlumno = id;
            calif.IdDocente = docente.Id;
            calif.IdPeriodo =(int)docente.Periodo;
            calif.IdAsignatura = docente.IdAsigantura;

            EvaluacionesAlumno = await service.VerCalifs(calif);

            calificacionesView = new CalificacionesView() { BindingContext = this };

            await Shell.Current.GoToAsync("calificacionesView");
            //await Application.Current.MainPage.Navigation.PushAsync(calificacionesView);
        }

        private async void VerAlumnos(int id)
        {

            AlumnosDelGrupo = await service.GetAlumnos(id); 

            alumnosView = new AlumnosView() { BindingContext = this };
            await Shell.Current.GoToAsync("AlumnosView");
           // Application.Current.MainPage = new NavigationPage(alumnosView);
        }

        private async void IniciarSesionAsync()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            try { 
            if (accessType == NetworkAccess.Internet)
            {
                if(string.IsNullOrWhiteSpace(loginUser.Usuario1))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ingrese un usuario", "Ok");
                        return;

                }
               else if (string.IsNullOrWhiteSpace(loginUser.Contraseña))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ingrese una contraseña", "Ok");
                        return;

                    }
             

           
               
                docente = await service.Login(loginUser);
               
              
                if (docente != null)
                {
                    GruposDelDocente = await service.GetGrupos(docente.Id);
                    mainView = new MainView() { BindingContext = this };

                    Application.Current.MainPage = mainView;


                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Usuario o contraseña incorrectos", "Ok");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No hay internet", "Ok");

            }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }



        }

        public void Actualizar(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

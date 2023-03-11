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

        public List<Grupos> GruposDelDocente { get; set; }

        public List<Alumnos> AlumnosDelGrupo { get; set; }
        public ICommand LoginCommand { get; set; }

        public ICommand VerAlumnosCommand { get; set; }

        public DocenteAsignService service;

        public event PropertyChangedEventHandler PropertyChanged;

        public DocenteAsignaturaViewModel()
        {
            LoginCommand = new Command(IniciarSesionAsync);
            VerAlumnosCommand = new Command<int>(VerAlumnos);
            service = new DocenteAsignService();

        }

        private async void VerAlumnos(int id)
        {

            AlumnosDelGrupo = await service.GetAlumnos(id); ;

            alumnosView = new AlumnosView() { BindingContext = this };
            Application.Current.MainPage = alumnosView;
        }

        private async void IniciarSesionAsync()
        {


            GruposDelDocente =await service.GetGrupos(1);
             mainView = new MainView() { BindingContext = this };
          
        Application.Current.MainPage = mainView;


        }
    }
}

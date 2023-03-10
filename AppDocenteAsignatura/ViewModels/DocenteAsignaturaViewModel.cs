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

        public List<Grupos> GruposDelDocente { get; set; }
        public ICommand LoginCommand { get; set; }

        public DocenteAsignService service;

        public event PropertyChangedEventHandler PropertyChanged;

        public DocenteAsignaturaViewModel()
        {
            LoginCommand = new Command(IniciarSesionAsync);
            service = new DocenteAsignService();

        }

        private async void IniciarSesionAsync()
        {


            GruposDelDocente =await service.GetGrupos(1);
             mainView = new MainView() { BindingContext = this };
          
        Application.Current.MainPage = mainView;


        }
    }
}

using AppDocenteAsignatura.Views;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppDocenteAsignatura.ViewModels
{
    public class DocenteAsignaturaViewModel
    {

        MainView mainView;

        public ICommand LoginCommand { get; set; }
        public DocenteAsignaturaViewModel()
        {
            LoginCommand = new Command(IniciarSesion);

        }

        private  void IniciarSesion()
        {
           
            mainView = new MainView() { BindingContext = this };
          
        Application.Current.MainPage = mainView;


        }
    }
}

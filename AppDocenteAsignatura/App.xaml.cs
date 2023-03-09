using AppDocenteAsignatura.Views;

namespace AppDocenteAsignatura;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new MainView();
	}
}

namespace AppDocenteAsignatura.Views;

public partial class MainView : Shell
{
	public MainView()
	{
		InitializeComponent(); 
        Routing.RegisterRoute("AlumnosView", typeof(AlumnosView));

        Routing.RegisterRoute("calificacionesView", typeof(CalificacionesView));
    }
}
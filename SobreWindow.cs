using Gtk;

public class SobreWindow
{
    private Window window;

    public SobreWindow()
    {
        var builder = new Builder(null, "sobre.glade", null);
        window = (Window)builder.GetObject("Frmsobre");

        var btnSair = (Button)builder.GetObject("BitBtn1");
        btnSair.Clicked += (s, e) => window.Destroy();

        window.ShowAll();
    }
}


using System;
using Gtk;

public class MainWindowApp
{
    private Builder builder;
    private Window mainWindow;

    // ✅ Construtor correto
    public MainWindowApp()
    {
        // Carrega o arquivo .glade
        builder = new Builder(null, "Principal.glade", null);
       
        // Obtém a janela principal do Glade
        mainWindow = (Window)builder.GetObject("MainWindow");

        // Conecta sinais do Glade com este código
        builder.Autoconnect(new Callbacks());

        // Configura evento de fechamento
        mainWindow.DeleteEvent += delegate { Application.Quit(); };

        // Mostra a janela principal
        mainWindow.ShowAll();
    }

    // Classe interna com os callbacks dos sinais definidos no Glade
    private class Callbacks
    {
        // Abre a janela Sobre ao clicar em "Informações"
        public void on_informacoes_activate(object sender, EventArgs e)
        {
            new SobreWindow();
        }
    }
}


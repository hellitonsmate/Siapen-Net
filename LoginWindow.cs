using System;
using Gtk;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Configuration;

public class LoginWindow : Window
{
    private Entry entryLogin;
    private Entry entrySenha;
    private Label labelStatus;


    public LoginWindow() : base("SIAPEN - Login")
    {
        LoadConnectionString();
        SetDefaultSize(350, 200);
        SetPosition(WindowPosition.Center);
        BorderWidth = 10;

        //VBox vbox = new VBox(false, 8);

        // Login
        Label labelLogin = new Label("Login:");
        entryLogin = new Entry();
        vbox.PackStart(labelLogin, false, false, 0);
        vbox.PackStart(entryLogin, false, false, 0);

        // Senha
        Label labelSenha = new Label("Senha:");
        entrySenha = new Entry();
        entrySenha.Visibility = false; // oculta senha
        vbox.PackStart(labelSenha, false, false, 0);
        vbox.PackStart(entrySenha, false, false, 0);

        // Botões
        //HBox hboxButtons = new HBox(true, 8);

        Box vbox = new Box(Orientation.Vertical, 8);
        Box hboxButtons = new Box(Orientation.Horizontal, 8);
        hboxButtons.Homogeneous = true;

        Button btnEntrar = new Button("Entrar");
        Button btnSair = new Button("Sair");

        btnEntrar.Clicked += OnLoginClicked;
        btnSair.Clicked += (s, e) => Application.Quit();

        hboxButtons.PackStart(btnEntrar, true, true, 0);
        hboxButtons.PackStart(btnSair, true, true, 0);
        vbox.PackStart(hboxButtons, false, false, 0);

        // Status
        labelStatus = new Label();
        labelStatus.Name = "statusLabel"; 
        vbox.PackStart(labelStatus, false, false, 0);

        Add(vbox);
        ShowAll();
    }
    
    private void SetStatusStyle(string css)
    {
        var provider = new CssProvider();
        provider.LoadFromData(css);
        StyleContext.AddProviderForScreen(Gdk.Screen.Default, provider, Gtk.StyleProviderPriority.Application);
    }

    private void OnLoginClicked(object sender, EventArgs e)
    {
        string login = entryLogin.Text.Trim();
        string senha = entrySenha.Text.Trim();

        if (ValidarLogin(login, senha))
        {
            labelStatus.Text = "✅ Login realizado com sucesso!";
            SetStatusStyle("#statusLabel { color: green; font-weight: bold; }");

            // ✨ Fecha janela de login e abre a janela principal
            Hide();              // ou Destroy();
            new MainWindowApp();
        }
        else
        {
            labelStatus.Text = "❌ Usuário ou senha inválidos.";
            SetStatusStyle("#statusLabel { color: red; font-weight: bold; }");
        }
    }

    private bool ValidarLogin(string login, string senhaDigitada)
    {
        string senhaConvertida = Senha(senhaDigitada);

        try
        {
            using (var conn = new FbConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new FbCommand("SELECT COUNT(*) FROM FUNCIONARIO WHERE LOGIN = @LOGIN AND SENHA = @SENHA", conn))
                {
                    cmd.Parameters.AddWithValue("@LOGIN", login);
                    cmd.Parameters.AddWithValue("@SENHA", senhaConvertida);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        catch (Exception ex)
        {
            labelStatus.Text = "Erro: " + ex.Message;
            return false;
        }
    }

    private void LoadConnectionString()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfigurationRoot configuration = builder.Build();
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // mesma função Delphi convertida
    public static string Senha(string strValue, ushort chave = 256)
    {
        string outValue = "";
        foreach (char c in strValue)
        {
            int code = (int)c;
            int transformed = ~(code - chave);
            outValue += (char)transformed;
        }
        return outValue;
    }
}

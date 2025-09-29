using System;
using Gtk;
using FirebirdSql.Data.FirebirdClient;

public class LoginWindow : Window
{
    private Entry entryLogin;
    private Entry entrySenha;
    private Label labelStatus;

    private string connectionString =
        "User=SYSDBA;" +
        "Password=masterkey;" +
        "Database=172.23.15.251:Siapen;" +
        "DataSource=172.23.15.251;" +
        "Port=3050;" +
        "Dialect=3;" +
        "Charset=UTF8;";

    public LoginWindow() : base("SIAPEN - Login")
    {
        SetDefaultSize(350, 200);
        SetPosition(WindowPosition.Center);
        BorderWidth = 10;

        VBox vbox = new VBox(false, 8);

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
        HBox hboxButtons = new HBox(true, 8);
        Button btnEntrar = new Button("Entrar");
        Button btnSair = new Button("Sair");

        btnEntrar.Clicked += OnLoginClicked;
        btnSair.Clicked += (s, e) => Application.Quit();

        hboxButtons.PackStart(btnEntrar, true, true, 0);
        hboxButtons.PackStart(btnSair, true, true, 0);
        vbox.PackStart(hboxButtons, false, false, 0);

        // Status
        labelStatus = new Label();
        vbox.PackStart(labelStatus, false, false, 0);

        Add(vbox);
        ShowAll();
    }

    private void OnLoginClicked(object sender, EventArgs e)
    {
        string login = entryLogin.Text.Trim();
        string senha = entrySenha.Text.Trim();

        if (ValidarLogin(login, senha))
        {
            labelStatus.Text = "✅ Login realizado com sucesso!";
            labelStatus.ModifyFg(StateType.Normal, new Gdk.Color(0, 150, 0));

            // ✨ Fecha janela de login e abre a janela principal
            Hide();              // ou Destroy();
            new MainWindowApp();
        }
        else
        {
            labelStatus.Text = "❌ Usuário ou senha inválidos.";
            labelStatus.ModifyFg(StateType.Normal, new Gdk.Color(200, 0, 0));
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


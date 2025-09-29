# Siapen-Net

Projeto de conversão do sistema **SIAPEN** de Delphi para **C#**, utilizando **GTK# 3** para interface gráfica.

O sistema possui:

- Tela de **login** com validação de usuário e senha via **Firebird**;  
- **Janela principal** (`MainWindow`) com menu suspenso;  
- **Janela Sobre** exibindo informações do sistema;  
- Layouts criados no **Glade** (`.glade` files).

---

## Requisitos

- **.NET Framework / Mono**  
- **GTK# 3**  
- **Firebird Client** (`FirebirdSql.Data.FirebirdClient`)  
- Banco de dados Firebird com tabela `FUNCIONARIO` (campos `LOGIN` e `SENHA`).  

> No Windows, instale o **GTK# 3 runtime**: [https://www.mono-project.com/download/stable/](https://www.mono-project.com/download/stable/)  
> No Linux, instale via gerenciador de pacotes (ex: `sudo apt install gtk-sharp3 mono-complete`).

---

## Estrutura do Projeto

- `Program.cs` → Ponto de entrada do aplicativo.  
- `LoginWindow.cs` → Tela de login.  
- `MainWindow.cs` → Tela principal com menu suspenso.  
- `SobreWindow.cs` → Tela “Sobre” com informações do sistema.  
- `*.glade` → Layouts GTK.  
- `LoginGTK.sln` e `LoginGTK.csproj` → Solução e projeto para Visual Studio.

---

## Como Compilar

### Windows

1. Abra o **Visual Studio**.  
2. Abra o arquivo `LoginGTK.sln`.  
3. Certifique-se de adicionar referência à biblioteca **FirebirdClient**:

   - Botão direito em `References` → `Add Reference...` → `Browse` → selecione `FirebirdSql.Data.FirebirdClient.dll`.  

4. Compile (`Build Solution`).  
5. Execute (`Start`).

---

### Linux (Mono)

1. Instale GTK# 3 e Mono:

sudo apt install mono-complete gtk-sharp3
Instale o FirebirdClient (caso necessário, copie FirebirdSql.Data.FirebirdClient.dll para o projeto).

Compile o projeto usando mcs:

mcs -pkg:gtk-sharp-3.0 Program.cs LoginWindow.cs MainWindow.cs SobreWindow.cs -r:FirebirdSql.Data.FirebirdClient.dll -out:SIAPEN.exe
Execute:

mono SIAPEN.exe

### Observações
A função de criptografia de senha é baseada em um algoritmo legado do Delphi.

O menu “Cadastro → Interno” ainda não está implementado.

A janela Sobre abre ao clicar em “Sobre → Informações”.

### Licença
Este projeto é open-source. Use livremente, respeitando a atribuição.

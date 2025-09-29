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

---

## Estrutura do Projeto

- `Program.cs` → Ponto de entrada do aplicativo.  
- `LoginWindow.cs` → Tela de login.  
- `MainWindow.cs` → Tela principal com menu suspenso.  
- `SobreWindow.cs` → Tela “Sobre” com informações do sistema.  
- `*.glade` → Layouts GTK.  
- `LoginGTK.sln` e `LoginGTK.csproj` → Solução e projeto para Visual Studio.

---

## Como Rodar

1. Clone o repositório:

```bash
git clone https://github.com/hellitonsmate/Siapen-Net.git
cd Siapen-Net

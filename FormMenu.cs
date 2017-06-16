using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Usando nessa class para pega o response
using System.Net.Http;

namespace SQL_Scan_by_Biggs
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            //Evitar de diminuir a coluna
            InfoView.Columns[0].Width = 385;
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;

            try
            {
                Uri UrlScan = new Uri(OriginUrl.Text);

                if (OriginUrl.Text.Length > 50)
                {
                    InfoView.Items.Add(DateTime.Now.ToShortTimeString() + " [ Iniciando Scan em " + OriginUrl.Text.Substring(0, 50) + "... ]").ForeColor = Color.Green;
                }
                else
                {
                    InfoView.Items.Add(DateTime.Now.ToShortTimeString() + " [ Iniciando Scan em " + OriginUrl.Text + "... ]").ForeColor = Color.Green;
                }
           
                InfoView.Items.Add(DateTime.Now.ToShortTimeString() + " [ Fazendo requisição e coletando dados... ]").ForeColor = Color.Green;

                var NewScan = new SetURL();
                HttpResponseMessage response = await NewScan.responsiveUrl(UrlScan);

                InfoView.Items.Add(DateTime.Now.ToShortTimeString() + " [ Status Code " + response.IsSuccessStatusCode.ToString() + " ]").ForeColor = Color.Green;

                if (response.ReasonPhrase.ToString() == "OK")
                { //Verde
                    InfoView.Items.Add(DateTime.Now.ToShortTimeString() + " [ ReasonPhrase Resposta: OK ]").ForeColor = Color.Green;
                }
                else
                { //Laranja + Vermelho
                    InfoView.Items.Add(DateTime.Now.ToShortTimeString() + " [ ReasonPhrase Resposta: " + response.ReasonPhrase.ToString() + " ]").ForeColor = Color.OrangeRed;
                }

                InfoView.Items.Add(DateTime.Now.ToShortTimeString() + " [ " + response.RequestMessage.ToString() + " ]").ForeColor = Color.Black;

                if (response.IsSuccessStatusCode)
                {
                    //Extrair Pagina 
                    string scan_result = await response.Content.ReadAsStringAsync();
                    //Lista de erros
                    string[] vulnerabilidades = { "mysql_num_rows()", "mysql_fetch_array()", "Error Occurred While Processing Request", "Server Error in '/' Application", "Microsoft OLE DB Provider for ODBC Drivers error", "error in your SQL syntax", "Invalid Querystring", "OLE DB Provider for ODBC", "VBScript Runtime", "ADODB.Field", "BOF or EOF", "ADODB.Command", "JET Database", "mysql_fetch_row()", "Syntax error", "include()", "mysql_fetch_assoc()", "mysql_fetch_object()", "mysql_numrows()", "GetArray()", "FetchRow()", "Input string was not in a correct format", "session_start()", "array_merge()", "preg_match()", "ilesize()", "filesize()", "", "SQL Error", "[MySQL][ODBC 5.1 Driver][mysqld-4.1.22-community-nt-log]You have an error in your SQL syntax", "You have an error in your SQL syntax", "mysql_query()", "mysql_fetch_object()", "Query failed:", "Warning include() [function.include]", "mysql_num_rows()", "Database Query Failed", "mysql_fetch_assoc()", "mysql_free_result()", "Query failed (SELECT * FROM WHERE id = )", "num_rows", "Error Executing Database Query", "Unclosed quotation mark", "Error Occured While Processing Request", "FetchRows()", "Microsoft JET Database", "ODBC Microsoft Access Driver", "OLE DB Provider for SQL Server", "SQLServer JDBC Driver", "Error Executing Database Query", "ORA-01756", "getimagesize()", "unknown()", "mysql_result()", "pg_exec()", "require()", "Microsoft JET Database", "ADODB.Recordset", "500 - Internal server error", "Microsoft OLE DB Provider", "Unclosed quotes", "ADODB.Command", "ADODB.Field error", "Microsoft VBScript", "Microsoft OLE DB Provider for SQL Server", "Unclosed quotation mark", "Microsoft OLE DB Provider for Oracle", "Active Server Pages error", "OLE/DB provider returned message", "OLE DB Provider for ODBC", "Unclosed quotation mark after the character string", "SQL Server", "Warning: odbc_", "ORA-00921: unexpected end of SQL command", "ORA-01756", "ORA-", "Oracle ODBC", "Oracle Error", "Oracle Driver", "Oracle DB2", "error ORA-", "SQL command not properly ended", "DB2 ODBC", "DB2 error", "DB2 Driver", "ODBC SQL", "ODBC DB2", "ODBC Driver", "ODBC Error", "ODBC Microsoft Access", "ODBC Oracle", "ODBC Microsoft Access Driver", "Warning: pg_", "PostgreSql Error:", "function.pg", "Supplied argument is not a valid PostgreSQL result", "PostgreSQL query failed: ERROR: parser: parse error", ": pg_", "Warning: sybase_", "function.sybase", "Sybase result index", "Sybase Error:", "Sybase: Server message:", "sybase_", "ODBC Driver", "java.sql.SQLSyntaxErrorException: ORA-", "org.springframework.jdbc.BadSqlGrammarException:", "javax.servlet.ServletException:", "java.lang.NullPointerException", "Error Executing Database Query", "SQLServer JDBC Driver", "JDBC SQL", "JDBC Oracle", "JDBC MySQL", "JDBC error", "JDBC Driver", "java.io.IOException: InfinityDB", "Warning: include", "Fatal error: include", "Warning: require", "Fatal error: require", "ADODB_Exception", "Warning: include", "Warning: require_once", "function.include", "Disallowed Parent Path", "function.require", "Warning: main", @"Warning: session_start\(\)", @"Warning: getimagesize\(\)", @"Warning: array_merge\(\)", @"Warning: preg_match\(\)", @"GetArray\(\)", @"FetchRow\(\)", "Warning: preg_", @"Warning: ociexecute\(\)", @"Warning: ocifetchstatement\(\)", "PHP Warning:", "Version Information: Microsoft .NET Framework", "Server.Execute Error", "ASP.NET_SessionId", "ASP.NET is configured to show verbose error messages", "BOF or EOF", "Unclosed quotation mark", "Error converting data type varchar to numeric", "LuaPlayer ERROR:", "CGILua message", "Lua error", "Incorrect syntax near", "Fatal error", "Invalid Querystring", "Input string was not in a correct format", "An illegal character has been found in the statement", "MariaDB server version for the right syntax" };

                    InfoView.Items.Add(DateTime.Now.ToShortTimeString() + " [ Verificando se existir algum erro ]").ForeColor = Color.Black;

                    bool erro = false;
                    foreach (string rs in vulnerabilidades)
                    {
                        if (rs != String.Empty)
                        {
                            if (scan_result.ToLower().Contains(rs.ToLower()))
                            {
                                erro = true;
                                InfoView.Items.Add(DateTime.Now.ToShortTimeString() + " [ Erro " + rs + " Encontrado!!! ]").ForeColor = Color.Red;
                            }
                        }
                    }

                    if (erro)
                    {
                        InfoView.Items.Add(DateTime.Now.ToShortTimeString() + " [ Site Vulneravel " + response.RequestMessage.RequestUri.ToString() + " ]").ForeColor = Color.Red;
                        MessageBox.Show("Pagina com vulnerabilidades!!", "Erro encontrado");
                    }
                    else
                    {
                        InfoView.Items.Add(DateTime.Now.ToShortTimeString() + " [ Site Limpo sem Nenhum erro! ]").ForeColor = Color.Red;
                    }
                }
                else
                {
                    InfoView.Items.Add(DateTime.Now.ToShortTimeString() + " [ Não é possivel verificar erros neste site! ]").ForeColor = Color.Black;
                    InfoView.Items.Add(DateTime.Now.ToShortTimeString() + " [ Content Null! ]").ForeColor = Color.Black;

                    MessageBox.Show("Não foi possivel verificar erros neste site...");
                }
            }
            catch (UriFormatException erroUrl)
            {
                MessageBox.Show("Informe o site que deseja scannear" + "\r\n" +
                    erroUrl.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve um erro desconhecido ao acessar o site!");
            }

            btnConnect.Enabled = true;
        }

        //ListProducts.EnsureVisible(ListProducts.Items.Count - 1);
    }
}

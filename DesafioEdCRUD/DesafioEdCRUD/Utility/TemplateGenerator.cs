using Entities.Models;
using System.Text;

namespace DesafioEdCRUD.Utility
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString(BookAuthorReport[] bookrReport)
        {            
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'><h1>Relatório de Livros por Autor.</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Autor</th>
                                        <th>Título do Livro</th>
                                        <th>Editora</th>
                                        <th>Edição</th>
                                        <th>Assunto</th>
                                        <th>Ano de Publicação</th>
                                        <th>Valor</th>
                                    </tr>");
            foreach (var book in bookrReport)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    <td>{6}</td>                                   
                                  </tr>", book.AuthorName, book.BookTitle, book.BookCompany, book.BookEdition
                                                , book.BookSubject, book.BookPublishYear, book.BookValue);
            }
            sb.Append(@"
                                </table>
                            </body>
                        </html>");
            return sb.ToString();
        }
    }
}

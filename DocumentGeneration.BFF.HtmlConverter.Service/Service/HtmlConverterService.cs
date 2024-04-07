using DocumentGeneration.BFF.Core.Interfaces;
using DocumentGeneration.BFF.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentGeneration.BFF.HtmlConverter.Service.Service
{
    internal class HtmlConverterService : IConvertToHtmlUsecase
    {
        public string ToHtml(documentBaseClass fileInfo)
        {
            StringBuilder htmlBuilder = new StringBuilder();

            // Start the HTML document
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html>");
            htmlBuilder.AppendLine("<head>");
            htmlBuilder.AppendLine("<title>" + fileInfo.Name + "</title>");
            htmlBuilder.AppendLine("<style>");
            htmlBuilder.AppendLine("body { font-family: Arial, sans-serif; }");
            htmlBuilder.AppendLine("table { border-collapse: collapse; width: 100%; }");
            htmlBuilder.AppendLine("th, td { border: 1px solid #dddddd; text-align: left; padding: 8px; }");
            htmlBuilder.AppendLine("th { background-color: #f2f2f2; }");
            htmlBuilder.AppendLine("</style>");
            htmlBuilder.AppendLine("</head>");
            htmlBuilder.AppendLine("<body>");

            // Add the class/struct/interface information
            htmlBuilder.AppendLine("<h1>" + fileInfo.Name + "</h1>");
            htmlBuilder.AppendLine("<p>Type: " + fileInfo.Type + "</p>");
            htmlBuilder.AppendLine("<p>Access Modifier: " + fileInfo.AccessModifier + "</p>");

            // Add the fields information
            htmlBuilder.AppendLine("<h2>Fields</h2>");
            htmlBuilder.AppendLine("<table>");
            htmlBuilder.AppendLine("<tr><th>Name</th><th>Type</th><th>Access Modifier</th></tr>");
            foreach (var field in fileInfo.Fields)
            {
                htmlBuilder.AppendLine("<tr><td>" + field.FielddName + "</td><td>" + field.Type + "</td><td>" + field.AccessModifier + "</td></tr>");
            }
            htmlBuilder.AppendLine("</table>");

            // Add the methods information
            htmlBuilder.AppendLine("<h2>Methods</h2>");
            htmlBuilder.AppendLine("<table>");
            htmlBuilder.AppendLine("<tr><th>Name</th><th>Type</th><th>Access Modifier</th><th>Parameters</th></tr>");
            foreach (var method in fileInfo.Methods)
            {
                htmlBuilder.AppendLine("<tr><td>" + method.MethodName + "</td><td>" + method.Type + "</td><td>" + method.AccessModifier + "</td><td>" + string.Join(", ", method.Parameters.Select(p => p.type + " " + p.name)) + "</td></tr>");
            }
            htmlBuilder.AppendLine("</table>");

            // Add the inheritance information
            htmlBuilder.AppendLine("<h2>Inherits From</h2>");
            htmlBuilder.AppendLine("<ul>");
            foreach (var inheritance in fileInfo.InheritsFrom)
            {
                htmlBuilder.AppendLine("<li>" + inheritance.name + " (" + inheritance.type + ")</li>");
            }
            htmlBuilder.AppendLine("</ul>");

            // Add the dependency information
            htmlBuilder.AppendLine("<h2>Dependencies</h2>");
            htmlBuilder.AppendLine("<ul>");
            foreach (var dependency in fileInfo.Dependency)
            {
                htmlBuilder.AppendLine("<li>" + dependency + "</li>");
            }
            htmlBuilder.AppendLine("</ul>");

            // End the HTML document
            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            return htmlBuilder.ToString();
        }
    }
}

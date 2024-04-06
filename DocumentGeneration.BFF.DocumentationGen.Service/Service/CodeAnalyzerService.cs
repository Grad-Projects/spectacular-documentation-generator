using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using DocumentGeneration.BFF.Core.Interfaces;
using System.Reflection.Metadata;


namespace DocumentGeneration.BFF.DocumentationGen.Service.Service
{
    internal class CodeAnalyzerService : IGenerateDocumentationUsecase 
    {

        public MemoryStream ConvertByteArrayToMemoryStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public string Analyze(string document)
        {
            byte[] bytes = Convert.FromBase64String(document);
            using (MemoryStream stream = new MemoryStream(bytes))
            using (StreamReader reader = new StreamReader(stream))
            {
                stream.Position = 0;

                // Parse the stream contents into a syntax tree
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(reader.ReadToEnd());

                // Extract class names, field names, properties, etc.
                var fieldDeclarations = syntaxTree.GetRoot()
                    .DescendantNodes()
                    .OfType<FieldDeclarationSyntax>()
                    .SelectMany(f => f.Declaration.Variables.Select(v => v.Identifier.Text))
                    .ToList();



                // Return extracted information
                return string.Join(", ", fieldDeclarations);
            }
        }
    }
}

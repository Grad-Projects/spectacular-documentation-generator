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


namespace DocumentGeneration.BFF.DocumentationGen.Service.Service
{
    internal class CodeAnalyzerService : IGenerateDocumentationUsecase 
    {

        public MemoryStream ConvertByteArrayToMemoryStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public string Analyze(byte[] bytes)
        {
            MemoryStream stream = ConvertByteArrayToMemoryStream(bytes);
            stream.Position = 0;

            // Parse the stream contents into a syntax tree
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(new StreamReader(stream).ReadToEnd());

            // Extract class names, field names, properties, etc.
            var classDeclarations = syntaxTree.GetRoot()
                .DescendantNodes()
                .OfType<ClassDeclarationSyntax>()
                .Select(c => c.Identifier.Text)
                .ToList();

            // Return extracted information
            return string.Join(", ", classDeclarations);
        }
    }
}

using documentationGeneratorTest.test1;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using System.Reflection;


namespace documentationgenerator
{
    public class Generator
    {
       

        static void Main(string[] args)
        {
            string filePath = @"put_file_path_here";
            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found.");
                return;
            }

            // Read the contents of the file
            string fileContents = File.ReadAllText(filePath);

            // Parse the C# code
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(fileContents);

            // Create a compilation
            CSharpCompilation compilation = CSharpCompilation.Create("MyCompilation")
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                .AddSyntaxTrees(syntaxTree);

            // Extract information
            foreach (var syntaxNode in syntaxTree.GetRoot().DescendantNodes())
            {
                if (syntaxNode is MethodDeclarationSyntax methodSyntax)
                {

                    
                    Console.WriteLine($"Method: {methodSyntax.Identifier}");
                    Console.WriteLine($"  Return Type: {methodSyntax.ReturnType}");
                    Console.WriteLine($"Method: {methodSyntax.Modifiers}");
                    foreach (var parameterSyntax in methodSyntax.ParameterList.Parameters)
                    {
                        Console.WriteLine($"  Parameter: {parameterSyntax.Identifier}");
                    }


                }else if (syntaxNode is FieldDeclarationSyntax fieldSyntax)
                {
                    foreach (var variable in fieldSyntax.Declaration.Variables)
                    {
                        Console.WriteLine($"Class Field: {variable.Identifier}");
                    }
                }
            }

            Console.WriteLine("Documentation extraction complete.");
        }
        
        
    }
    

}


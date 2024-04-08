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
using DocumentGeneration.BFF.Core.Models;
using System.Reflection.Metadata;
using System.Reflection;


namespace DocumentGeneration.BFF.DocumentationGen.Service.Service
{
    internal class CodeAnalyzerService
    {

        public MemoryStream ConvertByteArrayToMemoryStream(byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        public documentBaseClass Analyze(string document)
        {
            documentBaseClass codeInfo = new documentBaseClass();

            byte[] bytes = Convert.FromBase64String(document);
            using (MemoryStream stream = new MemoryStream(bytes))
            using (StreamReader reader = new StreamReader(stream))
            {
                stream.Position = 0;

                // Parse the stream contents into a syntax tree
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(reader.ReadToEnd());

                //general info
                var list = getEntityInfo(syntaxTree);
                codeInfo.Name = list[0].Name;
                codeInfo.Type = list[0].Type;
                codeInfo.AccessModifier = list[0].AccessModifier;

                //inheriteance info
                var inheritanceList = getInheritanceInfo(syntaxTree);
                codeInfo.InheritsFrom = inheritanceList;

                //dependency info
                var dependency = GetDependencies(syntaxTree);
                codeInfo.Dependency = dependency;

                //feild info
                var feildList = getFeildInfo(syntaxTree);
                codeInfo.Fields = feildList;

                //method info
                var methodList = getMethodInfo(syntaxTree);
                codeInfo.Methods = methodList;
            }

            // Return extracted information
            return codeInfo;
        }

        private List<(string Name, string Type, string AccessModifier)> getEntityInfo(SyntaxTree syntaxTree)
        {
            var workingList = new List<(string Name, string Type, string AccessModifier)>();
            foreach (var node in syntaxTree.GetRoot().DescendantNodesAndSelf().OfType<SyntaxNode>())
            {
                switch (node)
                {
                    case ClassDeclarationSyntax classSyntax:
                        workingList.Add((classSyntax.Identifier.Text, "Class", classSyntax.Modifiers.ToString()));
                        break;
                    case InterfaceDeclarationSyntax interfaceSyntax:
                        workingList.Add((interfaceSyntax.Identifier.Text, "Interface", interfaceSyntax.Modifiers.ToString()));
                        break;
                    case StructDeclarationSyntax structSyntax:
                        workingList.Add((structSyntax.Identifier.Text, "Struct", structSyntax.Modifiers.ToString()));
                        break;
                    default:
                        break;
                }
            }
            return workingList;
        }

        private List<(string name, string type)> getInheritanceInfo(SyntaxTree syntaxTree)
        {
            var workingList = new List<(string name, string type)>();
            foreach (var node in syntaxTree.GetRoot().DescendantNodesAndSelf().OfType<SyntaxNode>())
            {
                switch (node)
                {
                    case ClassDeclarationSyntax classNode:
                        if (classNode.BaseList != null && classNode.BaseList.Types.Count > 0)
                        {
                            foreach (var baseType in classNode.BaseList.Types)
                            {
                                workingList.Add((baseType.ToString(), baseType.Kind().ToString()));
                            }
                        }
                        break;

                    case StructDeclarationSyntax structNode:
                        if (structNode.BaseList != null && structNode.BaseList.Types.Count > 0)
                        {
                            foreach (var baseType in structNode.BaseList.Types)
                            {
                                workingList.Add((baseType.ToString(), baseType.Kind().ToString()));
                            }
                        }
                        break;

                    case InterfaceDeclarationSyntax interfaceNode:
                        if (interfaceNode.BaseList != null && interfaceNode.BaseList.Types.Count > 0)
                        {
                            foreach (var baseType in interfaceNode.BaseList.Types)
                            {
                                workingList.Add((baseType.ToString(), baseType.Kind().ToString()));
                            }
                        }
                        break;
                }
            } 
            
            return workingList;
        }

        private static List<string> GetDependencies(SyntaxTree syntaxTree)
        {
            var dependencies = new List<string>();
            var root = syntaxTree.GetRoot();

            foreach (var node in root.DescendantNodes().OfType<UsingDirectiveSyntax>())
            {
                var dependency = node.Name?.ToString();
                dependencies.Add(dependency);
            }

            return dependencies;
        }

        private List<documentFeildClass> getFeildInfo(SyntaxTree syntaxTree)
        {
            var workingList = new List<documentFeildClass>();
            foreach (var node in syntaxTree.GetRoot().DescendantNodesAndSelf().OfType<FieldDeclarationSyntax>())
            {
                foreach (var variable in node.Declaration.Variables)
                {
                    documentFeildClass currentFeild = new documentFeildClass();
                    currentFeild.FielddName = variable.Identifier.Text;
                    currentFeild.Type = node.Declaration.Type.ToString();
                    currentFeild.AccessModifier = node.Modifiers.ToString();

                    workingList.Add(currentFeild);
                }
            }

            return workingList;
        }

        private List<documentMethodClass> getMethodInfo(SyntaxTree syntaxTree)
        {
            var workingList = new List<documentMethodClass>();
            foreach (var node in syntaxTree.GetRoot().DescendantNodesAndSelf().OfType<MethodDeclarationSyntax>())
            {
                documentMethodClass currentMethod = new documentMethodClass();
                currentMethod.MethodName = node.Identifier.Text;
                currentMethod.Type = node.ReturnType.ToString();
                currentMethod.AccessModifier = node.Modifiers.ToString();

                var parameters = node.ParameterList.Parameters;
                foreach (var parameter in parameters)
                {
                    currentMethod.Parameters.Add((parameter.Type.ToString(), parameter.Identifier.Text));
                }

                workingList.Add(currentMethod);
            }


            return workingList;
        }
    }
}

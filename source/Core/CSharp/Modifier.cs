﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp.Comparers;
using Roslynator.CSharp.ModifierHelpers;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Roslynator.CSharp
{
    /// <summary>
    /// 
    /// </summary>
    public static class Modifier
    {
        internal static SyntaxNode Insert(SyntaxNode node, Accessibility accessibility, IModifierComparer comparer)
        {
            switch (accessibility)
            {
                case Accessibility.Private:
                    {
                        return Insert(node, SyntaxKind.PrivateKeyword, comparer);
                    }
                case Accessibility.Protected:
                    {
                        return Insert(node, SyntaxKind.ProtectedKeyword, comparer);
                    }
                case Accessibility.ProtectedAndInternal:
                    {
                        node = Insert(node, SyntaxKind.PrivateKeyword, comparer);

                        return Insert(node, SyntaxKind.ProtectedKeyword, comparer);
                    }
                case Accessibility.Internal:
                    {
                        return Insert(node, SyntaxKind.InternalKeyword, comparer);
                    }
                case Accessibility.Public:
                    {
                        return Insert(node, SyntaxKind.PublicKeyword, comparer);
                    }
                case Accessibility.ProtectedOrInternal:
                    {
                        node = Insert(node, SyntaxKind.ProtectedKeyword, comparer);

                        return Insert(node, SyntaxKind.InternalKeyword, comparer);
                    }
            }

            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static TNode Insert<TNode>(TNode node, SyntaxKind modifierKind, IModifierComparer comparer = null) where TNode : SyntaxNode
        {
            return Insert(node, Token(modifierKind), comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static TNode Insert<TNode>(TNode node, SyntaxToken modifier, IModifierComparer comparer = null) where TNode : SyntaxNode
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    return (TNode)(SyntaxNode)Insert((ClassDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.ConstructorDeclaration:
                    return (TNode)(SyntaxNode)Insert((ConstructorDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.ConversionOperatorDeclaration:
                    return (TNode)(SyntaxNode)Insert((ConversionOperatorDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.DelegateDeclaration:
                    return (TNode)(SyntaxNode)Insert((DelegateDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.DestructorDeclaration:
                    return (TNode)(SyntaxNode)Insert((DestructorDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.EnumDeclaration:
                    return (TNode)(SyntaxNode)Insert((EnumDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.EventDeclaration:
                    return (TNode)(SyntaxNode)Insert((EventDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.EventFieldDeclaration:
                    return (TNode)(SyntaxNode)Insert((EventFieldDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.FieldDeclaration:
                    return (TNode)(SyntaxNode)Insert((FieldDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.IndexerDeclaration:
                    return (TNode)(SyntaxNode)Insert((IndexerDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.InterfaceDeclaration:
                    return (TNode)(SyntaxNode)Insert((InterfaceDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.MethodDeclaration:
                    return (TNode)(SyntaxNode)Insert((MethodDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.OperatorDeclaration:
                    return (TNode)(SyntaxNode)Insert((OperatorDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.PropertyDeclaration:
                    return (TNode)(SyntaxNode)Insert((PropertyDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.StructDeclaration:
                    return (TNode)(SyntaxNode)Insert((StructDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    return (TNode)(SyntaxNode)Insert((AccessorDeclarationSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.LocalDeclarationStatement:
                    return (TNode)(SyntaxNode)Insert((LocalDeclarationStatementSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.LocalFunctionStatement:
                    return (TNode)(SyntaxNode)Insert((LocalFunctionStatementSyntax)(SyntaxNode)node, modifier, comparer);
                case SyntaxKind.Parameter:
                    return (TNode)(SyntaxNode)Insert((ParameterSyntax)(SyntaxNode)node, modifier, comparer);
            }

            Debug.Assert(node.IsKind(SyntaxKind.IncompleteMember), node.ToString());

            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static TNode Remove<TNode>(TNode node, SyntaxKind modifierKind) where TNode : SyntaxNode
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    return (TNode)(SyntaxNode)Remove((ClassDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.ConstructorDeclaration:
                    return (TNode)(SyntaxNode)Remove((ConstructorDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.ConversionOperatorDeclaration:
                    return (TNode)(SyntaxNode)Remove((ConversionOperatorDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.DelegateDeclaration:
                    return (TNode)(SyntaxNode)Remove((DelegateDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.DestructorDeclaration:
                    return (TNode)(SyntaxNode)Remove((DestructorDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.EnumDeclaration:
                    return (TNode)(SyntaxNode)Remove((EnumDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.EventDeclaration:
                    return (TNode)(SyntaxNode)Remove((EventDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.EventFieldDeclaration:
                    return (TNode)(SyntaxNode)Remove((EventFieldDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.FieldDeclaration:
                    return (TNode)(SyntaxNode)Remove((FieldDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.IndexerDeclaration:
                    return (TNode)(SyntaxNode)Remove((IndexerDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.InterfaceDeclaration:
                    return (TNode)(SyntaxNode)Remove((InterfaceDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.MethodDeclaration:
                    return (TNode)(SyntaxNode)Remove((MethodDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.OperatorDeclaration:
                    return (TNode)(SyntaxNode)Remove((OperatorDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.PropertyDeclaration:
                    return (TNode)(SyntaxNode)Remove((PropertyDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.StructDeclaration:
                    return (TNode)(SyntaxNode)Remove((StructDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    return (TNode)(SyntaxNode)Remove((AccessorDeclarationSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.LocalDeclarationStatement:
                    return (TNode)(SyntaxNode)Remove((LocalDeclarationStatementSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.LocalFunctionStatement:
                    return (TNode)(SyntaxNode)Remove((LocalFunctionStatementSyntax)(SyntaxNode)node, modifierKind);
                case SyntaxKind.Parameter:
                    return (TNode)(SyntaxNode)Remove((ParameterSyntax)(SyntaxNode)node, modifierKind);
            }

            Debug.Assert(node.IsKind(SyntaxKind.IncompleteMember), node.ToString());

            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static TNode Remove<TNode>(TNode node, SyntaxToken modifier) where TNode : SyntaxNode
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    return (TNode)(SyntaxNode)Remove((ClassDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.ConstructorDeclaration:
                    return (TNode)(SyntaxNode)Remove((ConstructorDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.ConversionOperatorDeclaration:
                    return (TNode)(SyntaxNode)Remove((ConversionOperatorDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.DelegateDeclaration:
                    return (TNode)(SyntaxNode)Remove((DelegateDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.DestructorDeclaration:
                    return (TNode)(SyntaxNode)Remove((DestructorDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.EnumDeclaration:
                    return (TNode)(SyntaxNode)Remove((EnumDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.EventDeclaration:
                    return (TNode)(SyntaxNode)Remove((EventDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.EventFieldDeclaration:
                    return (TNode)(SyntaxNode)Remove((EventFieldDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.FieldDeclaration:
                    return (TNode)(SyntaxNode)Remove((FieldDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.IndexerDeclaration:
                    return (TNode)(SyntaxNode)Remove((IndexerDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.InterfaceDeclaration:
                    return (TNode)(SyntaxNode)Remove((InterfaceDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.MethodDeclaration:
                    return (TNode)(SyntaxNode)Remove((MethodDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.OperatorDeclaration:
                    return (TNode)(SyntaxNode)Remove((OperatorDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.PropertyDeclaration:
                    return (TNode)(SyntaxNode)Remove((PropertyDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.StructDeclaration:
                    return (TNode)(SyntaxNode)Remove((StructDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    return (TNode)(SyntaxNode)Remove((AccessorDeclarationSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.LocalDeclarationStatement:
                    return (TNode)(SyntaxNode)Remove((LocalDeclarationStatementSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.LocalFunctionStatement:
                    return (TNode)(SyntaxNode)Remove((LocalFunctionStatementSyntax)(SyntaxNode)node, modifier);
                case SyntaxKind.Parameter:
                    return (TNode)(SyntaxNode)Remove((ParameterSyntax)(SyntaxNode)node, modifier);
            }

            Debug.Assert(node.IsKind(SyntaxKind.IncompleteMember), node.ToString());

            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static TNode RemoveAt<TNode>(TNode node, int index) where TNode : SyntaxNode
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((ClassDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.ConstructorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((ConstructorDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.ConversionOperatorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((ConversionOperatorDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.DelegateDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((DelegateDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.DestructorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((DestructorDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.EnumDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((EnumDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.EventDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((EventDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.EventFieldDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((EventFieldDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.FieldDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((FieldDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.IndexerDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((IndexerDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.InterfaceDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((InterfaceDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.MethodDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((MethodDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.OperatorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((OperatorDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.PropertyDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((PropertyDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.StructDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((StructDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAt((AccessorDeclarationSyntax)(SyntaxNode)node, index);
                case SyntaxKind.LocalDeclarationStatement:
                    return (TNode)(SyntaxNode)RemoveAt((LocalDeclarationStatementSyntax)(SyntaxNode)node, index);
                case SyntaxKind.LocalFunctionStatement:
                    return (TNode)(SyntaxNode)RemoveAt((LocalFunctionStatementSyntax)(SyntaxNode)node, index);
                case SyntaxKind.Parameter:
                    return (TNode)(SyntaxNode)RemoveAt((ParameterSyntax)(SyntaxNode)node, index);
            }

            Debug.Assert(node.IsKind(SyntaxKind.IncompleteMember), node.ToString());

            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static TNode RemoveAccessibility<TNode>(TNode node) where TNode : SyntaxNode
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((ClassDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.ConstructorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((ConstructorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.ConversionOperatorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((ConversionOperatorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.DelegateDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((DelegateDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.DestructorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((DestructorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.EnumDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((EnumDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.EventDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((EventDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.EventFieldDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((EventFieldDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.FieldDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((FieldDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.IndexerDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((IndexerDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.InterfaceDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((InterfaceDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.MethodDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((MethodDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.OperatorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((OperatorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.PropertyDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((PropertyDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.StructDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((StructDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAccessibility((AccessorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.LocalDeclarationStatement:
                    return (TNode)(SyntaxNode)RemoveAccessibility((LocalDeclarationStatementSyntax)(SyntaxNode)node);
                case SyntaxKind.LocalFunctionStatement:
                    return (TNode)(SyntaxNode)RemoveAccessibility((LocalFunctionStatementSyntax)(SyntaxNode)node);
                case SyntaxKind.Parameter:
                    return (TNode)(SyntaxNode)RemoveAccessibility((ParameterSyntax)(SyntaxNode)node);
            }

            Debug.Assert(node.IsKind(SyntaxKind.IncompleteMember), node.ToString());

            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static TNode RemoveAll<TNode>(TNode node) where TNode : SyntaxNode
        {
            switch (node.Kind())
            {
                case SyntaxKind.ClassDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((ClassDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.ConstructorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((ConstructorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.ConversionOperatorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((ConversionOperatorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.DelegateDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((DelegateDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.DestructorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((DestructorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.EnumDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((EnumDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.EventDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((EventDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.EventFieldDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((EventFieldDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.FieldDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((FieldDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.IndexerDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((IndexerDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.InterfaceDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((InterfaceDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.MethodDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((MethodDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.OperatorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((OperatorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.PropertyDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((PropertyDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.StructDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((StructDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                case SyntaxKind.UnknownAccessorDeclaration:
                    return (TNode)(SyntaxNode)RemoveAll((AccessorDeclarationSyntax)(SyntaxNode)node);
                case SyntaxKind.LocalDeclarationStatement:
                    return (TNode)(SyntaxNode)RemoveAll((LocalDeclarationStatementSyntax)(SyntaxNode)node);
                case SyntaxKind.LocalFunctionStatement:
                    return (TNode)(SyntaxNode)RemoveAll((LocalFunctionStatementSyntax)(SyntaxNode)node);
                case SyntaxKind.Parameter:
                    return (TNode)(SyntaxNode)RemoveAll((ParameterSyntax)(SyntaxNode)node);
            }

            Debug.Assert(node.IsKind(SyntaxKind.IncompleteMember), node.ToString());

            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax Insert(ClassDeclarationSyntax classDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return ClassDeclarationModifierHelper.Instance.InsertModifier(classDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax Insert(ClassDeclarationSyntax classDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return ClassDeclarationModifierHelper.Instance.InsertModifier(classDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax Insert(ConstructorDeclarationSyntax constructorDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return ConstructorDeclarationModifierHelper.Instance.InsertModifier(constructorDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax Insert(ConstructorDeclarationSyntax constructorDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return ConstructorDeclarationModifierHelper.Instance.InsertModifier(constructorDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax Insert(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.InsertModifier(conversionOperatorDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax Insert(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.InsertModifier(conversionOperatorDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax Insert(DelegateDeclarationSyntax delegateDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return DelegateDeclarationModifierHelper.Instance.InsertModifier(delegateDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax Insert(DelegateDeclarationSyntax delegateDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return DelegateDeclarationModifierHelper.Instance.InsertModifier(delegateDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax Insert(DestructorDeclarationSyntax destructorDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return DestructorDeclarationModifierHelper.Instance.InsertModifier(destructorDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax Insert(DestructorDeclarationSyntax destructorDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return DestructorDeclarationModifierHelper.Instance.InsertModifier(destructorDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax Insert(EnumDeclarationSyntax enumDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return EnumDeclarationModifierHelper.Instance.InsertModifier(enumDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax Insert(EnumDeclarationSyntax enumDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return EnumDeclarationModifierHelper.Instance.InsertModifier(enumDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax Insert(EventDeclarationSyntax eventDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return EventDeclarationModifierHelper.Instance.InsertModifier(eventDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax Insert(EventDeclarationSyntax eventDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return EventDeclarationModifierHelper.Instance.InsertModifier(eventDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax Insert(EventFieldDeclarationSyntax eventFieldDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return EventFieldDeclarationModifierHelper.Instance.InsertModifier(eventFieldDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax Insert(EventFieldDeclarationSyntax eventFieldDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return EventFieldDeclarationModifierHelper.Instance.InsertModifier(eventFieldDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax Insert(FieldDeclarationSyntax fieldDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return FieldDeclarationModifierHelper.Instance.InsertModifier(fieldDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax Insert(FieldDeclarationSyntax fieldDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return FieldDeclarationModifierHelper.Instance.InsertModifier(fieldDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax Insert(IndexerDeclarationSyntax indexerDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return IndexerDeclarationModifierHelper.Instance.InsertModifier(indexerDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax Insert(IndexerDeclarationSyntax indexerDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return IndexerDeclarationModifierHelper.Instance.InsertModifier(indexerDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax Insert(InterfaceDeclarationSyntax interfaceDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return InterfaceDeclarationModifierHelper.Instance.InsertModifier(interfaceDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax Insert(InterfaceDeclarationSyntax interfaceDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return InterfaceDeclarationModifierHelper.Instance.InsertModifier(interfaceDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax Insert(MethodDeclarationSyntax methodDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return MethodDeclarationModifierHelper.Instance.InsertModifier(methodDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax Insert(MethodDeclarationSyntax methodDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return MethodDeclarationModifierHelper.Instance.InsertModifier(methodDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax Insert(OperatorDeclarationSyntax operatorDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return OperatorDeclarationModifierHelper.Instance.InsertModifier(operatorDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax Insert(OperatorDeclarationSyntax operatorDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return OperatorDeclarationModifierHelper.Instance.InsertModifier(operatorDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax Insert(PropertyDeclarationSyntax propertyDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return PropertyDeclarationModifierHelper.Instance.InsertModifier(propertyDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax Insert(PropertyDeclarationSyntax propertyDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return PropertyDeclarationModifierHelper.Instance.InsertModifier(propertyDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax Insert(StructDeclarationSyntax structDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return StructDeclarationModifierHelper.Instance.InsertModifier(structDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax Insert(StructDeclarationSyntax structDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return StructDeclarationModifierHelper.Instance.InsertModifier(structDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax Insert(AccessorDeclarationSyntax accessorDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return AccessorDeclarationModifierHelper.Instance.InsertModifier(accessorDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax Insert(AccessorDeclarationSyntax accessorDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return AccessorDeclarationModifierHelper.Instance.InsertModifier(accessorDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax Insert(LocalDeclarationStatementSyntax localDeclaration, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return LocalDeclarationStatementModifierHelper.Instance.InsertModifier(localDeclaration, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax Insert(LocalDeclarationStatementSyntax localDeclaration, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return LocalDeclarationStatementModifierHelper.Instance.InsertModifier(localDeclaration, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localFunction"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax Insert(LocalFunctionStatementSyntax localFunction, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return LocalFunctionStatementModifierHelper.Instance.InsertModifier(localFunction, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localFunction"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax Insert(LocalFunctionStatementSyntax localFunction, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return LocalFunctionStatementModifierHelper.Instance.InsertModifier(localFunction, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ParameterSyntax Insert(ParameterSyntax parameter, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            return ParameterModifierHelper.Instance.InsertModifier(parameter, modifier, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static ParameterSyntax Insert(ParameterSyntax parameter, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return ParameterModifierHelper.Instance.InsertModifier(parameter, modifierKind, comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="modifierKind"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static SyntaxTokenList Insert(SyntaxTokenList modifiers, SyntaxKind modifierKind, IModifierComparer comparer = null)
        {
            return Insert(modifiers, Token(modifierKind), comparer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifiers"></param>
        /// <param name="modifier"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static SyntaxTokenList Insert(SyntaxTokenList modifiers, SyntaxToken modifier, IModifierComparer comparer = null)
        {
            int index = 0;

            if (modifiers.Any())
            {
                index = (comparer ?? ModifierComparer.Instance).GetInsertIndex(modifiers, modifier);

                if (index == 0)
                {
                    SyntaxToken firstModifier = modifiers[index];

                    SyntaxTriviaList trivia = firstModifier.LeadingTrivia;

                    if (trivia.Any())
                    {
                        SyntaxTriviaList leadingTrivia = modifier.LeadingTrivia;

                        if (!leadingTrivia.IsSingleElasticMarker())
                            trivia = trivia.AddRange(leadingTrivia);

                        modifier = modifier.WithLeadingTrivia(trivia);

                        modifiers = modifiers.ReplaceAt(index, firstModifier.WithoutLeadingTrivia());
                    }
                }
            }

            return modifiers.Insert(index, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax Remove(ClassDeclarationSyntax classDeclaration, SyntaxToken modifier)
        {
            return ClassDeclarationModifierHelper.Instance.RemoveModifier(classDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax Remove(ClassDeclarationSyntax classDeclaration, SyntaxKind modifierKind)
        {
            return ClassDeclarationModifierHelper.Instance.RemoveModifier(classDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax Remove(ConstructorDeclarationSyntax constructorDeclaration, SyntaxToken modifier)
        {
            return ConstructorDeclarationModifierHelper.Instance.RemoveModifier(constructorDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax Remove(ConstructorDeclarationSyntax constructorDeclaration, SyntaxKind modifierKind)
        {
            return ConstructorDeclarationModifierHelper.Instance.RemoveModifier(constructorDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax Remove(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration, SyntaxToken modifier)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.RemoveModifier(conversionOperatorDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax Remove(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration, SyntaxKind modifierKind)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.RemoveModifier(conversionOperatorDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax Remove(DelegateDeclarationSyntax delegateDeclaration, SyntaxToken modifier)
        {
            return DelegateDeclarationModifierHelper.Instance.RemoveModifier(delegateDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax Remove(DelegateDeclarationSyntax delegateDeclaration, SyntaxKind modifierKind)
        {
            return DelegateDeclarationModifierHelper.Instance.RemoveModifier(delegateDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax Remove(DestructorDeclarationSyntax destructorDeclaration, SyntaxToken modifier)
        {
            return DestructorDeclarationModifierHelper.Instance.RemoveModifier(destructorDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax Remove(DestructorDeclarationSyntax destructorDeclaration, SyntaxKind modifierKind)
        {
            return DestructorDeclarationModifierHelper.Instance.RemoveModifier(destructorDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax Remove(EnumDeclarationSyntax enumDeclaration, SyntaxToken modifier)
        {
            return EnumDeclarationModifierHelper.Instance.RemoveModifier(enumDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax Remove(EnumDeclarationSyntax enumDeclaration, SyntaxKind modifierKind)
        {
            return EnumDeclarationModifierHelper.Instance.RemoveModifier(enumDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax Remove(EventDeclarationSyntax eventDeclaration, SyntaxToken modifier)
        {
            return EventDeclarationModifierHelper.Instance.RemoveModifier(eventDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax Remove(EventDeclarationSyntax eventDeclaration, SyntaxKind modifierKind)
        {
            return EventDeclarationModifierHelper.Instance.RemoveModifier(eventDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax Remove(EventFieldDeclarationSyntax eventFieldDeclaration, SyntaxToken modifier)
        {
            return EventFieldDeclarationModifierHelper.Instance.RemoveModifier(eventFieldDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax Remove(EventFieldDeclarationSyntax eventFieldDeclaration, SyntaxKind modifierKind)
        {
            return EventFieldDeclarationModifierHelper.Instance.RemoveModifier(eventFieldDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax Remove(FieldDeclarationSyntax fieldDeclaration, SyntaxToken modifier)
        {
            return FieldDeclarationModifierHelper.Instance.RemoveModifier(fieldDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax Remove(FieldDeclarationSyntax fieldDeclaration, SyntaxKind modifierKind)
        {
            return FieldDeclarationModifierHelper.Instance.RemoveModifier(fieldDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax Remove(IndexerDeclarationSyntax indexerDeclaration, SyntaxToken modifier)
        {
            return IndexerDeclarationModifierHelper.Instance.RemoveModifier(indexerDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax Remove(IndexerDeclarationSyntax indexerDeclaration, SyntaxKind modifierKind)
        {
            return IndexerDeclarationModifierHelper.Instance.RemoveModifier(indexerDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax Remove(InterfaceDeclarationSyntax interfaceDeclaration, SyntaxToken modifier)
        {
            return InterfaceDeclarationModifierHelper.Instance.RemoveModifier(interfaceDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax Remove(InterfaceDeclarationSyntax interfaceDeclaration, SyntaxKind modifierKind)
        {
            return InterfaceDeclarationModifierHelper.Instance.RemoveModifier(interfaceDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax Remove(MethodDeclarationSyntax methodDeclaration, SyntaxToken modifier)
        {
            return MethodDeclarationModifierHelper.Instance.RemoveModifier(methodDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax Remove(MethodDeclarationSyntax methodDeclaration, SyntaxKind modifierKind)
        {
            return MethodDeclarationModifierHelper.Instance.RemoveModifier(methodDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax Remove(OperatorDeclarationSyntax operatorDeclaration, SyntaxToken modifier)
        {
            return OperatorDeclarationModifierHelper.Instance.RemoveModifier(operatorDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax Remove(OperatorDeclarationSyntax operatorDeclaration, SyntaxKind modifierKind)
        {
            return OperatorDeclarationModifierHelper.Instance.RemoveModifier(operatorDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax Remove(PropertyDeclarationSyntax propertyDeclaration, SyntaxToken modifier)
        {
            return PropertyDeclarationModifierHelper.Instance.RemoveModifier(propertyDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax Remove(PropertyDeclarationSyntax propertyDeclaration, SyntaxKind modifierKind)
        {
            return PropertyDeclarationModifierHelper.Instance.RemoveModifier(propertyDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax Remove(StructDeclarationSyntax structDeclaration, SyntaxToken modifier)
        {
            return StructDeclarationModifierHelper.Instance.RemoveModifier(structDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax Remove(StructDeclarationSyntax structDeclaration, SyntaxKind modifierKind)
        {
            return StructDeclarationModifierHelper.Instance.RemoveModifier(structDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax Remove(AccessorDeclarationSyntax accessorDeclaration, SyntaxToken modifier)
        {
            return AccessorDeclarationModifierHelper.Instance.RemoveModifier(accessorDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax Remove(AccessorDeclarationSyntax accessorDeclaration, SyntaxKind modifierKind)
        {
            return AccessorDeclarationModifierHelper.Instance.RemoveModifier(accessorDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax Remove(LocalDeclarationStatementSyntax localDeclaration, SyntaxToken modifier)
        {
            return LocalDeclarationStatementModifierHelper.Instance.RemoveModifier(localDeclaration, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax Remove(LocalDeclarationStatementSyntax localDeclaration, SyntaxKind modifierKind)
        {
            return LocalDeclarationStatementModifierHelper.Instance.RemoveModifier(localDeclaration, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localFunction"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax Remove(LocalFunctionStatementSyntax localFunction, SyntaxToken modifier)
        {
            return LocalFunctionStatementModifierHelper.Instance.RemoveModifier(localFunction, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localFunction"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax Remove(LocalFunctionStatementSyntax localFunction, SyntaxKind modifierKind)
        {
            return LocalFunctionStatementModifierHelper.Instance.RemoveModifier(localFunction, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public static ParameterSyntax Remove(ParameterSyntax parameter, SyntaxToken modifier)
        {
            return ParameterModifierHelper.Instance.RemoveModifier(parameter, modifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="modifierKind"></param>
        /// <returns></returns>
        public static ParameterSyntax Remove(ParameterSyntax parameter, SyntaxKind modifierKind)
        {
            return ParameterModifierHelper.Instance.RemoveModifier(parameter, modifierKind);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax RemoveAt(ClassDeclarationSyntax classDeclaration, int index)
        {
            return ClassDeclarationModifierHelper.Instance.RemoveModifierAt(classDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax RemoveAt(ConstructorDeclarationSyntax constructorDeclaration, int index)
        {
            return ConstructorDeclarationModifierHelper.Instance.RemoveModifierAt(constructorDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax RemoveAt(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration, int index)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.RemoveModifierAt(conversionOperatorDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax RemoveAt(DelegateDeclarationSyntax delegateDeclaration, int index)
        {
            return DelegateDeclarationModifierHelper.Instance.RemoveModifierAt(delegateDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax RemoveAt(DestructorDeclarationSyntax destructorDeclaration, int index)
        {
            return DestructorDeclarationModifierHelper.Instance.RemoveModifierAt(destructorDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax RemoveAt(EnumDeclarationSyntax enumDeclaration, int index)
        {
            return EnumDeclarationModifierHelper.Instance.RemoveModifierAt(enumDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax RemoveAt(EventDeclarationSyntax eventDeclaration, int index)
        {
            return EventDeclarationModifierHelper.Instance.RemoveModifierAt(eventDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax RemoveAt(EventFieldDeclarationSyntax eventFieldDeclaration, int index)
        {
            return EventFieldDeclarationModifierHelper.Instance.RemoveModifierAt(eventFieldDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax RemoveAt(FieldDeclarationSyntax fieldDeclaration, int index)
        {
            return FieldDeclarationModifierHelper.Instance.RemoveModifierAt(fieldDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax RemoveAt(IndexerDeclarationSyntax indexerDeclaration, int index)
        {
            return IndexerDeclarationModifierHelper.Instance.RemoveModifierAt(indexerDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax RemoveAt(InterfaceDeclarationSyntax interfaceDeclaration, int index)
        {
            return InterfaceDeclarationModifierHelper.Instance.RemoveModifierAt(interfaceDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax RemoveAt(MethodDeclarationSyntax methodDeclaration, int index)
        {
            return MethodDeclarationModifierHelper.Instance.RemoveModifierAt(methodDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax RemoveAt(OperatorDeclarationSyntax operatorDeclaration, int index)
        {
            return OperatorDeclarationModifierHelper.Instance.RemoveModifierAt(operatorDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax RemoveAt(PropertyDeclarationSyntax propertyDeclaration, int index)
        {
            return PropertyDeclarationModifierHelper.Instance.RemoveModifierAt(propertyDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax RemoveAt(StructDeclarationSyntax structDeclaration, int index)
        {
            return StructDeclarationModifierHelper.Instance.RemoveModifierAt(structDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax RemoveAt(AccessorDeclarationSyntax accessorDeclaration, int index)
        {
            return AccessorDeclarationModifierHelper.Instance.RemoveModifierAt(accessorDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax RemoveAt(LocalDeclarationStatementSyntax localDeclaration, int index)
        {
            return LocalDeclarationStatementModifierHelper.Instance.RemoveModifierAt(localDeclaration, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localFunction"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax RemoveAt(LocalFunctionStatementSyntax localFunction, int index)
        {
            return LocalFunctionStatementModifierHelper.Instance.RemoveModifierAt(localFunction, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ParameterSyntax RemoveAt(ParameterSyntax parameter, int index)
        {
            return ParameterModifierHelper.Instance.RemoveModifierAt(parameter, index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax RemoveAccessibility(ClassDeclarationSyntax classDeclaration)
        {
            return ClassDeclarationModifierHelper.Instance.RemoveAccessibility(classDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax RemoveAccessibility(ConstructorDeclarationSyntax constructorDeclaration)
        {
            return ConstructorDeclarationModifierHelper.Instance.RemoveAccessibility(constructorDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax RemoveAccessibility(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.RemoveAccessibility(conversionOperatorDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax RemoveAccessibility(DelegateDeclarationSyntax delegateDeclaration)
        {
            return DelegateDeclarationModifierHelper.Instance.RemoveAccessibility(delegateDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax RemoveAccessibility(DestructorDeclarationSyntax destructorDeclaration)
        {
            return DestructorDeclarationModifierHelper.Instance.RemoveAccessibility(destructorDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax RemoveAccessibility(EnumDeclarationSyntax enumDeclaration)
        {
            return EnumDeclarationModifierHelper.Instance.RemoveAccessibility(enumDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax RemoveAccessibility(EventDeclarationSyntax eventDeclaration)
        {
            return EventDeclarationModifierHelper.Instance.RemoveAccessibility(eventDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax RemoveAccessibility(EventFieldDeclarationSyntax eventFieldDeclaration)
        {
            return EventFieldDeclarationModifierHelper.Instance.RemoveAccessibility(eventFieldDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax RemoveAccessibility(FieldDeclarationSyntax fieldDeclaration)
        {
            return FieldDeclarationModifierHelper.Instance.RemoveAccessibility(fieldDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax RemoveAccessibility(IndexerDeclarationSyntax indexerDeclaration)
        {
            return IndexerDeclarationModifierHelper.Instance.RemoveAccessibility(indexerDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax RemoveAccessibility(InterfaceDeclarationSyntax interfaceDeclaration)
        {
            return InterfaceDeclarationModifierHelper.Instance.RemoveAccessibility(interfaceDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax RemoveAccessibility(MethodDeclarationSyntax methodDeclaration)
        {
            return MethodDeclarationModifierHelper.Instance.RemoveAccessibility(methodDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax RemoveAccessibility(OperatorDeclarationSyntax operatorDeclaration)
        {
            return OperatorDeclarationModifierHelper.Instance.RemoveAccessibility(operatorDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax RemoveAccessibility(PropertyDeclarationSyntax propertyDeclaration)
        {
            return PropertyDeclarationModifierHelper.Instance.RemoveAccessibility(propertyDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax RemoveAccessibility(StructDeclarationSyntax structDeclaration)
        {
            return StructDeclarationModifierHelper.Instance.RemoveAccessibility(structDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax RemoveAccessibility(AccessorDeclarationSyntax accessorDeclaration)
        {
            return AccessorDeclarationModifierHelper.Instance.RemoveAccessibility(accessorDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax RemoveAccessibility(LocalDeclarationStatementSyntax localDeclaration)
        {
            return LocalDeclarationStatementModifierHelper.Instance.RemoveAccessibility(localDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localFunction"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax RemoveAccessibility(LocalFunctionStatementSyntax localFunction)
        {
            return LocalFunctionStatementModifierHelper.Instance.RemoveAccessibility(localFunction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static ParameterSyntax RemoveAccessibility(ParameterSyntax parameter)
        {
            return ParameterModifierHelper.Instance.RemoveAccessibility(parameter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classDeclaration"></param>
        /// <returns></returns>
        public static ClassDeclarationSyntax RemoveAll(ClassDeclarationSyntax classDeclaration)
        {
            return ClassDeclarationModifierHelper.Instance.RemoveModifiers(classDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constructorDeclaration"></param>
        /// <returns></returns>
        public static ConstructorDeclarationSyntax RemoveAll(ConstructorDeclarationSyntax constructorDeclaration)
        {
            return ConstructorDeclarationModifierHelper.Instance.RemoveModifiers(constructorDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversionOperatorDeclaration"></param>
        /// <returns></returns>
        public static ConversionOperatorDeclarationSyntax RemoveAll(ConversionOperatorDeclarationSyntax conversionOperatorDeclaration)
        {
            return ConversionOperatorDeclarationModifierHelper.Instance.RemoveModifiers(conversionOperatorDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delegateDeclaration"></param>
        /// <returns></returns>
        public static DelegateDeclarationSyntax RemoveAll(DelegateDeclarationSyntax delegateDeclaration)
        {
            return DelegateDeclarationModifierHelper.Instance.RemoveModifiers(delegateDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destructorDeclaration"></param>
        /// <returns></returns>
        public static DestructorDeclarationSyntax RemoveAll(DestructorDeclarationSyntax destructorDeclaration)
        {
            return DestructorDeclarationModifierHelper.Instance.RemoveModifiers(destructorDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumDeclaration"></param>
        /// <returns></returns>
        public static EnumDeclarationSyntax RemoveAll(EnumDeclarationSyntax enumDeclaration)
        {
            return EnumDeclarationModifierHelper.Instance.RemoveModifiers(enumDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventDeclaration"></param>
        /// <returns></returns>
        public static EventDeclarationSyntax RemoveAll(EventDeclarationSyntax eventDeclaration)
        {
            return EventDeclarationModifierHelper.Instance.RemoveModifiers(eventDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventFieldDeclaration"></param>
        /// <returns></returns>
        public static EventFieldDeclarationSyntax RemoveAll(EventFieldDeclarationSyntax eventFieldDeclaration)
        {
            return EventFieldDeclarationModifierHelper.Instance.RemoveModifiers(eventFieldDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldDeclaration"></param>
        /// <returns></returns>
        public static FieldDeclarationSyntax RemoveAll(FieldDeclarationSyntax fieldDeclaration)
        {
            return FieldDeclarationModifierHelper.Instance.RemoveModifiers(fieldDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexerDeclaration"></param>
        /// <returns></returns>
        public static IndexerDeclarationSyntax RemoveAll(IndexerDeclarationSyntax indexerDeclaration)
        {
            return IndexerDeclarationModifierHelper.Instance.RemoveModifiers(indexerDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceDeclaration"></param>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax RemoveAll(InterfaceDeclarationSyntax interfaceDeclaration)
        {
            return InterfaceDeclarationModifierHelper.Instance.RemoveModifiers(interfaceDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodDeclaration"></param>
        /// <returns></returns>
        public static MethodDeclarationSyntax RemoveAll(MethodDeclarationSyntax methodDeclaration)
        {
            return MethodDeclarationModifierHelper.Instance.RemoveModifiers(methodDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operatorDeclaration"></param>
        /// <returns></returns>
        public static OperatorDeclarationSyntax RemoveAll(OperatorDeclarationSyntax operatorDeclaration)
        {
            return OperatorDeclarationModifierHelper.Instance.RemoveModifiers(operatorDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyDeclaration"></param>
        /// <returns></returns>
        public static PropertyDeclarationSyntax RemoveAll(PropertyDeclarationSyntax propertyDeclaration)
        {
            return PropertyDeclarationModifierHelper.Instance.RemoveModifiers(propertyDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="structDeclaration"></param>
        /// <returns></returns>
        public static StructDeclarationSyntax RemoveAll(StructDeclarationSyntax structDeclaration)
        {
            return StructDeclarationModifierHelper.Instance.RemoveModifiers(structDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessorDeclaration"></param>
        /// <returns></returns>
        public static AccessorDeclarationSyntax RemoveAll(AccessorDeclarationSyntax accessorDeclaration)
        {
            return AccessorDeclarationModifierHelper.Instance.RemoveModifiers(accessorDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localDeclaration"></param>
        /// <returns></returns>
        public static LocalDeclarationStatementSyntax RemoveAll(LocalDeclarationStatementSyntax localDeclaration)
        {
            return LocalDeclarationStatementModifierHelper.Instance.RemoveModifiers(localDeclaration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localFunction"></param>
        /// <returns></returns>
        public static LocalFunctionStatementSyntax RemoveAll(LocalFunctionStatementSyntax localFunction)
        {
            return LocalFunctionStatementModifierHelper.Instance.RemoveModifiers(localFunction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static ParameterSyntax RemoveAll(ParameterSyntax parameter)
        {
            return ParameterModifierHelper.Instance.RemoveModifiers(parameter);
        }
    }
}

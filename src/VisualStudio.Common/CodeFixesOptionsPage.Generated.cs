﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

// <auto-generated>

using Roslynator.CSharp;

namespace Roslynator.VisualStudio
{
    public partial class CodeFixesOptionsPage
    {
        protected override string DisabledByDefault
        {
            get;
        }

        = $"{CodeFixIdentifiers.RemoveReturnExpression},{CodeFixIdentifiers.RemoveReturnKeyword}";
        protected override string MaxId
        {
            get;
        }

        = CodeFixIdentifiers.ReplaceInvocationWithMemberAccessOrViceVersa;
    }
}
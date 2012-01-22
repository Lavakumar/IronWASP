//
// Copyright 2011-2012 Lavakumar Kuppan
//
// This file is part of IronWASP
//
// IronWASP is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, version 3 of the License.
//
// IronWASP is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with IronWASP.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace IronWASP
{
    enum JintState
    {
        Identifier,
        ValueExpression,
        AssignmentExpressionLeft,
        AssignmentExpressionRight,
        ArrayDeclaration,
        Property,
        MethodName,
        MethodArgumentIdentifier,
        MethodArgument,
        StringValue,
        NonStringValue,
        IntValue,
        Indexer,
        StringIndex,//a["a"]
        IntIndex,//a[1]
        IdentifierIndex,//a[a]
        ItemIndex,
        WithStringValue,
        WithExpression,
        MethodCallName,
        MethodCallArgument,
        MethodCallArgumentTaintPointer,
        MethodReturn,
        BinaryOperator,
        AnonymousMethod//a dummy value required in oneplace inside the MethoCall statement analyzer, has no use anywhere else
    }
}

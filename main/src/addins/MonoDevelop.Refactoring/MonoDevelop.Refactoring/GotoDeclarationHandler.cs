// 
// GotoDeclarationHandler.cs
//  
// Author:
//       Mike Krüger <mkrueger@novell.com>
// 
// Copyright (c) 2009 Novell, Inc (http://www.novell.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using MonoDevelop.Core;
using MonoDevelop.Ide.Gui;
using MonoDevelop.Components.Commands;
using MonoDevelop.Refactoring;
using MonoDevelop.Ide;

namespace MonoDevelop.Refactoring
{
	public class GotoDeclarationHandler : CommandHandler
	{
		protected override void Run (object data)
		{
			var doc = IdeApp.Workbench.ActiveDocument;
			if (doc == null || doc.FileName == FilePath.Null)
				return;
			JumpToDeclaration (doc, CurrentRefactoryOperationsHandler.GetSymbolInfoAsync (doc, doc.Editor.CaretOffset).Result);
		}
		
		public static void JumpToDeclaration (MonoDevelop.Ide.Gui.Document doc, RefactoringSymbolInfo info)
		{
			if (info.Symbol != null)
				IdeApp.ProjectOperations.JumpToDeclaration (info.Symbol, doc.Project);
			if (info.CandidateSymbols.Length > 0)
				IdeApp.ProjectOperations.JumpToDeclaration (info.CandidateSymbols[0], doc.Project);
		}
	}
}

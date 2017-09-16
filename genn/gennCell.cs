//
// MyClass.cs
//
// Author:
//       Pret D.B. <pret-db@pret-page.com>
//
// Copyright (c) 2017 
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;

namespace genn
{
	public delegate float ActiveFunction(float input);

	public class gennCell
	{

		static public ActiveFunction ActiveFunc;
		static public ActiveFunction InvActiveFunc;
		public float outputValue
		{
			get;
			protected set;
		}


		protected HashSet<gennCell> inputCells;
		protected HashSet<gennCell> outputCells;
		protected Dictionary<gennCell, float> inputWeight;
		protected float bias;



		private int inputCount = 0;
		private Random randomGenerator;

		public gennCell(Random rand)
		{
			this.randomGenerator = rand;

			this.inputCells = new HashSet<gennCell>();
			this.inputCells = new HashSet<gennCell>();
			this.inputWeight = new Dictionary<gennCell, float>();
			this.bias = (float)randomGenerator.NextDouble();
		}


		#region I/O management
		public void AddInputCell(gennCell cell)
		{
			this.inputCells.Add(cell);
			this.inputWeight.Add(cell, (float)this.randomGenerator.NextDouble());
		}
		public void RemoveInputCell(gennCell cell)
		{
			this.outputCells.Remove(cell);
			this.inputWeight.Remove(cell);
		}
		public void AddOutputCell(gennCell cell)
		{
			this.outputCells.Add(cell);
		}
		public void RemoveOutputCell(gennCell cell)
		{
			this.outputCells.Remove(cell);
		}
		#endregion


		public void Notation()
		{
			if (this.inputCount != this.inputCells.Count)
			{
				this.inputCount++;
			}
			else
			{
				this.inputCount = 0;
			}

		}
		public void ForceTrig(float thita = 0)
		{
			this.Active(thita);

		}

		private void Active(float thita = 0)
		{
			float sum = thita;
			float tmpWeight = 0;

			// get sum
			foreach (gennCell cell in this.inputCells)
			{
				this.inputWeight.TryGetValue(cell, out tmpWeight);
				sum += cell.outputValue * tmpWeight;
			}
			// active function;
			sum += this.bias;
			this.outputValue = ActiveFunc(sum);

			// Notation
			foreach (gennCell cell in outputCells)
			{
				cell.Notation();
			}
		}

		~gennCell()
		{
			foreach (gennCell cell in this.outputCells)
			{
				cell.RemoveInputCell(this);
			}
			foreach (gennCell cell in this.inputCells)
			{
				cell.RemoveOutputCell(this);
			}
		}
	}
}

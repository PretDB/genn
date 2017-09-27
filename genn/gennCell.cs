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
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

namespace genn
{
	public delegate float ActiveFunction(float input);

	public class gennCell : IComparable
	{
		static float learningRate = 0.1f;
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

		protected Dictionary<gennCell, float> deltaWeight;
		protected Dictionary<gennCell, float> backError;



		private int inputCount = 0;
		private string ID = "";
		private Random randomGenerator;

		public gennCell(float bias = 0f)
		{
			this.randomGenerator = new Random(DateTime.Now.Millisecond);

			this.inputCells = new HashSet<gennCell>();
			this.inputCells = new HashSet<gennCell>();
			this.outputCells = new HashSet<gennCell>();
			this.inputWeight = new Dictionary<gennCell, float>();
			this.deltaWeight = new Dictionary<gennCell, float>();
			this.backError = new Dictionary<gennCell, float>();

			this.bias = bias;
		}


		#region I/O management
		public void AddFlowTo(gennCell cell, float weight = 0f)
		{
			this.outputCells.Add(cell);
			cell.AddInputCell(this, weight);
		}
		#region Input management
		public void AddInputCell(gennCell cell, float weight = 0f)
		{
			this.inputCells.Add(cell);
			if (weight - 0f < 0.001f)
			{
				this.inputWeight.Add(cell, (float)this.randomGenerator.NextDouble());
			}
			else
			{
				this.inputWeight.Add(cell, weight);
			}
			this.deltaWeight.Add(cell, default(float));
			this.backError.Add(cell, default(float));
		}
		public void RemoveInputCell(gennCell cell)
		{
			this.outputCells.Remove(cell);
			this.inputWeight.Remove(cell);
			this.deltaWeight.Remove(cell);
			this.backError.Remove(cell);
		}
		#endregion
		#region Output management
		public void AddOutputCell(gennCell cell)
		{
			this.outputCells.Add(cell);
		}
		public void RemoveOutputCell(gennCell cell)
		{
			this.outputCells.Remove(cell);
		}
		#endregion
		#endregion

		#region Running
		public void Notation()
		{
			if (this.inputCount != this.inputCells.Count)
			{
				this.inputCount++;
			}
			else
			{
				this.inputCount = 0;
				this.Active();
			}

		}

		/// <summary>
		/// Fixs parameters of this cell with input target value.
		/// </summary>
		/// <param name="target">Target value which eaquals to (realValue + Error).</param>
		public void FixPara(float target)
		{
			float de = default(float);
			float xi = default(float);
			float wi = default(float);

			de = -gennCell.learningRate * (this.outputValue - target) * gennCell.InvActiveFunc(this.outputValue);

			foreach (gennCell cell in this.inputCells)
			{
				xi = this.inputWeight[cell];
				wi = cell.outputValue;

				this.deltaWeight[cell] = de * xi;
				this.backError[cell] = de * wi;
				this.bias += de;

				this.inputWeight[cell] = this.inputWeight[cell] + this.deltaWeight[cell];
				cell.FixPara(cell.outputValue + this.backError[cell]);
			}
		}

		/// <summary>
		/// Active the neural cell with specified forceInput.
		/// Then notate cells follows this cell.
		/// </summary>
		/// <returns>The active.</returns>
		/// <param name="forceInput">Force input.</param>
		public void Active(float forceInput = 0)
		{
			float sum = forceInput;
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
			foreach (gennCell cell in this.outputCells)
			{
				cell.Notation();
			}
		}
		#endregion

		public override string ToString()
		{
			return this.GetID();
		}
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			gennCell cell = obj as gennCell;
			if (cell != null)
			{
				return this.GetID().CompareTo(cell.GetID());
			}
			else
			{
				return 1;
			}
		}
		public string GetID()
		{
			if (this.ID != "")
			{
			}
			else
			{
				this.ID = Guid.NewGuid().ToString("B");
			}
			return this.ID;
		}

		public void Traverse()
		{
			foreach (gennCell cell in this.outputCells)
			{
				cell.Traverse();
			}
			Console.WriteLine(this.ToString());
		}

		~gennCell()
		{
			foreach (gennCell cell in this.outputCells)
			{
				if (cell.inputCells.Count != 0)
				{
					cell.RemoveInputCell(this);
				}
			}
			foreach (gennCell cell in this.inputCells)
			{
				if (cell.outputCells.Count != 0)
				{
					cell.RemoveOutputCell(this);
				}
			}
		}
	}
}

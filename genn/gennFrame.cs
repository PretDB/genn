//
//  gennFrame.cs
//
//  Author:
//       Pret D.B. <pret-db@pret-page.com>
//
//  Copyright (c) 2017 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;

namespace genn
{
	public class gennFrame
	{
		private ArrayList inputValues;
		private ArrayList outputValues;
		private HashSet<gennCell> cells;
		private HashSet<gennCell> inputCells;
		private HashSet<gennCell> outputCells;



		public gennFrame()
		{
			this.cells = new HashSet<gennCell>();
			this.inputCells = new HashSet<gennCell>();
			this.outputCells = new HashSet<gennCell>();
		}

		public void SetIO(int input, int output)
		{
			for (int a = 0; a < input; a++)
			{
			}
		}
	}
}

﻿//
//  Program.cs
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
using System.Collections.Generic;
using genn;
using System.Runtime.CompilerServices;

namespace genn
{
	class MainClass
	{
		static private HashSet<gennCell> layer0 = new HashSet<gennCell>();
		static private HashSet<gennCell> layer1 = new HashSet<gennCell>();
		static private HashSet<gennCell> layer2 = new HashSet<gennCell>();
		public static void Main(string[] args)
		{
			gennCell.ActiveFunc = (float input) => (float)(1 / (1 + Math.Exp(-input)));
			for (int a = 0; a < 2; a++)
			{
				gennCell c = new gennCell();

			}

			Console.WriteLine("Hello World!");
		}

	}
}
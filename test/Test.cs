//
//  Test.cs
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
using NUnit.Framework;
using System;

using genn;
namespace test
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void TestForward()
		{
			float input0 = 1f;
			float input1 = 2f;
			float output1 = 1f;
			float input2 = 1f;
			float input3 = 0.1f;
			float output2 = 0f;

			gennCell c1 = new gennCell();
			gennCell c2 = new gennCell();
			gennCell cf = new gennCell();

			c1.AddFlowTo(cf, 0);
			c2.AddFlowTo(cf, 0);

			c1.Traverse();
			c2.Traverse();

			c1.Active(input0);
			c1.Active(input1);


			Console.ReadKey();
			Console.ReadKey();
		}
	}
}

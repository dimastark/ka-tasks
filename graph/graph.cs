using System;
using System.Collections.Generic;
using System.Linq;

namespace Dima_Starkov_1 {
	class Graph {
		public LinkedList<int>[] IncList;
		public int Size { get { return IncList.Length; } }

		public Graph(int count) {
			IncList = new LinkedList<int>[count + 1];
			for (int i = 1; i <= count; i++)
				IncList[i] = new LinkedList<int> ();
		}

		public LinkedList<int> IncedentNodesFor(int node) {
			return IncList [node];
		}

		public void Connect (int first, int second) {
			IncList [first].AddLast (second);
			IncList [second].AddLast (first);
		}
	}

	class MainClass {
		public static Graph Gr;
		public static int[] Color;
		public static int[] Prevs;
		static int CycleStart, CycleEnd;

		public static bool DepthSearch(int v, int prev) {
			Color[v] = 1;
			foreach (var incnode in Gr.IncedentNodesFor(v))
				if (incnode != prev)
				if (Color[incnode] == 0) {
					Prevs[incnode] = v;
					if (incnode > Gr.IncList.Count ())
						break;
					if (DepthSearch (incnode, v))  return true;
				}
				else if (Color[incnode] == 1) {
					CycleEnd = v;
					CycleStart = incnode;
					return true;
				}
			Color[v] = 2;
			return false;
		}

		public static void InitData() {
			CycleStart = -1;
			string[] text = System.IO.File.ReadAllLines("in.txt");
			int n = int.Parse (text [0]);
			Prevs = new int[n + 1];
			Gr = new Graph (n);
			Color = new int[n + 2];
			for (int i = 1; i < n; i++)
				foreach (string j in text[i].Split())
					if (j == "0")
						break;
					else
						Gr.Connect (i, int.Parse (j));
		}

		public static void Main() {
			InitData ();
			int n = Gr.Size;
			for (int i = 1; i <= n; i++)
				if (DepthSearch (i, 0))
					break;
			Console.WriteLine (CycleStart);
			if (CycleStart == -1)
				System.IO.File.WriteAllLines("out.txt", new string[] {"A"});
			else {
				List<int> cycle = new List<int>();
				cycle.Add(CycleStart);
				for (int v = CycleEnd; v != CycleStart; v = Prevs[v])
					cycle.Add (v);
				List<string> toWrite = new List<string> ();
				toWrite.Add ("N");
				toWrite = toWrite
					.Concat(new string[] {cycle
						    .OrderBy(x => x)
						    .Select (x => x.ToString ())
							.Aggregate((x, y) => x + " " + y)})
					.ToList();
				System.IO.File.WriteAllLines ("out.txt", toWrite);
			}
		}
	}
}

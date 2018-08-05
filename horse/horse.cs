using System;
using System.Collections.Generic;
using System.Linq;

namespace Dima_Starkov_1 {
	static class Extension {
		public static string ToChessNotation(this Tuple<int, int> pair) {
			return ((char)(96 + pair.Item1)).ToString () + pair.Item2.ToString ();
		}
		public static int ToCoord(this char letter) {
			return (int)letter - (int)'a' + 1;
		}
	}
	class MainClass {
		public static Tuple<int, int>[,] Prevs = new Tuple<int, int>[9,9];
		public static Tuple<int, int> From;
		public static Tuple<int, int> To;
		public static bool Find;
		public static Tuple<int, int> Dng1;
		public static Tuple<int, int> Dng2;

		public static void BreadthSearch(int startX, int startY)
		{
			var visited = new HashSet<Tuple<int, int>>();
			var queue = new Queue<Tuple<int, int>>();
			queue.Enqueue(new Tuple<int, int>(startX, startY));
			while (queue.Count != 0)
			{
				var node = queue.Dequeue();
				if (visited.Contains(node)) continue;
				visited.Add(node);
				foreach (var incidentNode in FindAllSteps(node.Item1, node.Item2)) {
						Prevs [incidentNode.Item1, incidentNode.Item2] = node;
						if (incidentNode.Equals (To)) {
							Find = true;
							return;
						}
						queue.Enqueue (incidentNode);
				}
			}
		}

		public static IEnumerable<Tuple<int, int>> FindAllSteps(int first, int second)
		{
			var moves = new Tuple<int, int>[] { new Tuple<int, int>(-1, 2), new Tuple<int, int>(1, 2), 
				new Tuple<int, int>(2, 1), new Tuple<int, int>(2, -1),
				new Tuple<int, int>(1, -2), new Tuple<int, int>(-1, -2),
				new Tuple<int, int>(-2, -1), new Tuple<int, int>(-2, 1) };
			foreach (var move in moves) {
				var dx = move.Item1;
				var dy = move.Item2;
				if (Math.Abs (dx) != Math.Abs (dy))
				if (first + dx > 0 && second + dy > 0 && second + dy < 9 && first + dx < 9)
					yield return new Tuple<int, int> (first + dx, second + dy);
			}
		}

		public static void InitData() {
			string[] data = System.IO.File.ReadAllLines ("in.txt");
			From = new Tuple<int, int>(data[0][0].ToCoord(), int.Parse(data[0][1].ToString()));
			To = new Tuple<int, int>(data[1][0].ToCoord(), int.Parse(data[1][1].ToString()));
			Dng1 = new Tuple<int, int> (data [1] [0].ToCoord () - 1, int.Parse (data [1] [1].ToString ()) - 1);
			Dng2 = new Tuple<int, int> (data [1] [0].ToCoord () + 1, int.Parse (data [1] [1].ToString ()) - 1);
		}

		public static void Main() {
			InitData ();
			BreadthSearch (From.Item1, From.Item2);
			if (Find) {
				var way = new List<string> ();
				var prev = To;
				way.Add(prev.ToChessNotation ());
				while (!prev .Equals(From)) {
					prev = Prevs [prev.Item1, prev.Item2];
					way.Add(prev.ToChessNotation ());
				}
				way.Reverse ();
				System.IO.File.WriteAllLines ("out.txt", way.ToArray());
			}
		}
	}
}

def all_steps(x, y):
	moves = [(-1, 2), (1, 2), 
	         (2, 1), (2, -1), 
	         (1, -2), (-1, -2),
	         (-2, -1), (-2, 1)]
	for dx, dy in moves:
		if 0 <= x + dx < 8 and 0 <= y + dy < 8:
			yield x + dx, y + dy


def to_chess_not(pair):
	return chr(97 + pair[0]) + str(pair[1] + 1)

def to_coord(letter):
	return ord(letter) - ord('a')

def bfs(s_x, s_y, e_x, e_y, dng1, dng2):
	prevs = [[0 for i in range(8)] for j in range(8)]
	visited = set()
	queue = []
	queue.append((s_x, s_y))
	while queue:
		node = queue.pop(0)
		if node in visited:
			continue
		visited.add(node)
		for inc_x, inc_y in all_steps(node[0], node[1]):
			if (inc_x, inc_y) != dng1 and (inc_x, inc_y) != dng2:
				if not prevs [inc_x][inc_y]:
				    prevs [inc_x][inc_y] = node
				if inc_x == e_x and inc_y == e_y:
					return prevs
				queue.append((inc_x, inc_y))

def read_data():
	with open("in.txt") as file:
	    data = file.readlines()
	fr = to_coord(data[0][0]), int(data[0][1]) - 1
	to = to_coord(data[1][0]), int(data[1][1]) - 1
	dng1 = to[0] - 1, to[1] - 1
	dng2 = to[0] + 1, to[1] - 1
	return fr, to, dng1, dng2

fr, to, dng1, dng2 = read_data()
path = bfs(fr[0], fr[1], to[0], to[1], dng1, dng2)
way = []
prev = to
way.append(to_chess_not(prev))
while not prev == fr:
	prev = path [prev[0]][prev[1]]
	way.append(to_chess_not(prev))
way.reverse()
with open("out.txt", mode="w") as out:
	out.write("\n".join(way))
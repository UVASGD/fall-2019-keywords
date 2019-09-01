#keywordDungeonGen.py

import sys
import random
width = int(sys.argv[1])
small = 4
if width <= small:
    print("that's a pretty small width. Some weird out of bounds stuff may happen")
middle = width//2
grid = []
numSquares = width*width
numCheckedOff = [0]
roomNum = [1]

def main():
    fillGrid()
    while(numCheckedOff[0] < numSquares):
        addRoom(random.randrange(0,width),random.randrange(0,width),random.randrange(5,10),random.randrange(5,10))
    printGrid()
    print(roomNum)


def fillGrid():
    for i in range(width):
        grid.append([])
        for j in range(width):
            grid[i].append(0)

def gridContainsZero():
    for row in grid:
        if zero in row:
            return True
    return False

def inBounds(x,y):
    return (x>=0 and x < width and y>=0 and y < width)

def addRoom(x,y,width,height):
    for i in range(-width//2,width//2+1):
        for j in range(-width//2,width//2+1):
            if(inBounds(i+x,j+y)):
                if grid[i+x][j+y] == 0:
                    numCheckedOff[0] += 1
                grid[i+x][j+y] = roomNum[0]
    roomNum[0] += 1

def printGrid():
    for i in range(width):
        thisRow = ''
        for j in range(width):
            thisRow += chr(grid[i][j]%94 + 32) + ' '
        print(thisRow)


main()
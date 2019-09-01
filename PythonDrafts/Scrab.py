import sys

with open('Scrab.txt','r') as file:
    file.readline()
    words = []
    line = file.readline()
    while(line):
        words.append(line[:-1])#[:-1] gets rid of newline character
        line = file.readline()
    with open("ScrabTrimmed.txt",'w') as output:
        letters = sys.argv[1]
        numWords = 0
        for word in words:
            c = 0
            done = False
            while not done and c < len(word):
                if word[c] not in letters:
                    done = True
                c += 1
            if not done:
                numWords += 1
                print(word)
                output.write(word+'\n')
        print(numWords)


# with open('Scrabble.txt','r') as file:
#     file.readline()
#     words = []
#     line = file.readline()
#     while(line):
#         words.append(line[:-1])#[:-1] gets rid of newline character
#         line = file.readline()
#     with open("Scrab.txt",'w') as output:
#         for word in words:
#             if(len(word) > 3 and len(word) < 8):
#                 output.write(word+'\n')
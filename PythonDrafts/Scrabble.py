import sys
import random

words = []
numletterwords = []
num = int(sys.argv[1])
addedletters = 'abcdefghijklmnopqrstuvwxyz'
freqs = {}
with open('Scrab.txt','r') as file:
    file.readline()
    line = file.readline()
    while(line):
        for c in line:
            if c in freqs:
                freqs[c] += 1
            else:
                freqs[c] = 1
        words.append(line[:-1])#[:-1] gets rid of newline character
        if(len(line) == num+1):
            numletterwords.append(line[:-1])
        line = file.readline()

# for thing in reversed(sorted(freqs.items(),key=lambda x: x[1])):
#     print(thing)
def getScore(letters):
    score = 0
    for word in words:
        c = 0
        done = False
        while not done and c < len(word):
            if word[c] not in letters:
                done = True
            c += 1
        if not done:
            # print('\t',word)
            score += 1
            if(len(word) == num and word in numletterwords):
                numletterwords.remove(word)
    return score

def getWords():
    for i in range(20):
        randomword = numletterwords[random.randrange(len(numletterwords))]
        print(randomword,getScore(randomword))

def getWordsPlusLetters():
    for i in range(20):
        randomword = numletterwords[random.randrange(len(numletterwords))]
        randomletter = addedletters[random.randrange(len(addedletters))]
        randomword += randomletter
        print(randomword[:-1],randomletter,getScore(randomword))

def getGoodWords(lowerThreshold=20,upperThreshold=700):
    for i in range(20):
        randomword = numletterwords[random.randrange(len(numletterwords))]
        while(getScore(randomword) < lowerThreshold or getScore(randomword) > upperThreshold):
            randomword = numletterwords[random.randrange(len(numletterwords))]
        print(randomword,getScore(randomword))

def getGoodWordsPlusLetters(lowerThreshold=20,upperThreshold=700):
    for i in range(20):
        randomword = numletterwords[random.randrange(len(numletterwords))]
        while(getScore(randomword) < lowerThreshold or getScore(randomword) > upperThreshold):
            randomword = numletterwords[random.randrange(len(numletterwords))]
        randomletter = addedletters[random.randrange(len(addedletters))]
        randomword += randomletter
        print(randomword[:-1],randomletter,getScore(randomword))

# getWords()
# getWordsPlusLetters()
getGoodWords(30,250)
# getGoodWordsPlusLetters(30,250)
# print(len(words))
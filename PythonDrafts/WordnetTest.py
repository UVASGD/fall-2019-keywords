import urllib.request as web
import sys
from nltk.corpus import wordnet as wn

with open("definition.txt",'w') as file:
    searchterm = sys.argv[1]
    content = str(web.urlopen("http://wordnetweb.princeton.edu/perl/webwn?s="+searchterm+"&sub=Search+WordNet&o2=&o0=1&o8=1&o1=1&o7=&o5=&o9=&o6=&o3=&o4=&h=").read())
    if(content.find("Your search did not return any results.") == -1):
        content = content[content.find('<div class="key"'):]
        content = content[content.find('<a class="pos"'):]
        partOfSpeech = content[content.find('('):content.find('</a>')].replace("\\'","'")
        content = content[content.find('</a>'):]
        definition = content[content.find('('):content.find('<i>')].replace("\\'","'")
        file.write(searchterm + ' ' + partOfSpeech + ' ' + definition)
        print(searchterm,partOfSpeech,definition)
    else:
        file.write('')
        print("no results")

print(wn.synsets(sys.argv[1]))
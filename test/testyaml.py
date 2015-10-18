#!python

import yaml
import IPython

with open('../hashlauncher.yaml') as f:
    data = yaml.load(f)

print data

#IPython.embed()

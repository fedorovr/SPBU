from mpl_toolkits.basemap import Basemap
import matplotlib.pyplot as plt
from matplotlib import colors
import numpy as np
import pickle
import six
import random

input_file = 'filtered_processed_table_two'
colors_ = list(six.iteritems(colors.cnames))
stadium_coordinates = 30.286389, 59.951944 #longitude and latitude

plt.figure(figsize=(12, 12))
my_map = Basemap(projection='merc', lat_0=59.95, lon_0=30.3,
    resolution = 'h', # resolution = 'f',
    area_thresh = 0.01,
    llcrnrlon=30.1, llcrnrlat=59.85,
    urcrnrlon=30.5, urcrnrlat=60.05)
 
my_map.drawcoastlines()
my_map.drawcountries()
my_map.fillcontinents(color='lightyellow')
# my_map.drawmapboundary()

x, y = my_map(*stadium_coordinates)
my_map.plot(x, y, 'bo', markersize=12)

coord_list = []
with open(input_file, 'rb') as f:
    coord_list = pickle.load(f)

for path in coord_list:
	lons = path[::2]
	lats = path[1::2]
	x, y = my_map(lons, lats)
	my_map.plot(x, y, marker=None, color=random.choice(colors_)[0])

# my_map.drawmeridians(np.arange(0, 360, 30))
# my_map.drawparallels(np.arange(-90, 90, 30))

plt.savefig('map.png', bbox_inches='tight')
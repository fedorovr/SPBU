from __future__ import division

import gmplot
import googlemaps
import numpy as np
import pickle
import random


def plot(route):
	latitudes, longitudes = [], []
	for point in route:
		latitudes.append(point['lat'])
		longitudes.append(point['lng'])
	gmap.plot(latitudes, longitudes, random.choice(list(gmplot.gmplot.html_color_codes)), edge_width=2) # use random color
	# gmap.plot(latitudes, longitudes, 'cornflowerblue', edge_width=2) # use single color
	gmap.heatmap(latitudes, longitudes) # optional
	gmap.draw("mymap.html")


input_file = 'filtered_processed_table_two'
stadium_coordinates = 59.951944, 30.286389 #latitude and longitude
gmap = gmplot.GoogleMapPlotter(stadium_coordinates[0], stadium_coordinates[1], 32)

routes = []
with open(input_file, 'rb') as f:
    routes = pickle.load(f)

client = googlemaps.Client(key='YOUR_KEY')
count_of_points = 0

for route in routes:
    print(route)
    all_route_points = []
    lons = route[::2]
    lats = route[1::2]
    for point_idx in list(range(len(lats) - 1)):
        sub_route = client.directions(str(lats[point_idx]) + ', ' + str(lons[point_idx]),
                                      str(lats[point_idx + 1]) + ', ' + str(lons[point_idx + 1]),
                                      mode="walking", units="metric")
        steps = sub_route[0][u'legs'][0][u'steps']
        for step in steps:
            all_route_points += googlemaps.convert.decode_polyline(step[u'polyline'][u'points'])
    plot(all_route_points)
    count_of_points += len(all_route_points)

print('Avg route contains ' + str(count_of_points / len(routes)) + ' points') 

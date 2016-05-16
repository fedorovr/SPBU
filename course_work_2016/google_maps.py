from __future__ import division
from collections import defaultdict

import gmplot
import googlemaps
import numpy as np
import pickle
import random

segments_and_counts = defaultdict(int) # (lat_1, lon_1, lat_2, lon_2) -> Count

def plot_map():
    max_count = max(list(segments_and_counts.values()))
    for segment in segments_and_counts:
        gmap.plot([segment[0], segment[2]], [segment[1], segment[3]], 'red', edge_width=max(2, 100 * segments_and_counts[segment] / max_count))
        # gmap.plot(latitudes, longitudes, random.choice(list(gmplot.gmplot.html_color_codes)), edge_width=2) # use random color
    	# gmap.plot(latitudes, longitudes, 'cornflowerblue', edge_width=2) # use single color
    	# gmap.heatmap(latitudes, longitudes) # optional
    gmap.draw("mymap2.html")


input_file = 'filtered_processed_table_two'
stadium_coordinates = 59.951944, 30.286389 #latitude and longitude
gmap = gmplot.GoogleMapPlotter(stadium_coordinates[0], stadium_coordinates[1], 12)

routes = []
with open(input_file, 'rb') as f:
    routes = pickle.load(f)

client = googlemaps.Client(key='YOUR_KEY')

for route in routes:
    all_route_points = []
    lons = route[::2]
    lats = route[1::2]
    # for each pair of points from base stations get a detailed route from google maps api
    for point_idx in list(range(len(lats) - 1)):
        sub_route = client.directions(str(lats[point_idx]) + ', ' + str(lons[point_idx]),
                                      str(lats[point_idx + 1]) + ', ' + str(lons[point_idx + 1]),
                                      mode="walking", units="metric")
        steps = sub_route[0][u'legs'][0][u'steps']
        for step in steps:
            all_route_points += googlemaps.convert.decode_polyline(step[u'polyline'][u'points'])
    # save each segment to dictionary to find segments' frequency 
    for point_idx in list(range(len(all_route_points) - 1)):
        segments_and_counts[(all_route_points[point_idx]['lat'],     all_route_points[point_idx]['lng'],
                             all_route_points[point_idx + 1]['lat'], all_route_points[point_idx + 1]['lng'])] += 1

plot_map()
print('Avg route contains ' + str(sum(list(segments_and_counts.values())) / len(routes)) + ' segments') 

import pickle
from math import radians, cos, sin, asin, sqrt

input_file = 'filtered_processed_table'
output_file = 'filtered_processed_table_two'
threshold = 2.0 # km to the stadium
stadium_coordinates = 30.286389, 59.951944 #longitude and latitude

coord_list = []
filtered_coord_list = []

with open(input_file, 'rb') as f:
    coord_list = pickle.load(f)


def haversine(lon1, lat1, lon2, lat2):
    """
    Calculate the great circle distance between two points 
    on the earth (specified in decimal degrees)
    """
    # convert decimal degrees to radians 
    lon1, lat1, lon2, lat2 = list(map(radians, [lon1, lat1, lon2, lat2]))
    # haversine formula 
    dlon = lon2 - lon1 
    dlat = lat2 - lat1 
    a = sin(dlat/2)**2 + cos(lat1) * cos(lat2) * sin(dlon/2)**2
    c = 2 * asin(sqrt(a)) 
    km = 6367 * c
    return km


def distance_to_stadium(lon, lat):
    return haversine(lon, lat, *stadium_coordinates)

removed_path_count = 0
for path in coord_list:
    bad_path = True
    # At least one point is close to the stadium
    for i in range(len(path) - 1):
        if distance_to_stadium(path[i], path[i+1]) < threshold:
            bad_path = False
    if bad_path:
        removed_path_count += 1
    else:
        filtered_coord_list.append(path)

print("Removed " + str(removed_path_count) + " pathes of " + str(len(coord_list)))
with open(output_file, 'wb') as f:
    pickle.dump(filtered_coord_list, f)

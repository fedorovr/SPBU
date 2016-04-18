import pickle
from math import radians, cos, sin, asin, sqrt

input_file = 'processed_table'
output_file = 'filtered_processed_table'
threshold = 20.0 # km between 2 points

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

removed_path_count = 0
for path in coord_list:
	bad_path = False
	# if at least one distance is bigger than threshold, remove path
	for i in range(0, len(path) - 3, 2):
	    if haversine(path[i], path[i+1], path[i+2], path[i+3]) > threshold:
	    	bad_path = True
	if bad_path:
		removed_path_count += 1
	else:
		filtered_coord_list.append(path)

print("Removed " + str(removed_path_count) + " pathes of " + str(len(coord_list)))
with open(output_file, 'wb') as f:
    pickle.dump(filtered_coord_list, f)

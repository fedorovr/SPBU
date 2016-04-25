#encoding: UTF-8
from datetime import datetime, timedelta

import numpy as np
import pandas as pd
import pickle
import itertools

# Name of columns in dataframe
cellid_str = 'cellid'
lacid_str = 'lacid'
call_time_str = 'call time'
user_id_str = 'subsid'
# Name of column in report
cellid_str_report = "Cell ID"
lacid_str_report = "LAC"
lon_str_report = "LON"
lat_str_report = "LAT"


def index_of_min(values):
    return min(range(len(values)), key=values.__getitem__)


def get_time(x):
    return datetime.strptime(x, "%d.%m.%Y %H.%M.%S")


def create_aligned_path(data, start_time, end_time, interval):
    # Get sequence of cellids, lacids, times convert to aligned sequnce of coordinates with corresponding interval
    cells, lacs, times = data[cellid_str], data[lacid_str], data[call_time_str]
    step = timedelta(minutes=interval)
    current_time = start_time
    acc = []
    while current_time <= end_time:
        time_differences = [abs(current_time - get_time(some_time)) for some_time in times]
        index = index_of_min(time_differences)
        current_coordinates = report_dict.get((cells[index], lacs[index]))
        # if station is unknown, return None and drop afterwards
        if current_coordinates:
            acc.append(current_coordinates)
        else:
            return None
        current_time += step
    return acc


def between_time(x):
    return to_time > get_time(x) > from_time


def get_coords(path):
    path_ret = []
    for station in path:
        station_info = report_dict[station]
        lon, lat = station_info[1], station_info[2]
        path_ret += [lon, lat]

    return path_ret

from_time = get_time("24.11.2015 20.00.00")
to_time = get_time("24.11.2015 23.00.00")
time_interval = 15 # minutes

# main() {          // :) 

# Create dictionary {(cellid, lacid) -> (lon, lat)}, maps base station to its coordinates 
report_df = pd.read_csv('report_cleared.csv')
report_df['Base station'] = list(zip(report_df[cellid_str_report], report_df[lacid_str_report]))
report_df['Coordinates'] = list(zip(report_df[lon_str_report], report_df[lat_str_report]))
del report_df[cellid_str_report]
del report_df[lacid_str_report]
del report_df[lon_str_report]
del report_df[lat_str_report]
report_dict = report_df.set_index('Base station').to_dict().get('Coordinates')


df = pd.read_csv('petrovsky_11.csv', nrows=10000)
# Choose only events in some time period
# Group people by code - unique number for every user
# Collect all base stations for every user in a list
df1 = df[df[call_time_str].apply(between_time)]
df2 = df1.dropna()
cells = df2.groupby(user_id_str)[cellid_str].apply(lambda x: x.tolist())
lacs = df2.groupby(user_id_str)[lacid_str].apply(lambda x: x.tolist())
times = df2.groupby(user_id_str)[call_time_str].apply(lambda x: x.tolist())

# Join 3 series to dataframe on user id
data = pd.concat([cells, lacs, times], axis=1).reset_index()

# Add alignded coordinates sequences
data['Path'] = data.apply(lambda x: create_aligned_path(x, from_time, to_time, time_interval), axis=1)
# Drop pathes with unknown stations
data = data.dropna()

coord_list = data['Path'].tolist()
# Flatten coord_list: from list of tuples to flat list
coord_list = [list(itertools.chain(*path)) for path in coord_list]

with open('processed_table', 'wb') as f:
    pickle.dump(coord_list, f)

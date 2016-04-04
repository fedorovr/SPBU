#encoding: UTF-8
from datetime import datetime, timedelta

import numpy as np
import pandas as pd
import pickle


df = pd.read_csv('petrovsky_11.csv', nrows=10000)
report_df = pd.read_csv('report_cleared.csv')

# Name of columns in dataframe
cellid_str = 'cellid'
call_time_str = 'call time'
user_id_str = 'subsid'

# Name of column in report
cellid_str_report = "Cell ID"


def index_of_min(values):
    return min(range(len(values)), key=values.__getitem__)


def get_time(x):
    return datetime.strptime(x, "%d.%m.%Y %H.%M.%S")


from_time = get_time("24.11.2015 11.00.00")
to_time = get_time("24.11.2015 15.00.00")


def create_aligned_path(data, start_time, end_time, interval):
    cells, times = data[cellid_str], data[call_time_str]
    step = timedelta(minutes=interval)
    current_time = start_time
    acc = []
    while current_time <= end_time:
        time_differences = [abs(current_time - get_time(some_time)) for some_time in times]
        index = index_of_min(time_differences)
        acc.append(cells[index])
        current_time += step
    return acc


def between_time(x):
    return to_time > get_time(x) > from_time

# Choose only events in some time period
# Group people by code - unique number for every user
# Collect all base stations for every user in a list
df1 = df[df[call_time_str].apply(between_time)]
df2 = df1.dropna(subset=[cellid_str])
cells = df2.groupby(user_id_str)[cellid_str].apply(lambda x: x.tolist())
# The same with all times for every base station
times = df2.groupby(user_id_str)[call_time_str].apply(lambda x: x.tolist())  # TODO Is double groupby bad?

# Join 2 series to dataframe on user id
data = pd.concat([cells, times], axis=1).reset_index()

# Add alignded base station sequences
data['Path'] = data.apply(lambda x: create_aligned_path(x, from_time, to_time, 30), axis=1)

report_dict = report_df.set_index(cellid_str_report).T.to_dict('list')


def get_coords(path):
    path_ret = []
    for station in path:
        station_info = report_dict[station]
        lon, lat = station_info[1], station_info[2]
        path_ret += [lon, lat]

    return path_ret


def split_timespan(chunk_count):
    delta = (to_time - from_time) / chunk_count
    return [to_time + delta * idx for idx in range(chunk_count)]


coord_list = data['Path'].apply(get_coords).tolist()
with open('processed_table', 'wb') as f:
    pickle.dump(coord_list, f)

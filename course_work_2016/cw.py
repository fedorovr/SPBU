import pandas
from datetime import datetime, timedelta

xl = pandas.ExcelFile("m1.xlsx")

df = xl.parse("Sheet1")


def index_of_min(values):
    return min(range(len(values)), key=values.__getitem__)


def get_time(x):
    return datetime.strptime(x, "%d.%m.%Y %H.%M.%S")


from_time = get_time("20.10.2015 19.00.00")
to_time = get_time("20.10.2015 21.00.00")


def create_aligned_path(data):
	lacs, times = data["LAC"], data["Call time"]
	all_time = to_time - from_time
	step = timedelta(minutes=30)
	current_time = from_time
	acc = []
	while current_time <= to_time:
		time_differences = [abs(current_time - get_time(some_time)) for some_time in times]
		index = index_of_min(time_differences)
		acc.append(lacs[index])
		current_time += step
	return acc

def between_time(x):
	return to_time > get_time(x) > from_time

# Firstly, chooses only events in some time period
# Secondly, groups people by code - unique number for every user
# After all collects all LAC's (base stations) for every user in a list
lacs = df[df["Call time"].apply(between_time)].groupby("Code")["LAC"].apply(lambda x: x.tolist())
# The same with all times for every LAC
times = df[df["Call time"].apply(between_time)].groupby("Code")["Call time"].apply(lambda x: x.tolist())

# Join 2 series to dataframe on Code
data = pandas.concat([lacs, times], axis=1).reset_index()

# Add alignded LACs sequences
data['Path'] = data.apply(create_aligned_path, axis=1)

print(data)